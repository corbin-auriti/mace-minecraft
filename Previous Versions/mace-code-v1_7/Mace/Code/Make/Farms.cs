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
using Substrate;

namespace Mace
{
    static class Farms
    {
        private enum FarmTypes { Cactus, Wheat, SugarCane, Mushroom, Orchard, Hill, Pond };
        // these ids are close to being fully grown
        public const int SaplingSpruceDataID = 9;
        public const int SaplingBirchDataID = 10;
        public const int SaplingOakDataID = 11;        

        public static void MakeFarms(BetaWorld world, BlockManager bm, int intFarmLength, int intMapLength)
        {
            AddBuildings(bm, intFarmLength, intMapLength);
            int intFail = 0;
            int intFarms = 0;
            while (intFail <= 500)
            {
                int xlen = RandomHelper.Next(8, 26);
                int x1 = RandomHelper.Next(4, intMapLength - (4 + xlen));
                int zlen = RandomHelper.Next(8, 26);
                int z1 = RandomHelper.Next(4, intMapLength - (4 + zlen));
                if (!(x1 >= intFarmLength && z1 >= intFarmLength &&
                      x1 <= intMapLength - intFarmLength && z1 <= intMapLength - intFarmLength))
                {
                    bool booValid = true;
                    for (int x = x1 - 2; x <= x1 + xlen + 2 && booValid; x++)
                    {
                        for (int z = z1 - 2; z <= z1 + zlen + 2 && booValid; z++)
                        {
                            // make sure it doesn't overlap with the spawn point or another farm
                            if ((x == intMapLength / 2 && z == intMapLength - (intFarmLength - 10)) ||
                                bm.GetID(x, 63, z) != BlockType.GRASS || bm.GetID(x, 64, z) != BlockType.AIR)
                            {
                                booValid = false;
                                intFail++;
                            }
                        }
                    }
                    if (booValid)
                    {
                        // first there is a 25% chance of a hill or pond
                        // if not, for large farms, there is a 50% chance it'll be an orchard
                        // if not, 33% are cactus, 33% are wheat and 33% are sugarcane

                        FarmTypes curFarm;
                        int intFarmType = RandomHelper.Next(100);
                        if (xlen >= 10 && zlen >= 10 && intFarmType > 75)
                        {
                            if (RandomHelper.NextDouble() > 0.5)
                            {
                                curFarm = FarmTypes.Pond;
                                MakePond(bm, x1, xlen, z1, zlen, false);
                            }
                            else
                            {
                                curFarm = FarmTypes.Hill;
                                MakeHill(bm, x1, xlen, z1, zlen, false);
                            }
                        }
                        else
                        {
                            intFarmType = RandomHelper.Next(100);
                            if (((xlen >= 11 && zlen >= 16) || (xlen >= 16 && zlen >= 11)) && intFarmType > 50)
                            {
                                curFarm = FarmTypes.Orchard;
                                xlen = ((int)((xlen - 1) / 5) * 5) + 1;
                                zlen = ((int)((zlen - 1) / 5) * 5) + 1;
                            }
                            else
                            {
                                intFarmType = RandomHelper.Next(3);
                                if (intFarmType == 2)
                                {
                                    curFarm = FarmTypes.Cactus;
                                }
                                else if (intFarmType == 1)
                                {
                                    curFarm = FarmTypes.Wheat;
                                    xlen += (xlen % 2) - 1;
                                    zlen += (zlen % 2) - 1;
                                }
                                else
                                {
                                    curFarm = FarmTypes.SugarCane;
                                    xlen += (xlen % 2) - 1;
                                    zlen += (zlen % 2) - 1;
                                }
                            }
                        }

                        int intWallMaterial = RandomHelper.NextDouble() > 0.5 ? BlockType.FENCE : BlockType.LEAVES;

                        switch (curFarm)
                        {
                            case FarmTypes.Hill:
                            case FarmTypes.Pond:
                                intWallMaterial = 0;
                                break;
                            case FarmTypes.SugarCane:
                            case FarmTypes.Mushroom:
                                if (RandomHelper.NextDouble() > 0.5)
                                {
                                    intWallMaterial = 0;
                                }
                                break;
                            case FarmTypes.Wheat:
                                intWallMaterial = BlockType.LEAVES;
                                break;
                                // no need for default - the other types don't need anything here
                        }
                        switch (intWallMaterial)
                        {
                            case BlockType.FENCE:
                                BlockShapes.MakeHollowLayers(x1, x1 + xlen, 64, 64, z1, z1 + zlen,
                                                             BlockType.FENCE, 0, -1);
                                break;
                            case BlockType.LEAVES:
                                // the saplings will all disappear if one of them is broken.
                                // so we put wood beneath them to stop that happening
                                BlockShapes.MakeHollowLayers(x1, x1 + xlen, 63, 63, z1, z1 + zlen,
                                                             BlockType.WOOD, 0, -1);
                                BlockShapes.MakeHollowLayers(x1, x1 + xlen, 64, 65, z1, z1 + zlen, BlockType.LEAVES, 0, 
                                  RandomHelper.RandomNumber((int)LeafType.OAK, (int)LeafType.SPRUCE,
                                                            (int)LeafType.BIRCH));
                                break;
                            case 0:
                                // no wall
                                break;
                            default:
                                Debug.Fail("Invalid switch result");
                                break;
                        }

                        switch (curFarm)
                        {
                            case FarmTypes.Orchard:
                                int intSaplingType = RandomHelper.RandomNumber(SaplingBirchDataID,
                                                                               SaplingOakDataID,
                                                                               SaplingSpruceDataID);
                                for (int x = x1 + 3; x <= x1 + xlen - 3; x += 5)
                                {
                                    for (int z = z1 + 3; z <= z1 + zlen - 3; z += 5)
                                    {
                                        bm.SetID(x, 63, z, BlockType.GRASS);
                                        BlockShapes.MakeBlock(x, 64, z, BlockType.SAPLING, intSaplingType);
                                    }
                                }
                                break;
                            case FarmTypes.Cactus:
                                int intAttempts = 0;
                                do
                                {
                                    int xCactus = RandomHelper.Next(x1 + 1, x1 + xlen);
                                    int zCactus = RandomHelper.Next(z1 + 1, z1 + zlen);
                                    bool booValidFarm = true;
                                    for (int xCheck = xCactus - 1; xCheck <= xCactus + 1 && booValidFarm; xCheck++)
                                    {
                                        for (int zCheck = zCactus - 1; zCheck <= zCactus + 1 && booValidFarm; zCheck++)
                                        {
                                            if (bm.GetID(xCheck, 64, zCheck) != BlockType.AIR)
                                            {
                                                booValidFarm = false;
                                            }
                                        }
                                    }
                                    if (booValidFarm)
                                    {
                                        bm.SetID(xCactus, 64, zCactus, BlockType.CACTUS);
                                        if (RandomHelper.NextDouble() > 0.5)
                                        {
                                            bm.SetID(xCactus, 65, zCactus, BlockType.CACTUS);
                                        }
                                    }
                                }
                                while (++intAttempts < 100);
                                break;
                            case FarmTypes.Wheat:
                                BlockShapes.MakeHollowLayers(x1, x1 + xlen, 66, 66, z1, z1 + zlen,
                                                             BlockType.GLASS, 0, -1);
                                BlockShapes.MakeSolidBox(x1 + 1, x1 + xlen - 1, 67, 67, z1 + 1, z1 + zlen - 1,
                                                         BlockType.GLASS, 0);
                                break;
                            // no need for a default, because there's nothing to do for the other farms
                        }

                        for (int x = x1 + 1; x <= x1 + xlen - 1; x++)
                        {
                            for (int z = z1 + 1; z <= z1 + zlen - 1; z++)
                            {
                                switch (curFarm)
                                {
                                    case FarmTypes.Cactus:
                                        bm.SetID(x, 63, z, BlockType.SAND);
                                        break;
                                    case FarmTypes.Wheat:
                                        if (z == z1 + 1)
                                        {
                                            bm.SetID(x, 63, z, BlockType.DOUBLE_SLAB);
                                        }
                                        else if (x % 2 == 0)
                                        {
                                            BlockShapes.MakeBlock(x, 63, z, BlockType.FARMLAND, 1);
                                            bm.SetID(x, 64, z, BlockType.CROPS);
                                        }
                                        else
                                        {
                                            bm.SetID(x, 63, z, BlockType.STATIONARY_WATER);
                                        }
                                        break;
                                    case FarmTypes.SugarCane:
                                        if (z != z1 + 1)
                                        {
                                            if (x % 2 == 0)
                                            {
                                                bm.SetID(x, 64, z, BlockType.SUGAR_CANE);
                                                if (RandomHelper.Next(100) > 50)
                                                {
                                                    bm.SetID(x, 65, z, BlockType.SUGAR_CANE);
                                                }
                                            }
                                            else
                                            {
                                                bm.SetID(x, 63, z, BlockType.STATIONARY_WATER);
                                            }
                                        }
                                        break;
                                    // no need for a default, because there's nothing to do for the other farms
                                }
                            }
                        }
                        int intDoorPosition = x1 + RandomHelper.Next(1, xlen - 1);
                        if (curFarm == FarmTypes.Wheat)
                        {
                            bm.SetID(intDoorPosition, 63, z1, BlockType.DOUBLE_SLAB);
                        }
                        if (intWallMaterial != 0)
                        {
                            BlockShapes.MakeBlock(intDoorPosition, 64, z1, BlockType.WOOD_DOOR, 4);
                            BlockShapes.MakeBlock(intDoorPosition, 65, z1, BlockType.WOOD_DOOR,
                                                  4 + (int)DoorState.TOPHALF);
                        }
                        intFail = 0;
                        if (++intFarms > 10)
                        {
                            world.Save();
                            intFarms = 0;
                        }
                    }
                }
            }
            MakeMiniPondsAndHills(world, bm, intFarmLength, intMapLength);
            MakeFlowers(bm, intFarmLength, intMapLength);
        }
        private static void AddBuildings(BlockManager bm, int intFarmLength, int intMapLength)
        {
            SourceWorld.Building CurrentBuilding;
            List<int> lstAllFarmingBuildings = new List<int>();
            int intBuildings = 0;
            do
            {                
                do
                {
                    CurrentBuilding = SourceWorld.SelectRandomBuilding(SourceWorld.BuildingTypes.Farming, 0);
                } while (lstAllFarmingBuildings.Contains(CurrentBuilding.intID));
                AddBuilding(bm, intFarmLength, intMapLength, CurrentBuilding.intID);
                if (CurrentBuilding.booUnique)
                {
                    lstAllFarmingBuildings.Add(CurrentBuilding.intID);
                }
            } while (++intBuildings < Math.Max(1, (intMapLength / 16) / 2));
        }
        private static void AddBuilding(BlockManager bm, int intFarmLength, int intMapLength, int intBuildingID)
        {
            bool booAddedBuilding = false;
            SourceWorld.Building bldInsert = SourceWorld.GetBuilding(intBuildingID);
            int intLen = bldInsert.intSizeX;
            bool booValid;
            do
            {
                booValid = false;
                int x1 = RandomHelper.Next(2, intMapLength - (2 + intLen));
                int z1 = RandomHelper.Next(2, intMapLength - (2 + intLen));
                if (!(x1 >= intFarmLength && z1 >= intFarmLength &&
                      x1 <= intMapLength - intFarmLength && z1 <= intMapLength - intFarmLength))
                {
                    booValid = true;
                    for (int x = x1 - 2; x <= x1 + intLen + 2 && booValid; x++)
                    {
                        for (int z = z1 - 2; z <= z1 + intLen + 2 && booValid; z++)
                        {
                            // make sure it doesn't overlap with the spawn point or another farm
                            if ((x == intMapLength / 2 && z == intMapLength - (intFarmLength - 10)) ||
                                bm.GetID(x, 63, z) != BlockType.GRASS ||
                                bm.GetID(x, 64, z) != BlockType.AIR ||
                                bm.GetID(x, 53, z) == BlockType.AIR)
                            {
                                booValid = false;
                            }
                        }
                    }
                }
                if (booValid)
                {
                    SourceWorld.InsertBuilding(bm, new int[intMapLength, intMapLength], 0, x1, z1, bldInsert, 0);
                    booAddedBuilding = true;
                }
            } while (!booAddedBuilding);
        }
        private static void MakePond(BlockManager bm, int x, int xlen, int z, int zlen, bool booSmallPond)
        {
            int[,] intArea = new int[xlen, zlen];
            int a, b;

            int c = 0, d = 0, clen = -1, dlen = -1;
            if (!booSmallPond)
            {
                c = RandomHelper.Next(1, intArea.GetUpperBound(0) - 7);
                d = RandomHelper.Next(1, intArea.GetUpperBound(1) - 7);
                clen = RandomHelper.Next(3, 7);
                dlen = RandomHelper.Next(3, 7);
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

            intArea = CheckAgainstNeighbours(intArea, 0, 0, 7, 0, RandomHelper.Next(3, 6), true);
            intArea = CheckAgainstNeighbours(intArea, 6, 0, 2, 2, 1, false);
            intArea = CheckAgainstNeighbours(intArea, 14, 0, 3, 3, 1, false);

            bool booLava = (!booSmallPond && RandomHelper.NextDouble() < 0.15);

            for (a = 1; a <= intArea.GetUpperBound(0) - 1; a++)
            {
                for (b = 1; b <= intArea.GetUpperBound(1) - 1; b++)
                {
                    if (intArea[a, b] == 1)
                    {
                        bm.SetID(x + a, 64, z + b, BlockType.TALL_GRASS);
                        bm.SetData(x + a, 64, z + b, RandomHelper.Next(1, 3));
                    }
                    else
                    {
                        for (int y = 2; y <= intArea[a, b]; y++)
                        {
                            if (booLava)
                            {
                                bm.SetID(x + a, 65 - y, z + b, BlockType.STATIONARY_LAVA);
                            }
                            else
                            {
                                bm.SetID(x + a, 65 - y, z + b, BlockType.STATIONARY_WATER);
                            }
                        }
                    }
                }
            }
        }
        private static void MakeHill(BlockManager bm, int x, int xlen, int z, int zlen, bool booMini)
        {
            int[,] intArea = new int[xlen, zlen];
            int a, b;

            int c = 0, d = 0, clen = -1, dlen = -1;
            if (!booMini)
            {
                c = RandomHelper.Next(1, intArea.GetUpperBound(0) - 7);
                d = RandomHelper.Next(1, intArea.GetUpperBound(1) - 7);
                clen = RandomHelper.Next(3, 7);
                dlen = RandomHelper.Next(3, 7);
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

            intArea = CheckAgainstNeighbours(intArea, 0, 0, 7, 0, RandomHelper.Next(7, 11), true);
            intArea = CheckAgainstNeighbours(intArea, 6, 0, 3, 2, 1, false);
            intArea = CheckAgainstNeighbours(intArea, 14, 0, 3, 3, 1, false);

            for (a = 1; a <= intArea.GetUpperBound(0) - 1; a++)
            {
                for (b = 1; b <= intArea.GetUpperBound(1) - 1; b++)
                {
                    for (int y = 1; y <= intArea[a, b]; y++)
                    {
                        if (y == intArea[a, b])
                        {
                            bm.SetID(x + a, y + 63, z + b, BlockType.GRASS);
                            switch (RandomHelper.Next(0, 10))
                            {
                                case 0:
                                    bm.SetID(x + a, y + 64, z + b, BlockType.RED_ROSE);
                                    break;
                                case 1:
                                    bm.SetID(x + a, y + 64, z + b, BlockType.YELLOW_FLOWER);
                                    break;
                                case 2:
                                case 3:
                                    bm.SetID(x + a, y + 64, z + b, BlockType.TALL_GRASS);
                                    bm.SetData(x + a, y + 64, z + b, RandomHelper.Next(1, 3));
                                    break;
                                // we want to skip the other numbers, so there's no need for a default
                            }
                        }
                        else
                        {
                            bm.SetID(x + a, y + 63, z + b, BlockType.DIRT);
                        }
                    }
                }
            }
        }
        private static int[,] CheckAgainstNeighbours(int[,] intArea, int intMin, int intRandMin, int intRandMax,
                                                     int intSet, int intTimes, bool booCheckDown)
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
                            if (Utils.SumOfNeighbours2D(intOriginal, a, b) < intMin + RandomHelper.Next(intRandMin, intRandMax))
                            {
                                intArea[a, b] = intSet;
                            }
                        }
                        else
                        {
                            if (Utils.SumOfNeighbours2D(intOriginal, a, b) >= intMin + RandomHelper.Next(intRandMin, intRandMax))
                            {
                                intArea[a, b] = intSet;
                            }
                        }
                    }
                }
            }
            return intArea;
        }
        private static void MakeMiniPondsAndHills(BetaWorld world, BlockManager bm, int intFarmLength, int intMapLength)
        {
            int intFail = 0;
            int intAdded = 0;
            do
            {
                int xlen = RandomHelper.Next(4, 12);
                int x1 = RandomHelper.Next(1, intMapLength - (1 + xlen));
                int zlen = RandomHelper.Next(4, 12);
                int z1 = RandomHelper.Next(1, intMapLength - (1 + zlen));
                if (!(x1 >= intFarmLength && z1 >= intFarmLength &&
                      x1 <= intMapLength - intFarmLength && z1 <= intMapLength - intFarmLength))
                {
                    bool booValid = true;
                    for (int x = x1 - 1; x <= x1 + xlen + 1 && booValid; x++)
                    {
                        for (int z = z1 - 1; z <= z1 + zlen + 1 && booValid; z++)
                        {
                            // make sure it doesn't overlap with the spawn point or another farm
                            if ((x == intMapLength / 2 && z == intMapLength - (intFarmLength - 10)) ||
                                bm.GetID(x, 63, z) != BlockType.GRASS || bm.GetID(x, 64, z) != BlockType.AIR)
                            {
                                booValid = false;
                            }
                        }
                    }
                    if (booValid)
                    {
                        if (RandomHelper.NextDouble() > 0.5)
                        {
                            MakePond(bm, x1, xlen, z1, zlen, true);
                        }
                        else
                        {
                            MakeHill(bm, x1, xlen, z1, zlen, true);
                        }
                        intFail = 0;
                        if (++intAdded % 25 == 0)
                        {
                            world.Save();
                        }
                    }
                    else
                    {
                        intFail++;
                    }
                }
            } while (intFail < 500);
        }
        private static void MakeFlowers(BlockManager bm, int intFarmLength, int intMapLength)
        {
            for (int x = 0; x <= intMapLength; x++)
            {
                for (int z = 0; z <= intMapLength; z++)
                {
                    if (x <= intFarmLength || z <= intFarmLength ||
                        x >= intMapLength - intFarmLength || z >= intMapLength - intFarmLength)
                    {
                        bool booFree = true;
                        for (int xCheck = x - 1; xCheck <= x + 1 && booFree; xCheck++)
                        {
                            for (int zCheck = z - 1; zCheck <= z + 1 && booFree; zCheck++)
                            {
                                booFree = bm.GetID(xCheck, 63, zCheck) == BlockType.GRASS &&
                                          bm.GetID(xCheck, 64, zCheck) == BlockType.AIR;
                            }
                        }
                        if (booFree && RandomHelper.NextDouble() > 0.90)
                        {
                            switch (RandomHelper.Next(4))
                            {
                                case 0:
                                    bm.SetID(x, 64, z, BlockType.RED_ROSE);
                                    break;
                                case 1:
                                    bm.SetID(x, 64, z, BlockType.YELLOW_FLOWER);
                                    break;
                                case 2:
                                case 3:
                                    bm.SetID(x, 64, z, BlockType.TALL_GRASS);
                                    bm.SetData(x, 64, z, RandomHelper.Next(1, 3));
                                    break;
                                default:
                                    Debug.Fail("Invalid switch result");
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
