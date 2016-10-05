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
using System.Collections.Generic;
using System.Diagnostics;
using Substrate;
using System.IO;

namespace Mace
{
    class Farms2
    {
        private const int NumberOfBuildingsBetweenSaves = 10;

        public static void MakeFarms(AnvilWorld world, BlockManager bm)
        {
            world.Save();
            MakeBuildings(bm, FillArea(City.mapLength - 12, City.farmLength - 4), world);
            world.Save();         
        }
        // this is a simplified version of the FillArea method from Paths.cs
        private static int[,] FillArea(int intSizeX, int intSizeZ)
        {
            int[,] intDistrict = new int[intSizeX, intSizeZ];
            int[,] intFinal = new int[intSizeX, intSizeZ];
            int intWasted = intSizeX * intSizeZ, intAttempts = 15, intFail = 0;
            int intBonus = 0;
            List<int> lstBuildings = new List<int>();
            do
            {
                lstBuildings.Clear();
                intBonus = 0;
                do
                {
                    SourceWorld.Building CurrentBuilding;
                    do
                    {
                        CurrentBuilding = SourceWorld.SelectRandomBuilding(SourceWorld.BuildingTypes.Farming, 0);
                    } while (!IsValidBuilding(CurrentBuilding, lstBuildings));
                    bool booFound = false;
                    if (RNG.NextDouble() > 0.5)
                    {
                        intDistrict = intDistrict.RotateArray(RNG.Next(4));
                    }
                    int x, z = 0;
                    for (x = 0; x < intDistrict.GetLength(0) - CurrentBuilding.intSizeX && !booFound; x++)
                    {
                        for (z = 0; z < intDistrict.GetLength(1) - CurrentBuilding.intSizeZ && !booFound; z++)
                        {
                            booFound = intDistrict.IsArraySectionAllZeros2D(x, z, x + CurrentBuilding.intSizeX, z + CurrentBuilding.intSizeZ);
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
                        if (CurrentBuilding.booUnique)
                        {
                            intBonus += 15;
                        }
                        lstBuildings.Add(CurrentBuilding.intID);
                        intDistrict[x + 1, z + 1] = 100 + CurrentBuilding.intID;
                        intDistrict[x + CurrentBuilding.intSizeX - 1, z + CurrentBuilding.intSizeZ - 1] = 100 + CurrentBuilding.intID;
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
                }
                Array.Clear(intDistrict, 0, intDistrict.Length);
                intAttempts--;
            } while (intAttempts > 0);
            if (intSizeX == intFinal.GetLength(1))
            {
                intFinal = intFinal.RotateArray(1);
            }
            return intFinal;
        }
        // this is a simplified version of the IsValidBuilding method from Paths.cs
        private static bool IsValidBuilding(SourceWorld.Building bldCheck, List<int> lstBuildings)
        {
            if (bldCheck.booUnique)
            {
                return !lstBuildings.Contains(bldCheck.intID);
            }
            // todo low: need some way to limit (very) rare buildings
            switch (bldCheck.strFrequency)
            {
                case "very common":
                case "common":
                case "average":
                case "rare":
                case "very rare":
                    return true;
                // should never get here to either of these, but just in case
                case "exclude":
                    Debug.WriteLine("Excluded buildings are not allowed here");
                    return false;
                default:
                    Debug.WriteLine("Unknown frequency type encountered");
                    return false;
            }
        }
        // this is a simplified version of the MakeBuildings method from Buildings.cs
        private static void MakeBuildings(BlockManager bm, int[,] intArea, AnvilWorld world)
        {
            int intBuildings = 0;
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int z = 0; z < intArea.GetLength(1); z++)
                {
                    // hack low: this 100 to 500 stuff is all a bit hackish really, need to find a proper solution
                    if (intArea[x, z] >= 100 && intArea[x, z] <= 500)
                    {
                        SourceWorld.Building CurrentBuilding = SourceWorld.GetBuilding(intArea[x, z] - 100);

                        if (CurrentBuilding.intSizeX >= 10 && RNG.NextDouble() > 0.8)
                        {
                            if (RNG.NextDouble() > 0.5)
                            {
                                MakePond(bm, 6 + x, CurrentBuilding.intSizeX, (6 - City.farmLength) + z, CurrentBuilding.intSizeZ);
                            }
                            else
                            {
                                MakeHill(bm, 6 + x, CurrentBuilding.intSizeX, (6 - City.farmLength) + z, CurrentBuilding.intSizeZ);
                            }
                        }
                        else
                        {
                            SourceWorld.InsertBuilding(bm, intArea, 0, 6 + x, (6 - City.farmLength) + z, CurrentBuilding, 0, -1);
                        }

                        intArea[x + CurrentBuilding.intSizeX - 2, z + CurrentBuilding.intSizeZ - 2] = 0;
                        if (++intBuildings == NumberOfBuildingsBetweenSaves)
                        {
                            world.Save();
                            intBuildings = 0;
                        }
                    }
                }
            }
        }
        private static void MakePond(BlockManager bm, int x, int xlen, int z, int zlen)
        {
            int[,] intArea = new int[xlen, zlen];
            int a, b;

            int c = 0, d = 0, clen = -1, dlen = -1;
            if (xlen >= 12 && zlen >= 12)
            {
                c = RNG.Next(1, intArea.GetUpperBound(0) - 7);
                d = RNG.Next(1, intArea.GetUpperBound(1) - 7);
                clen = RNG.Next(2, 4);
                dlen = RNG.Next(2, 4);
            }

            for (a = 1; a <= intArea.GetUpperBound(0) - 1; a++)
            {
                for (b = 1; b <= intArea.GetUpperBound(1) - 1; b++)
                {
                    if (a < c || a > c + clen || b < d || b > d + dlen)
                    {
                        intArea[a, b] = 1;
                    }
                }
            }

            intArea = CheckAgainstNeighbours(intArea, 0, 0, 7, 0, RNG.Next(3, 6), true);
            intArea = CheckAgainstNeighbours(intArea, 6, 0, 2, 2, 1, false);
            intArea = CheckAgainstNeighbours(intArea, 14, 0, 3, 3, 1, false);

            bool booLava = RNG.NextDouble() < 0.15;

            for (a = 1; a < intArea.GetUpperBound(0); a++)
            {
                for (b = 1; b < intArea.GetUpperBound(1); b++)
                {
                    if (intArea[a, b] == 1)
                    {
                        if (!booLava && RNG.NextDouble() < 0.05)
                        {
                            bm.SetID(x + a, 64, z + b, BlockInfo.SugarCane.ID);
                        }
                        else
                        {
                            bm.SetID(x + a, 64, z + b, BlockInfo.TallGrass.ID);
                            bm.SetData(x + a, 64, z + b, RNG.Next(1, 3));
                        }
                    }
                    else
                    {
                        for (int y = 2; y <= intArea[a, b]; y++)
                        {
                            if (booLava)
                            {
                                bm.SetID(x + a, 65 - y, z + b, BlockInfo.StationaryLava.ID);
                            }
                            else
                            {
                                bm.SetID(x + a, 65 - y, z + b, BlockInfo.StationaryWater.ID);
                                if (y == 2 && RNG.NextDouble() < 0.1)
                                {
                                    bm.SetID(x + a, 66 - y, z + b, BlockInfo.LillyPad.ID);
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void MakeHill(BlockManager bm, int x, int xlen, int z, int zlen)
        {
            int[,] intArea = new int[xlen, zlen];
            int a, b;

            int c = 0, d = 0, clen = -1, dlen = -1;
            if (xlen >= 8 && zlen >= 8)
            {
                c = RNG.Next(1, intArea.GetUpperBound(0) - 7);
                d = RNG.Next(1, intArea.GetUpperBound(1) - 7);
                clen = RNG.Next(3, 5);
                dlen = RNG.Next(3, 5);
            }

            for (a = 1; a <= intArea.GetUpperBound(0) - 1; a++)
            {
                for (b = 1; b <= intArea.GetUpperBound(1) - 1; b++)
                {
                    if (a < c || a > c + clen || b < d || b > d + dlen)
                    {
                        intArea[a, b] = 1;
                    }
                }
            }

            intArea = CheckAgainstNeighbours(intArea, 0, 0, 7, 0, RNG.Next(7, 11), true);
            intArea = CheckAgainstNeighbours(intArea, 6, 0, 3, 2, 1, false);
            intArea = CheckAgainstNeighbours(intArea, 14, 0, 3, 3, 1, false);

            for (a = 1; a < intArea.GetUpperBound(0); a++)
            {
                for (b = 1; b < intArea.GetUpperBound(1); b++)
                {
                    for (int y = 1; y <= intArea[a, b]; y++)
                    {
                        if (y == intArea[a, b])
                        {
                            bm.SetID(x + a, y + 63, z + b, BlockInfo.Grass.ID);
                            switch (RNG.Next(0, 10))
                            {
                                case 0:
                                    bm.SetID(x + a, y + 64, z + b, BlockInfo.RedRose.ID);
                                    break;
                                case 1:
                                    bm.SetID(x + a, y + 64, z + b, BlockInfo.YellowFlower.ID);
                                    break;
                                case 2:
                                case 3:
                                    bm.SetID(x + a, y + 64, z + b, BlockInfo.TallGrass.ID);
                                    bm.SetData(x + a, y + 64, z + b, RNG.Next(1, 3));
                                    break;
                                // we want to skip the other numbers, so there's no need for a default
                            }
                        }
                        else
                        {
                            bm.SetID(x + a, y + 63, z + b, BlockInfo.Dirt.ID);
                        }
                    }
                }
            }
        }
        private static int[,] CheckAgainstNeighbours(int[,] intArea, int intMin, int intRandMin, int intRandMax, int intSet, int intTimes, bool booCheckDown)
        {
            int[,] intOriginal = new int[intArea.GetLength(0), intArea.GetLength(1)];
            for (int intTime = 1; intTime <= intTimes; intTime++)
            {
                Array.Copy(intArea, intOriginal, intArea.Length);
                for (int a = 1; a <= intArea.GetUpperBound(0) - 1; a++)
                {
                    for (int b = 1; b <= intArea.GetUpperBound(1) - 1; b++)
                    {
                        if (booCheckDown)
                        {
                            if (intOriginal.SumOfNeighbours2D(a, b) < intMin + RNG.Next(intRandMin, intRandMax))
                            {
                                intArea[a, b] = intSet;
                            }
                        }
                        else
                        {
                            if (intOriginal.SumOfNeighbours2D(a, b) >= intMin + RNG.Next(intRandMin, intRandMax))
                            {
                                intArea[a, b] = intSet;
                            }
                        }
                    }
                }
            }
            return intArea;
        }
    }
}