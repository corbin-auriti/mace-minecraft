/*
    Mace
    Copyright (C) 2011 Robson
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
using System.Text;
using Substrate;

namespace Mace
{
    static class Chunks
    {
        public static void CreateInitialChunks(BetaChunkManager cm, int intEnd, frmMace frmLogForm)
        {
            int[, ,] intUndergroundTerrain = MakeUndergroundTerrain(intEnd * 16, 64, intEnd * 16);
            for (int xi = 0; xi < intEnd; xi++)
            {
                for (int zi = 0; zi < intEnd; zi++)
                {
                    ChunkRef chunkActive = cm.CreateChunk(xi, zi);
                    chunkActive.IsTerrainPopulated = true;
                    chunkActive.Blocks.AutoLight = false;
                    CreateFlatChunk(chunkActive, intUndergroundTerrain);
                    cm.Save();
                }
                frmLogForm.UpdateProgress((1 + xi) * 24 / intEnd);
            }
            cm.Save();
        }
        private static int[, ,] MakeUndergroundTerrain(int SizeX, int SizeY, int SizeZ)
        {
            int[, ,] intArea = new int[(SizeX / 8) + 1, (SizeY / 8) + 1, (SizeZ / 8) + 1];
            int[] intGroundBlockIDs = new int[] { BlockType.STONE, BlockType.DIRT, BlockType.SAND,
                                                  BlockType.GRAVEL };
            int[] intGroundBlockChances = new int[] { 3, 2, 1, 1 };
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int y = 0; y < intArea.GetLength(1); y++)
                {
                    for (int z = 0; z < intArea.GetLength(2); z++)
                    {
                        intArea[x, y, z] = intGroundBlockIDs[RandomHelper.RandomWeightedNumber(intGroundBlockChances)];
                    }
                }
            }

            double dblSmudgeArrayChance = 0.6;
            intArea = Utils.SmudgeArray3D(Utils.EnlargeThreeDimensionalArray(intArea, 2, 2, 2), dblSmudgeArrayChance);
            intArea = Utils.SmudgeArray3D(Utils.EnlargeThreeDimensionalArray(intArea, 2, 2, 2), dblSmudgeArrayChance);
            intArea = AddResources(intArea, SizeX, SizeY, SizeZ);
            intArea = Utils.SmudgeArray3D(Utils.EnlargeThreeDimensionalArray(intArea, 2, 2, 2), dblSmudgeArrayChance);

            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int y = 0; y < intArea.GetLength(1); y++)
                {
                    for (int z = 0; z < intArea.GetLength(2); z++)
                    {
                        if (intArea[x, y, z] == BlockType.SAND && RandomHelper.NextDouble() < 0.2)
                        {
                            intArea[x, y, z] = BlockType.SANDSTONE;
                        }
                    }
                }
            }

            return intArea;
        }
        private static int[, ,] AddResources(int[, ,] intEnlargedArea, int X, int Y, int Z)
        {
            int intResources = (X * Y * Z) / 250;
            do
            {
                X = RandomHelper.Next(1, intEnlargedArea.GetLength(0) - 1);
                Y = RandomHelper.Next(1, intEnlargedArea.GetLength(1) - 1);
                Z = RandomHelper.Next(1, intEnlargedArea.GetLength(2) - 1);
                if (intEnlargedArea[X, Y, Z] == BlockType.STONE)
                {
                    double dblDepth = (double)Y / intEnlargedArea.GetLength(1);
                    // this increases ore frequency as we get lower
                    if (dblDepth < RandomHelper.NextDouble() * 1.5)
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
                return RandomHelper.RandomNumber(BlockType.COAL_ORE,
                                                 BlockType.IRON_ORE);
            }
            else if (dblDepth > 0.33)
            {
                return RandomHelper.RandomNumber(BlockType.COAL_ORE,
                                                 BlockType.IRON_ORE,
                                                 BlockType.LAPIS_ORE,
                                                 BlockType.REDSTONE_ORE);
            }
            else
            {
                return RandomHelper.RandomNumber(BlockType.COAL_ORE,
                                                 BlockType.IRON_ORE,
                                                 BlockType.LAPIS_ORE,
                                                 BlockType.REDSTONE_ORE,
                                                 BlockType.GOLD_ORE,
                                                 BlockType.DIAMOND_ORE);
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
                        chunk.Blocks.SetID(x, y, z, BlockType.BEDROCK);
                    }
                    for (int y = 2; y < 63; y++)
                    {
                        chunk.Blocks.SetID(x, y, z, intUndergroundTerrain[(chunk.X * 16) + x, y, (chunk.Z * 16) + z]);
                    }
                    for (int y = 63; y < 64; y++)
                    {
                        chunk.Blocks.SetID(x, y, z, BlockType.GRASS);
                    }
                }
            }
        }
        public static void ResetLighting(BetaWorld world, BetaChunkManager cm, frmMace frmLogForm, int intTotalChunks)
        {
            int intChunksProcessed = 0;
            // we process each chunk twice, hence this:
            intTotalChunks *= 2;
            //this code is based on a substrate example
            //http://code.google.com/p/substrate-minecraft/source/browse/trunk/Substrate/SubstrateCS/Examples/Relight/Program.cs
            //see the <License Substrate.txt> file for copyright information
            foreach (ChunkRef chunk in cm)
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
                    frmLogForm.UpdateProgress(62 + ((intChunksProcessed * (100 - 62)) / intTotalChunks));
                }
            }
            foreach (ChunkRef chunk in cm)
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
                    frmLogForm.UpdateProgress(62 + ((intChunksProcessed * (100 - 62)) / intTotalChunks));
                }
            }
            world.Save();
        }
        public static void PositionRails(BetaWorld worldDest, BlockManager bm, int intCitySize)
        {
            int intReplaced = 0;
            for (int x = 0; x < intCitySize; x++)
            {
                for (int z = 0; z < intCitySize; z++)
                {
                    for (int y = 0; y < 128; y++)
                    {
                        if (bm.GetID(x, y, z) == BlockType.RAILS)
                        {
                            BlockHelper.MakeRail(x, y, z);
                            if (++intReplaced > 100)
                            {
                                worldDest.Save();
                                intReplaced = 0;
                            }
                        }
                    }
                }
            }
        }
        public static void ReplaceValuableBlocks(BetaWorld worldDest, BlockManager bm, int intCitySize,
                                                 int intWallMaterial)
        {
            int intReplaced = 0;
            for (int x = 0; x < intCitySize; x++)
            {
                for (int z = 0; z < intCitySize; z++)
                {
                    for (int y = 32; y < 128; y++)
                    {
                        if ((int)bm.GetID(x, y, z) != intWallMaterial)
                        {
                            switch ((int)bm.GetID(x, y, z))
                            {
                                case BlockType.GOLD_BLOCK:
                                    BlockShapes.MakeBlock(x, y, z, BlockType.WOOL, (int)WoolColor.YELLOW);
                                    intReplaced++;
                                    break;
                                case BlockType.IRON_BLOCK:
                                    BlockShapes.MakeBlock(x, y, z, BlockType.WOOL, (int)WoolColor.LIGHT_GRAY);
                                    intReplaced++;
                                    break;
                                case BlockType.OBSIDIAN:
                                    BlockShapes.MakeBlock(x, y, z, BlockType.WOOL, (int)WoolColor.BLACK);
                                    intReplaced++;
                                    break;
                                case BlockType.DIAMOND_BLOCK:
                                    BlockShapes.MakeBlock(x, y, z, BlockType.WOOL, (int)WoolColor.LIGHT_BLUE);
                                    intReplaced++;
                                    break;
                                case BlockType.LAPIS_BLOCK:
                                    BlockShapes.MakeBlock(x, y, z, BlockType.WOOL, (int)WoolColor.BLUE);
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

        //public static void MoveChunks(BetaWorld world, BetaChunkManager cm)
        //{
        //    cm.Save();
        //    for (int x = 1; x < 4; x++)
        //    {
        //        for (int z = 1; z < 4; z++)
        //        {
        //            ChunkRef chunkSource = cm.GetChunkRef(x + 4, z + 4);
        //            ChunkRef chunkDestination = cm.CreateChunk(x + 20, z + 20);
        //            cm.CopyChunk(x + 4, z + 4, x + 20, z + 20);
        //            cm.Save();
        //            //chunkDestination.SetChunkRef(chunkSource.GetChunkCopy());
        //            Debug.WriteLine("Moved " + x + " : " + z);
        //        }
        //    }
        //    world.Save();
        //}
    }
}
