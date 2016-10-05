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

namespace Mace
{
    public class City
    {
        public static string Name;
        public static string CityNamePrefix;
        public static string CityNamePrefixFilename;
        public static string CityNameSuffixFilename;
        public static string ThemeName;
        public static int ID;

        // types
        public static string MoatType;
        public static string CityEmblemType;
        public static string OutsideLightType;
        public static string TowersAdditionType;
        public static string StreetLightType;

        // inclusions
        public static bool HasFarms;
        public static bool HasMoat;
        public static bool HasWalls;
        public static bool HasDrawbridges;
        public static bool HasGuardTowers;
        public static bool HasBuildings;
        public static bool HasPaths;
        public static bool HasMineshaft;
        public static bool HasEmblems;
        public static bool HasOutsideLights;
        public static bool HasGuardTowersAddition;
        public static bool HasFlowers;
        public static bool HasSkyFeature;
        public static bool HasStreetLights;
        public static bool HasValuableBlocks;
        public static bool HasItemsInChests;
        public static bool HasTorchesOnWalkways;

        // lengths
        public static int CityLength;
        public static int EdgeLength;
        public static int FarmLength;
        public static int MapLength;

        // blocks
        public static int GroundBlockID;
        public static int GroundBlockData;
        public static int WallMaterialID;
        public static int WallMaterialData;

        // paths
        public static string PathType;
        public static int PathExtends;
        public static int PathBlockID;
        public static int PathBlockData;
        public static int PathAlternativeBlockID;
        public static int PathAlternativeBlockData;

        // misc
        public static int FlowerSpawnPercent;
        public static string NPCs;

        public static void ClearAllCityData()
        {
            Name = String.Empty;
            CityNamePrefix = String.Empty;
            CityNamePrefixFilename = String.Empty;
            CityNameSuffixFilename = String.Empty;
            ThemeName = String.Empty;
            ID = 0;

            // types
            MoatType = String.Empty;
            CityEmblemType = String.Empty;
            OutsideLightType = String.Empty;
            TowersAdditionType = String.Empty;
            StreetLightType = String.Empty;

            // inclusions
            HasFarms = false;
            HasMoat = false;
            HasWalls = false;
            HasDrawbridges = false;
            HasGuardTowers = false;
            HasBuildings = false;
            HasPaths = false;
            HasMineshaft = false;
            HasEmblems = false;
            HasOutsideLights = false;
            HasGuardTowersAddition = false;
            HasFlowers = false;
            HasSkyFeature = false;
            HasStreetLights = false;
            HasValuableBlocks = false;
            HasItemsInChests = false;
            HasTorchesOnWalkways = false;

            // lengths
            CityLength = 0;
            EdgeLength = 0;
            FarmLength = 0;
            MapLength = 0;

            // blocks
            GroundBlockID = 0;
            GroundBlockData = 0;
            WallMaterialID = 0;
            WallMaterialData = 0;

            // paths
            PathType = String.Empty;
            PathExtends = 1;
            PathBlockID = 0;
            PathBlockData = 0;
            PathAlternativeBlockID = 0;
            PathAlternativeBlockData = 0;

            // misc
            FlowerSpawnPercent = 0;
            NPCs = String.Empty;
        }
    }
}
