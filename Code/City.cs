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
using Substrate;

namespace Mace
{
    public class City
    {
        public static string name;
        public static string cityNamePrefix;
        public static string cityNamePrefixFilename;
        public static string cityNameSuffixFilename;
        public static string themeName;
        public static int id;

        // types
        public static string moatType;
        public static string cityEmblemType;
        public static string outsideLightType;
        public static string towersAdditionType;
        public static string streetLightType;

        // inclusions
        public static bool hasFarms;
        public static bool hasMoat;
        public static bool hasWalls;
        public static bool hasDrawbridges;
        public static bool hasGuardTowers;
        public static bool hasBuildings;
        public static bool hasPaths;
        public static bool hasMainStreets;
        public static bool hasMineshaft;
        public static bool HasEmblems;
        public static bool hasOutsideLights;
        public static bool hasGuardTowersAddition;
        public static bool hasFlowers;
        public static bool hasSkyFeature;
        public static bool hasStreetLights;
        public static bool hasValuableBlocks;
        public static bool hasItemsInChests;
        public static bool hasTorchesOnWalkways;

        // lengths
        public static int cityLength;
        public static int edgeLength;
        public static int farmLength;
        public static int mapLength;

        // blocks
        public static int groundBlockID;
        public static int groundBlockData;
        public static int wallMaterialID;
        public static int wallMaterialData;

        // paths
        public static string pathType;
        public static int pathExtends;
        public static int pathBlockID;
        public static int pathBlockData;
        public static int pathAlternativeBlockID;
        public static int pathAlternativeBlockData;

        // misc
        public static int flowerSpawnPercent;
        public static string npcs;
        public static int biome;
        public static bool stop;
        public static int[] buildingFrequency;
        public static int[] farmBuildingFrequency;

        public static void ClearAllCityData()
        {
            name = String.Empty;
            cityNamePrefix = String.Empty;
            cityNamePrefixFilename = String.Empty;
            cityNameSuffixFilename = String.Empty;
            themeName = String.Empty;
            id = 0;

            // types
            moatType = String.Empty;
            cityEmblemType = String.Empty;
            outsideLightType = String.Empty;
            towersAdditionType = String.Empty;
            streetLightType = String.Empty;

            // inclusions
            hasFarms = false;
            hasMoat = false;
            hasWalls = false;
            hasDrawbridges = false;
            hasGuardTowers = false;
            hasBuildings = false;
            hasPaths = false;
            hasMainStreets = false;
            hasMineshaft = false;
            HasEmblems = false;
            hasOutsideLights = false;
            hasGuardTowersAddition = false;
            hasFlowers = false;
            hasSkyFeature = false;
            hasStreetLights = false;
            hasValuableBlocks = false;
            hasItemsInChests = false;
            hasTorchesOnWalkways = false;

            // lengths
            cityLength = 0;
            edgeLength = 0;
            farmLength = 0;
            mapLength = 0;

            // blocks
            groundBlockID = 0;
            groundBlockData = 0;
            wallMaterialID = 0;
            wallMaterialData = 0;

            // paths
            pathType = String.Empty;
            pathExtends = 1;
            pathBlockID = 0;
            pathBlockData = 0;
            pathAlternativeBlockID = 0;
            pathAlternativeBlockData = 0;

            // misc
            flowerSpawnPercent = 0;
            npcs = String.Empty;
            biome = BiomeType.Plains;
            stop = false;
            buildingFrequency = null;
            farmBuildingFrequency = null;
        }
    }
}
