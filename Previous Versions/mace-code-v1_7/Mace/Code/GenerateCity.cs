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
    along with this program.  If not, see <http://www.gnu.org/licenses/>
*/

using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Substrate;
using Substrate.Entities;
using Substrate.TileEntities;
using Substrate.ImportExport;
using Substrate.Core;

namespace Mace
{
    static class GenerateCity
    {

        public static void CropMaceWorld(frmMace frmLogForm)
        {
            // thank you to Surrogard <surrogard@googlemail.com> for providing a linux friendly version of this code:            
            Directory.CreateDirectory(Utils.GetMinecraftSavesDirectory("macecopy"));
            BetaWorld bwCopy = BetaWorld.Create(Utils.GetMinecraftSavesDirectory("macecopy"));
            BetaChunkManager cmCopy = bwCopy.GetChunkManager();
            BetaWorld bwCrop = BetaWorld.Open(Utils.GetMinecraftSavesDirectory("mace"));
            BetaChunkManager cmCrop = bwCrop.GetChunkManager();

            foreach (ChunkRef chunk in cmCrop)
            {
                if (chunk.X >= 0 && chunk.X <= 7 && chunk.Z >= 0 && chunk.Z <= 10)
                {
                    Debug.WriteLine("Copying chunk " + chunk.X + "," + chunk.Z);
                    cmCopy.SetChunk(chunk.X, chunk.Z, chunk.GetChunkRef());
                }
            }
            cmCopy.Save();
            bwCopy.Save();
        }

