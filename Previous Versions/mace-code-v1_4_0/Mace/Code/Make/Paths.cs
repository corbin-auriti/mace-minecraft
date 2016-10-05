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

namespace Mace
{
    class Paths
    {

        static Random rand = new Random();
        static int intBlockStart = 0;
        static int intStreet = 0;

        public static int[,] MakePaths(BetaWorld world, BlockManager bm, int intFarmSize, int intMapSize)
        {
            intStreet = 0;
            intBlockStart = intFarmSize + 14;
            int intPlotBlocks = (1 + intMapSize) - (intBlockStart * 2);
            int[,] intArea = new int[intPlotBlocks, intPlotBlocks];
            // make the main roads
            for (int a = 0; a <= intArea.GetUpperBound(0); a++)
            {
                for (int b = -1; b <= 1; b++)
                {
                    intArea[a, (intArea.GetUpperBound(0) / 2) + b] = 1;
                    intArea[(intArea.GetUpperBound(0) / 2) + b, a] = 1;
                }
            }
            // make the districts
            SplitArea(bm, intArea, 0, 0,
                                   (intArea.GetUpperBound(0) / 2) - 1, (intArea.GetUpperBound(0) / 2) - 1);
            SplitArea(bm, intArea, (intArea.GetUpperBound(0) / 2) + 1, (intArea.GetUpperBound(0) / 2) + 1,
                                   intArea.GetUpperBound(0), intArea.GetUpperBound(0));
            SplitArea(bm, intArea, (intArea.GetUpperBound(0) / 2) + 1, 0,
                                   intArea.GetUpperBound(0), (intArea.GetUpperBound(0) / 2) - 1);
            SplitArea(bm, intArea, 0, (intArea.GetUpperBound(0) / 2) + 1,
                                   (intArea.GetUpperBound(0) / 2) - 1, intArea.GetUpperBound(0));
            MakePaths(world, bm, intArea);
            MakeStreetLights(bm, intMapSize, intFarmSize);
            return intArea;
        }
        private static void SplitArea(BlockManager bm, int[,] intArea, int x1, int z1, int x2, int z2)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = z1; y <= z2; y++)
                {
                    if (x == x1 || x == x2 || y == z1 || y == z2)
                    {
                        if (intArea[x, y] < 500)
                        {
                            intArea[x, y] = 1;
                        }
                    }
                }
            }
            bool booPossibleToSplit = true;
            bool booSplitByX = false;
            if (Math.Abs(x1 - x2) > 50 && Math.Abs(z1 - z2) > 50)
            {
                booSplitByX = rand.NextDouble() > 0.5;
            }
            else if (Math.Abs(x1 - x2) > 50)
            {
                booSplitByX = true;
            }
            else if (Math.Abs(z1 - z2) <= 50)
            {
                booPossibleToSplit = false;
            }
            if (booPossibleToSplit)
            {
                if (booSplitByX)
                {
                    int intSplitPoint = rand.Next(x1 + 20, x2 - 20);
                    SplitArea(bm, intArea, x1, z1, intSplitPoint, z2);
                    SplitArea(bm, intArea, intSplitPoint, z1, x2, z2);
                    intStreet++;
                    MakeStreetSign(bm, intSplitPoint - 1, z1 + 1, intSplitPoint - 1, z2 - 1);
                }
                else
                {
                    int intSplitPoint = rand.Next(z1 + 20, z2 - 20);
                    SplitArea(bm, intArea, x1, z1, x2, intSplitPoint);
                    SplitArea(bm, intArea, x1, intSplitPoint, x2, z2);
                    intStreet++;
                    MakeStreetSign(bm, x1 + 1, intSplitPoint - 1, x2 - 1, intSplitPoint - 1);
                }
            }
            else
            {
                int[,] intDistrict = FillArea((x2 - x1) - 1, (z2 - z1) - 1, x1, z1);
                for (int x = 0; x < intDistrict.GetUpperBound(0); x++)
                {
                    for (int y = 0; y < intDistrict.GetUpperBound(1); y++)
                    {
                        intArea[x1 + x + 1, z1 + y + 1] = intDistrict[x, y];
                    }
                }
            }
        }
        private static int[,] FillArea(int intSizeX, int intSizeY, int intStartX, int intStartZ)
        {
            int[,] intDist = new int[intSizeX, intSizeY];
            int[,] intFinal = new int[intSizeX, intSizeY];
            int intWasted = intSizeX * intSizeY, intAttempts = 15, intFail = 0;
            do
            {
                do
                {
                    SourceWorld.Building CurrentBuilding = SourceWorld.SelectRandomBuilding();
                    bool booFound = false;
                    if (rand.NextDouble() > 0.5)
                    {
                        intDist = RotateArray(intDist, -1);
                    }
                    int x, z = 0;
                    for (x = 0; x < intDist.GetLength(0) - CurrentBuilding.intSize && !booFound; x++)
                    {
                        for (z = 0; z < intDist.GetLength(1) - CurrentBuilding.intSize && !booFound; z++)
                        {
                            booFound = IsFree(intDist, x, z, x + CurrentBuilding.intSize, z + CurrentBuilding.intSize);
                        }
                    }
                    x--;
                    z--;
                    if (booFound)
                    {
                        for (int a = x + 1; a <= x + CurrentBuilding.intSize - 1; a++)
                        {
                            for (int b = z + 1; b <= z + CurrentBuilding.intSize - 1; b++)
                            {
                                intDist[a, b] = 2;
                            }
                        }
                        intDist[x + 1, z + 1] = 100 + CurrentBuilding.intID;
                        intDist[x + CurrentBuilding.intSize - 1, z + CurrentBuilding.intSize - 1] = 100 + CurrentBuilding.intID;
                        intFail = 0;
                    }
                    else
                    {
                        intFail++;
                    }
                } while (intFail < 10);
                int intCurWasted = SquaresWasted(intDist);
                if (intCurWasted < intWasted)
                {
                    intFinal = new int[intDist.GetLength(0), intDist.GetLength(1)];
                    Array.Copy(intDist, intFinal, intDist.Length);
                    intWasted = intCurWasted;
                    intAttempts = 10;
                }
                Array.Clear(intDist, 0, intDist.Length);
                intAttempts--;
            } while (intAttempts > 0);
            if (intSizeX == intFinal.GetLength(1))
            {
                intFinal = RotateArray(intFinal, 1);
            }
            return intFinal;
        }
        private static int SquaresWasted(int[,] intCheck)
        {
            int intWasted = 0;
            for (int x = 0; x < intCheck.GetLength(0); x++)
            {
                for (int y = 0; y < intCheck.GetLength(1); y++)
                {
                    if (intCheck[x, y] == 0)
                    {
                        intWasted++;
                    }
                }
            }
            return intWasted;
        }
        private static bool IsFree(int[,] intArray, int x1, int y1, int x2, int y2)
        {
            bool booValid = true;
            for (int a = x1; a <= x2 && booValid; a++)
            {
                for (int b = y1; b <= y2 && booValid; b++)
                {
                    if (intArray[a, b] > 0)
                    {
                        booValid = false;
                    }
                }
            }
            return booValid;
        }
        private static int[,] RotateArray(int[,] intArray, int intRotate = -1)
        {
            if (intRotate == -1)
            {
                intRotate = rand.Next(4);
            }
            if (intRotate == 0)
            {
                return intArray;
            }
            else
            {
                int[,] intNewArray;
                if (intRotate == 2)
                {
                    intNewArray = new int[intArray.GetLength(0), intArray.GetLength(1)];
                }
                else
                {
                    intNewArray = new int[intArray.GetLength(1), intArray.GetLength(0)];
                }
                for (int x = 0; x < intArray.GetLength(0); x++)
                {
                    for (int y = 0; y < intArray.GetLength(1); y++)
                    {
                        switch (intRotate)
                        {
                            case 1:
                                intNewArray[y, x] = intArray[x, y];
                                break;
                            case 2:
                                intNewArray[x, y] = intArray[intArray.GetUpperBound(0) - x,
                                                             intArray.GetUpperBound(1) - y];
                                break;
                            case 3:
                                intNewArray[intNewArray.GetUpperBound(0) - y,
                                            intNewArray.GetUpperBound(1) - x] = intArray[x, y];
                                break;
                        }
                    }
                }
                return intNewArray;
            }
        }
        private static void MakePaths(BetaWorld world, BlockManager bm, int[,] intArea)
        {
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int z = 0; z < intArea.GetLength(1); z++)
                {
                    if (intArea[x, z] == 1)
                    {
                        bm.SetID(intBlockStart + x, 63, intBlockStart + z, (int)BlockType.DOUBLE_SLAB);
                    }
                }
                if (x % 20 == 0)
                {
                    world.Save();
                }
            }
        }
        private static void MakeStreetLights(BlockManager bm, int intMapSize, int intFarmSize)
        {
            for (int a = intMapSize / 2; a >= intFarmSize + 16; a -= 8)
            {
                BlockShapes.MakeSolidBox(a, a, 64, 66, intMapSize / 2, intMapSize / 2, (int)BlockType.FENCE, 2);
                BlockShapes.MakeBlock(a, 67, intMapSize / 2, (int)BlockType.WOOD_PLANK, 2);
                BlockHelper.MakeTorch(a - 1, 67, intMapSize / 2, (int)BlockType.WOOD_PLANK, 2);
                BlockHelper.MakeTorch(a + 1, 67, intMapSize / 2, (int)BlockType.WOOD_PLANK, 2);
                BlockHelper.MakeTorch(a, 67, (intMapSize / 2) - 1, (int)BlockType.WOOD_PLANK, 2);
                BlockHelper.MakeTorch(a, 67, (intMapSize / 2) + 1, (int)BlockType.WOOD_PLANK, 2);
            }
        }
        private static void MakeStreetSign(BlockManager bm, int x1, int z1, int x2, int z2)
        {
            x1 += intBlockStart;
            z1 += intBlockStart;
            x2 += intBlockStart;
            z2 += intBlockStart;
            bm.SetID(x1, 64, z1, (int)BlockType.WOOD_PLANK);
            bm.SetID(x2, 64, z2, (int)BlockType.WOOD_PLANK);
            if (z1 == z2)
            {
                x1--;
                x2++;
            }
            else
            {
                z1--;
                z2++;
            }
            string strStreetName = RandomHelper.RandomFileLine("Resources\\CityStartingWords.txt");
            string strStreetType = RandomHelper.RandomFileLine("Resources\\RoadTypes.txt"); 
            BlockHelper.MakeSign(x1, 64, z1, "|" + strStreetName + "|" + strStreetType + "|", (int)BlockType.WOOD_PLANK);
            BlockHelper.MakeSign(x2, 64, z2, "|" + strStreetName + "|" + strStreetType + "|", (int)BlockType.WOOD_PLANK);
        }
    }
}
