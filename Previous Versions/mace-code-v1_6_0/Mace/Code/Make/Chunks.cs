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
using Substrate;
using System.Diagnostics;
using System.Text;

namespace Mace
{
    class Chunks
    {
        public static void MakeChunks(ChunkManager cm, int intEnd, frmMace frmLogForm)
        {
            int[, ,] intUndergroundTerrain = MakeUndergroundTerrain(intEnd * 16, 64, intEnd * 16);
            for (int xi = 0; xi < intEnd; xi++)
            {
                for (int zi = 0; zi < intEnd; zi++)
                {
                    ChunkRef chunkActive = cm.CreateChunk(xi, zi);
                    chunkActive.IsTerrainPopulated = true;
                    chunkActive.Blocks.AutoLight = false;
                    FlatChunk(chunkActive, intUndergroundTerrain);
                    cm.Save();
                }
                frmLogForm.UpdateProgress((1 + xi) * 24 / intEnd);
            }
            cm.Save();
        }
        private static int[, ,] MakeUndergroundTerrain(int X, int Y, int Z)
        {
            int[, ,] intArea = new int[(X / 8) + 1, (Y / 8) + 1, (Z / 8) + 1];
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int y = 0; y < intArea.GetLength(1); y++)
                {
                    for (int z = 0; z < intArea.GetLength(2); z++)
                    {
                        switch (RandomHelper.Next(8))
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                intArea[x, y, z] = (int)BlockType.STONE;
                                break;
                            case 4:
                            case 5:
                                intArea[x, y, z] = (int)BlockType.DIRT;
                                break;
                            case 6:
                                intArea[x, y, z] = (int)BlockType.SAND;
                                break;
                            case 7:
                                intArea[x, y, z] = (int)BlockType.GRAVEL;
                                break;
                        }
                    }
                }
            }

            int[, ,] intEnlargedArea = intArea;
            intEnlargedArea = SmudgeArray3D(EnlargeThreeDimensionalArray(intEnlargedArea, 2, 2, 2), 0.6);
            intEnlargedArea = SmudgeArray3D(EnlargeThreeDimensionalArray(intEnlargedArea, 2, 2, 2), 0.6);
            intEnlargedArea = AddResources(intEnlargedArea, X, Y, Z);
            intEnlargedArea = SmudgeArray3D(EnlargeThreeDimensionalArray(intEnlargedArea, 2, 2, 2), 0.6);

            for (int x = 0; x < intEnlargedArea.GetLength(0); x++)
            {
                for (int y = 0; y < intEnlargedArea.GetLength(1); y++)
                {
                    for (int z = 0; z < intEnlargedArea.GetLength(2); z++)
                    {
                        if (intEnlargedArea[x, y, z] == BlockType.SAND && RandomHelper.NextDouble() < 0.2)
                        {
                            intEnlargedArea[x, y, z] = BlockType.SANDSTONE;
                        }
                    }
                }
            }

            return intEnlargedArea;
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
                    if (dblDepth < RandomHelper.NextDouble())
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
                return RandomHelper.RandomNumber(BlockType.LAPIS_ORE,
                                                 BlockType.REDSTONE_ORE);
            }
            else
            {
                return RandomHelper.RandomNumber(BlockType.GOLD_ORE,
                                                 BlockType.DIAMOND_ORE);
            }
        }
        private static int[, ,] SmudgeArray3D(int[, ,] intArea, double dblChance)
        {
            int[, ,] intNew = new int[intArea.GetLength(0), intArea.GetLength(1), intArea.GetLength(2)];
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int y = 0; y < intArea.GetLength(1); y++)
                {
                    for (int z = 0; z < intArea.GetLength(2); z++)
                    {
                        if (RandomHelper.NextDouble() <= dblChance)
                        {
                            int intNewX, intNewY, intNewZ;
                            do
                            {
                                intNewX = RandomHelper.Next(-1, 2);
                                intNewY = RandomHelper.Next(-1, 2);
                                intNewZ = RandomHelper.Next(-1, 2);
                            } while (!IsValidElement(intArea, x + intNewX, y + intNewY, z + intNewZ) ||
                                     Math.Abs(intNewX) + Math.Abs(intNewY) + Math.Abs(intNewZ) > 1);
                            intNew[x, y, z] = intArea[x + intNewX, y + intNewY, z + intNewZ];
                        }
                        else
                        {
                            intNew[x, y, z] = intArea[x, y, z];
                        }
                    }
                }
            }
            return intNew;
        }
        private static bool IsValidElement(int[, ,] intArray, int X, int Y, int Z)
        {
            return X >= 0 && Y >= 0 && Z >= 0 &&
                   X <= intArray.GetUpperBound(0) &&
                   Y <= intArray.GetUpperBound(1) &&
                   Z <= intArray.GetUpperBound(2);
        }
        private static int[, ,] EnlargeThreeDimensionalArray(int[, ,] intArea, int intMultiplierX, int intMultiplierY, int intMultiplierZ)
        {
            int[, ,] intReturn = new int[intArea.GetLength(0) * intMultiplierX,
                                        intArea.GetLength(1) * intMultiplierY,
                                        intArea.GetLength(2) * intMultiplierZ];
            for (int x = 0; x < intReturn.GetLength(0); x++)
            {
                for (int y = 0; y < intReturn.GetLength(1); y++)
                {
                    for (int z = 0; z < intReturn.GetLength(2); z++)
                    {
                        intReturn[x, y, z] = intArea[x / intMultiplierX, y / intMultiplierY, z / intMultiplierZ];
                    }
                }
            }
            return intReturn;
        }
        private static void FlatChunk(ChunkRef chunk, int[, ,] intUndergroundTerrain)
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

        public static void ResetLighting(BetaWorld world, ChunkManager cm, frmMace frmLogForm, int intTotalChunks)
        {
            int intChunksProcessed = 0;
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
                    chunk.Blocks.RebuildBlockLight();
                    chunk.Blocks.RebuildSkyLight();
                    cm.Save();
                    intChunksProcessed++;
                    if (intChunksProcessed % 10 == 0)
                    {
                        frmLogForm.UpdateProgress(62 + ((intChunksProcessed * (100 - 62)) / intTotalChunks));
                        world.Save();
                    }
                }
                catch (Exception)
                {
                    Debug.WriteLine("Chunk light fail");
                }
            }
            cm.Save();
            world.Save();
        }

        public static void ReplaceValuableBlocks(BetaWorld worldDest, BlockManager bm, int intCitySize)
        {
            int intReplaced = 0;
            for (int x = 0; x < intCitySize; x++)
            {
                for (int z = 0; z < intCitySize; z++)
                {
                    for (int y = 32; y < 128; y++)
                    {
                        switch ((int)bm.GetID(x, y, z))
                        {
                            case (int)BlockType.GOLD_BLOCK:
                                bm.SetID(x, y, z, (int)BlockType.WOOL);
                                bm.SetData(x, y, z, (int)WoolColor.YELLOW);
                                intReplaced++;
                                break;
                            case (int)BlockType.IRON_BLOCK:
                                bm.SetID(x, y, z, (int)BlockType.WOOL);
                                bm.SetData(x, y, z, (int)WoolColor.LIGHT_GRAY);
                                intReplaced++;
                                break;
                            case (int)BlockType.OBSIDIAN:
                                bm.SetID(x, y, z, (int)BlockType.WOOL);
                                bm.SetData(x, y, z, (int)WoolColor.BLACK);
                                intReplaced++;
                                break;
                            case (int)BlockType.DIAMOND_BLOCK:
                                bm.SetID(x, y, z, (int)BlockType.WOOL);
                                bm.SetData(x, y, z, (int)WoolColor.LIGHT_BLUE);
                                intReplaced++;
                                break;
                            case (int)BlockType.LAPIS_BLOCK:
                                bm.SetID(x, y, z, (int)BlockType.WOOL);
                                bm.SetData(x, y, z, (int)WoolColor.BLUE);
                                intReplaced++;
                                break;
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

        //public static void MoveChunks(BetaWorld world, ChunkManager cm)
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
