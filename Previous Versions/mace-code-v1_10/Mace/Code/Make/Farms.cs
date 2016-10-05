///*
//    Mace
//    Copyright (C) 2011-2012 Robson
//    http://iceyboard.no-ip.org

//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
//*/
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using Substrate;
//using System.IO;

//namespace Mace
//{
//    static class Farms
//    {
//        private const int NumberOfFlowersBetweenSaves = 250;
//        private const int MinimumFreeNeighboursNeededForFlowers = 3;
//        private const int SpawnZ = 20;

//        private enum FarmTypes { Cactus, Wheat, SugarCane, Mushroom, Orchard, Hill, Pond };
//        // these ids are close to being fully grown
//        public const int SaplingSpruceDataID = 9;
//        public const int SaplingBirchDataID = 10;
//        public const int SaplingOakDataID = 11;     

//        public static void MakeFarms(BetaWorld world, BlockManager bm, frmMace frmLogForm)
//        {
//            if (City.HasFarms)
//            {
//                frmLogForm.UpdateLog("Creating farm buildings", true, true);
//                AddBuildings(bm);
//                frmLogForm.UpdateLog("Creating farms and outside features", true, true);
//                int intFail = 0;
//                int intFarms = 0;
//                while (intFail <= 500)
//                {
//                    int xlen = RNG.Next(8, 26);
//                    int x1 = RNG.Next(5, City.MapLength - (5 + xlen));
//                    int zlen = RNG.Next(8, 26);
//                    int z1 = RNG.Next(5, City.MapLength - (5 + zlen));
//                    if (!(x1 >= City.EdgeLength && z1 >= City.EdgeLength &&
//                          x1 <= City.MapLength - City.EdgeLength && z1 <= City.MapLength - City.EdgeLength))
//                    {
//                        bool booValid = true;
//                        for (int x = x1 - 2; x <= x1 + xlen + 2 && booValid; x++)
//                        {
//                            for (int z = z1 - 2; z <= z1 + zlen + 2 && booValid; z++)
//                            {
//                                // make sure it doesn't overlap with the spawn point or another farm
//                                if ((x == City.MapLength / 2 && z == SpawnZ) ||
//                                    bm.GetID(x, 63, z) != City.GroundBlockID ||
//                                    bm.GetID(x, 64, z) != BlockInfo.Air.ID)
//                                {
//                                    booValid = false;
//                                    intFail++;
//                                }
//                            }
//                        }
//                        if (booValid)
//                        {
//                            // first there is a 25% chance of a hill or pond
//                            // if not, for large farms, there is a 50% chance it'll be an orchard
//                            // if not, 33% are cactus, 33% are wheat and 33% are sugarcane

//                            FarmTypes curFarm;
//                            int intFarmType = RNG.Next(100);
//                            if (xlen >= 10 && zlen >= 10 && intFarmType > 75)
//                            {
//                                if (RNG.NextDouble() > 0.5)
//                                {
//                                    curFarm = FarmTypes.Pond;
//                                    MakePond(bm, x1, xlen, z1, zlen, false);
//                                }
//                                else
//                                {
//                                    curFarm = FarmTypes.Hill;
//                                    MakeHill(bm, x1, xlen, z1, zlen, false);
//                                }
//                            }
//                            else
//                            {
//                                intFarmType = RNG.Next(100);
//                                if (((xlen >= 11 && zlen >= 16) || (xlen >= 16 && zlen >= 11)) && intFarmType > 50)
//                                {
//                                    curFarm = FarmTypes.Orchard;
//                                    xlen = ((int)((xlen - 1) / 5) * 5) + 1;
//                                    zlen = ((int)((zlen - 1) / 5) * 5) + 1;
//                                }
//                                else
//                                {
//                                    intFarmType = RNG.Next(3);
//                                    if (intFarmType == 2)
//                                    {
//                                        curFarm = FarmTypes.Cactus;
//                                    }
//                                    else if (intFarmType == 1)
//                                    {
//                                        curFarm = FarmTypes.Wheat;
//                                        xlen += (xlen % 2) - 1;
//                                        zlen += (zlen % 2) - 1;
//                                    }
//                                    else
//                                    {
//                                        curFarm = FarmTypes.SugarCane;
//                                        xlen += (xlen % 2) - 1;
//                                        zlen += (zlen % 2) - 1;
//                                    }
//                                }
//                            }

