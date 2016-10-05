/*
    Mace
    Copyright (C) 2011-2012 Robson
    http://iceyboard.no-ip.org

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Substrate;
using System.Collections.Generic;
using Substrate.Nbt;
using Substrate.Data;
using Substrate.Core;

namespace Mace
{
    static class Chunks
    {
        public const int CITY_RELOCATION_CHUNKS = 30;
        public static Dictionary<string, int> biomes = new Dictionary<string, int>();

        public static void CreateInitialChunks(RegionChunkManager cm, frmMace frmLogForm, string strUndergroundOres)
        {
            int[, ,] intUndergroundTerrain = MakeUndergroundTerrain(64, frmLogForm, strUndergroundOres);
            for (int xi = 0; xi < City.mapLength / 16; xi++)
            {
                for (int zi = -City.farmLength / 16; zi < City.mapLength / 16; zi++)
                {
                    ChunkRef chunkActive = cm.CreateChunk(xi, zi);
                    chunkActive.IsTerrainPopulated = true;
                    chunkActive.Blocks.AutoLight = false;
                    CreateFlatChunk(chunkActive, intUndergroundTerrain);
                    cm.Save();
                }
                frmLogForm.UpdateProgress(((1 + xi) * 24 / (City.mapLength / 16)) / 100);
            }
            cm.Save();
        }
        private static int[, ,] MakeUndergroundTerrain(int SizeY, frmMace frmLogForm, string strUndergroundOres)
        {
            int[, ,] intArea = new int[(City.mapLength / 8) + 1, (SizeY / 8) + 1, ((City.farmLength + City.mapLength) / 8) + 1];

            int[] intGroundBlockIDs = Utils.ArrayFromXMLElement(Path.Combine("Resources", "Themes", City.themeName + ".xml"),
                                                                "options", "underground").StringArrayToIntArray();
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int y = 0; y < intArea.GetLength(1); y++)
                {
                    for (int z = 0; z < intArea.GetLength(2); z++)
                    {
                        intArea[x, y, z] = intGroundBlockIDs[RNG.Next(intGroundBlockIDs.GetLength(0))];
                    }
                }
            }

            double dblSmudgeArrayChance = 0.6;
            intArea = intArea.EnlargeArray3D(4, 4, 4).SmudgeArray3D(dblSmudgeArrayChance);
            intArea = AddResources(intArea, SizeY, frmLogForm, strUndergroundOres);
            intArea = intArea.EnlargeArray3D(2, 2, 2).SmudgeArray3D(dblSmudgeArrayChance);

            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int y = 0; y < intArea.GetLength(1); y++)
                {
                    for (int z = 0; z < intArea.GetLength(2); z++)
                    {
                        if (intArea[x, y, z] == BlockInfo.Sand.ID && RNG.NextDouble() < 0.2)
                        {
                            intArea[x, y, z] = BlockInfo.Sandstone.ID;
                        }
                    }
                }
            }

            return intArea;
        }
        private static int[, ,] AddResources(int[, ,] intEnlargedArea, int Y, frmMace frmLogForm, string strUndergroundOres)
        {
            int X, Z;
            int intResources = City.mapLength * Y * (City.farmLength + City.mapLength);
            switch (strUndergroundOres)
            {
                case "Sparse":   intResources /= 400; break;
                case "Uncommon": intResources /= 300; break;
                case "Normal":   intResources /= 200; break;
                case "Common":   intResources /= 100; break;
                case "Dense":    intResources /=  50; break;
                default: intResources /= 200; Debug.Fail("Invalid switch for underground ores."); break;
            }
            
            frmLogForm.UpdateLog("Adding resource patches: " + intResources, true, true);
            do
            {
                X = RNG.Next(1, intEnlargedArea.GetLength(0) - 1);
                Y = RNG.Next(1, intEnlargedArea.GetLength(1) - 1);
                Z = RNG.Next(1, intEnlargedArea.GetLength(2) - 1);
                if (intEnlargedArea[X, Y, Z] == BlockInfo.Stone.ID)
                {
                    double dblDepth = (double)Y / intEnlargedArea.GetLength(1);
                    // this increases ore frequency as we get lower
                    if (dblDepth < RNG.NextDouble() * 1.5)
                    {
                        intEnlargedArea[X, Y, Z] = SelectRandomResource(dblDepth);
                        intResources--;
                    }
                }
            } while (intResources > 0);
            return intEnlargedArea;
        }
        private static int SelectRandomResource(double dblDepth)
        {
            if (dblDepth > 0.66)
            {
                return RNG.RandomItem(BlockInfo.CoalOre.ID,
                                      BlockInfo.IronOre.ID);
            }
            else if (dblDepth > 0.33)
            {
                return RNG.RandomItem(BlockInfo.CoalOre.ID,
                                      BlockInfo.IronOre.ID,
                                      BlockInfo.LapisOre.ID,
                                      BlockInfo.RedstoneOre.ID);
            }
            else
            {
                return RNG.RandomItem(BlockInfo.CoalOre.ID,
                                      BlockInfo.IronOre.ID,
                                      BlockInfo.LapisOre.ID,
                                      BlockInfo.RedstoneOre.ID,
                                      BlockInfo.GoldOre.ID,
                                      BlockInfo.DiamondOre.ID);
            }
        }
        private static void CreateFlatChunk(ChunkRef chunk, int[, ,] intUndergroundTerrain)
        {
            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    for (int y = 0; y < 2; y++)
                    {
                        chunk.Blocks.SetID(x, y, z, BlockInfo.Bedrock.ID);
                    }
                    for (int y = 2; y < 63; y++)
                    {
                        chunk.Blocks.SetID(x, y, z, intUndergroundTerrain[(chunk.X * 16) + x, y, City.farmLength + (chunk.Z * 16) + z]);
                    }
                    for (int y = 63; y < 64; y++)
                    {
                        chunk.Blocks.SetID(x, y, z, City.groundBlockID);
                        chunk.Blocks.SetData(x, y, z, City.groundBlockData);
                    }
                }
            }
        }
        public static void ResetLighting(AnvilWorld world, RegionChunkManager cm, frmMace frmLogForm)
        {
            int intChunksProcessed = 0, intTotalChunks = 0;
            //this code is based on a Substrate example
            //http://code.google.com/p/substrate-minecraft/source/browse/trunk/Substrate/SubstrateCS/Examples/Relight/Program.cs
            //see the <License Substrate.txt> file for copyright information
            foreach (ChunkRef chunk in cm)
            {
                if (chunk.IsTerrainPopulated)
                {
                    if (chunk.X * 16 >= 0 &&
                        chunk.X * 16 <= City.mapLength &&
                        chunk.Z * 16 >= -City.farmLength &&
                        chunk.Z * 16 <= City.mapLength)
                    {
                        // we process each chunk twice, hence this:
                        intTotalChunks += 2;
                    }
                }
            }
            foreach (ChunkRef chunk in cm)
            {
                if (chunk.IsTerrainPopulated)
                {
                    if (chunk.X * 16 >= 0 &&
                        chunk.X * 16 <= City.mapLength &&
                        chunk.Z * 16 >= -City.farmLength &&
                        chunk.Z * 16 <= City.mapLength)
                    {
                        try
                        {
                            chunk.Blocks.RebuildHeightMap();
                            chunk.Blocks.ResetBlockLight();
                            chunk.Blocks.ResetSkyLight();
                        }
                        catch (Exception)
                        {
                            Debug.WriteLine("Chunk reset light fail");
                        }
                        if (++intChunksProcessed % 10 == 0)
                        {
                            cm.Save();
                            frmLogForm.UpdateProgress(0.5 + (intChunksProcessed * 0.45 / intTotalChunks));
                        }
                    }
                }
            }
            foreach (ChunkRef chunk in cm)
            {
                if (chunk.IsTerrainPopulated)
                {
                    if (chunk.X * 16 >= 0 &&
                        chunk.X * 16 <= City.mapLength &&
                        chunk.Z * 16 >= -City.farmLength &&
                        chunk.Z * 16 <= City.mapLength)
                    {
                        try
                        {
                            chunk.Blocks.RebuildBlockLight();
                            chunk.Blocks.RebuildSkyLight();
                        }
                        catch (Exception)
                        {
                            Debug.WriteLine("Chunk rebuild light fail");
                        }
                        if (++intChunksProcessed % 10 == 0)
                        {
                            cm.Save();
                            frmLogForm.UpdateProgress(0.5 + (intChunksProcessed * 0.45 / intTotalChunks));
                        }
                    }
                }
            }
            world.Save();
        }
        public static void PositionRails(AnvilWorld worldDest, BlockManager bm)
        {
            // todo low: different elevations
            int intReplaced = 0;
            for (int x = 0; x < City.mapLength; x++)
            {
                for (int z = -City.farmLength; z < City.mapLength; z++)
                {
                    for (int y = 0; y < 128; y++)
                    {
                        switch (bm.GetID(x, y, z))
                        {
                            case BlockType.POWERED_RAIL:
                            case BlockType.DETECTOR_RAIL:
                            case BlockType.RAILS:
                                BlockHelper.MakeRail(x, y, z);
                                if (++intReplaced > 100)
                                {
                                    worldDest.Save();
                                    intReplaced = 0;
                                }
                                break;
                        }
                    }
                }
            }
        }
        public static void ReplaceValuableBlocks(AnvilWorld worldDest, BlockManager bm)
        {
            int intReplaced = 0;
            for (int x = 0; x < City.mapLength; x++)
            {
                for (int z = -City.farmLength; z < City.mapLength; z++)
                {
                    for (int y = 40; y < 128; y++)
                    {
                        if (bm.GetID(x, y, z) != City.wallMaterialID ||
                            bm.GetData(x, y, z) != City.wallMaterialData)
                        {
                            switch ((int)bm.GetID(x, y, z))
                            {
                                case BlockType.GOLD_BLOCK:
                                    BlockShapes.MakeBlock(x, y, z, BlockInfo.Wool.ID, (int)WoolColor.YELLOW);
                                    intReplaced++;
                                    break;
                                case BlockType.IRON_BLOCK:
                                    BlockShapes.MakeBlock(x, y, z, BlockInfo.Wool.ID, (int)WoolColor.LIGHT_GRAY);
                                    intReplaced++;
                                    break;
                                case BlockType.OBSIDIAN:
                                    BlockShapes.MakeBlock(x, y, z, BlockInfo.Wool.ID, (int)WoolColor.BLACK);
                                    intReplaced++;
                                    break;
                                case BlockType.DIAMOND_BLOCK:
                                    BlockShapes.MakeBlock(x, y, z, BlockInfo.Wool.ID, (int)WoolColor.LIGHT_BLUE);
                                    intReplaced++;
                                    break;
                                case BlockType.LAPIS_BLOCK:
                                    BlockShapes.MakeBlock(x, y, z, BlockInfo.Wool.ID, (int)WoolColor.BLUE);
                                    intReplaced++;
                                    break;
                                // no need for a default, because we purposefully want to skip all the other blocks
                            }
                            if (intReplaced > 25)
                            {
                                worldDest.Save();
                                intReplaced = 0;
                            }
                        }
                    }
                }
            }
        }
        public static void MoveChunks(AnvilWorld world, RegionChunkManager cm, int CityX, int CityZ)
        {
            cm.Save();
            world.Save();
            for (int x = 0; x < City.mapLength / 16; x++)
            {
                for (int z = -City.farmLength / 16; z < City.mapLength / 16; z++)
                {
                    cm.CopyChunk(x, z, CityX + x + CITY_RELOCATION_CHUNKS, CityZ + z + CITY_RELOCATION_CHUNKS);
                    ChunkRef chunkActive = cm.GetChunkRef(CityX + x + CITY_RELOCATION_CHUNKS, CityZ + z + CITY_RELOCATION_CHUNKS);
                    chunkActive.IsTerrainPopulated = true;
                    chunkActive.Blocks.AutoLight = true;
                    biomes.Add(chunkActive.X + "." + chunkActive.Z, City.biome);
                    cm.DeleteChunk(x, z);
                    cm.Save();
                    world.Save();
                }                
            }
        }
        public static void SetBiomeData(string dest)
        {
            NbtWorld world = NbtWorld.Open(dest);
            IChunkManager cm = world.GetChunkManager();
            foreach (ChunkRef chunk in cm)
            {
                AnvilChunk anvil_chunk = chunk.GetChunkRef() as AnvilChunk;
                TagNodeByteArray biomeNode = anvil_chunk.Tree.Root["Level"].ToTagCompound()["Biomes"].ToTagByteArray();
                ZXByteArray biomeData = new ZXByteArray(16, 16, biomeNode.Data);
                for (int x = 0; x <= 15; x++)
                {
                    for (int y = 0; y <= 15; y++)
                    {
                        biomeData[x, y] = biomes[chunk.X + "." + chunk.Z];
                    }
                }
                chunk.SetChunkRef(anvil_chunk);
                cm.Save();
            }
            world.Save();
        }
    }
}