        public static void Generate(frmMace frmLogForm, string strUserCityName,
                                    bool booIncludeFarms, bool booIncludeMoat, bool booIncludeWalls,
                                    bool booIncludeDrawbridges, bool booIncludeGuardTowers, bool booIncludeBuildings,
                                    bool booIncludePaths, bool booIncludeMineshaft, bool booIncludeItemsInChests,
                                    bool booIncludeValuableBlocks, bool booIncludeGhostdancerSpawners,
                                    string strCitySize, string strMoatType, string strCityEmblem,
                                    string strOutsideLights, string strTowerAddition, string strWallMaterial,
                                    string strCitySeed, string strWorldSeed, bool booExportSchematic)
        {

            #region seed the random number generators
            int intCitySeed, intWorldSeed;
            Random randSeeds = new Random();
            if (strCitySeed.Length == 0)
            {
                intCitySeed = randSeeds.Next();
                frmLogForm.UpdateLog("Random city seed: " + intCitySeed);
            }
            else
            {
                intCitySeed = JavaStringHashCode(strCitySeed);
                frmLogForm.UpdateLog("Random city seed: " + strCitySeed);
            }
            if (strWorldSeed.Length == 0)
            {
                intWorldSeed = randSeeds.Next();
                frmLogForm.UpdateLog("Random world seed: " + intWorldSeed);
            }
            else
            {
                intWorldSeed = JavaStringHashCode(strWorldSeed);
                frmLogForm.UpdateLog("Random world seed: " + strWorldSeed);
            }
            RandomHelper.SetSeed(intCitySeed);
            #endregion

            #region create minecraft world directory from a random unused city name
            string strFolder = String.Empty, strCityName = String.Empty;

            strUserCityName = SafeFilename(strUserCityName);
            if (strUserCityName.ToLower().Trim().Length == 0)
            {
                strUserCityName = "random";
            }

            if (strUserCityName.ToLower().Trim() != "random")
            {
                if (Directory.Exists(Utils.GetMinecraftSavesDirectory(strUserCityName)))
                {

                    if (MessageBox.Show("A world called \"" + strUserCityName + "\" already exists. " +
                                    "Would you like to use a random name instead?", "World already exists",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        frmLogForm.UpdateLog("Cancelled, because a world with this name already exists.");
                        return;
                    }
                }
                else
                {
                    strCityName = strUserCityName;
                    strFolder = Utils.GetMinecraftSavesDirectory(strCityName);
                }
            }
            if (strCityName.Length == 0)
            {
                string strStart, strEnd;
                do
                {
                    strStart = RandomHelper.RandomFileLine(Path.Combine("Resources", "CityAdj.txt"));
                    strEnd = RandomHelper.RandomFileLine(Path.Combine("Resources", "CityNoun.txt"));
                    strCityName = "City of " + strStart + strEnd;
                    strFolder = Utils.GetMinecraftSavesDirectory(strCityName);
                } while (strStart.ToLower().Trim() == strEnd.ToLower().Trim() || Directory.Exists(strFolder) || (strStart + strEnd).Length > 14);
            }
            Directory.CreateDirectory(strFolder);
            RandomHelper.SetSeed(intCitySeed);
            #endregion            

            #region get handles to world, chunk manager and block manager
            BetaWorld worldDest = BetaWorld.Create(@strFolder);
            BetaChunkManager cmDest = worldDest.GetChunkManager();
            BlockManager bmDest = worldDest.GetBlockManager();
            bmDest.AutoLight = false;
            #endregion

            #region determine block sizes
            // first we set the city size by chunks
            int intCitySize = 12;
            switch (strCitySize)
            {
                case "Random":
                    intCitySize = RandomHelper.Next(8, 16);
                    break;
                case "Very small":
                    intCitySize = 5;
                    break;
                case "Small":
                    intCitySize = 8;
                    break;
                case "Medium":
                    intCitySize = 12;
                    break;
                case "Large":
                    intCitySize = 16;
                    break;
                case "Very large":
                    intCitySize = 25;
                    break;
                default:
                    Debug.Fail("Invalid switch result");
                    break;
            }
            // then we multiply by 16, because that's the x and z of a chunk
            intCitySize *= 16;
            int intFarmLength = booIncludeFarms ? 32 : 8;
            int intMapLength = intCitySize + (intFarmLength * 2);
            #endregion

            #region setup classes
            BlockShapes.SetupClass(bmDest, intMapLength);
            BlockHelper.SetupClass(bmDest, intMapLength);
            if (!SourceWorld.SetupClass(worldDest, booIncludeItemsInChests, booIncludeGhostdancerSpawners))
            {
                return;
            }
            NoticeBoard.SetupClass(intCitySeed, intWorldSeed);
            #endregion

            #region determine random options
            // ensure selected options take priority, but don't set things on fire
            if (strTowerAddition == "Random")
            {
                strTowerAddition = RandomHelper.RandomString("Fire beacon", "Flag");
            }
            if (strWallMaterial.StartsWith("Wood"))
            {
                if (strMoatType == "Lava" || strMoatType == "Fire" || strMoatType == "Random")
                {
                    strMoatType = RandomHelper.RandomString("Drop to Bedrock", "Cactus", "Water");
                }
                strOutsideLights = "Torches";
            }
            if (strWallMaterial == "Random")
            {
                if (strMoatType == "Lava" || strMoatType == "Fire" || strOutsideLights == "Fire")
                {
                    strWallMaterial = RandomHelper.RandomString("Brick", "Cobblestone", "Sandstone", "Stone");
                }
                else
                {
                    strWallMaterial = RandomHelper.RandomString("Brick", "Cobblestone", "Sandstone",
                                                                "Stone", "Wood Planks");
                    if (strWallMaterial.StartsWith("Wood"))
                    {
                        if (strMoatType == "Random")
                        {
                            strMoatType = RandomHelper.RandomString("Drop to Bedrock", "Cactus", "Water");
                        }
                        strOutsideLights = "Torches";
                    }
                }
            }
            if (strOutsideLights == "Random")
            {
                strOutsideLights = RandomHelper.RandomString("Fire", "Torches");
            }
            if (strMoatType == "Random")
            {
                int intRand = RandomHelper.Next(100);
                if (intRand >= 90)
                {
                    strMoatType = "Drop to Bedrock";
                }
                else if (intRand >= 80)
                {
                    strMoatType = "Cactus";
                }
                else if (intRand >= 50)
                {
                    strMoatType = "Lava";
                }
                else if (intRand >= 40)
                {
                    strMoatType = "Fire";
                }
                else
                {
                    strMoatType = "Water";
                }
            }

            int intWallMaterial = BlockType.STONE;
            switch (strWallMaterial)
            {
                case "Brick":
                    intWallMaterial = BlockType.BRICK_BLOCK;
                    break;
                case "Cobblestone":
                    intWallMaterial = BlockType.COBBLESTONE;
                    break;
                case "Sandstone":
                    intWallMaterial = BlockType.SANDSTONE;
                    break;
                case "Stone":
                    intWallMaterial = BlockType.STONE;
                    break;
                case "Wood Planks":
                    intWallMaterial = BlockType.WOOD_PLANK;
                    break;
                case "Wood Logs":
                    intWallMaterial = BlockType.WOOD;
                    break;
                case "Bedrock":
                    intWallMaterial = BlockType.BEDROCK;
                    break;
                case "Mossy Cobblestone":
                    intWallMaterial = BlockType.MOSS_STONE;
                    break;
                case "Netherrack":
                    intWallMaterial = BlockType.NETHERRACK;
                    break;
                case "Glass":
                    intWallMaterial = BlockType.GLASS;
                    break;
                case "Ice":
                    intWallMaterial = BlockType.ICE;
                    break;
                case "Snow":
                    intWallMaterial = BlockType.SNOW_BLOCK;
                    break;
                case "Glowstone":
                    intWallMaterial = BlockType.GLOWSTONE_BLOCK;
                    break;
                case "Dirt":
                    intWallMaterial = BlockType.DIRT;
                    break;
                case "Obsidian":
                    intWallMaterial = BlockType.OBSIDIAN;
                    break;
                case "Jack-o-Lantern":
                    intWallMaterial = BlockType.JACK_O_LANTERN;
                    break;
                case "Soul sand":
                    intWallMaterial = BlockType.SOUL_SAND;
                    break;
                case "Gold":
                    intWallMaterial = BlockType.GOLD_BLOCK;
                    break;
                case "Diamond":
                    intWallMaterial = BlockType.DIAMOND_BLOCK;
                    break;
                default:
                    Debug.Fail("Invalid switch result");
                    break;
            }
            #endregion

            #region make the city
            frmLogForm.UpdateLog("Creating underground terrain");
            Chunks.CreateInitialChunks(cmDest, intMapLength / 16, frmLogForm);
            frmLogForm.UpdateProgress(25);

            Buildings.structPoint spMineshaftEntrance = new Buildings.structPoint();

            if (booIncludeWalls)
            {
                frmLogForm.UpdateLog("Creating walls");
                Walls.MakeWalls(worldDest, intFarmLength, intMapLength, strCityEmblem, strOutsideLights, intWallMaterial);
            }
            frmLogForm.UpdateProgress(34);
            if (booIncludeBuildings || booIncludePaths)
            {
                frmLogForm.UpdateLog("Creating paths");
                int[,] intArea = Paths.MakePaths(worldDest, bmDest, intFarmLength, intMapLength, strCitySize,
                                                 booIncludeMineshaft);
                frmLogForm.UpdateProgress(36);
                if (booIncludeBuildings)
                {
                    frmLogForm.UpdateLog("Creating buildings");
                    spMineshaftEntrance = Buildings.MakeInsideCity(bmDest, worldDest, intArea, intFarmLength, intMapLength, booIncludePaths);
                    frmLogForm.UpdateProgress(45);
                    if (booIncludeMineshaft)
                    {
                        frmLogForm.UpdateLog("Creating mineshaft");
                        Mineshaft.MakeMineshaft(worldDest, bmDest, intFarmLength, intMapLength, spMineshaftEntrance);
                    }
                }
            }
            frmLogForm.UpdateProgress(51);

            if (booIncludeMoat)
            {
                frmLogForm.UpdateLog("Creating moat");
                Moat.MakeMoat(intFarmLength, intMapLength, strMoatType, booIncludeGuardTowers);
            }
            frmLogForm.UpdateProgress(52);

            if (booIncludeDrawbridges)
            {
                frmLogForm.UpdateLog("Creating drawbridges");
                Drawbridge.MakeDrawbridges(bmDest, intFarmLength, intMapLength, booIncludeMoat,
                                           booIncludeWalls, booIncludeItemsInChests, intWallMaterial, strMoatType, strCityName);
            }
            frmLogForm.UpdateProgress(53);

            if (booIncludeGuardTowers)
            {
                frmLogForm.UpdateLog("Creating guard towers");
                GuardTowers.MakeGuardTowers(bmDest, intFarmLength, intMapLength, booIncludeWalls,
                                            strOutsideLights, strTowerAddition, booIncludeItemsInChests, intWallMaterial);
            }
            frmLogForm.UpdateProgress(54);

            if (booIncludeFarms)
            {
                frmLogForm.UpdateLog("Creating farms");
                Farms.MakeFarms(worldDest, bmDest, intFarmLength, intMapLength);
            }
            frmLogForm.UpdateProgress(58);

            if (!booIncludeValuableBlocks)
            {
                cmDest.Save();
                worldDest.Save();
                Chunks.ReplaceValuableBlocks(worldDest, bmDest, intMapLength, intWallMaterial);
            }
            frmLogForm.UpdateProgress(60);
            Chunks.PositionRails(worldDest, bmDest, intMapLength);
            frmLogForm.UpdateProgress(62);
            #endregion

            #region world settings
            // spawn looking at one of the city entrances
            if (booIncludeFarms)
            {
                SpawnPoint spLevel = new SpawnPoint(7, 11, 13);
                worldDest.Level.Spawn = spLevel;
                worldDest.Level.Spawn = new SpawnPoint(intMapLength / 2, 64, intMapLength - (intFarmLength - 10));
            }
            else
            {
                worldDest.Level.Spawn = new SpawnPoint(intMapLength / 2, 64, intMapLength - (intFarmLength - 7));
            }            
            // spawn in the middle of the city
//#if DEBUG
            //worldDest.Level.Spawn = new SpawnPoint(intMapLength / 2, 64, intMapLength / 2);
//#endif
            if (strWorldSeed.Length > 0)
            {
                worldDest.Level.RandomSeed = intWorldSeed;
            }

#if RELEASE
            worldDest.Level.Time = RandomHelper.Next(24000);

            if (RandomHelper.NextDouble() < 0.15)
            {
                worldDest.Level.IsRaining = true;
                // one-quarter to three-quarters of a day
                worldDest.Level.RainTime = RandomHelper.Next(6000, 18000);
                if (RandomHelper.NextDouble() < 0.25)
                {
                    worldDest.Level.IsThundering = true;
                    worldDest.Level.ThunderTime = worldDest.Level.RainTime;
                }
            }
#endif
            #endregion

#if DEBUG
            MakeHelperChest(bmDest, worldDest.Level.Spawn.X + 2, worldDest.Level.Spawn.Y, worldDest.Level.Spawn.Z + 2);
#endif

            frmLogForm.UpdateLog("Creating lighting data");
            Chunks.ResetLighting(worldDest, cmDest, frmLogForm, (int)Math.Pow(intMapLength / 16, 2));

            worldDest.Level.LevelName = strCityName;
            worldDest.Save();

            if (booExportSchematic)
            {
                frmLogForm.UpdateLog("Creating schematic");
                AlphaBlockCollection abcExport = new AlphaBlockCollection(intMapLength, 128, intMapLength);
                for (int x = 0; x < intMapLength; x++)
                {
                    for (int z = 0; z < intMapLength; z++)
                    {
                        for (int y = 0; y < 128; y++)
                        {
                            abcExport.SetBlock(x, y, z, bmDest.GetBlock(x, y, z));
                        }
                    }
                }
                Schematic CitySchematic = new Schematic(intMapLength, 128, intMapLength);
                CitySchematic.Blocks = abcExport;
                CitySchematic.Export(Utils.GetMinecraftSavesDirectory(strCityName) + "\\" + strCityName + ".schematic");
            }

            frmLogForm.UpdateLog("\r\nCreated the " + strCityName + "!");
            frmLogForm.UpdateLog("It'll be at the end of your MineCraft world list.");
        }
        static string SafeFilename(string strFilename)
        {
            string strUnsafe = @"*/\:?<>|" + '"';
            for (int a = 0; a < strUnsafe.Length; a++)
            {
                strFilename = strFilename.Replace(strUnsafe.Substring(a, 1), String.Empty);
            }
            return strFilename;
        }
        private static void MakeHelperChest(BlockManager bm, int x, int y, int z)
        {
            TileEntityChest tec = new TileEntityChest();
            tec.Items[0] = BlockHelper.MakeItem(ItemInfo.DiamondSword.ID, 1);
            tec.Items[1] = BlockHelper.MakeItem(ItemInfo.DiamondPickaxe.ID, 1);
            tec.Items[2] = BlockHelper.MakeItem(ItemInfo.DiamondShovel.ID, 1);
            tec.Items[3] = BlockHelper.MakeItem(ItemInfo.DiamondAxe.ID, 1);
            tec.Items[4] = BlockHelper.MakeItem(BlockType.LADDER, 64);
            tec.Items[5] = BlockHelper.MakeItem(BlockType.DIRT, 64);
            tec.Items[6] = BlockHelper.MakeItem(BlockType.SAND, 64);
            tec.Items[7] = BlockHelper.MakeItem(BlockType.CRAFTING_TABLE, 64);
            tec.Items[8] = BlockHelper.MakeItem(BlockType.FURNACE, 64);
            tec.Items[9] = BlockHelper.MakeItem(ItemInfo.Bread.ID, 64);
            tec.Items[10] = BlockHelper.MakeItem(BlockType.TORCH, 64);
            tec.Items[11] = BlockHelper.MakeItem(BlockType.STONE, 64);
            tec.Items[12] = BlockHelper.MakeItem(BlockType.CHEST, 64);
            tec.Items[13] = BlockHelper.MakeItem(BlockType.GLASS, 64);
            tec.Items[14] = BlockHelper.MakeItem(BlockType.WOOD, 64);
            tec.Items[15] = BlockHelper.MakeItem(ItemInfo.Cookie.ID, 64);
            tec.Items[16] = BlockHelper.MakeItem(ItemInfo.RedstoneDust.ID, 64);
            tec.Items[17] = BlockHelper.MakeItem(BlockType.IRON_BLOCK, 64);
            tec.Items[18] = BlockHelper.MakeItem(BlockType.DIAMOND_BLOCK, 64);
            tec.Items[19] = BlockHelper.MakeItem(BlockType.GOLD_BLOCK, 64);
            bm.SetID(x, y, z, BlockType.CHEST);
            bm.SetTileEntity(x, y, z, tec);
        }
        private static int JavaStringHashCode(string strHash)
        {
            int intReturn = 0;
            foreach (char chrLetter in strHash)
            {
                intReturn = 31 * intReturn + chrLetter;
            }
            return intReturn;
        }
    }
}