//                            int intWallMaterial = RNG.NextDouble() > 0.5 ? BlockInfo.Fence.ID : BlockInfo.Leaves.ID;

//                            switch (curFarm)
//                            {
//                                case FarmTypes.Hill:
//                                case FarmTypes.Pond:
//                                    intWallMaterial = 0;
//                                    break;
//                                case FarmTypes.SugarCane:
//                                case FarmTypes.Mushroom:
//                                    if (RNG.NextDouble() > 0.5)
//                                    {
//                                        intWallMaterial = 0;
//                                    }
//                                    break;
//                                case FarmTypes.Wheat:
//                                    intWallMaterial = BlockInfo.Leaves.ID;
//                                    break;
//                                    // no need for default - the other types don't need anything here
//                            }
//                            switch (intWallMaterial)
//                            {
//                                case BlockType.FENCE:
//                                    BlockShapes.MakeHollowLayers(x1, x1 + xlen, 64, 64, z1, z1 + zlen,
//                                                                 BlockInfo.Fence.ID, 0, -1);
//                                    break;
//                                case BlockType.LEAVES:
//                                    // the saplings will all disappear if one of them is broken.
//                                    // so we put wood beneath them to stop that happening
//                                    BlockShapes.MakeHollowLayers(x1, x1 + xlen, 63, 63, z1, z1 + zlen,
//                                                                 BlockInfo.Wood.ID, 0, -1);
//                                    BlockShapes.MakeHollowLayers(x1, x1 + xlen, 64, 65, z1, z1 + zlen, BlockInfo.Leaves.ID, 0, 
//                                      RNG.RandomNumber((int)LeafType.OAK, (int)LeafType.SPRUCE,
//                                                                (int)LeafType.BIRCH));
//                                    break;
//                                case 0:
//                                    // no wall
//                                    break;
//                                default:
//                                    Debug.Fail("Invalid switch result");
//                                    break;
//                            }

//                            switch (curFarm)
//                            {
//                                case FarmTypes.Orchard:
//                                    int intSaplingType = RNG.RandomNumber(SaplingBirchDataID,
//                                                                                   SaplingOakDataID,
//                                                                                   SaplingSpruceDataID);
//                                    for (int x = x1 + 3; x <= x1 + xlen - 3; x += 5)
//                                    {
//                                        for (int z = z1 + 3; z <= z1 + zlen - 3; z += 5)
//                                        {
//                                            BlockShapes.MakeBlock(x, 63, z, City.GroundBlockID, City.GroundBlockData);
//                                            BlockShapes.MakeBlock(x, 64, z, BlockInfo.Sapling.ID, intSaplingType);
//                                        }
//                                    }
//                                    break;
//                                case FarmTypes.Cactus:
//                                    int intAttempts = 0;
//                                    do
//                                    {
//                                        int xCactus = RNG.Next(x1 + 1, x1 + xlen);
//                                        int zCactus = RNG.Next(z1 + 1, z1 + zlen);
//                                        bool booValidFarm = true;
//                                        for (int xCheck = xCactus - 1; xCheck <= xCactus + 1 && booValidFarm; xCheck++)
//                                        {
//                                            for (int zCheck = zCactus - 1; zCheck <= zCactus + 1 && booValidFarm; zCheck++)
//                                            {
//                                                if (bm.GetID(xCheck, 64, zCheck) != BlockInfo.Air.ID)
//                                                {
//                                                    booValidFarm = false;
//                                                }
//                                            }
//                                        }
//                                        if (booValidFarm)
//                                        {
//                                            bm.SetID(xCactus, 64, zCactus, BlockInfo.Cactus.ID);
//                                            if (RNG.NextDouble() > 0.5)
//                                            {
//                                                bm.SetID(xCactus, 65, zCactus, BlockInfo.Cactus.ID);
//                                            }
//                                        }
//                                    }
//                                    while (++intAttempts < 100);
//                                    break;
//                                case FarmTypes.Wheat:
//                                    BlockShapes.MakeHollowLayers(x1, x1 + xlen, 66, 66, z1, z1 + zlen,
//                                                                 BlockInfo.Glass.ID, 0, -1);
//                                    BlockShapes.MakeSolidBox(x1 + 1, x1 + xlen - 1, 67, 67, z1 + 1, z1 + zlen - 1,
//                                                             BlockInfo.Glass.ID, 0);
//                                    break;
//                                // no need for a default, because there's nothing to do for the other farms
//                            }

