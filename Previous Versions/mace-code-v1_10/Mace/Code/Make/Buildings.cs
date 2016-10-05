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

        enum StreetLights { None, Glowstone, Torches };

        public static structPoint MakeInsideCity(BlockManager bm, BetaWorld worldDest, int[,] intArea, frmMace frmLogForm)
        {
            int intBlockStart = City.EdgeLength + 13;
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
                        SourceWorld.InsertBuilding(bm, intArea, intBlockStart, x, z, CurrentBuilding, 0, -1);

                        if (CurrentBuilding.BuildingType == SourceWorld.BuildingTypes.MineshaftEntrance)
                        {
                            structLocationOfMineshaftEntrance.x = intBlockStart + x + (int)(CurrentBuilding.intSizeX / 2);
                            structLocationOfMineshaftEntrance.z = intBlockStart + z + (int)(CurrentBuilding.intSizeZ / 2);
                            if (City.HasSkyFeature)
                            {
                                SourceWorld.Building BalloonBuilding = SourceWorld.SelectRandomBuilding(SourceWorld.BuildingTypes.SkyFeature, 0);
                                SourceWorld.InsertBuilding(bm, new int[0, 0], intBlockStart,
                                                           x + ((CurrentBuilding.intSizeX - BalloonBuilding.intSizeX) / 2),
                                                           z + ((CurrentBuilding.intSizeZ - BalloonBuilding.intSizeZ) / 2),
                                                           BalloonBuilding, 0, -1);
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
        private static void RemovePaths(BlockManager bm, int[,] intArea, int intBlockStart)
        {
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int z = 0; z < intArea.GetLength(1); z++)
                {
                    if (intArea[x, z] == 1)
                    {
                        BlockShapes.MakeBlock(intBlockStart + x, 63, intBlockStart + z, City.GroundBlockID, City.GroundBlockData);
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
                for (int a = City.PathExtends + 1; a <= (City.MapLength / 2) - (City.EdgeLength + 16); a += 8)
                {
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) - a, (City.MapLength / 2) - (City.PathExtends + 1),
                                                   (City.MapLength / 2) - a, (City.MapLength / 2) - City.PathExtends);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) - a, (City.MapLength / 2) + (City.PathExtends + 1),
                                                   (City.MapLength / 2) - a, (City.MapLength / 2) + City.PathExtends);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) + a, (City.MapLength / 2) - (City.PathExtends + 1),
                                                   (City.MapLength / 2) + a, (City.MapLength / 2) - City.PathExtends);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) + a, (City.MapLength / 2) + (City.PathExtends + 1),
                                                   (City.MapLength / 2) + a, (City.MapLength / 2) + City.PathExtends);

                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) - (City.PathExtends + 1), (City.MapLength / 2) - a,
                                                   (City.MapLength / 2) - City.PathExtends, (City.MapLength / 2) - a);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) + (City.PathExtends + 1), (City.MapLength / 2) - a,
                                                   (City.MapLength / 2) + City.PathExtends, (City.MapLength / 2) - a);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) - (City.PathExtends + 1), (City.MapLength / 2) + a,
                                                   (City.MapLength / 2) - City.PathExtends, (City.MapLength / 2) + a);
                    MakeStreetLight(bm, slCurrent, (City.MapLength / 2) + (City.PathExtends + 1), (City.MapLength / 2) + a,
                                                   (City.MapLength / 2) + City.PathExtends, (City.MapLength / 2) + a);
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
        private static void JoinPathsToRoad(BlockManager bm)
        {
            int intBlockStart = City.EdgeLength + 13;
            int intHalfPlotBlocksLength = (((City.PathExtends / 2) + City.MapLength) - (2 * intBlockStart)) / 2;
            for (int a = 0; a <= 2 * intHalfPlotBlocksLength; a++)
            {
                for (int b = -(City.PathExtends + 1); b <= City.PathExtends + 1; b += (City.PathExtends + 1) * 2)
                {
                    if (bm.GetID(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + b + Math.Sign(b)) == City.PathBlockID &&
                        bm.GetData(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + (b - Math.Sign(b))) == City.PathBlockData &&
                        (bm.GetID(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + b) == City.GroundBlockID || 
                         bm.GetID(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + b) == City.PathBlockID) &&
                        bm.GetID(intBlockStart + a, 64, intBlockStart + intHalfPlotBlocksLength + b) == BlockInfo.Air.ID)
                    {
                        bm.SetID(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + b, City.PathBlockID);
                        bm.SetData(intBlockStart + a, 63, intBlockStart + intHalfPlotBlocksLength + b, City.PathBlockData);
                        bm.SetID(intBlockStart + a, 64, intBlockStart + intHalfPlotBlocksLength + b - Math.Sign(b), BlockInfo.Air.ID);
                        bm.SetData(intBlockStart + a, 64, intBlockStart + intHalfPlotBlocksLength + b - Math.Sign(b), 0);
                    }
                    if (bm.GetID(intBlockStart + intHalfPlotBlocksLength + b + Math.Sign(b), 63, intBlockStart + a) == City.PathBlockID &&
                        bm.GetData(intBlockStart + intHalfPlotBlocksLength + (b - Math.Sign(b)), 63, intBlockStart + a) == City.PathBlockData &&
                        (bm.GetID(intBlockStart + intHalfPlotBlocksLength + b, 63, intBlockStart + a) == City.GroundBlockID ||
                         bm.GetID(intBlockStart + intHalfPlotBlocksLength + b, 63, intBlockStart + a) == City.PathBlockID) &&
                        bm.GetID(intBlockStart + intHalfPlotBlocksLength + b, 64, intBlockStart + a) == BlockInfo.Air.ID)
                    {
                        bm.SetID(intBlockStart + intHalfPlotBlocksLength + b, 63, intBlockStart + a, City.PathBlockID);
                        bm.SetData(intBlockStart + intHalfPlotBlocksLength + b, 63, intBlockStart + a, City.PathBlockData);
                        bm.SetID(intBlockStart + intHalfPlotBlocksLength + b - Math.Sign(b), 64, intBlockStart + a, BlockInfo.Air.ID);
                        bm.SetData(intBlockStart + intHalfPlotBlocksLength + b - Math.Sign(b), 64, intBlockStart + a, 0);
                    }
                }
            }
        }
    }
}
