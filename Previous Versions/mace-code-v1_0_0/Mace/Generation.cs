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

// todo: split this into multiple files

using System;
using Substrate;
using Substrate.TileEntities;

namespace Mace
{
    class Generation
    {
        static int intMapSize, intPlotSize, intFarmSize;
        static BlockManager bm;
        static Random rand = new Random();
        public static void SetupClass(BlockManager bmOriginal, int intMapSizeOriginal, int intPlotSizeOriginal, int intFarmSizeOriginal)
        {
            bm = bmOriginal;
            intMapSize = intMapSizeOriginal;
            intPlotSize = intPlotSizeOriginal;
            intFarmSize = intFarmSizeOriginal;
        }
        
        #region Make Chunks
        public static void MakeChunks(ChunkManager cm, int intStart, int intEnd)
        {
            Console.WriteLine("Making chunks");
            for (int xi = intStart; xi < intEnd; xi++)
            {
                for (int zi = intStart; zi < intEnd; zi++)
                {
                    ChunkRef chunkOriginal = cm.CreateChunk(xi, zi);
                    chunkOriginal.IsTerrainPopulated = true;
                    chunkOriginal.Blocks.AutoLight = false;
                    FlatChunk(chunkOriginal);
                    chunkOriginal.Blocks.RebuildBlockLight();
                    chunkOriginal.Blocks.RebuildSkyLight();
                    cm.Save();
                }
            }
        }
        public static void FlatChunk(ChunkRef chunk)
        {
            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    for (int y = 0; y < 2; y++)
                        chunk.Blocks.SetID(x, y, z, (int)BlockType.BEDROCK);
                    for (int y = 2; y < 59; y++)
                        chunk.Blocks.SetID(x, y, z, (int)BlockType.STONE);
                    for (int y = 59; y < 63; y++)
                        chunk.Blocks.SetID(x, y, z, (int)BlockType.DIRT);
                    for (int y = 63; y < 64; y++)
                        chunk.Blocks.SetID(x, y, z, (int)BlockType.GRASS);
                }
            }
        }
        #endregion
        #region Make Sewers
        public static bool[,] MakeSewers()
        {
            Console.WriteLine("Making sewers");

            int intPlots = 1 + (((intMapSize - (intFarmSize + 20)) - (intFarmSize + 18)) / intPlotSize);

            bool[,] booNewSewerEntrances = MakeSewerEntrances(intPlots);
            bool[,] booOldSewerEntrances = booNewSewerEntrances;

            Maze maze = new Maze();
            // 51, 43, 35, 27, 19, 11, 3
            for (int y = 3; y <= 51; y += 8)
            {
                booOldSewerEntrances = booNewSewerEntrances;
                booNewSewerEntrances = MakeSewerEntrances(intPlots);
                bool[,] booMaze = maze.GenerateMaze((intPlots * 2) + 1, (intPlots * 2) + 1);                
                //booMaze = maze.CreateLoops(booMaze, booMaze.GetLength(0) / 2);
                if (y == 3)
                    booMaze = maze.DeleteDeadEnds(booMaze, booNewSewerEntrances, booNewSewerEntrances);
                else
                    booMaze = maze.DeleteDeadEnds(booMaze, booOldSewerEntrances, booNewSewerEntrances);
                for (int x = 0; x < booMaze.GetUpperBound(0); x++)
                    for (int z = 0; z < booMaze.GetUpperBound(1); z++)
                        if (booMaze[x, z])
                            MakeSewerSection(booMaze, x, z, intFarmSize + 18 + (x * intPlotSize / 2), intFarmSize + 18 + (z * intPlotSize / 2), y);
                for (int x = 0; x < booMaze.GetUpperBound(0); x++)
                {
                    for (int z = 0; z < booMaze.GetUpperBound(1); z++)
                    {
                        if (booMaze[x, z])
                        {
                            if (y > 3 &&
                                x % 2 == 1 && z % 2 == 1 &&
                                booOldSewerEntrances[(x - 1) / 2, (z - 1) / 2])
                            {
                                BlockShapes.MakeSolidBox(intFarmSize + 18 + (x * intPlotSize / 2) - 1, intFarmSize + 18 + (x * intPlotSize / 2) + 1,
                                                         y - 7, y + 1,
                                                         intFarmSize + 18 + (z * intPlotSize / 2) - 1, intFarmSize + 18 + (z * intPlotSize / 2) + 1,
                                                         (int)BlockType.AIR);
                                BlockShapes.MakeHollowLayers(intFarmSize + 18 + (x * intPlotSize / 2) - 1, intFarmSize + 18 + (x * intPlotSize / 2) + 1,
                                                             y, y,
                                                             intFarmSize + 18 + (z * intPlotSize / 2) - 1, intFarmSize + 18 + (z * intPlotSize / 2) + 1,
                                                             (int)BlockType.STONE);
                                BlockShapes.MakeSolidBox(intFarmSize + 18 + (x * intPlotSize / 2), intFarmSize + 18 + (x * intPlotSize / 2),
                                                         y - 7, y + 1,
                                                         intFarmSize + 18 + (z * intPlotSize / 2) + 1, intFarmSize + 18 + (z * intPlotSize / 2) + 1,
                                                         (int)BlockType.STONE);
                                BlockShapes.MakeLadder(intFarmSize + 18 + (x * intPlotSize / 2), y - 7, y + 1, intFarmSize + 18 + (z * intPlotSize / 2), 2);
                            }
                        }
                    }
                }
            }
            return booNewSewerEntrances;
        }
        static bool[,] MakeSewerEntrances(int intPlots)
        {
            bool[,] booSewerEntrances = new bool[intPlots + 1, intPlots + 1];
            booSewerEntrances[rand.Next(0, (intPlots / 2) - 2), rand.Next(0, (intPlots / 2) - 2)] = true;
            booSewerEntrances[intPlots - rand.Next(1, (intPlots / 2) - 2), rand.Next(1, (intPlots / 2) - 2)] = true;
            booSewerEntrances[rand.Next(0, (intPlots / 2) - 2), intPlots - rand.Next(1, (intPlots / 2) - 2)] = true;
            booSewerEntrances[intPlots - rand.Next(1, (intPlots / 2) - 2), intPlots - rand.Next(1, (intPlots / 2) - 2)] = true;
            return booSewerEntrances;
        }
        static void MakeSewerSection(bool[,] booMaze, int intMazeX, int intMazeZ, int intMapX, int intMapZ, int intStartY)
        {
            BlockShapes.MakeSolidBox(intMapX - 1, intMapX + 1, intStartY, intStartY,
                                     intMapZ - 1, intMapZ + 1, (int)BlockType.STATIONARY_WATER);
            BlockShapes.MakeSolidBox(intMapX - 2, intMapX + 2, intStartY + 1, intStartY + 3,
                                     intMapZ - 2, intMapZ + 2, (int)BlockType.AIR);
            BlockShapes.MakeSolidBox(intMapX - 1, intMapX + 1, intStartY + 4, intStartY + 4,
                                     intMapZ - 1, intMapZ + 1, (int)BlockType.AIR);
            if (booMaze[intMazeX - 1, intMazeZ])
            {
                BlockShapes.MakeSolidBox(intMapX - (intPlotSize / 2), intMapX - 1, intStartY, intStartY,
                                         intMapZ - 1, intMapZ + 1, (int)BlockType.STATIONARY_WATER);
                BlockShapes.MakeSolidBox(intMapX - (intPlotSize / 2), intMapX - 2, intStartY + 1, intStartY + 3,
                                         intMapZ - 2, intMapZ + 2, (int)BlockType.AIR);
                BlockShapes.MakeSolidBox(intMapX - (intPlotSize / 2), intMapX - 1, intStartY + 4, intStartY + 4,
                                         intMapZ - 1, intMapZ + 1, (int)BlockType.AIR);
            }
            if (booMaze[intMazeX + 1, intMazeZ])
            {
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX + (intPlotSize / 2), intStartY, intStartY,
                                         intMapZ - 1, intMapZ + 1, (int)BlockType.STATIONARY_WATER);
                BlockShapes.MakeSolidBox(intMapX - 2, intMapX + (intPlotSize / 2), intStartY + 1, intStartY + 3,
                                         intMapZ - 2, intMapZ + 2, (int)BlockType.AIR);
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX + (intPlotSize / 2), intStartY + 4, intStartY + 4,
                                         intMapZ - 1, intMapZ + 1, (int)BlockType.AIR);
            }
            if (booMaze[intMazeX, intMazeZ - 1])
            {
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX - 1, intStartY, intStartY,
                                         intMapZ - (intPlotSize / 2), intMapZ + 1, (int)BlockType.STATIONARY_WATER);
                BlockShapes.MakeSolidBox(intMapX - 2, intMapX - 2, intStartY + 1, intStartY + 3,
                                         intMapZ - (intPlotSize / 2), intMapZ + 2, (int)BlockType.AIR);
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX - 1, intStartY + 4, intStartY + 4,
                                         intMapZ - (intPlotSize / 2), intMapZ + 1, (int)BlockType.AIR);
            }
            if (booMaze[intMazeX, intMazeZ + 1])
            {
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX + 1, intStartY, intStartY,
                                         intMapZ - 1, intMapZ + (intPlotSize / 2), (int)BlockType.STATIONARY_WATER);
                BlockShapes.MakeSolidBox(intMapX - 2, intMapX + 2, intStartY + 1, intStartY + 3,
                                         intMapZ - 2, intMapZ + (intPlotSize / 2), (int)BlockType.AIR);
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX + 1, intStartY + 4, intStartY + 4,
                                         intMapZ - 1, intMapZ + (intPlotSize / 2), (int)BlockType.AIR);
            }
        }
        #endregion
        public static void MakePlots(bool[,] booSewerEntrances)
        {
            Console.WriteLine("Making plots");
            int intPlots = 1 + (((intMapSize - (intFarmSize + 20)) - (intFarmSize + 18)) / intPlotSize);
            for (int x = intFarmSize + 18; x < intMapSize - (intFarmSize + 20); x += intPlotSize)
            {
                for (int z = intFarmSize + 18; z < intMapSize - (intFarmSize + 20); z += intPlotSize)
                {
                    if (booSewerEntrances[(x - (intFarmSize + 18)) / intPlotSize, (z - (intFarmSize + 18)) / intPlotSize])
                    {
                        BlockShapes.MakeHollowBox(x + (intPlotSize / 2) - 2, x + (intPlotSize / 2) + 2, 63, 67,
                                                  z + (intPlotSize / 2) - 2, z + (intPlotSize / 2) + 2,
                                                  (int)BlockType.STONE);
                        BlockShapes.MakeSolidBox(x + (intPlotSize / 2), x + (intPlotSize / 2), 64, 65,
                                                 z + (intPlotSize / 2) - 2, z + (intPlotSize / 2) - 2,
                                                 (int)BlockType.AIR);
                        BlockShapes.MakeSolidBox(x + (intPlotSize / 2), x + (intPlotSize / 2), 55, 63,
                                                 z + (intPlotSize / 2), z + (intPlotSize / 2),
                                                 (int)BlockType.AIR);
                        BlockShapes.MakeSolidBox(x + (intPlotSize / 2), x + (intPlotSize / 2), 52, 55,
                                                 z + (intPlotSize / 2) + 1, z + (intPlotSize / 2) + 1, (int)BlockType.STONE);
                        BlockShapes.MakeLadder(x + (intPlotSize / 2), 52, 63, z + (intPlotSize / 2), 2);
                        bm.SetID(x + (intPlotSize / 2), 64, z + (intPlotSize / 2), 96); // hatch
                        bm.SetData(x + (intPlotSize / 2), 64, z + (intPlotSize / 2), 2);
                        bm.SetID(x + (intPlotSize / 2) + 1, 64, z + (intPlotSize / 2) + 1, (int)BlockType.CHEST);
                        TileEntityChest tec = new TileEntityChest();
                        tec.Items[0] = MakeItem((int)BlockType.REDSTONE_TORCH_ON, 32);
                        tec.Items[1] = MakeItem((int)BlockType.TORCH, 8);
                        bm.SetTileEntity(x + (intPlotSize / 2) + 1, 64, z + (intPlotSize / 2) + 1, tec);
                        MakeSign(x + (intPlotSize / 2), 66, z + (intPlotSize / 2) - 3, "Sewers||Currently|empty!", (int)BlockType.STONE);
                    }
                    else
                    {
                        int intRandBuilding = rand.Next(100);
                        if (intRandBuilding >= 90)
                        {
                            BlockShapes.MakeHollowLayers(x + 3, x + 9, 63, 63, z + 3, z + 9, (int)BlockType.DOUBLE_SLAB);
                            for (int a = 0; a <= 3; a++)
                                bm.SetID(x + 6, 63, z + a, (int)BlockType.DOUBLE_SLAB);
                            int x1 = x + rand.Next(5, 7);
                            int z1 = z + rand.Next(5, 7);
                            BlockShapes.MakeSolidBox(x1, x1 + 1, 63, 63, z1, z1 + 1, (int)BlockType.STATIONARY_WATER);
                            int intFail = 0, intTrees = 10;
                            bool blnValid = true;
                            do
                            {
                                x1 = x + 1 + rand.Next(0, intPlotSize - 2);
                                z1 = z + 1 + rand.Next(0, intPlotSize - 2);
                                if (bm.GetID(x1, 63, z1) != (int)BlockType.DOUBLE_SLAB &&
                                    bm.GetID(x1, 63, z1) != (int)BlockType.STATIONARY_WATER)
                                {
                                    blnValid = true;
                                    for (int a = x1 - 4; a <= x1 + 4 && blnValid; a++)
                                        for (int b = z1 - 4; b <= z1 + 4 && blnValid; b++)
                                            if (bm.GetID(a, 64, b) == (int)BlockType.SAPLING)
                                                blnValid = false;
                                    if (blnValid)
                                    {
                                        bm.SetID(x1, 64, z1, (int)BlockType.SAPLING);
                                        bm.SetData(x1, 64, z1, 15);
                                        intTrees--;
                                        intFail = 0;
                                    }
                                    else
                                    {
                                        intFail++;
                                    }
                                }
                            } while (intTrees > 0 && intFail < 200);
                            for (x1 = x + 1; x1 < x + intPlotSize - 1; x1++)
                            {
                                for (z1 = z + 1; z1 < z + intPlotSize - 1; z1++)
                                {
                                    if (bm.GetID(x1, 63, z1) != (int)BlockType.STATIONARY_WATER &&
                                        bm.GetID(x1, 64, z1) != (int)BlockType.SAPLING &&
                                        bm.GetID(x1, 63, z1) != (int)BlockType.DOUBLE_SLAB)
                                    {
                                        int intRand = rand.Next(100);
                                        if (intRand > 66)
                                        {
                                            bm.SetID(x1, 64, z1, (int)BlockType.YELLOW_FLOWER);
                                        }
                                        else if (intRand > 33)
                                        {
                                            bm.SetID(x1, 64, z1, (int)BlockType.RED_ROSE);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            BlockShapes.MakeHollowBox(x + 2, x + 10, 63, 67, z + 2, z + 10, (int)BlockType.WOOD_PLANK);
                            for (int a = 0; a <= 4; a++)
                                BlockShapes.MakeSolidBox(x + 2, x + 10, 67 + a, 67 + a, z + 2 + a, z + 10 - a, (int)BlockType.WOOD);
                            BlockShapes.MakeSolidBox(x + 10, x + 10, 65, 65, z + 6, z + 8, (int)BlockType.GLASS);
                            BlockShapes.MakeSolidBox(x + 2, x + 2, 65, 65, z + 4, z + 8, (int)BlockType.GLASS);
                            BlockShapes.MakeSolidBox(x + 4, x + 8, 65, 65, z + 2, z + 2, (int)BlockType.GLASS);
                            BlockShapes.MakeSolidBox(x + 4, x + 8, 65, 65, z + 10, z + 10, (int)BlockType.GLASS);
                            bm.SetID(x + 10, 64, z + 4, (int)BlockType.WOOD_DOOR);
                            bm.SetData(x + 10, 64, z + 4, 5);
                            bm.SetID(x + 10, 65, z + 4, (int)BlockType.WOOD_DOOR);
                            bm.SetData(x + 10, 65, z + 4, 13);
                        }
                    }
                    BlockShapes.MakeHollowLayers(x, x + intPlotSize, 63, 63, z, z + intPlotSize, (int)BlockType.DOUBLE_SLAB);
                }
            }            
        }
        public static void MakeWall()
        {
            Console.WriteLine("Making walls");
            // walls
            for (int a = intFarmSize + 6; a <= intFarmSize + 10; a++)
                BlockShapes.MakeHollowLayers(a, intMapSize - a, 58, 71, a, intMapSize - a, (int)BlockType.STONE);
            // outside and inside edges at the top
            BlockShapes.MakeHollowLayers(intFarmSize + 6, intMapSize - (intFarmSize + 6), 72, 72, intFarmSize + 6, intMapSize - (intFarmSize + 6), (int)BlockType.STONE);
            BlockShapes.MakeHollowLayers(intFarmSize + 10, intMapSize - (intFarmSize + 10), 72, 72, intFarmSize + 10, intMapSize - (intFarmSize + 10), (int)BlockType.STONE);
            // alternating blocks on top of the edges
            for (int a = intFarmSize + 7; a <= intMapSize - (intFarmSize + 7); a += 2)
                BlockShapes.MakeBlock(a, 73, intFarmSize + 6, (int)BlockType.STONE, 2);
            for (int a = intFarmSize + 11; a <= intMapSize - (intFarmSize + 11); a += 2)
                BlockShapes.MakeBlock(a, 73, intFarmSize + 10, (int)BlockType.STONE, 2);
        }
        public static void MakeMoat()
        {
            Console.WriteLine("Making moat");
            int intMoatLiquid = (int)BlockType.STATIONARY_WATER;
            if (rand.Next(100) > 75)
                intMoatLiquid = (int)BlockType.STATIONARY_LAVA;

            for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                BlockShapes.MakeHollowLayers(a, intMapSize - a, 59, 62, a, intMapSize - a, intMoatLiquid);
            for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                BlockShapes.MakeHollowLayers(a, intMapSize - a, 63, 63, a, intMapSize - a, (int)BlockType.AIR);
        }
        public static void MakeDrawbridges()
        {
            Console.WriteLine("Making drawbridges");
            // drawbridge
            BlockShapes.MakeSolidBox((intMapSize / 2) - 2, intMapSize / 2, 63, 63, intFarmSize - 2, intFarmSize + 11, (int)BlockType.STONE, 2);
            // carve out the entrance/exit
            BlockShapes.MakeSolidBox((intMapSize / 2) - 2, intMapSize / 2, 64, 67, intFarmSize + 6, intFarmSize + 10, (int)BlockType.AIR, 2);
            // add the bottom of a portcullis
            BlockShapes.MakeSolidBox((intMapSize / 2) - 2, intMapSize / 2, 67, 67, intFarmSize + 6, intFarmSize + 6, (int)BlockType.FENCE, 2);
            // ladder
            BlockShapes.MakeLadder((intMapSize / 2) - 4, 64, 72, intFarmSize + 11);
            BlockShapes.MakeLadder((intMapSize / 2) + 4, 64, 72, intFarmSize + 11);
            BlockShapes.MakeLadder((intMapSize / 2) - 4, 64, 72, intMapSize - (intFarmSize + 11));
            BlockShapes.MakeLadder((intMapSize / 2) + 4, 64, 72, intMapSize - (intFarmSize + 11));
            BlockShapes.MakeLadder(intFarmSize + 11, 64, 72, (intMapSize / 2) - 4);
            BlockShapes.MakeLadder(intFarmSize + 11, 64, 72, (intMapSize / 2) + 4);
            BlockShapes.MakeLadder(intMapSize - (intFarmSize + 11), 64, 72, (intMapSize / 2) - 4);
            BlockShapes.MakeLadder(intMapSize - (intFarmSize + 11), 64, 72, (intMapSize / 2) + 4);
        }
        #region Make Guard Towers
        public static void MakeGuardTowers()
        {
            Console.WriteLine("Making guard towers");
            // remove wall
            BlockShapes.MakeSolidBox(intFarmSize + 5, intFarmSize + 11, 64, 79, intFarmSize + 5, intFarmSize + 11, (int)BlockType.AIR, 1);
            // add tower
            BlockShapes.MakeHollowBox(intFarmSize + 4, intFarmSize + 12, 63, 80, intFarmSize + 4, intFarmSize + 12, (int)BlockType.STONE, 1);
            // divide into two rooms
            BlockShapes.MakeSolidBox(intFarmSize + 4, intFarmSize + 12, 71, 71, intFarmSize + 4, intFarmSize + 12, (int)BlockType.STONE, 1);
            // add openings to the walls
            for (int y = 72; y <= 74; y++)
                for (int x = intFarmSize + 7; x <= intFarmSize + 9; x++)
                    BlockShapes.MakeBlock(x, y, intFarmSize + 12, (int)BlockType.AIR, 2);
            // add blocks on top of the towers
            BlockShapes.MakeHollowLayers(intFarmSize + 4, intFarmSize + 12, 81, 81, intFarmSize + 4, intFarmSize + 12, (int)BlockType.STONE, 1);
            // alternating top blocks
            for (int x = intFarmSize + 4; x <= intFarmSize + 12; x += 2)
                for (int z = intFarmSize + 4; z <= intFarmSize + 12; z += 2)
                    if (x == intFarmSize + 4 || x == intFarmSize + 12 || z == intFarmSize + 4 || z == intFarmSize + 12)
                        BlockShapes.MakeBlock(x, 82, z, (int)BlockType.STONE, 1);
            // add central columns
            BlockShapes.MakeSolidBox(intFarmSize + 8, intFarmSize + 8, 72, 81, intFarmSize + 8, intFarmSize + 8, (int)BlockType.STONE, 1);
            BlockShapes.MakeLadder(intFarmSize + 7, 72, 81, intFarmSize + 8, 0, 1);
            BlockShapes.MakeLadder(intFarmSize + 9, 72, 81, intFarmSize + 8, 0, 1);
            BlockShapes.MakeLadder(intFarmSize + 8, 72, 81, intFarmSize + 7, 0, 1);
            BlockShapes.MakeLadder(intFarmSize + 8, 72, 81, intFarmSize + 9, 0, 1);
            // add cobwebs
            BlockShapes.MakeBlock(intFarmSize + 5, 79, intFarmSize + 5, 30, 1, 30);
            BlockShapes.MakeBlock(intFarmSize + 5, 79, intFarmSize + 11, 30, 1, 30);
            BlockShapes.MakeBlock(intFarmSize + 11, 79, intFarmSize + 5, 30, 1, 30);
            BlockShapes.MakeBlock(intFarmSize + 11, 79, intFarmSize + 11, 30, 1, 30);
            // add chests
            MakeGuardChest(intFarmSize + 5, 72, intFarmSize + 5);
            MakeGuardChest(intMapSize - (intFarmSize + 5), 72, intFarmSize + 5);
            MakeGuardChest(intFarmSize + 5, 72, intMapSize - (intFarmSize + 5));
            MakeGuardChest(intMapSize - (intFarmSize + 5), 72, intMapSize - (intFarmSize + 5));
        }
        private static void MakeGuardChest(int x, int y, int z)
        {
            TileEntityChest tec = new TileEntityChest();
            // todo: replace numbers with constants when next version of Substrate is released
            for (int a = 0; a < 5; a++)
                tec.Items[a] = MakeItem(SelectRandomNumber(267, 268, 272), 1); // random sword (iron, wood, stone)
            tec.Items[6] = MakeItem(261, 1); // crossbow
            tec.Items[7] = MakeItem(262, 64); // arrows
            int intArmourStartID = SelectRandomNumber(298, 302, 306);
            for (int a = 9; a < 18; a++)
                tec.Items[a] = MakeItem(SelectRandomNumber(intArmourStartID, intArmourStartID + 1,
                                                           intArmourStartID + 2, intArmourStartID + 3), 1); // random armour
            bm.SetID(x, y, z, (int)BlockType.CHEST);
            bm.SetTileEntity(x, y, z, tec);
        }
        #endregion
        public static void MakeNoticeBoard()
        {
            TextGenerators tg = new TextGenerators();
            for (int x = (intMapSize / 2) + 6; x < (intMapSize / 2) + 9; x++)
                for (int y = 65; y < 68; y++)
                    MakeSign(x, y, intFarmSize + 11, tg.RandomSign(), (int)BlockType.STONE);
        }
        public static void MakeFarms()
        {
            Console.WriteLine("Making farms");
            int intFarms = intMapSize / 6;
            while (intFarms > 0)
            {
                int xlen = rand.Next(6, 14);
                int x1 = rand.Next(0, intMapSize - xlen);
                int zlen = rand.Next(6, 14);
                int z1 = rand.Next(0, intMapSize - zlen);
                if (!(x1 >= intFarmSize && z1 >= intFarmSize && x1 <= intMapSize - intFarmSize && z1 <= intMapSize - intFarmSize))
                {
                    bool blnValid = true;
                    for (int x = x1 - 2; x <= x1 + xlen + 2 && blnValid; x++)
                        for (int z = z1 - 2; z <= z1 + zlen + 2 && blnValid; z++)
                            if (bm.GetID(x, 63, z) != (int)BlockType.GRASS || bm.GetID(x, 64, z) != (int)BlockType.AIR)
                                blnValid = false;
                    if (blnValid)
                    {
                        int intFarmType = rand.Next(100);
                         for (int x = x1 + 1; x <= x1 + xlen - 1; x++)
                        {
                            for (int z = z1 + 1; z <= z1 + zlen - 1; z++)
                            {
                                if (z == z1 + 1)
                                {
                                    if (intFarmType < 75)
                                        bm.SetID(x, 63, z, (int)BlockType.DOUBLE_SLAB);
                                }
                                else if (x % 2 == 0)
                                {
                                    if (intFarmType >= 75)
                                    {
                                        bm.SetID(x, 64, z, (int)BlockType.SUGAR_CANE);
                                        if (rand.Next(100) > 50)
                                            bm.SetID(x, 65, z, (int)BlockType.SUGAR_CANE);
                                    }
                                    else
                                    {
                                        bm.SetID(x, 63, z, (int)BlockType.FARMLAND);
                                        bm.SetData(x, 63, z, 1);
                                        bm.SetID(x, 64, z, (int)BlockType.CROPS);
                                    }
                                }
                                else
                                {
                                    bm.SetID(x, 63, z, (int)BlockType.STATIONARY_WATER);
                                }
                            }
                        }
                        BlockShapes.MakeHollowLayers(x1, x1 + xlen, 64, 64, z1, z1 + zlen, (int)BlockType.FENCE);
                        int d = rand.Next(x1 + 1, x1 + xlen - 1);
                        if (intFarmType < 75)
                            bm.SetID(d, 63, z1, (int)BlockType.DOUBLE_SLAB);
                        bm.SetID(d, 64, z1, (int)BlockType.WOOD_DOOR);
                        bm.SetData(d, 64, z1, 4);
                        bm.SetID(d, 65, z1, (int)BlockType.WOOD_DOOR);
                        bm.SetData(d, 65, z1, 12);
                        intFarms--;
                    }
                }
            }
        }
        
        private static void MakeSign(int x, int y, int z, string strSignText, int intBlockAgainst)
        {
            bm.SetID(x, y, z, (int)BlockType.WALL_SIGN);
            Substrate.TileEntities.TileEntitySign tes = new Substrate.TileEntities.TileEntitySign();
            string[] strRandomSign = strSignText.Split('|');
            for (int a = 0; a <= 3; a++)
            {
                while (strRandomSign[a].Length < 12)
                {
                    strRandomSign[a] += ' ';
                    if (strRandomSign[a].Length < 12)
                        strRandomSign[a] = ' ' + strRandomSign[a];
                }
            }
            tes.Text1 = strRandomSign[0];
            tes.Text2 = strRandomSign[1];
            tes.Text3 = strRandomSign[2];
            tes.Text4 = strRandomSign[3];
            bm.SetTileEntity(x, y, z, tes);
            bm.SetData(x, y, z, BlockShapes.BlockDirection(x, y, z, intBlockAgainst));
        }
        public static Item MakeItem(int intID, int intCount = 1)
        {
            Item i = new Item();
            i.ID = intID;
            i.Count = intCount;
            return i;
        }
        public static int SelectRandomNumber(params int[] intNumbers)
        {
            return intNumbers[rand.Next(intNumbers.Length)];
        }
    }
}