//                            for (int x = x1 + 1; x <= x1 + xlen - 1; x++)
//                            {
//                                for (int z = z1 + 1; z <= z1 + zlen - 1; z++)
//                                {
//                                    switch (curFarm)
//                                    {
//                                        case FarmTypes.Cactus:
//                                            bm.SetID(x, 63, z, BlockInfo.Sand.ID);
//                                            break;
//                                        case FarmTypes.Wheat:
//                                            if (z == z1 + 1)
//                                            {
//                                                bm.SetID(x, 63, z, BlockInfo.DoubleSlab.ID);
//                                            }
//                                            else if (x % 2 == 0)
//                                            {
//                                                BlockShapes.MakeBlock(x, 63, z, BlockInfo.Farmland.ID, 1);
//                                                bm.SetID(x, 64, z, BlockInfo.Crops.ID);
//                                            }
//                                            else
//                                            {
//                                                bm.SetID(x, 63, z, BlockInfo.StationaryWater.ID);
//                                            }
//                                            break;
//                                        case FarmTypes.SugarCane:
//                                            if (z != z1 + 1)
//                                            {
//                                                if (x % 2 == 0)
//                                                {
//                                                    bm.SetID(x, 64, z, BlockInfo.SugarCane.ID);
//                                                    if (RNG.Next(100) > 50)
//                                                    {
//                                                        bm.SetID(x, 65, z, BlockInfo.SugarCane.ID);
//                                                    }
//                                                }
//                                                else
//                                                {
//                                                    bm.SetID(x, 63, z, BlockInfo.StationaryWater.ID);
//                                                }
//                                            }
//                                            break;
//                                        // no need for a default, because there's nothing to do for the other farms
//                                    }
//                                }
//                            }
//                            int intDoorPosition = x1 + RNG.Next(1, xlen - 1);
//                            if (curFarm == FarmTypes.Wheat)
//                            {
//                                bm.SetID(intDoorPosition, 63, z1, BlockInfo.DoubleSlab.ID);
//                            }
//                            if (intWallMaterial != 0)
//                            {
//                                if (curFarm == FarmTypes.Wheat || intWallMaterial == BlockInfo.Leaves.ID)
//                                {
//                                    BlockShapes.MakeBlock(intDoorPosition, 64, z1, BlockInfo.WoodDoor.ID, 4);
//                                    BlockShapes.MakeBlock(intDoorPosition, 65, z1, BlockInfo.WoodDoor.ID, 4 + (int)DoorState.TOPHALF);
//                                }
//                                else
//                                {
//                                    bm.SetID(intDoorPosition, 64, z1, BlockInfo.FenceGate.ID);
//                                    bm.SetData(intDoorPosition, 64, z1, 0);
//                                }
//                            }
//                            intFail = 0;
//                            if (++intFarms > 10)
//                            {
//                                world.Save();
//                                intFarms = 0;
//                            }
//                        }
//                    }
//                }
//                MakeMiniPondsAndHills(world, bm);
//            }
//            if (City.HasFlowers)
//            {
//                MakeFlowers(world, bm);
//            }
//        }
//        private static void AddBuildings(BlockManager bm)
//        {
//            SourceWorld.Building CurrentBuilding;
//            List<int> lstAllFarmingBuildings = new List<int>();
//            int intBuildings = 0;
//            do
//            {                
//                do
//                {
//                    CurrentBuilding = SourceWorld.SelectRandomBuilding(SourceWorld.BuildingTypes.Farming, 0, false);
//                } while (lstAllFarmingBuildings.Contains(CurrentBuilding.intID));
//                AddBuilding(bm, CurrentBuilding.intID);
//                if (CurrentBuilding.booUnique)
//                {
//                    lstAllFarmingBuildings.Add(CurrentBuilding.intID);
//                }
//            } while (++intBuildings < Math.Max(1, (City.MapLength / 16) / 2));
//        }
//        private static void AddBuilding(BlockManager bm, int intBuildingID)
//        {
//            bool booAddedBuilding = false;
//            SourceWorld.Building bldInsert = SourceWorld.GetBuilding(intBuildingID);
//            int intLen = bldInsert.intSizeX;
//            bool booValid;
//            int FailCounter = 0;
//            do
//            {
//                booValid = false;
//                int x1 = RNG.Next(3, City.MapLength - (3 + intLen));
//                int z1 = RNG.Next(3, City.MapLength - (3 + intLen));
//                if (!(x1 >= City.EdgeLength && z1 >= City.EdgeLength &&
//                      x1 <= City.MapLength - City.EdgeLength && z1 <= City.MapLength - City.EdgeLength))
//                {
//                    booValid = true;
//                    for (int x = x1 - 2; x <= x1 + intLen + 2 && booValid; x++)
//                    {
//                        for (int z = z1 - 2; z <= z1 + intLen + 2 && booValid; z++)
//                        {
//                            // make sure it doesn't overlap with the spawn point or another farm
//                            if ((x == City.MapLength / 2 && z == SpawnZ) ||
//                                bm.GetID(x, 63, z) != City.GroundBlockID ||
//                                bm.GetID(x, 64, z) != BlockInfo.Air.ID ||
//                                bm.GetID(x, 53, z) == BlockInfo.Air.ID)
//                            {
//                                booValid = false;
//                            }
//                        }
//                    }
//                }
//                if (booValid)
//                {
//                    SourceWorld.InsertBuilding(bm, new int[City.MapLength, City.MapLength], 0, x1, z1, bldInsert, 0);
//                    booAddedBuilding = true;
//                    FailCounter = 0;
//                }
//                else
//                {
//                    FailCounter++;
//                }
//            } while (!booAddedBuilding && FailCounter <= 500);
//        }
//        private static void MakePond(BlockManager bm, int x, int xlen, int z, int zlen, bool booSmallPond)
//        {
//            int[,] intArea = new int[xlen, zlen];
//            int a, b;

