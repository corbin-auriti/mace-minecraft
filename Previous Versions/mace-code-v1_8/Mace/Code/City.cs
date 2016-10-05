using System;

namespace Mace
{
    public class City
    {
        public static string Name;
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
        public static bool HasTowersAddition;
        public static bool HasFlowers;
        public static bool HasSkyFeature;
        public static bool HasStreetLights;
        public static bool HasGhostdancerSpawners;
        public static bool HasValuableBlocks;
        public static bool HasItemsInChests;

        // lengths
        public static int CityLength;
        public static int FarmLength;
        public static int MapLength;

        // blocks
        public static int GroundBlockID;
        public static int GroundBlockData;
        public static int WallMaterialID;
        public static int WallMaterialData;
        public static int PathBlockID;
        public static int PathBlockData;

        // misc
        public static int FlowerSpawnPercent;

        public static void ClearAllCityData()
        {
            Name = String.Empty;
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
            HasTowersAddition = false;
            HasFlowers = false;
            HasSkyFeature = false;
            HasStreetLights = false;
            HasGhostdancerSpawners = false;
            HasValuableBlocks = false;
            HasItemsInChests = false;

            // lengths
            CityLength = 0;
            FarmLength = 0;
            MapLength = 0;

            // blocks
            GroundBlockID = 0;
            GroundBlockData = 0;
            WallMaterialID = 0;
            WallMaterialData = 0;
            PathBlockID = 0;
            PathBlockData = 0;

            // misc
            FlowerSpawnPercent = 0;
        }
    }
}
