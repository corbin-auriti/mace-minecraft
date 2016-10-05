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
using System.IO;
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
        private const int MinimumFreeNeighboursNeededForFlowers = 3;

        enum StreetLights { None, Glowstone, Torches };

        public static structPoint MakeInsideCity(BlockManager bm, BetaWorld worldDest, int[,] intArea, frmMace frmLogForm)
        {
            int intBlockStart = City.FarmLength + 13;
            structPoint spMineshaftEntrance = MakeBuildings(bm, intArea, intBlockStart, worldDest);
            worldDest.Save();
            if (City.HasPaths)
            {
                JoinPathsToRoad(bm);
            }
            else
            {
                RemovePaths(bm, intArea, intBlockStart);
            }
            frmLogForm.UpdateLog("Creating street lights: " + City.StreetLightType, true, true);
            MakeStreetLights(bm);
            if (City.HasFlowers)
            {
                frmLogForm.UpdateLog("Creating flowers", true, true);
                MakeFlowers(bm, worldDest);
            }
            return spMineshaftEntrance;
        }
        private static structPoint MakeBuildings(BlockManager bm, int[,] intArea, int intBlockStart, BetaWorld world)
        {
            structPoint structLocationOfMineshaftEntrance = new structPoint();
            structLocationOfMineshaftEntrance.x = -1;
            structLocationOfMineshaftEntrance.z = -1;
            int intBuildings = 0;
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int z = 0; z < intArea.GetLength(1); z++)
                {
                    // hack low: this 100 to 500 stuff is all a bit hackish really, need to find a proper solution
                    if (intArea[x, z] >= 100 && intArea[x, z] <= 500)
                    {
                        SourceWorld.Building CurrentBuilding = SourceWorld.GetBuilding(intArea[x, z] - 100);
                        SourceWorld.InsertBuilding(bm, intArea, intBlockStart, x, z,
                                                   CurrentBuilding, 0);

                        if (CurrentBuilding.btThis == SourceWorld.BuildingTypes.MineshaftEntrance)
                        {
                            structLocationOfMineshaftEntrance.x = intBlockStart + x + (int)(CurrentBuilding.intSizeX / 2);
                            structLocationOfMineshaftEntrance.z = intBlockStart + z + (int)(CurrentBuilding.intSizeZ / 2);
                            if (City.HasSkyFeature)
                            {
                                SourceWorld.Building BalloonBuilding = SourceWorld.GetBuilding("Sky Feature");
                                SourceWorld.InsertBuilding(bm, new int[0, 0], intBlockStart,
                                                           x + ((CurrentBuilding.intSizeX - BalloonBuilding.intSizeX) / 2),
                                                           z + ((CurrentBuilding.intSizeZ - BalloonBuilding.intSizeZ) / 2),
                                                           BalloonBuilding, 0);
                            }
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
        private static void MakeFlowers(BlockManager bm, BetaWorld worldDest)
        {
            int intFlowers = 0;
            for (int x = City.FarmLength + 11; x <= City.MapLength - (City.FarmLength + 11); x++)
            {
                for (int z = City.FarmLength + 11; z <= City.MapLength - (City.FarmLength + 11); z++)
                {
                    if (bm.GetID(x, 63, z) == City.GroundBlockID &&
                        bm.GetID(x, 64, z) == BlockInfo.Air.ID)
                    {
                        int intFree = 0;
                        for (int xCheck = x - 1; xCheck <= x + 1; xCheck++)
                        {
                            for (int zCheck = z - 1; zCheck <= z + 1; zCheck++)
                            {
                                if (bm.GetID(xCheck, 63, zCheck) == City.GroundBlockID &&
                                    bm.GetID(xCheck, 64, zCheck) == BlockInfo.Air.ID)
                                {
                                    intFree++;
                                }
                            }
                        }
                        if (intFree >= MinimumFreeNeighboursNeededForFlowers &&
                            RandomHelper.NextDouble() * 100 <= City.FlowerSpawnPercent)
                        {
                            AddFlowersToBlock(bm, x, z, intFree);
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
        private static void RemovePaths(BlockManager bm, int[,] intArea, int intBlockStart)
        {
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int z = 0; z < intArea.GetLength(1); z++)
                {
                    if (intArea[x, z] == 1)
                    {
                        BlockShapes.MakeBlock(intBlockStart + x, 63, intBlockStart + z,
                                              City.GroundBlockID, City.GroundBlockData);
                    }
                }
            }
        }
        private static void MakeStreetLights(BlockManager bm)
        {
            StreetLights slCurrent = StreetLights.None;
            switch (City.StreetLightType.ToLower())
            {
                case "glowstones":
                    slCurrent = StreetLights.Glowstone;
                    break;
                case "torches":
                    slCurrent = StreetLights.Torches;
                    break;
            }
            if (slCurrent == StreetLights.Glowstone || slCurrent == StreetLights.Torches)
            {
                for (int a = 2; a <= (City.MapLength / 2) - (City.FarmLength + 16); a += 8)
                {
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) - a, (City.MapLength / 2) - 2,
                                                   (City.MapLength / 2) - a, (City.MapLength / 2) - 1);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) - a, (City.MapLength / 2) + 2,
                                                   (City.MapLength / 2) - a, (City.MapLength / 2) + 1);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) + a, (City.MapLength / 2) - 2,
                                                   (City.MapLength / 2) + a, (City.MapLength / 2) - 1);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) + a, (City.MapLength / 2) + 2,
                                                   (City.MapLength / 2) + a, (City.MapLength / 2) + 1);

                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) - 2, (City.MapLength / 2) - a,
                                                   (City.MapLength / 2) - 1, (City.MapLength / 2) - a);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) + 2, (City.MapLength / 2) - a,
                                                   (City.MapLength / 2) + 1, (City.MapLength / 2) - a);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) - 2, (City.MapLength / 2) + a,
                                                   (City.MapLength / 2) - 1, (City.MapLength / 2) + a);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) + 2, (City.MapLength / 2) + a,
                                                   (City.MapLength / 2) + 1, (City.MapLength / 2) + a);
                }
            }
        }
        private static void MakeStreetLight(BlockManager bm, StreetLights slCurrent, int x1, int z1, int x2, int z2)
        {
            if (bm.GetID(x1, 63, z1) == City.GroundBlockID &&
                bm.GetData(x1, 63, z1) == City.GroundBlockData &&
                bm.GetID(x1, 64, z1) == BlockInfo.Air.ID &&
                bm.GetID(x1, 65, z1) == BlockInfo.Air.ID)
            {
                switch (slCurrent)
                {
                    case StreetLights.Glowstone:
                        bm.SetID(x1, 64, z1, BlockInfo.Fence.ID);
                        bm.SetID(x1, 65, z1, BlockInfo.Fence.ID);
                        bm.SetID(x1, 66, z1, BlockInfo.Fence.ID);
                        bm.SetID(x1, 67, z1, BlockInfo.Fence.ID);
                        bm.SetID(x1, 68, z1, BlockInfo.Fence.ID);
                        bm.SetID(x2, 68, z2, BlockInfo.Fence.ID);
                        bm.SetID(x2, 67, z2, BlockInfo.Glowstone.ID);
                        break;
                    case StreetLights.Torches:
                        bm.SetID(x1, 64, z1, BlockInfo.Fence.ID);
                        bm.SetID(x1, 65, z1, BlockInfo.Fence.ID);
                        bm.SetID(x1, 66, z1, BlockInfo.Fence.ID);
                        bm.SetID(x1, 67, z1, BlockInfo.Fence.ID);
                        bm.SetID(x1, 68, z1, BlockInfo.Fence.ID);
                        bm.SetID(x2, 68, z2, BlockInfo.Fence.ID);
                        bm.SetID(x2, 67, z2, BlockInfo.WoodPlank.ID);
                        if (z1 == z2)
                        {
                            BlockHelper.MakeTorch(x2, 67, z2 - 1, BlockInfo.WoodPlank.ID, 0);
                            BlockHelper.MakeTorch(x2, 67, z2 + 1, BlockInfo.WoodPlank.ID, 0);
                        }
                        else
                        {
                            BlockHelper.MakeTorch(x2 - 1, 67, z2, BlockInfo.WoodPlank.ID, 0);
                            BlockHelper.MakeTorch(x2 + 1, 67, z2, BlockInfo.WoodPlank.ID, 0);
                        }
                        break;
                }
            }
        }
        private static void AddFlowersToBlock(BlockManager bm, int x, int z, int intFreeNeighbours)
        {
            string strFlower = Utils.RandomValueFromXMLElement(Path.Combine("Resources", "Themes", City.ThemeName + ".xml"),
                                                   "options", "flowers");
            int intBlock = Convert.ToInt32(strFlower.Split('_')[0]);
            int intID = 0;
            if (strFlower.Contains("_"))
            {
                intID = City.GroundBlockData = Convert.ToInt32(strFlower.Split('_')[1]);
            }
            if (intBlock != BlockInfo.Cactus.ID || intFreeNeighbours == 9)
            {
                BlockShapes.MakeBlock(x, 64, z, intBlock, intID);
            }
        }
        private static void JoinPathsToRoad(BlockManager bm)
        {
            int intBlockStart = City.FarmLength + 13;
            int intHalfPlotBlocksLength = ((1 + City.MapLength) - (2 * intBlockStart)) / 2;
            for (int a = 0; a <= 2 * intHalfPlotBlocksLength; a++)
            {
                for (int b = -2; b <= 2; b+=4)
                {
                    if (bm.GetID(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + (int)(b * 1.5)) == City.PathBlockID &&
                        bm.GetID(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + (int)(b / 2)) == City.PathBlockID &&
                        bm.GetID(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + b) == City.GroundBlockID &&
                        bm.GetData(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + b) == City.GroundBlockData &&
                        bm.GetID(intBlockStart + a, 64, intBlockStart + intHalfPlotBlocksLength + b) == BlockInfo.Air.ID)
                    {
                        bm.SetID(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + b, City.PathBlockID);
                        bm.SetData(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + b, City.PathBlockData);
                    }
                    if (bm.GetID(intBlockStart + intHalfPlotBlocksLength + (int)(b * 1.5), 63, intBlockStart + a) == City.PathBlockID &&
                        bm.GetID(intBlockStart + intHalfPlotBlocksLength + (int)(b / 2), 63, intBlockStart + a) == City.PathBlockID &&
                        bm.GetID(intBlockStart + intHalfPlotBlocksLength + b, 63, intBlockStart + a) == City.GroundBlockID &&
                        bm.GetData(intBlockStart + intHalfPlotBlocksLength + b, 63, intBlockStart + a) == City.GroundBlockData &&
                        bm.GetID(intBlockStart + intHalfPlotBlocksLength + b, 64, intBlockStart + a) == BlockInfo.Air.ID)
                    {
                        bm.SetID(intBlockStart + intHalfPlotBlocksLength + b, 63, intBlockStart + a, City.PathBlockID);
                        bm.SetData(intBlockStart + intHalfPlotBlocksLength + b, 63, intBlockStart + a, City.PathBlockData);
                    }
                }
            }
        }
    }
}