//            int c = 0, d = 0, clen = -1, dlen = -1;
//            if (!booSmallPond)
//            {
//                c = RNG.Next(1, intArea.GetUpperBound(0) - 7);
//                d = RNG.Next(1, intArea.GetUpperBound(1) - 7);
//                clen = RNG.Next(3, 7);
//                dlen = RNG.Next(3, 7);
//            }

//            for (a = 1; a <= intArea.GetUpperBound(0) - 1; a++)
//            {
//                for (b = 1; b <= intArea.GetUpperBound(1) - 1; b++)
//                {
//                    if (a < c || a > c + clen || b < d || b > d + dlen)
//                    {
//                        intArea[a, b] = 1;
//                    }
//                }
//            }

//            intArea = CheckAgainstNeighbours(intArea, 0, 0, 7, 0, RNG.Next(3, 6), true);
//            intArea = CheckAgainstNeighbours(intArea, 6, 0, 2, 2, 1, false);
//            intArea = CheckAgainstNeighbours(intArea, 14, 0, 3, 3, 1, false);

//            bool booLava = (!booSmallPond && RNG.NextDouble() < 0.15);

//            for (a = 1; a <= intArea.GetUpperBound(0) - 1; a++)
//            {
//                for (b = 1; b <= intArea.GetUpperBound(1) - 1; b++)
//                {
//                    if (intArea[a, b] == 1)
//                    {
//                        bm.SetID(x + a, 64, z + b, BlockInfo.TallGrass.ID);
//                        bm.SetData(x + a, 64, z + b, RNG.Next(1, 3));
//                    }
//                    else
//                    {
//                        for (int y = 2; y <= intArea[a, b]; y++)
//                        {
//                            if (booLava)
//                            {
//                                bm.SetID(x + a, 65 - y, z + b, BlockInfo.StationaryLava.ID);
//                            }
//                            else
//                            {
//                                bm.SetID(x + a, 65 - y, z + b, BlockInfo.StationaryWater.ID);
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        private static void MakeHill(BlockManager bm, int x, int xlen, int z, int zlen, bool booMini)
//        {
//            int[,] intArea = new int[xlen, zlen];
//            int a, b;

