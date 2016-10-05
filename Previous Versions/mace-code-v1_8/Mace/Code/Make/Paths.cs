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
using System.Diagnostics;
using System.IO;
using Substrate;

namespace Mace
{
    static class Paths
    {

        static int _intBlockStart = 0;
        static int _intStreet = 0;
        struct structDistrict
        {
            public int x1;
            public int x2;
            public int z1;
            public int z2;
        }
        static List<structDistrict> _lstDistricts = new List<structDistrict>();
        static List<string> _lstStreetsUsed = new List<string>();
        static List<int> _lstAllBuildings = new List<int>();
        static bool _booIncludedMineshaft = false;

        public static int[,] MakePaths(BetaWorld world, BlockManager bm)
        {
            _intBlockStart = City.FarmLength + 13; 
            _intStreet = 0;
            _lstDistricts.Clear();
            _lstStreetsUsed.Clear();
            _lstAllBuildings.Clear();            
            // if the user doesn't want a mineshaft, we just tell Mace it's already been added
            _booIncludedMineshaft = !City.HasMineshaft;
            int intPlotBlocks = (1 + City.MapLength) - (_intBlockStart * 2);
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

            ShuffleDistricts(_lstDistricts);
            foreach (structDistrict stdCurrent in _lstDistricts)
            {
                int[,] intDistrict = FillArea(intArea, (stdCurrent.x2 - stdCurrent.x1),
                                              (stdCurrent.z2 - stdCurrent.z1), stdCurrent.x1, stdCurrent.z1,
                                              City.CityLength > 5 * 16);
                for (int x = 0; x < intDistrict.GetUpperBound(0) - 1; x++)
                {
                    for (int y = 0; y < intDistrict.GetUpperBound(1) - 1; y++)
                    {
                        intArea[stdCurrent.x1 + x + 1, stdCurrent.z1 + y + 1] = intDistrict[x + 1, y + 1];
                    }
                }
            }

            MakePaths(world, bm, intArea);

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
                    _intStreet++;
                    MakeStreetSign(bm, intSplitPoint - 1, z1 + 1, intSplitPoint - 1, z2 - 1);
                }
                else
                {
                    int intSplitPoint = RandomHelper.Next(z1 + 20, z2 - 20);
                    SplitArea(bm, intArea, x1, z1, x2, intSplitPoint);
                    SplitArea(bm, intArea, x1, intSplitPoint, x2, z2);
                    _intStreet++;
                    MakeStreetSign(bm, x1 + 1, intSplitPoint - 1, x2 - 1, intSplitPoint - 1);
                }
            }
            else
            {
                structDistrict stdCurrent = new structDistrict();
                stdCurrent.x1 = x1;
                stdCurrent.x2 = x2;
                stdCurrent.z1 = z1;
                stdCurrent.z2 = z2;
                _lstDistricts.Add(stdCurrent);
            }
        }
        private static int[,] FillArea(int[,] intArea, int intSizeX, int intSizeZ,
                                       int intStartX, int intStartZ, bool booUniqueBonus)
        {            
            int[,] intDistrict = new int[intSizeX, intSizeZ];
            int[,] intFinal = new int[intSizeX, intSizeZ];
            int intWasted = intSizeX * intSizeZ, intAttempts = 15, intFail = 0;
            int intBonus = 0;
            List<int> lstBuildings = new List<int>();
            List<int> lstAcceptedBuildings = new List<int>();
            bool booAreaNeedsMineshaft = false;
            do
            {
                lstBuildings.Clear();
                intBonus = 0;
                if (!_booIncludedMineshaft)
                {
                    booAreaNeedsMineshaft = true;
                }
                do
                {                    
                    SourceWorld.Building CurrentBuilding;
                    if (booAreaNeedsMineshaft)
                    {
                        CurrentBuilding = SourceWorld.SelectRandomBuilding(SourceWorld.BuildingTypes.MineshaftEntrance, 0);
                        // mineshaft is always the first building, so therefore it will always be possible to place it
                        booAreaNeedsMineshaft = false;
                    }
                    else
                    {
                        do
                        {
                            CurrentBuilding = SourceWorld.SelectRandomBuilding(SourceWorld.BuildingTypes.City, 0);
                        } while (!IsValidBuilding(CurrentBuilding, lstBuildings, intArea,
                                                  intStartX, intStartZ, intSizeX, intSizeZ));
                    }
                    bool booFound = false;
                    if (RandomHelper.NextDouble() > 0.5)
                    {
                        intDistrict = Utils.RotateArray(intDistrict, RandomHelper.Next(4));
                    }
                    int x, z = 0;
                    for (x = 0; x < intDistrict.GetLength(0) - CurrentBuilding.intSizeX && !booFound; x++)
                    {
                        for (z = 0; z < intDistrict.GetLength(1) - CurrentBuilding.intSizeZ && !booFound; z++)
                        {
                            booFound = Utils.IsArraySectionAllZeros2D(intDistrict, x, z, x + CurrentBuilding.intSizeX,
                                                                      z + CurrentBuilding.intSizeZ);
                        }
                    }
                    x--;
                    z--;
                    if (booFound)
                    {
                        for (int a = x + 1; a <= x + CurrentBuilding.intSizeX - 1; a++)
                        {
                            for (int b = z + 1; b <= z + CurrentBuilding.intSizeZ - 1; b++)
                            {
                                intDistrict[a, b] = 2;
                            }
                        }
                        if (CurrentBuilding.booUnique && booUniqueBonus)
                        {
                            // we want to include the unique buildings,
                            //   so we give a slight preference to those

                            intBonus += 15;
                        }
                        lstBuildings.Add(CurrentBuilding.intID);
                        intDistrict[x + 1, z + 1] = 100 + CurrentBuilding.intID;
                        intDistrict[x + CurrentBuilding.intSizeX - 1,
                                    z + CurrentBuilding.intSizeZ - 1] = 100 + CurrentBuilding.intID;
                        intFail = 0;
                    }
                    else
                    {
                        intFail++;
                    }
                } while (intFail < 10);

                int intCurWasted = Utils.ZerosInArray2D(intDistrict) - intBonus;
                if (intCurWasted < intWasted)
                {
                    intFinal = new int[intDistrict.GetLength(0), intDistrict.GetLength(1)];
                    Array.Copy(intDistrict, intFinal, intDistrict.Length);
                    intWasted = intCurWasted;
                    intAttempts = 10;
                    lstAcceptedBuildings.Clear();
                    lstAcceptedBuildings.AddRange(lstBuildings);
                }
                Array.Clear(intDistrict, 0, intDistrict.Length);
                intAttempts--;
            } while (intAttempts > 0);
            if (intSizeX == intFinal.GetLength(1))
            {
                intFinal = Utils.RotateArray(intFinal, 1);
            }
            _lstAllBuildings.AddRange(lstAcceptedBuildings);
            _booIncludedMineshaft = true;
            return intFinal;
        }
        private static bool IsValidBuilding(SourceWorld.Building bldCheck, List<int> lstBuildings, int[,] intArea,
                                            int intStartX, int intStartZ, int intSizeX, int intSizeZ)
        {
            if (bldCheck.booUnique)
            {
                if (_lstAllBuildings.Contains(bldCheck.intID) || lstBuildings.Contains(bldCheck.intID))
                {
                    return false;
                }
                else
                {
                    foreach (int intID in lstBuildings)
                    {
                        if (SourceWorld.GetBuilding(intID).booUnique)
                        {
                            return false;
                        }
                    }
                    for (int x = intStartX - 8; x < intStartX + intSizeX + 8; x++)
                    {
                        for (int z = intStartZ - 8; z < intStartZ + intSizeZ + 8; z++)
                        {
                            if (x >= 0 && z >= 0 && x <= intArea.GetUpperBound(0) && z <= intArea.GetUpperBound(1))
                            {
                                if (intArea[x, z] >= 100)
                                {
                                    if (SourceWorld.GetBuilding(intArea[x, z] - 100).booUnique)
                                    {
                                        return false;
                                    }                                    
                                }
                            }
                        }
                    }
                    return true;
                }
            }
            else
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
                                bm.SetID(_intBlockStart + x, 63, _intBlockStart + z, City.PathBlockID);
                                bm.SetData(_intBlockStart + x, 63, _intBlockStart + z, City.PathBlockData);
                            }
                        }
                        else
                        {
                            bm.SetID(_intBlockStart + x, 63, _intBlockStart + z, City.PathBlockID);
                            bm.SetData(_intBlockStart + x, 63, _intBlockStart + z, City.PathBlockData);
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

        private static void MakeStreetSign(BlockManager bm, int x1, int z1, int x2, int z2)
        {
            x1 += _intBlockStart;
            z1 += _intBlockStart;
            x2 += _intBlockStart;
            z2 += _intBlockStart;
            bm.SetID(x1, 64, z1, BlockInfo.Fence.ID);
            bm.SetID(x1, 65, z1, BlockInfo.WoodPlank.ID);
            bm.SetID(x1, 66, z1, BlockInfo.Torch.ID);
            bm.SetID(x2, 64, z2, BlockInfo.Fence.ID);
            bm.SetID(x2, 65, z2, BlockInfo.WoodPlank.ID);
            bm.SetID(x2, 66, z2, BlockInfo.Torch.ID);
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
                strStreetName = RandomHelper.RandomFileLine(Path.Combine("Resources", "Adjectives.txt"));
                strStreetType = RandomHelper.RandomFileLine(Path.Combine("Resources", "RoadTypes.txt"));
            } while (_lstStreetsUsed.Contains(strStreetName + " " + strStreetType));
            _lstStreetsUsed.Add(strStreetName + " " + strStreetType);
            BlockHelper.MakeSign(x1, 65, z1, "~" + strStreetName + "~" + strStreetType + "~", BlockInfo.WoodPlank.ID, 0);            
            BlockHelper.MakeSign(x2, 65, z2, "~" + strStreetName + "~" + strStreetType + "~", BlockInfo.WoodPlank.ID, 0);            
        }
        private static void ShuffleDistricts(List<structDistrict> lstShuffle)
        {
            int a = lstShuffle.Count;
            while (a > 1)
            {
                a--;
                int b = RandomHelper.Next(a + 1);
                structDistrict stdCurrent = lstShuffle[a];
                lstShuffle[a] = lstShuffle[b];
                lstShuffle[b] = stdCurrent;
            }
        }
    }
}