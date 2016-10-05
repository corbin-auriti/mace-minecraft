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
using System.Diagnostics;
using Substrate;
using Substrate.TileEntities;

namespace Mace
{    
    static class Buildings
    {
        public struct structPoint
        {
            public int x;
            public int z;
        }

        private const int NumberOfBuildingsBetweenSaves = 20;
        private const int NumberOfFlowersBetweenSaves = 250;
        private const double FlowerChance = 0.15;
        private const int MinimumFreeNeighboursNeededForFlowers = 3;

        enum StreetLights { Glowstone, Torches };

        public static structPoint MakeInsideCity(BlockManager bm, BetaWorld worldDest, int[,] intArea,
                                                 int intFarmLength, int intMapLength, bool booIncludePaths)
        {            
            int intBlockStart = intFarmLength + 13;
            structPoint spMineshaftEntrance = MakeBuildings(bm, intArea, intBlockStart, worldDest, intFarmLength);
            worldDest.Save();
            if (!booIncludePaths)
            {
                RemovePaths(bm, intArea, intBlockStart);
            }
            else
            {
                JoinPathsToRoad(bm, intMapLength, intFarmLength);
            }
            MakeStreetLights(bm, intMapLength, intFarmLength);
            MakeFlowers(bm, worldDest, intFarmLength, intMapLength);
            return spMineshaftEntrance;
        }
        private static structPoint MakeBuildings(BlockManager bmDest, int[,] intArea, int intBlockStart, BetaWorld world, int intFarmLength)
        {
            structPoint structLocationOfMineshaftEntrance = new structPoint();
            structLocationOfMineshaftEntrance.x = -1;
            structLocationOfMineshaftEntrance.z = -1;
            int intBuildings = 0;
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int z = 0; z < intArea.GetLength(1); z++)
                {
                    // hack: this 100 to 500 stuff is all a bit hackish really, need to find a proper solution
                    if (intArea[x, z] >= 100 && intArea[x, z] <= 500)
                    {
                        SourceWorld.Building CurrentBuilding = SourceWorld.GetBuilding(intArea[x, z] - 100);
                        SourceWorld.InsertBuilding(bmDest, intArea, intBlockStart, x, z,
                                                   CurrentBuilding, 0);

                        if (CurrentBuilding.btThis == SourceWorld.BuildingTypes.MineshaftEntrance)
                        {
                            structLocationOfMineshaftEntrance.x = intBlockStart + x + (int)(CurrentBuilding.intSizeX / 2);
                            structLocationOfMineshaftEntrance.z = intBlockStart + z + (int)(CurrentBuilding.intSizeZ / 2);
                            SourceWorld.Building BalloonBuilding = SourceWorld.GetBuilding("Sky Feature");
                            SourceWorld.InsertBuilding(bmDest, new int[0, 0], intBlockStart,
                                                       x + ((CurrentBuilding.intSizeX - BalloonBuilding.intSizeX) / 2),
                                                       z + ((CurrentBuilding.intSizeZ - BalloonBuilding.intSizeZ) / 2),
                                                       BalloonBuilding, 0);
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
            world.Save();
            return structLocationOfMineshaftEntrance;
        }
        private static void MakeFlowers(BlockManager bmDest, BetaWorld worldDest, int intFarmLength, int intMapLength)
        {
            int intFlowers = 0;
            for (int x = intFarmLength + 11; x <= intMapLength - (intFarmLength + 11); x++)
            {
                for (int z = intFarmLength + 11; z <= intMapLength - (intFarmLength + 11); z++)
                {
                    if (bmDest.GetID(x, 63, z) == BlockType.GRASS &&
                        bmDest.GetID(x, 64, z) == BlockType.AIR)
                    {
                        bool booFree = true;
                        int intFree = 0;
                        for (int xCheck = x - 1; xCheck <= x + 1 && booFree; xCheck++)
                        {
                            for (int zCheck = z - 1; zCheck <= z + 1 && booFree; zCheck++)
                            {
                                if (bmDest.GetID(xCheck, 63, zCheck) == BlockType.GRASS &&
                                    bmDest.GetID(xCheck, 64, zCheck) == BlockType.AIR)
                                {
                                    intFree++;
                                }
                            }
                        }
                        if (intFree >= MinimumFreeNeighboursNeededForFlowers &&
                            RandomHelper.NextDouble() <= FlowerChance)
                        {
                            AddFlowersToBlock(bmDest, x, z);
                            if (++intFlowers >= NumberOfFlowersBetweenSaves)
                            {
                                worldDest.Save();
                                intFlowers = 0;
                            }
                        }
                    }
                }
            }
        }
        private static void RemovePaths(BlockManager bmDest, int[,] intArea, int intBlockStart)
        {
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int z = 0; z < intArea.GetLength(1); z++)
                {
                    if (intArea[x, z] == 1)
                    {
                        bmDest.SetID(intBlockStart + x, 63, intBlockStart + z, BlockType.GRASS);
                    }
                }
            }
        }
        private static void MakeStreetLights(BlockManager bm, int intMapLength, int intFarmLength)
        {
            StreetLights slCurrent;
            if (RandomHelper.NextDouble() > 0.5)
            {
                slCurrent = StreetLights.Glowstone;
            }
            else
            {
                slCurrent = StreetLights.Torches;
            }
            for (int a = 2; a <= (intMapLength / 2) - (intFarmLength + 16); a += 8)
            {
                MakeStreetLight(bm, slCurrent, (intMapLength / 2) - a, (intMapLength / 2) - 2,
                                               (intMapLength / 2) - a, (intMapLength / 2) - 1);
                MakeStreetLight(bm, slCurrent, (intMapLength / 2) - a, (intMapLength / 2) + 2,
                                               (intMapLength / 2) - a, (intMapLength / 2) + 1);
                MakeStreetLight(bm, slCurrent, (intMapLength / 2) + a, (intMapLength / 2) - 2,
                                               (intMapLength / 2) + a, (intMapLength / 2) - 1);
                MakeStreetLight(bm, slCurrent, (intMapLength / 2) + a, (intMapLength / 2) + 2,
                                               (intMapLength / 2) + a, (intMapLength / 2) + 1);

                MakeStreetLight(bm, slCurrent, (intMapLength / 2) - 2, (intMapLength / 2) - a,
                                               (intMapLength / 2) - 1, (intMapLength / 2) - a);
                MakeStreetLight(bm, slCurrent, (intMapLength / 2) + 2, (intMapLength / 2) - a,
                                               (intMapLength / 2) + 1, (intMapLength / 2) - a);
                MakeStreetLight(bm, slCurrent, (intMapLength / 2) - 2, (intMapLength / 2) + a,
                                               (intMapLength / 2) - 1, (intMapLength / 2) + a);
                MakeStreetLight(bm, slCurrent, (intMapLength / 2) + 2, (intMapLength / 2) + a,
                                               (intMapLength / 2) + 1, (intMapLength / 2) + a);
            }
        }
        private static void MakeStreetLight(BlockManager bm, StreetLights slCurrent, int x1, int z1, int x2, int z2)
        {
            if (bm.GetID(x1, 63, z1) == BlockType.GRASS && bm.GetID(x1, 64, z1) == BlockType.AIR
                && bm.GetID(x1, 65, z1) == BlockType.AIR)
            {
                switch (slCurrent)
                {
                    case StreetLights.Glowstone:
                        bm.SetID(x1, 64, z1, BlockType.FENCE);
                        bm.SetID(x1, 65, z1, BlockType.FENCE);
                        bm.SetID(x1, 66, z1, BlockType.FENCE);
                        bm.SetID(x1, 67, z1, BlockType.FENCE);
                        bm.SetID(x1, 68, z1, BlockType.FENCE);
                        bm.SetID(x2, 68, z2, BlockType.FENCE);
                        bm.SetID(x2, 67, z2, BlockType.GLOWSTONE_BLOCK);
                        break;
                    case StreetLights.Torches:
                        bm.SetID(x1, 64, z1, BlockType.FENCE);
                        bm.SetID(x1, 65, z1, BlockType.FENCE);
                        bm.SetID(x1, 66, z1, BlockType.FENCE);
                        bm.SetID(x1, 67, z1, BlockType.FENCE);
                        bm.SetID(x1, 68, z1, BlockType.FENCE);
                        bm.SetID(x2, 68, z2, BlockType.FENCE);
                        bm.SetID(x2, 67, z2, BlockType.WOOD_PLANK);
                        if (z1 == z2)
                        {
                            BlockHelper.MakeTorch(x2, 67, z2 - 1, BlockType.WOOD_PLANK, 0);
                            BlockHelper.MakeTorch(x2, 67, z2 + 1, BlockType.WOOD_PLANK, 0);
                        }
                        else
                        {
                            BlockHelper.MakeTorch(x2 - 1, 67, z2, BlockType.WOOD_PLANK, 0);
                            BlockHelper.MakeTorch(x2 + 1, 67, z2, BlockType.WOOD_PLANK, 0);
                        }
                        break;
                }
            }
        }
        private static void AddFlowersToBlock(BlockManager bmDest, int x, int z)
        {
            switch (RandomHelper.Next(4))
            {
                case 0:
                    bmDest.SetID(x, 64, z, BlockType.RED_ROSE);
                    break;
                case 1:
                    bmDest.SetID(x, 64, z, BlockType.YELLOW_FLOWER);
                    break;
                case 2:
                case 3:
                    BlockShapes.MakeBlock(x, 64, z, BlockType.TALL_GRASS, RandomHelper.Next(1, 3));
                    break;
                default:
                    Debug.Fail("Invalid switch result");
                    break;
            }
        }
        private static void JoinPathsToRoad(BlockManager bmDest, int intMapLength, int intFarmLength)
        {
            int intBlockStart = intFarmLength + 13;
            int intPlotBlocksLength = (1 + intMapLength) - (intBlockStart * 2);
            for (int a = 0; a <= intPlotBlocksLength; a++)
            {
                for (int b = -2; b <= 2; b+=4)
                {
                    if (bmDest.GetID(intBlockStart + a, 63, intBlockStart + (intPlotBlocksLength / 2) + (int)(b * 1.5)) == BlockType.DOUBLE_SLAB &&
                        bmDest.GetID(intBlockStart + a, 63, intBlockStart + (intPlotBlocksLength / 2) + (int)(b / 2)) == BlockType.DOUBLE_SLAB &&
                        bmDest.GetID(intBlockStart + a, 63, intBlockStart + (intPlotBlocksLength / 2) + b) == BlockType.GRASS &&
                        bmDest.GetID(intBlockStart + a, 64, intBlockStart + (intPlotBlocksLength / 2) + b) == BlockType.AIR)
                    {
                        bmDest.SetID(intBlockStart + a, 63, intBlockStart + (intPlotBlocksLength / 2) + b, BlockType.DOUBLE_SLAB);
                    }
                    if (bmDest.GetID(intBlockStart + (intPlotBlocksLength / 2) + (int)(b * 1.5), 63, intBlockStart + a) == BlockType.DOUBLE_SLAB &&
                        bmDest.GetID(intBlockStart + (intPlotBlocksLength / 2) + (int)(b / 2), 63, intBlockStart + a) == BlockType.DOUBLE_SLAB &&
                        bmDest.GetID(intBlockStart + (intPlotBlocksLength / 2) + b, 63, intBlockStart + a) == BlockType.GRASS &&
                        bmDest.GetID(intBlockStart + (intPlotBlocksLength / 2) + b, 64, intBlockStart + a) == BlockType.AIR)
                    {
                        bmDest.SetID(intBlockStart + (intPlotBlocksLength / 2) + b, 63, intBlockStart + a, BlockType.DOUBLE_SLAB);
                    }
                }
            }
        }
    }
}
