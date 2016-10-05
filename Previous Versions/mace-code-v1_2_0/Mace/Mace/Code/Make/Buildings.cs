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
using Substrate.TileEntities;

namespace Mace
{
    class Plots
    {
        static Random rand = new Random();
        public static void MakeBuildings(BlockManager bm, bool[,] booSewerEntrances,
                                        int intFarmSize, int intMapSize, int intSewerSectionSize)
        {           
            int intPlotBlocks = (1 + intMapSize) - ((intFarmSize + 18) * 2);
            int[,] intArea = new int[intPlotBlocks, intPlotBlocks];

            for (int x = 0; x <= booSewerEntrances.GetUpperBound(0); x++)
            {
                for (int z = 0; z <= booSewerEntrances.GetUpperBound(1); z++)
                {
                    if (booSewerEntrances[x, z])
                    {
                        int a = intFarmSize + 20 + (x * intSewerSectionSize);
                        int b = intFarmSize + 20 + (z * intSewerSectionSize);
                        MakeSewerEntrance(bm, a, b, 8);
                        for (int xMap = a; xMap <= a + 8; xMap++)
                        {
                            for (int zMap = b; zMap <= b + 8; zMap++)
                            {
                                if (xMap == a || zMap == b || xMap == a + 8 || zMap == b + 8)
                                {
                                    intArea[xMap - (intFarmSize + 20), zMap - (intFarmSize + 20)] = 2;
                                }
                                else
                                {
                                    intArea[xMap - (intFarmSize + 20), zMap - (intFarmSize + 20)] = 2;
                                }
                            }
                        }
                    }
                }
            }
            for (int intPlotSize = 12; intPlotSize > 3; intPlotSize -= 2)
            {
                for (int intAttempts = 0; intAttempts < 1000; intAttempts++)
                {
                    int x = rand.Next(1 + (intArea.GetUpperBound(0) / 2)) * 2;
                    int z = rand.Next(1 + (intArea.GetUpperBound(1) / 2)) * 2;
                    if (x + intPlotSize <= intArea.GetUpperBound(0) && z + intPlotSize <= intArea.GetUpperBound(1))
                    {
                        bool booValid = true;
                        for (int d = x; d < x + intPlotSize; d++)
                        {
                            for (int e = z; e < z + intPlotSize; e++)
                            {
                                if (intArea[d, e] == 2)
                                {
                                    booValid = false;
                                }
                            }
                        }
                        if (booValid)
                        {
                            int xMap = x + (intFarmSize + 18);
                            int zMap = z + (intFarmSize + 18);
                            BlockShapes.MakeHollowLayers(xMap, xMap + intPlotSize, 63, 63,
                                                         zMap, zMap + intPlotSize, (int)BlockType.DOUBLE_SLAB);
                            switch (intPlotSize)
                            {
                                case 12:
                                    MakeHouse(bm, xMap, zMap);
                                    break;
                                case 10:
                                    MakePark(bm, xMap, zMap);
                                    break;
                                case 8:
                                    break;
                                case 6:
                                    break;
                                case 4:
                                    MakeSmallPark(bm, xMap, zMap);
                                    break;
                            }
                            for (int d = x; d <= x + intPlotSize; d++)
                            {
                                for (int e = z; e <= z + intPlotSize; e++)
                                {
                                    if (d == x || e == z || d == x + intPlotSize || e == z + intPlotSize)
                                    {
                                        intArea[d, e] = 1;
                                    }
                                    else
                                    {
                                        intArea[d, e] = 2;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void MakeHouse(BlockManager bm, int x, int z)
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
            BlockShapes.RotateBlocks(x + 2, x + 10, 63, 71, z + 2, z + 10);
        }
        private static void MakePark(BlockManager bm, int x, int z)
        {
            BlockShapes.MakeSolidBox(x + 4, x + 6, 63, 63, z + 4, z + 6, (int)BlockType.STATIONARY_WATER);
            for (int a = x + 1; a <= x + 7; a += 6)
            {
                for (int b = z + 1; b <= z + 7; b += 6)
                {
                    bm.SetID(a + 1, 64, b + 1, (int)BlockType.SAPLING);
                    bm.SetData(a + 1, 64, b + 1, 15);
                }
            }
            for (int x1 = x + 1; x1 <= x + 9; x1++)
            {
                for (int z1 = z + 1; z1 <= z + 9; z1++)
                {
                    if (bm.GetID(x1, 64, z1) != (int)BlockType.SAPLING &&
                        bm.GetID(x1, 63, z1) != (int)BlockType.STATIONARY_WATER)
                    {
                        int intRand = rand.Next(100);
                        if (intRand > 55)
                        {
                            bm.SetID(x1, 64, z1, (int)BlockType.YELLOW_FLOWER);
                        }
                        else if (intRand > 20)
                        {
                            bm.SetID(x1, 64, z1, (int)BlockType.RED_ROSE);
                        }
                    }
                }
            }
        }
        private static void MakeSmallPark(BlockManager bm, int x, int z)
        {
            for (int x1 = x + 1; x1 <= x + 3; x1++)
            {
                for (int z1 = z + 1; z1 <= z + 3; z1++)
                {
                    int intRand = rand.Next(100);
                    if (intRand > 55)
                    {
                        bm.SetID(x1, 64, z1, (int)BlockType.YELLOW_FLOWER);
                    }
                    else if (intRand > 20)
                    {
                        bm.SetID(x1, 64, z1, (int)BlockType.RED_ROSE);
                    }
                }
            }
            bm.SetID(x + 2, 64, z + 2, (int)BlockType.SAPLING);
        }
        private static void MakeSewerEntrance(BlockManager bm, int x, int z, int intPlotSize)
        {
            // path
            BlockShapes.MakeHollowLayers(x, x + 8, 63, 63, z, z + intPlotSize, (int)BlockType.DOUBLE_SLAB);
            // building
            BlockShapes.MakeHollowBox(x + 2, x + 6, 63, 67, z + 2, z + 6, (int)BlockType.STONE);
            // doorway
            BlockShapes.MakeSolidBox(x + 4, x + 4, 64, 65, z + 2, z + 2, (int)BlockType.AIR);
            // tunnel down
            BlockShapes.MakeSolidBox(x + 4, x + 4, 55, 63, z + 4, z + 4, (int)BlockType.AIR);
            // ladder back
            BlockShapes.MakeSolidBox(x + 4, x + 4, 52, 55, z + 5, z + 5, (int)BlockType.STONE);
            // ladder rungs
            BlockHelper.MakeLadder(x + 4, 52, 63, z + 4);
            // hatch
            bm.SetID(x + 4, 64, z + 4, (int)BlockType.TRAPDOOR);
            bm.SetData(x + 4, 64, z + 4, 2);
            // chest with torches
            bm.SetID(x + 5, 64, z + 5, (int)BlockType.CHEST);
            TileEntityChest tec = new TileEntityChest();
            tec.Items[0] = BlockHelper.MakeItem((int)BlockType.REDSTONE_TORCH_ON, 64);
            tec.Items[1] = BlockHelper.MakeItem((int)BlockType.TORCH, 32);
            bm.SetTileEntity(x + 5, 64, z + 5, tec);
            // sign
            BlockHelper.MakeSign(x + 4, 66, z + 1, "Sewers||Currently|empty!", (int)BlockType.STONE);
        }
    }
}