//            int c = 0, d = 0, clen = -1, dlen = -1;
//            if (!booMini)
//            {
//                c = RNG.Next(1, intArea.GetUpperBound(0) - 7);
//                d = RNG.Next(1, intArea.GetUpperBound(1) - 7);
//                clen = RNG.Next(3, 7);
//                dlen = RNG.Next(3, 7);
//            }

//            for (a = 1; a <= intArea.GetUpperBound(0) - 1; a++)
//            {
//                for (b = 1; b <= intArea.GetUpperBound(1) - 1; b++)
//                {
//                    if (a < c || a > c + clen || b < d || b > d + dlen)
//                    {
//                        intArea[a, b] = 1;
//                    }
//                }
//            }

//            intArea = CheckAgainstNeighbours(intArea, 0, 0, 7, 0, RNG.Next(7, 11), true);
//            intArea = CheckAgainstNeighbours(intArea, 6, 0, 3, 2, 1, false);
//            intArea = CheckAgainstNeighbours(intArea, 14, 0, 3, 3, 1, false);

//            for (a = 1; a <= intArea.GetUpperBound(0) - 1; a++)
//            {
//                for (b = 1; b <= intArea.GetUpperBound(1) - 1; b++)
//                {
//                    for (int y = 1; y <= intArea[a, b]; y++)
//                    {
//                        if (y == intArea[a, b])
//                        {
//                            bm.SetID(x + a, y + 63, z + b, BlockInfo.Grass.ID);
//                            switch (RNG.Next(0, 10))
//                            {
//                                case 0:
//                                    bm.SetID(x + a, y + 64, z + b, BlockInfo.RedRose.ID);
//                                    break;
//                                case 1:
//                                    bm.SetID(x + a, y + 64, z + b, BlockInfo.YellowFlower.ID);
//                                    break;
//                                case 2:
//                                case 3:
//                                    bm.SetID(x + a, y + 64, z + b, BlockInfo.TallGrass.ID);
//                                    bm.SetData(x + a, y + 64, z + b, RNG.Next(1, 3));
//                                    break;
//                                // we want to skip the other numbers, so there's no need for a default
//                            }
//                        }
//                        else
//                        {
//                            bm.SetID(x + a, y + 63, z + b, BlockInfo.Dirt.ID);
//                        }
//                    }
//                }
//            }
//        }
//        private static int[,] CheckAgainstNeighbours(int[,] intArea, int intMin, int intRandMin, int intRandMax,
//                                                     int intSet, int intTimes, bool booCheckDown)
//        {
//            int[,] intOriginal = new int[intArea.GetLength(0), intArea.GetLength(1)];
//            for (int intTime = 1; intTime <= intTimes; intTime++)
//            {
//                Array.Copy(intArea, intOriginal, intArea.Length);
//                for (int a = 1; a <= intArea.GetUpperBound(0) - 1; a++)
//                {
//                    for (int b = 1; b <= intArea.GetUpperBound(1) - 1; b++)
//                    {
//                        if (booCheckDown)
//                        {
//                            if (Utils.SumOfNeighbours2D(intOriginal, a, b) <
//                                intMin + RNG.Next(intRandMin, intRandMax))
//                            {
//                                intArea[a, b] = intSet;
//                            }
//                        }
//                        else
//                        {
//                            if (Utils.SumOfNeighbours2D(intOriginal, a, b) >=
//                                intMin + RNG.Next(intRandMin, intRandMax))
//                            {
//                                intArea[a, b] = intSet;
//                            }
//                        }
//                    }
//                }
//            }
//            return intArea;
//        }
//        private static void MakeMiniPondsAndHills(BetaWorld world, BlockManager bm)
//        {
//            int intFail = 0;
//            int intAdded = 0;
//            do
//            {
//                int xlen = RNG.Next(4, 12);
//                int x1 = RNG.Next(1, City.MapLength - (1 + xlen));
//                int zlen = RNG.Next(4, 12);
//                int z1 = RNG.Next(1, City.MapLength - (1 + zlen));
//                if (!(x1 >= City.EdgeLength && z1 >= City.EdgeLength &&
//                      x1 <= City.MapLength - City.EdgeLength && z1 <= City.MapLength - City.EdgeLength))
//                {
//                    bool booValid = true;
//                    for (int x = x1 - 1; x <= x1 + xlen + 1 && booValid; x++)
//                    {
//                        for (int z = z1 - 1; z <= z1 + zlen + 1 && booValid; z++)
//                        {
//                            // make sure it doesn't overlap with the spawn point or another farm
//                            if ((x == City.MapLength / 2 && z == SpawnZ) ||
//                                bm.GetID(x, 63, z) != City.GroundBlockID ||
//                                bm.GetID(x, 64, z) != BlockInfo.Air.ID)
//                            {
//                                booValid = false;
//                            }
//                        }
//                    }
//                    if (booValid)
//                    {
//                        if (RNG.NextDouble() > 0.5)
//                        {
//                            MakePond(bm, x1, xlen, z1, zlen, true);
//                        }
//                        else
//                        {
//                            MakeHill(bm, x1, xlen, z1, zlen, true);
//                        }
//                        intFail = 0;
//                        if (++intAdded % 25 == 0)
//                        {
//                            world.Save();
//                        }
//                    }
//                    else
//                    {
//                        intFail++;
//                    }
//                }
//            } while (intFail < 500);
//        }
//        private static void MakeFlowers(BetaWorld world, BlockManager bm)
//        {
//            int intFlowers = 0;
//            for (int x = 0; x <= City.MapLength; x++)
//            {
//                for (int z = 0; z <= City.MapLength; z++)
//                {
//                    if (x <= City.EdgeLength || z <= City.EdgeLength ||
//                        x >= City.MapLength - City.EdgeLength || z >= City.MapLength - City.EdgeLength)
//                    {
//                        if (bm.GetID(x, 63, z) == City.GroundBlockID &&
//                            bm.GetID(x, 64, z) == BlockInfo.Air.ID)
//                        {
//                            int intFree = 0;
//                            for (int xCheck = x - 1; xCheck <= x + 1; xCheck++)
//                            {
//                                for (int zCheck = z - 1; zCheck <= z + 1; zCheck++)
//                                {
//                                    if (bm.GetID(xCheck, 63, zCheck) == City.GroundBlockID &&
//                                        bm.GetID(xCheck, 64, zCheck) == BlockInfo.Air.ID)
//                                    {
//                                        intFree++;
//                                    }
//                                }
//                            }
//                            if (intFree >= MinimumFreeNeighboursNeededForFlowers &&
//                                RNG.NextDouble() * 100 <= City.FlowerSpawnPercent)
//                            {
//                                AddFlowersToBlock(bm, x, z, intFree);
//                                if (++intFlowers >= NumberOfFlowersBetweenSaves)
//                                {
//                                    intFlowers = 0;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        private static void AddFlowersToBlock(BlockManager bm, int x, int z, int intFreeNeighbours)
//        {
//            string strFlower = Utils.RandomValueFromXMLElement(Path.Combine("Resources", "Themes", City.ThemeName + ".xml"),
//                                                   "options", "flowers");
//            int intBlock = Convert.ToInt32(strFlower.Split('_')[0]);
//            int intID = 0;
//            if (strFlower.Contains("_"))
//            {
//                intID = City.GroundBlockData = Convert.ToInt32(strFlower.Split('_')[1]);
//            }
//            if (intBlock != BlockInfo.Cactus.ID || intFreeNeighbours == 9)
//            {
//                BlockShapes.MakeBlock(x, 64, z, intBlock, intID);
//            }
//        }
//    }
//}
