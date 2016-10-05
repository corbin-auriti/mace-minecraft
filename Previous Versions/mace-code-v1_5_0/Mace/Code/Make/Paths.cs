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
using System.Collections.Generic;
using Substrate;
using System.Diagnostics;
using System.IO;

namespace Mace
{
    class Paths
    {
        static int intBlockStart = 0;
        static int intStreet = 0;
        static List<string> strStreetsUsed = new List<string>();

        public static int[,] MakePaths(BetaWorld world, BlockManager bm, int intFarmSize, int intMapSize)
        {
            intStreet = 0;
            intBlockStart = intFarmSize + 13;
            strStreetsUsed.Clear();
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
            // top right
            SplitArea(bm, intArea, 0, 0,
                                   (intArea.GetLength(0) / 2) - 2, (intArea.GetLength(0) / 2) - 2);
            // bottom left
            SplitArea(bm, intArea, (intArea.GetLength(0) / 2) + 2, (intArea.GetLength(0) / 2) + 2,
                                   intArea.GetUpperBound(0), intArea.GetUpperBound(0));
            // bottom right
            SplitArea(bm, intArea, (intArea.GetLength(0) / 2) + 2, 0,
                                   intArea.GetUpperBound(0), (intArea.GetLength(0) / 2) - 2);
            // top left
            SplitArea(bm, intArea, 0, (intArea.GetLength(0) / 2) + 2,
                                   (intArea.GetLength(0) / 2) - 2, intArea.GetUpperBound(0));
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
                booSplitByX = RandomHelper.NextDouble() > 0.5;
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
                    int intSplitPoint = RandomHelper.Next(x1 + 20, x2 - 20);
                    SplitArea(bm, intArea, x1, z1, intSplitPoint, z2);
                    SplitArea(bm, intArea, intSplitPoint, z1, x2, z2);
                    intStreet++;
                    MakeStreetSign(bm, intSplitPoint - 1, z1 + 1, intSplitPoint - 1, z2 - 1);
                }
                else
                {
                    int intSplitPoint = RandomHelper.Next(z1 + 20, z2 - 20);
                    SplitArea(bm, intArea, x1, z1, x2, intSplitPoint);
                    SplitArea(bm, intArea, x1, intSplitPoint, x2, z2);
                    intStreet++;
                    MakeStreetSign(bm, x1 + 1, intSplitPoint - 1, x2 - 1, intSplitPoint - 1);
                }
            }
            else
            {
                int[,] intDistrict = FillArea(intArea, (x2 - x1), (z2 - z1), x1, z1);
                for (int x = 0; x < intDistrict.GetUpperBound(0) - 1; x++)
                {
                    for (int y = 0; y < intDistrict.GetUpperBound(1) - 1; y++)
                    {
                        intArea[x1 + x + 1, z1 + y + 1] = intDistrict[x + 1, y + 1];
                    }
                }
            }
        }
        private static int[,] FillArea(int[,] intArea, int intSizeX, int intSizeZ, int intStartX, int intStartZ)
        {            
            int[,] intDistrict = new int[intSizeX, intSizeZ];
            int[,] intFinal = new int[intSizeX, intSizeZ];
            int intWasted = intSizeX * intSizeZ, intAttempts = 15, intFail = 0;
            List<int> lstBuildings = new List<int>(); 
            do
            {
                lstBuildings.Clear();
                do
                {                    
                    SourceWorld.Building CurrentBuilding;
                    do
                    {
                        CurrentBuilding = SourceWorld.SelectRandomBuilding();
                    } while (!IsValidBuilding(CurrentBuilding, lstBuildings, intArea, intStartX, intStartZ, intSizeX, intSizeZ));
                    bool booFound = false;
                    if (RandomHelper.NextDouble() > 0.5)
                    {
                        intDistrict = RotateArray(intDistrict, RandomHelper.Next(4));
                    }
                    int x, z = 0;
                    for (x = 0; x < intDistrict.GetLength(0) - CurrentBuilding.intSize && !booFound; x++)
                    {
                        for (z = 0; z < intDistrict.GetLength(1) - CurrentBuilding.intSize && !booFound; z++)
                        {
                            booFound = IsFree(intDistrict, x, z, x + CurrentBuilding.intSize, z + CurrentBuilding.intSize);
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
                                intDistrict[a, b] = 2;
                            }
                        }
                        lstBuildings.Add(CurrentBuilding.intID);
                        intDistrict[x + 1, z + 1] = 100 + CurrentBuilding.intID;
                        intDistrict[x + CurrentBuilding.intSize - 1, z + CurrentBuilding.intSize - 1] = 100 + CurrentBuilding.intID;
                        intFail = 0;
                    }
                    else
                    {
                        intFail++;
                    }
                } while (intFail < 10);
                
                int intCurWasted = SquaresWasted(intDistrict);
                if (intCurWasted < intWasted)
                {
                    intFinal = new int[intDistrict.GetLength(0), intDistrict.GetLength(1)];
                    Array.Copy(intDistrict, intFinal, intDistrict.Length);
                    intWasted = intCurWasted;
                    intAttempts = 10;
                }
                Array.Clear(intDistrict, 0, intDistrict.Length);
                intAttempts--;
            } while (intAttempts > 0);
            if (intSizeX == intFinal.GetLength(1))
            {
                intFinal = RotateArray(intFinal, 1);
            }
            return intFinal;
        }
        private static bool IsValidBuilding(SourceWorld.Building bldCheck, List<int> lstBuildings, int[,] intArea,
                                            int intStartX, int intStartZ, int intSizeX, int intSizeZ)
        {
            switch (bldCheck.strFrequency)
            {
                case "very common":
                case "common":
                    return true;
                case "average":
                case "rare":
                case "very rare":
                    if (lstBuildings.Contains(bldCheck.intID))
                    {
                        return false;
                    }
                    else
                    {
                        int intDistance = 0;
                        switch (bldCheck.strFrequency)
                        {
                            case "average":
                                intDistance = 12;
                                break;
                            case "rare":
                                intDistance = 25;
                                break;
                            case "very rare":
                                intDistance = 50;
                                break;
                        }
                        for (int x = intStartX - intDistance; x < intStartX + intSizeX + intDistance; x++)
                        {
                            for (int z = intStartZ - intDistance; z < intStartZ + intSizeZ + intDistance; z++)
                            {
                                if (x >= 0 && z >= 0 && x <= intArea.GetUpperBound(0) && z <= intArea.GetUpperBound(1))
                                {
                                    if (intArea[x, z] - 100 == bldCheck.intID)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }
                // should never get here to either of these, but just in case
                case "exclude":
                    Debug.WriteLine("Excluded buildings are not allowed here");
                    return false;
                default:
                    Debug.WriteLine("Unknown frequency type encountered");
                    return false;
            }
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
        private static int[,] RotateArray(int[,] intArray, int intRotate)
        {
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
                        if (Math.Abs(x - (intArea.GetLength(0) / 2)) == 2 &&
                            Math.Abs(z - (intArea.GetLength(1) / 2)) == 2)
                        {
                            // don't need these
                        }
                        else if (Math.Abs(x - (intArea.GetLength(0) / 2)) == 2 ||
                                 Math.Abs(z - (intArea.GetLength(1) / 2)) == 2)
                        {
                            if (MultipleNeighbouringPaths(intArea, x, z))
                            {
                                bm.SetID(intBlockStart + x, 63, intBlockStart + z, (int)BlockType.DOUBLE_SLAB);
                            }
                        }
                        else
                        {
                            bm.SetID(intBlockStart + x, 63, intBlockStart + z, (int)BlockType.DOUBLE_SLAB);
                        }
                    }
                }
                if (x % 20 == 0)
                {
                    world.Save();
                }
            }
        }
        private static bool MultipleNeighbouringPaths(int[,] intArea, int x, int z)
        {
            int intPaths = 0;
            if (x == 0 || intArea[x - 1, z] == 1)
            {
                intPaths++;
            }
            if (z == 0 || intArea[x , z - 1] == 1)
            {
                intPaths++;
            }
            if (x == intArea.GetUpperBound(0) || intArea[x + 1, z] == 1)
            {
                intPaths++;
            }
            if (z == intArea.GetUpperBound(1) || intArea[x, z + 1] == 1)
            {
                intPaths++;
            }
            return intPaths == 4;
        }
        private static void MakeStreetLights(BlockManager bm, int intMapSize, int intFarmSize)
        {
            for (int a = 2; a <= (intMapSize / 2) - (intFarmSize + 16); a += 8)
            {
                MakeStreetLight(bm, (intMapSize / 2) - a, (intMapSize / 2) - 2,
                                    (intMapSize / 2) - a, (intMapSize / 2) - 1);
                MakeStreetLight(bm, (intMapSize / 2) - a, (intMapSize / 2) + 2,
                                    (intMapSize / 2) - a, (intMapSize / 2) + 1);
                MakeStreetLight(bm, (intMapSize / 2) + a, (intMapSize / 2) - 2,
                                    (intMapSize / 2) + a, (intMapSize / 2) - 1);
                MakeStreetLight(bm, (intMapSize / 2) + a, (intMapSize / 2) + 2,
                                    (intMapSize / 2) + a, (intMapSize / 2) + 1);

                MakeStreetLight(bm, (intMapSize / 2) - 2, (intMapSize / 2) - a,
                                    (intMapSize / 2) - 1, (intMapSize / 2) - a);
                MakeStreetLight(bm, (intMapSize / 2) + 2, (intMapSize / 2) - a,
                                    (intMapSize / 2) + 1, (intMapSize / 2) - a);
                MakeStreetLight(bm, (intMapSize / 2) - 2, (intMapSize / 2) + a,
                                    (intMapSize / 2) - 1, (intMapSize / 2) + a);
                MakeStreetLight(bm, (intMapSize / 2) + 2, (intMapSize / 2) + a,
                                    (intMapSize / 2) + 1, (intMapSize / 2) + a);
            }
        }
        private static void MakeStreetLight(BlockManager bm, int x1, int y1, int x2, int y2)
        {
            if (bm.GetID(x1, 64, y1) == (int)BlockType.AIR)
            {
                bm.SetID(x1, 64, y1, (int)BlockType.FENCE);
                bm.SetID(x1, 65, y1, (int)BlockType.FENCE);
                bm.SetID(x1, 66, y1, (int)BlockType.FENCE);
                bm.SetID(x1, 67, y1, (int)BlockType.FENCE);
                bm.SetID(x2, 67, y2, (int)BlockType.FENCE);
                bm.SetID(x2, 66, y2, (int)BlockType.GLOWSTONE_BLOCK);
                //if (bm.GetID(x2 - 1, 66, y2) == (int)BlockType.AIR)
                //{
                //    BlockHelper.MakeTorch(x2 - 1, 66, y2, (int)BlockType.WOOD_PLANK);
                //}
                //if (bm.GetID(x2, 66, y2 - 1) == (int)BlockType.AIR)
                //{
                //    BlockHelper.MakeTorch(x2, 66, y2 - 1, (int)BlockType.WOOD_PLANK);
                //}
                //if (bm.GetID(x2, 66, y2 + 1) == (int)BlockType.AIR)
                //{
                //    BlockHelper.MakeTorch(x2, 66, y2 + 1, (int)BlockType.WOOD_PLANK);
                //}
                //if (bm.GetID(x2 + 1, 66, y2) == (int)BlockType.AIR)
                //{
                //    BlockHelper.MakeTorch(x2 + 1, 66, y2, (int)BlockType.WOOD_PLANK);
                //}
            }
        }
        private static void MakeStreetSign(BlockManager bm, int x1, int z1, int x2, int z2)
        {
            x1 += intBlockStart;
            z1 += intBlockStart;
            x2 += intBlockStart;
            z2 += intBlockStart;
            bm.SetID(x1, 64, z1, (int)BlockType.WOOD_PLANK);
            bm.SetID(x1, 65, z1, (int)BlockType.TORCH);
            bm.SetID(x2, 64, z2, (int)BlockType.WOOD_PLANK);
            bm.SetID(x2, 65, z2, (int)BlockType.TORCH);
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
            string strStreetName, strStreetType;
            do
            {
                strStreetName = RandomHelper.RandomFileLine("Resources\\CityAdj.txt");
                strStreetType = RandomHelper.RandomFileLine("Resources\\RoadTypes.txt");
            } while (strStreetsUsed.Contains(strStreetName + " " + strStreetType));
            strStreetsUsed.Add(strStreetName + " " + strStreetType);
            BlockHelper.MakeSign(x1, 64, z1, "|" + strStreetName + "|" + strStreetType + "|", (int)BlockType.WOOD_PLANK);            
            BlockHelper.MakeSign(x2, 64, z2, "|" + strStreetName + "|" + strStreetType + "|", (int)BlockType.WOOD_PLANK);            
        }
    }
}