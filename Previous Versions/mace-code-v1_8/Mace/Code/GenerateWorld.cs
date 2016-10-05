using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Substrate;
using Substrate.TileEntities;

namespace Mace
{
    static class GenerateWorld
    {
        struct WorldCity
        {
            public string ThemeName;
            public int x;
            public int z;
            public int ChunkLength;
        }
        static WorldCity[] worldCities;
        public static List<string> lstCityNames = new List<string>();

        // todo low: long code is long
        static public void Generate(frmMace frmLogForm, string UserWorldName, string strWorldSeed,
                                    string strWorldType, bool booWorldMapFeatures, int TotalCities, string[] strCheckedThemes,
                                    int ChunksBetweenCities, string strSpawnPoint)
        {
            worldCities = new WorldCity[TotalCities];
            lstCityNames.Clear();

            RandomHelper.SetRandomSeed();

            #region create minecraft world directory from a random unused city name
            string strFolder = String.Empty, strWorldName = String.Empty;

            UserWorldName = Utils.SafeFilename(UserWorldName);
            if (UserWorldName.Trim().Length == 0)
            {
                UserWorldName = "random";
            }

            if (UserWorldName.ToLower().Trim() != "random")
            {
                if (Directory.Exists(Utils.GetMinecraftSavesDirectory(UserWorldName)))
                {
                    if (MessageBox.Show("A world called \"" + UserWorldName + "\" already exists. " +
                                    "Would you like to use a random name instead?", "World already exists",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        frmLogForm.UpdateLog("Cancelled, because a world with this name already exists.", false, false);
                        return;
                    }
                }
                else
                {
                    strWorldName = UserWorldName;
                    strFolder = Utils.GetMinecraftSavesDirectory(strWorldName);
                }
            }
            if (strWorldName.Length == 0)
            {
                strWorldName = Utils.GenerateWorldName();
                strFolder = Utils.GetMinecraftSavesDirectory(strWorldName);
            }
            Directory.CreateDirectory(strFolder);
            frmLogForm.UpdateLog("World name: " + strWorldName, false, true);
            #endregion
            
            #region get handles to world, chunk manager and block manager
            BetaWorld worldDest = BetaWorld.Create(@strFolder);
            worldDest.Level.LevelName = "Creating. Don't open until Mace is finished.";
            BetaChunkManager cmDest = worldDest.GetChunkManager();
            BlockManager bmDest = worldDest.GetBlockManager();
            bmDest.AutoLight = false;
            #endregion

            #region Determine themes
            // "how does this work, robson?"
            // well, I'm glad you asked!
            // we keep selecting a random unused checked theme, until they've all been used once.
            // after that, all other cities will have a random checked theme
            strCheckedThemes = RandomHelper.ShuffleArray(strCheckedThemes);
            
            for (int CurrentCityID = 0; CurrentCityID < TotalCities; CurrentCityID++)
            {
                if (CurrentCityID <= strCheckedThemes.GetUpperBound(0))
                {
                    worldCities[CurrentCityID].ThemeName = strCheckedThemes[CurrentCityID];
                }
                else
                {
                    worldCities[CurrentCityID].ThemeName = RandomHelper.RandomString(strCheckedThemes);
                }
                Debug.WriteLine(worldCities[CurrentCityID].ThemeName);
                City.ThemeName = worldCities[CurrentCityID].ThemeName;
                worldCities[CurrentCityID].ChunkLength = GetThemeRandomXMLElementNumber("options", "city_size");
            }
            #endregion

            GenerateCityLocations(TotalCities, ChunksBetweenCities);

            int intRandomCity = RandomHelper.Next(TotalCities);

            for (int CurrentCityID = 0; CurrentCityID < TotalCities; CurrentCityID++)
            {
                MakeCitySettings(frmLogForm, worldCities[CurrentCityID].ThemeName, CurrentCityID);
                GenerateCity.Generate(frmLogForm, worldDest, cmDest, bmDest, worldCities[CurrentCityID].x, worldCities[CurrentCityID].z);
                #region set spawn point
                if (City.ID == intRandomCity)
                {
                    switch (strSpawnPoint)
                    {
                        case "Away from the cities":
                            worldDest.Level.Spawn = new SpawnPoint(0, 65, 0);
                            break;
                        case "Inside a random city":
                            worldDest.Level.Spawn = new SpawnPoint(((worldCities[intRandomCity].x + 30) * 16) + (City.MapLength / 2),
                                                                   65,
                                                                   ((worldCities[intRandomCity].z + 30) * 16) + (City.MapLength / 2));
                            break;
                        case "Outside a random city":
                            if (City.HasFarms)
                            {
                                worldDest.Level.Spawn = new SpawnPoint(((worldCities[intRandomCity].x + 30) * 16) + (City.MapLength / 2),
                                                                       65,
                                                                       ((worldCities[intRandomCity].z + 30) * 16) + 20);
                            }
                            else
                            {
                                worldDest.Level.Spawn = new SpawnPoint(((worldCities[intRandomCity].x + 30) * 16) + (City.MapLength / 2),
                                                                       65,
                                                                       ((worldCities[intRandomCity].z + 30) * 16) + 2);
                            }
                            break;
                        default:
                            Debug.Fail("invalid spawn point");
                            break;
                    }
                    frmLogForm.UpdateLog("Spawn point set to " + worldDest.Level.Spawn.X + "," + worldDest.Level.Spawn.Y + "," + worldDest.Level.Spawn.Z, false, true);
                }
                #endregion

            }

            City.ID = TotalCities;
            frmLogForm.UpdateProgress(0);            
            
            #region weather
#if RELEASE
            frmLogForm.UpdateLog("Setting weather", false, true);
            worldDest.Level.Time = RandomHelper.Next(24000);
            if (RandomHelper.NextDouble() < 0.2)
            {
                frmLogForm.UpdateLog("Rain", false, true);
                worldDest.Level.IsRaining = true;
                // one-quarter to three-quarters of a day
                worldDest.Level.RainTime = RandomHelper.Next(6000, 18000);
                if (RandomHelper.NextDouble() < 0.25)
                {
                    frmLogForm.UpdateLog("Thunder", false, true);
                    worldDest.Level.IsThundering = true;
                    worldDest.Level.ThunderTime = worldDest.Level.RainTime;
                }
            }
#endif
            #endregion

#if DEBUG
                MakeHelperChest(bmDest, worldDest.Level.Spawn.X + 2, worldDest.Level.Spawn.Y, worldDest.Level.Spawn.Z + 2);
#endif

            #region lighting
            frmLogForm.UpdateLog("\nCreating world lighting data", false, false);
            Chunks.ResetLighting(worldDest, cmDest, frmLogForm);
            frmLogForm.UpdateProgress(0.95);
            #endregion

            #region world details
            worldDest.Level.LevelName = strWorldName;
            frmLogForm.UpdateLog("Setting world type: " + strWorldType, false, true);
            switch (strWorldType.ToLower())
            {
                case "creative":
                    worldDest.Level.GameType = GameType.CREATIVE;
                    break;
                case "survival":
                    worldDest.Level.GameType = GameType.SURVIVAL;
                    break;
                case "hardcore":
                    worldDest.Level.GameType = GameType.SURVIVAL;
                    worldDest.Level.UseHardcoreMode = true;
                    break;
                default:
                    Debug.Fail("Invalidate world type selected.");
                    break;
            }
            frmLogForm.UpdateLog("World map features: " + booWorldMapFeatures.ToString(), false, true);
            worldDest.Level.UseMapFeatures = booWorldMapFeatures;
            if (strWorldSeed != String.Empty)
            {                
                worldDest.Level.RandomSeed = Utils.JavaStringHashCode(strWorldSeed);
                frmLogForm.UpdateLog("Specified world seed: " + worldDest.Level.RandomSeed, false, true);
            }
            else
            {                
                worldDest.Level.RandomSeed = RandomHelper.Next();
                frmLogForm.UpdateLog("Random world seed: " + worldDest.Level.RandomSeed, false, true);
            }
            worldDest.Level.LastPlayed = (DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 10000;
            frmLogForm.UpdateLog("World time: " + worldDest.Level.LastPlayed, false, true);
            #endregion

            worldDest.Save();
            frmLogForm.UpdateProgress(1);

            frmLogForm.UpdateLog("\nCreated the " + strWorldName + "!", false, false);
            frmLogForm.UpdateLog("It'll be at the top of your MineCraft world list.", false, false);

            // todo low: export schematic
            #region export schematic
            //if (booExportSchematic)
            //{
            //    frmLogForm.UpdateLog("Creating schematic");
            //    AlphaBlockCollection abcExport = new AlphaBlockCollection(intMapLength, 128, intMapLength);
            //    for (int x = 0; x < intMapLength; x++)
            //    {
            //        for (int z = 0; z < intMapLength; z++)
            //        {
            //            for (int y = 0; y < 128; y++)
            //            {
            //                abcExport.SetBlock(x, y, z, bmDest.GetBlock(x, y, z));
            //            }
            //        }
            //    }
            //    Schematic CitySchematic = new Schematic(intMapLength, 128, intMapLength);
            //    CitySchematic.Blocks = abcExport;
            //    CitySchematic.Export(Utils.GetMinecraftSavesDirectory(strCityName) + "\\" + strCityName + ".schematic");
            //}
            #endregion
        }
        private static void MakeCitySettings(frmMace frmLogForm, string strThemeName, int CityID)
        {
            City.ClearAllCityData();

            City.ID = CityID;

            City.ThemeName = strThemeName;

            City.CityNamePrefixFilename = GetThemeRandomXMLElement("options", "city_prefix_file");
            City.CityNameSuffixFilename = GetThemeRandomXMLElement("options", "city_suffix_file");

            City.HasFarms = GetThemeRandomXMLElementBoolean("include", "farms");
            City.HasMoat = GetThemeRandomXMLElementBoolean("include", "moat");
            City.HasWalls = GetThemeRandomXMLElementBoolean("include", "walls");
            City.HasDrawbridges = GetThemeRandomXMLElementBoolean("include", "drawbridges");
            if (City.HasWalls)
            {
                City.HasGuardTowers = GetThemeRandomXMLElementBoolean("include", "guard_towers");
            }
            City.HasBuildings = GetThemeRandomXMLElementBoolean("include", "buildings");
            City.HasPaths = GetThemeRandomXMLElementBoolean("include", "paths");
            City.HasMineshaft = GetThemeRandomXMLElementBoolean("include", "mineshaft");
            City.HasEmblems = GetThemeRandomXMLElementBoolean("include", "emblems");
            City.HasOutsideLights = GetThemeRandomXMLElementBoolean("include", "outside_lights");
            City.HasTowersAddition = GetThemeRandomXMLElementBoolean("include", "tower_addition");
            City.HasFlowers = GetThemeRandomXMLElementBoolean("include", "flowers");
            City.HasSkyFeature = GetThemeRandomXMLElementBoolean("include", "sky_feature");
            City.HasStreetLights = GetThemeRandomXMLElementBoolean("include", "street_lights");

            City.HasGhostdancerSpawners = frmLogForm.chkIncludeSpawners.Checked;
            City.HasValuableBlocks = frmLogForm.chkValuableBlocks.Checked;
            City.HasItemsInChests = frmLogForm.chkItemsInChests.Checked;

            City.CityLength = Math.Max(5, GetThemeRandomXMLElementNumber("options", "city_size"));
            if (City.CityLength % 2 == 0)
            {
                City.CityLength++;
            }

            string strValue = String.Empty;
            strValue = GetThemeRandomXMLElement("options", "ground_block");
            City.GroundBlockID = Convert.ToInt32(strValue.Split('_')[0]);
            City.GroundBlockData = 0;
            if (strValue.Contains("_"))
            {
                City.GroundBlockData = Convert.ToInt32(strValue.Split('_')[1]);
            }

            strValue = GetThemeRandomXMLElement("options", "path");
            City.PathBlockID = Convert.ToInt32(strValue.Split('_')[0]);
            City.PathBlockData = 0;
            if (strValue.Contains("_"))
            {
                City.PathBlockData = Convert.ToInt32(strValue.Split('_')[1]);
            }

            if (City.HasMoat)
            {
                City.MoatType = GetThemeRandomXMLElement("options", "moat");
            }
            if (City.HasEmblems)
            {
                City.CityEmblemType = GetThemeRandomXMLElement("options", "emblem");
            }
            if (City.HasOutsideLights)
            {
                City.OutsideLightType = GetThemeRandomXMLElement("options", "outside_lights");
            }
            if (City.HasGuardTowers)
            {
                City.TowersAdditionType = GetThemeRandomXMLElement("options", "tower_addition");
            }
            if (City.HasFlowers)
            {
                City.FlowerSpawnPercent = GetThemeRandomXMLElementNumber("options", "flower_percent");
            }
            if (City.HasStreetLights)
            {
                City.StreetLightType = GetThemeRandomXMLElement("options", "street_lights");
            }
            if (City.HasWalls)
            {
                strValue = GetThemeRandomXMLElement("options", "wall_material");
                City.WallMaterialID = Convert.ToInt32(strValue.Split('_')[0]);
                City.WallMaterialData = 0;
                if (strValue.Contains("_"))
                {
                    City.WallMaterialData = Convert.ToInt32(strValue.Split('_')[1]);
                }
            }
        }
        private static bool GetThemeRandomXMLElementBoolean(string strSection, string strKey)
        {
            return Utils.IsAffirmative(Utils.RandomValueFromXMLElement(Path.Combine("Resources", "Themes", City.ThemeName + ".xml"), strSection, strKey));
        }
        private static int GetThemeRandomXMLElementNumber(string strSection, string strKey)
        {
            return Convert.ToInt32(Utils.RandomValueFromXMLElement(Path.Combine("Resources", "Themes", City.ThemeName + ".xml"), strSection, strKey));
        }
        private static string GetThemeRandomXMLElement(string strSection, string strKey)
        {
            return Utils.RandomValueFromXMLElement(Path.Combine("Resources", "Themes", City.ThemeName + ".xml"), strSection, strKey);
        }
        private static void MakeHelperChest(BlockManager bm, int x, int y, int z)
        {
            TileEntityChest tec = new TileEntityChest();
            tec.Items[0] = BlockHelper.MakeItem(ItemInfo.DiamondSword.ID, 1);
            tec.Items[1] = BlockHelper.MakeItem(ItemInfo.DiamondPickaxe.ID, 1);
            tec.Items[2] = BlockHelper.MakeItem(ItemInfo.DiamondShovel.ID, 1);
            tec.Items[3] = BlockHelper.MakeItem(ItemInfo.DiamondAxe.ID, 1);
            tec.Items[4] = BlockHelper.MakeItem(BlockInfo.Ladder.ID, 64);
            tec.Items[5] = BlockHelper.MakeItem(BlockInfo.Dirt.ID, 64);
            tec.Items[6] = BlockHelper.MakeItem(BlockInfo.Sand.ID, 64);
            tec.Items[7] = BlockHelper.MakeItem(BlockInfo.CraftTable.ID, 64);
            tec.Items[8] = BlockHelper.MakeItem(BlockInfo.Furnace.ID, 64);
            tec.Items[9] = BlockHelper.MakeItem(ItemInfo.Bread.ID, 64);
            tec.Items[10] = BlockHelper.MakeItem(BlockInfo.Torch.ID, 64);
            tec.Items[11] = BlockHelper.MakeItem(BlockInfo.Stone.ID, 64);
            tec.Items[12] = BlockHelper.MakeItem(BlockInfo.Chest.ID, 64);
            tec.Items[13] = BlockHelper.MakeItem(BlockInfo.Glass.ID, 64);
            tec.Items[14] = BlockHelper.MakeItem(BlockInfo.Wood.ID, 64);
            tec.Items[15] = BlockHelper.MakeItem(ItemInfo.Cookie.ID, 64);
            tec.Items[16] = BlockHelper.MakeItem(ItemInfo.RedstoneDust.ID, 64);
            tec.Items[17] = BlockHelper.MakeItem(BlockInfo.IronBlock.ID, 64);
            tec.Items[18] = BlockHelper.MakeItem(BlockInfo.DiamondBlock.ID, 64);
            tec.Items[19] = BlockHelper.MakeItem(BlockInfo.GoldBlock.ID, 64);
            bm.SetID(x, y, z, BlockInfo.Chest.ID);
            bm.SetTileEntity(x, y, z, tec);
        }

        static void GenerateCityLocations(int TotalCities, int ChunksBetweenCities)
        {
            int MapChunksLength = 50;

            for (int CityID = 0; CityID < TotalCities; CityID++)
            {
                bool IsValidCity = false;
                int Attempts = 0;
                do
                {
                    worldCities[CityID].x = RandomHelper.Next(2, (MapChunksLength - worldCities[CityID].ChunkLength) - 1);
                    worldCities[CityID].z = RandomHelper.Next(2, (MapChunksLength - worldCities[CityID].ChunkLength) - 1);
                    IsValidCity = true;
                    for (int CheckCityID = 0; CheckCityID < CityID && IsValidCity; CheckCityID++)
                    {
                        IsValidCity = !CitiesIntersect(worldCities[CityID], worldCities[CheckCityID], ChunksBetweenCities);
                    }
                    if (++Attempts > 50)
                    {
                        MapChunksLength += 10;
                        Attempts = 0;
                    }
                } while (!IsValidCity);
            }
        }
        static bool CitiesIntersect(WorldCity city1, WorldCity city2, int ChunksOfPadding)
        {
            return new Rectangle(city1.x, city1.z, city1.ChunkLength, city1.ChunkLength).IntersectsWith(
                   new Rectangle(city2.x - ChunksOfPadding, city2.z - ChunksOfPadding,
                                 city2.ChunkLength + (2 * ChunksOfPadding), city2.ChunkLength + (2 * ChunksOfPadding)));
        }
    
    }
}
