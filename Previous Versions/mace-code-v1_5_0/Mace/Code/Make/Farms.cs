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
    class Farms
    {
        enum FarmTypes { Cactus, Wheat, SugarCane, Mushroom, Orchard, Hill, Pond };

        public static void MakeFarms(BetaWorld world, BlockManager bm, int intFarmSize, int intMapSize)
        {
            AddMushroomFarms(bm, intFarmSize, intMapSize);
            int intFail = 0;
            int intFarms = 0;
            while (intFail <= 500)
            {
                int xlen = RandomHelper.Next(8, 26);
                int x1 = RandomHelper.Next(4, intMapSize - (4 + xlen));
                int zlen = RandomHelper.Next(8, 26);
                int z1 = RandomHelper.Next(4, intMapSize - (4 + zlen));
                if (!(x1 >= intFarmSize && z1 >= intFarmSize &&
                      x1 <= intMapSize - intFarmSize && z1 <= intMapSize - intFarmSize))
                {
                    bool booValid = true;
                    for (int x = x1 - 2; x <= x1 + xlen + 2 && booValid; x++)
                    {
                        for (int z = z1 - 2; z <= z1 + zlen + 2 && booValid; z++)
                        {
                            // make sure it doesn't overlap with the spawn point or another farm
                            if ((x == intMapSize / 2 && z == intMapSize - (intFarmSize - 10)) ||
                                bm.GetID(x, 63, z) != (int)BlockType.GRASS || bm.GetID(x, 64, z) != (int)BlockType.AIR)
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
                        // if not, 25% are cactus, 50% are wheat and 25% are sugarcane

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
                                intFarmType = RandomHelper.Next(100);
                                if (intFarmType > 75)
                                {
                                    curFarm = FarmTypes.Cactus;
                                }
                                else if (intFarmType > 25)
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

                        int intWallMaterial =
                            RandomHelper.NextDouble() > 0.5 ? (int)BlockType.FENCE : (int)BlockType.LEAVES;

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
                                intWallMaterial = (int)BlockType.LEAVES;
                                break;
                        }
                        switch (intWallMaterial)
                        {
                            case (int)BlockType.FENCE:
                                BlockShapes.MakeHollowLayers(x1, x1 + xlen, 64, 64, z1, z1 + zlen, (int)BlockType.FENCE, 0, -1);
                                break;
                            case (int)BlockType.LEAVES:
                                // the saplings will all disappear if one of them is broken.
                                // so we put wood beneath them to stop that happening
                                BlockShapes.MakeHollowLayers(x1, x1 + xlen, 63, 63, z1, z1 + zlen, (int)BlockType.WOOD, 0, -1);
                                BlockShapes.MakeHollowLayers(x1, x1 + xlen, 64, 65, z1, z1 + zlen, (int)BlockType.LEAVES, 0, 
                                  RandomHelper.RandomNumber((int)LeafType.OAK, (int)LeafType.SPRUCE, (int)LeafType.BIRCH));
                                break;
                        }

                        switch (curFarm)
                        {
                            case FarmTypes.Orchard:
                                int intSaplingType = RandomHelper.RandomNumber(BlockHelper.SaplingBirch,
                                                                               BlockHelper.SaplingOak,
                                                                               BlockHelper.SaplingSpruce);
                                for (int x = x1 + 3; x <= x1 + xlen - 3; x += 5)
                                {
                                    for (int z = z1 + 3; z <= z1 + zlen - 3; z += 5)
                                    {
                                        bm.SetID(x, 63, z, (int)BlockType.GRASS);
                                        bm.SetID(x, 64, z, (int)BlockType.SAPLING);
                                        bm.SetData(x, 64, z, intSaplingType);
                                    }
                                }
                                break;
                            case FarmTypes.Cactus:
                                int intAttempts = 0;
                                do
                                {
                                    int xCactus = RandomHelper.Next(x1 + 1, x1 + xlen), zCactus = RandomHelper.Next(z1 + 1, z1 + zlen);
                                    bool booValidFarm = true;
                                    for (int xCheck = xCactus - 1; xCheck <= xCactus + 1 && booValidFarm; xCheck++)
                                    {
                                        for (int zCheck = zCactus - 1; zCheck <= zCactus + 1 && booValidFarm; zCheck++)
                                        {
                                            if (bm.GetID(xCheck, 64, zCheck) != (int)BlockType.AIR)
                                            {
                                                booValidFarm = false;
                                            }
                                        }
                                    }
                                    if (booValidFarm)
                                    {
                                        bm.SetID(xCactus, 64, zCactus, (int)BlockType.CACTUS);
                                        if (RandomHelper.NextDouble() > 0.5)
                                        {
                                            bm.SetID(xCactus, 65, zCactus, (int)BlockType.CACTUS);
                                        }
                                    }
                                    intAttempts++;
                                }
                                while (intAttempts < 100);
                                break;
                            case FarmTypes.Wheat:
                                BlockShapes.MakeHollowLayers(x1, x1 + xlen, 66, 66, z1, z1 + zlen, (int)BlockType.GLASS, 0, -1);
                                BlockShapes.MakeSolidBox(x1 + 1, x1 + xlen - 1, 67, 67, z1 + 1, z1 + zlen - 1, (int)BlockType.GLASS, 0);
                                break;
                        }

                        for (int x = x1 + 1; x <= x1 + xlen - 1; x++)
                        {
                            for (int z = z1 + 1; z <= z1 + zlen - 1; z++)
                            {
                                switch (curFarm)
                                {
                                    case FarmTypes.Cactus:
                                        bm.SetID(x, 63, z, (int)BlockType.SAND);
                                        break;
                                    case FarmTypes.Wheat:
                                        if (z == z1 + 1)
                                        {
                                            bm.SetID(x, 63, z, (int)BlockType.DOUBLE_SLAB);
                                        }
                                        else if (x % 2 == 0)
                                        {
                                            bm.SetID(x, 63, z, (int)BlockType.FARMLAND);
                                            bm.SetData(x, 63, z, 1);
                                            bm.SetID(x, 64, z, (int)BlockType.CROPS);
                                        }
                                        else
                                        {
                                            bm.SetID(x, 63, z, (int)BlockType.STATIONARY_WATER);
                                        }
                                        break;
                                    case FarmTypes.SugarCane:
                                        if (z != z1 + 1)
                                        {
                                            if (x % 2 == 0)
                                            {
                                                bm.SetID(x, 64, z, (int)BlockType.SUGAR_CANE);
                                                if (RandomHelper.Next(100) > 50)
                                                {
                                                    bm.SetID(x, 65, z, (int)BlockType.SUGAR_CANE);
                                                }
                                            }
                                            else
                                            {
                                                bm.SetID(x, 63, z, (int)BlockType.STATIONARY_WATER);
                                            }
                                        }
                                        break;
                                }
                            }
                        }

                        int d = RandomHelper.Next(x1 + 1, x1 + xlen - 1);
                        if (curFarm == FarmTypes.Wheat)
                        {
                            bm.SetID(d, 63, z1, (int)BlockType.DOUBLE_SLAB);
                        }
                        if (intWallMaterial != 0)
                        {
                            bm.SetID(d, 64, z1, (int)BlockType.WOOD_DOOR);
                            bm.SetData(d, 64, z1, 4);
                            bm.SetID(d, 65, z1, (int)BlockType.WOOD_DOOR);
                            bm.SetData(d, 65, z1, 4 + (int)DoorState.TOPHALF);
                        }
                        intFail = 0;
                        intFarms++;
                        if (intFarms % 10 == 0)
                        {
                            world.Save();
                        }
                    }
                }
            }
            MakeMiniPondsAndHills(world, bm, intFarmSize, intMapSize);
            MakeFlowers(bm, intFarmSize, intMapSize);
        }
        private static void AddMushroomFarms(BlockManager bm, int intFarmSize, int intMapSize)
        {
            SourceWorld.Building bldMushroomFarm = SourceWorld.GetBuilding("Mushroom Farm");
            int len = bldMushroomFarm.intSize;
            int intMushroomFarms = (intMapSize / 16) / 5;

            bool booValid = false;
            do
            {
                booValid = false;
                int x1 = RandomHelper.Next(2, intMapSize - (2 + len));
                int z1 = RandomHelper.Next(2, intMapSize - (2 + len));
                if (!(x1 >= intFarmSize && z1 >= intFarmSize &&
                      x1 <= intMapSize - intFarmSize && z1 <= intMapSize - intFarmSize))
                {
                    booValid = true;
                    for (int x = x1 - 2; x <= x1 + len + 2 && booValid; x++)
                    {
                        for (int z = z1 - 2; z <= z1 + len + 2 && booValid; z++)
                        {
                            // make sure it doesn't overlap with the spawn point or another farm
                            if ((x == intMapSize / 2 && z == intMapSize - (intFarmSize - 10)) ||
                                bm.GetID(x, 63, z) != (int)BlockType.GRASS ||
                                bm.GetID(x, 64, z) != (int)BlockType.AIR ||
                                bm.GetID(x, 53, z) == (int)BlockType.AIR)
                            {
                                booValid = false;
                            }
                        }
                    }
                }
                if (booValid)
                {
                    SourceWorld.InsertBuilding(bm, new int[intMapSize, intMapSize], 0, x1, z1, bldMushroomFarm);
                    intMushroomFarms--;
                }
            } while (intMushroomFarms > 0);            
        }
        private static void MakePond(BlockManager bm, int x, int xlen, int z, int zlen, bool booMini)
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

            intArea = CheckAgainstNeighbours(intArea, 0, 0, 7, 0, RandomHelper.Next(3, 6), true);
            intArea = CheckAgainstNeighbours(intArea, 6, 0, 2, 2, 1, false);
            intArea = CheckAgainstNeighbours(intArea, 14, 0, 3, 3, 1, false);

            bool booLava = (!booMini && RandomHelper.NextDouble() < 0.15);

            for (a = 1; a <= intArea.GetUpperBound(0) - 1; a++)
            {
                for (b = 1; b <= intArea.GetUpperBound(1) - 1; b++)
                {
                    if (intArea[a, b] == 1)
                    {
                        bm.SetID(x + a, 64, z + b, (int)BlockType.TALL_GRASS);
                        bm.SetData(x + a, 64, z + b, RandomHelper.Next(1, 3));
                    }
                    else
                    {
                        for (int y = 2; y <= intArea[a, b]; y++)
                        {
                            if (booLava)
                            {
                                bm.SetID(x + a, 65 - y, z + b, (int)BlockType.STATIONARY_LAVA);
                            }
                            else
                            {
                                bm.SetID(x + a, 65 - y, z + b, (int)BlockType.STATIONARY_WATER);
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
                            bm.SetID(x + a, y + 63, z + b, (int)BlockType.GRASS);
                            switch (RandomHelper.Next(0, 10))
                            {
                                case 0:
                                    bm.SetID(x + a, y + 64, z + b, (int)BlockType.RED_ROSE);
                                    break;
                                case 1:
                                    bm.SetID(x + a, y + 64, z + b, (int)BlockType.YELLOW_FLOWER);
                                    break;
                                case 2:
                                case 3:
                                    bm.SetID(x + a, y + 64, z + b, (int)BlockType.TALL_GRASS);
                                    bm.SetData(x + a, y + 64, z + b, RandomHelper.Next(1, 3));
                                    break;
                            }
                        }
                        else
                        {
                            bm.SetID(x + a, y + 63, z + b, (int)BlockType.DIRT);
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
                            if (Neighbours(intOriginal, a, b) < intMin + RandomHelper.Next(intRandMin, intRandMax))
                            {
                                intArea[a, b] = intSet;
                            }
                        }
                        else
                        {
                            if (Neighbours(intOriginal, a, b) >= intMin + RandomHelper.Next(intRandMin, intRandMax))
                            {
                                intArea[a, b] = intSet;
                            }
                        }
                    }
                }
            }
            return intArea;
        }
        private static int Neighbours(int[,] intArray, int x, int z)
        {
            int intNeighbours = 0;
            for (int a = x - 1; a <= x + 1; a++)
            {
                for (int b = z - 1; b <= z + 1; b++)
                {
                    if (a != x || b != z)
                    {
                        intNeighbours += intArray[a, b];
                    }
                }
            }
            return intNeighbours;
        }
        private static void MakeMiniPondsAndHills(BetaWorld world, BlockManager bm, int intFarmSize, int intMapSize)
        {
            int intFail = 0;
            int intAdded = 0;
            while (intFail <= 250)
            {
                int xlen = RandomHelper.Next(6, 10);
                int x1 = RandomHelper.Next(1, intMapSize - (1 + xlen));
                int zlen = RandomHelper.Next(6, 10);
                int z1 = RandomHelper.Next(1, intMapSize - (1 + zlen));
                if (!(x1 >= intFarmSize && z1 >= intFarmSize &&
                      x1 <= intMapSize - intFarmSize && z1 <= intMapSize - intFarmSize))
                {
                    bool booValid = true;
                    for (int x = x1 - 1; x <= x1 + xlen + 1 && booValid; x++)
                    {
                        for (int z = z1 - 1; z <= z1 + zlen + 1 && booValid; z++)
                        {
                            // make sure it doesn't overlap with the spawn point or another farm
                            if ((x == intMapSize / 2 && z == intMapSize - (intFarmSize - 10)) ||
                                bm.GetID(x, 63, z) != (int)BlockType.GRASS || bm.GetID(x, 64, z) != (int)BlockType.AIR)
                            {
                                booValid = false;
                                intFail++;
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
                        intAdded++;
                        if (intAdded % 25 == 0)
                        {
                            world.Save();
                        }
                    }
                }
            }
        }
        private static void MakeFlowers(BlockManager bm, int intFarmSize, int intMapSize)
        {
            for (int x = 0; x <= intMapSize; x++)
            {
                for (int z = 0; z <= intMapSize; z++)
                {
                    if (x <= intFarmSize || z <= intFarmSize ||
                        x >= intMapSize - intFarmSize || z >= intMapSize - intFarmSize)
                    {
                        bool booFree = true;
                        for (int xCheck = x - 1; xCheck <= x + 1 && booFree; xCheck++)
                        {
                            for (int zCheck = z - 1; zCheck <= z + 1 && booFree; zCheck++)
                            {
                                if (bm.GetID(xCheck, 63, zCheck) != BlockType.GRASS)
                                {
                                    booFree = false;
                                }
                                else if (bm.GetID(xCheck, 64, zCheck) != BlockType.AIR)
                                {
                                    booFree = false;
                                }
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
                            }
                        }
                    }
                }
            }
        }
    }
}
