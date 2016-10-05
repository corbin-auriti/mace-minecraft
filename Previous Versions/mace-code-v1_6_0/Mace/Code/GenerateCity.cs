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
using Substrate;
using Substrate.TileEntities;
using System.Windows.Forms;
using System.Diagnostics;

namespace Mace
{
    class GenerateCity
    {

        public void CropMaceWorld(frmMace frmLogForm)
        {
            // thank you to Surrogard <surrogard@googlemail.com> for providing a linux friendly version of this code:            
            Directory.CreateDirectory(Utils.GetMinecraftSavesDir("macecopy"));
            BetaWorld bwCopy = BetaWorld.Create(Utils.GetMinecraftSavesDir("macecopy"));
            ChunkManager cmCopy = bwCopy.GetChunkManager();
            BetaWorld bwCrop = BetaWorld.Open(Utils.GetMinecraftSavesDir("mace"));
            ChunkManager cmCrop = bwCrop.GetChunkManager();

            foreach (ChunkRef chunk in cmCrop)
            {
                if (chunk.X >= 0 && chunk.X <= 7 && chunk.Z >= 0 && chunk.Z <= 7)
                {
                    Debug.WriteLine("Copying chunk " + chunk.X + "," + chunk.Z);
                    cmCopy.SetChunk(chunk.X, chunk.Z, chunk.GetChunkRef());
                }
            }
            cmCopy.Save();
            bwCopy.Save();
        }

        public void Generate(frmMace frmLogForm, string strUserCityName, bool booIncludeFarms, bool booIncludeMoat, bool booIncludeWalls,
                             bool booIncludeDrawbridges, bool booIncludeGuardTowers, bool booIncludeBuildings, bool booIncludePaths, bool booIncludeMineshaft,
                             bool booIncludeItemsInChests, bool booIncludeValuableBlocks,
                             string strCitySize, string strMoatType, string strCityEmblem, string strOutsideLights, string strTowerAddition, string strWallMaterial,
                             string strCitySeed, string strWorldSeed)
        {
            #region seed the random number generators
            int intCitySeed, intWorldSeed;
            Random randSeeds = new Random();
            if (strCitySeed == "")
            {
                intCitySeed = randSeeds.Next();
                frmLogForm.UpdateLog("Random city seed: " + intCitySeed);
            }
            else
            {
                intCitySeed = JavaStringHashCode(strCitySeed);
                frmLogForm.UpdateLog("Random city seed: " + strCitySeed);
            }
            if (strWorldSeed == "")
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
            string strFolder = "", strCityName = "";

            strUserCityName = SafeFilename(strUserCityName);
            if (strUserCityName.ToLower().Trim() == "")
            {
                strUserCityName = "Random";
            }

            if (strUserCityName.ToLower().Trim() != "random")
            {
                if (Directory.Exists(Utils.GetMinecraftSavesDir(strUserCityName)))
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
                    strFolder = Utils.GetMinecraftSavesDir(strCityName);
                }
            }
            if (strCityName == "")
            {
                string strStart, strEnd;
                do
                {
                    strStart = RandomHelper.RandomFileLine(Path.Combine("Resources", "CityAdj.txt"));
                    strEnd = RandomHelper.RandomFileLine(Path.Combine("Resources", "CityNoun.txt"));
                    strCityName = "City of " + strStart + strEnd;
                    strFolder = Utils.GetMinecraftSavesDir(strCityName);
                } while (strStart.ToLower().Trim() == strEnd.ToLower().Trim() || Directory.Exists(strFolder));
            }
            Directory.CreateDirectory(strFolder);
            RandomHelper.SetSeed(intCitySeed);
            #endregion            

            #region get handles to world, chunk manager and block manager
            BetaWorld worldDest = BetaWorld.Create(@strFolder);
            ChunkManager cmDest = worldDest.GetChunkManager();
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
            }
            // then we multiply by 16, because that's the x and z of a chunk
            intCitySize *= 16;
            int intFarmSize = booIncludeFarms ? 32 : 16;
            int intMapSize = intCitySize + (intFarmSize * 2);
            #endregion

            #region setup classes
            BlockShapes.SetupClass(bmDest, intMapSize);
            BlockHelper.SetupClass(bmDest, intMapSize);
            if (!SourceWorld.SetupClass(worldDest, booIncludeItemsInChests))
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
                if (strMoatType == "Lava" || strMoatType == "Random")
                {
                    strMoatType = RandomHelper.RandomString("Drop to Bedrock", "Cactus", "Water");
                }
                strOutsideLights = "Torches";
            }
            if (strWallMaterial == "Random")
            {
                if (strMoatType == "Lava" || strOutsideLights == "Fire")
                {
                    strWallMaterial = RandomHelper.RandomString("Brick", "Cobblestone", "Sandstone", "Stone");
                }
                else
                {
                    strWallMaterial = RandomHelper.RandomString("Brick", "Cobblestone", "Sandstone", "Stone", "Wood Planks");
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
                else
                {
                    strMoatType = "Water";
                }
            }

            int intWallMaterial = (int)BlockType.STONE;
            switch (strWallMaterial)
            {
                case "Brick":
                    intWallMaterial = (int)BlockType.BRICK_BLOCK;
                    break;
                case "Cobblestone":
                    intWallMaterial = (int)BlockType.COBBLESTONE;
                    break;
                case "Sandstone":
                    intWallMaterial = (int)BlockType.SANDSTONE;
                    break;
                case "Stone":
                    intWallMaterial = (int)BlockType.STONE;
                    break;
                case "Wood Planks":
                    intWallMaterial = (int)BlockType.WOOD_PLANK;
                    break;

                case "Wood Logs":
                    intWallMaterial = (int)BlockType.WOOD;
                    break;
                case "Bedrock":
                    intWallMaterial = (int)BlockType.BEDROCK;
                    break;
                case "Glass":
                    intWallMaterial = (int)BlockType.GLASS;
                    break;
                case "Dirt":
                    intWallMaterial = (int)BlockType.DIRT;
                    break;
                case "Obsidian":
                    intWallMaterial = (int)BlockType.OBSIDIAN;
                    break;
            }
            #endregion

            #region make the city
            frmLogForm.UpdateLog("Creating underground terrain");
            Chunks.MakeChunks(cmDest, intMapSize / 16, frmLogForm);
            frmLogForm.UpdateProgress(25);

            if (booIncludeBuildings || booIncludePaths)
            {
                frmLogForm.UpdateLog("Creating paths");
                int[,] intArea = Paths.MakePaths(worldDest, bmDest, intFarmSize, intMapSize);
                frmLogForm.UpdateProgress(34);
                if (booIncludeBuildings)
                {
                    frmLogForm.UpdateLog("Creating buildings");
                    Buildings.MakeInsideCity(bmDest, worldDest, intArea, intFarmSize, intMapSize, booIncludePaths);
                }
            }
            frmLogForm.UpdateProgress(43);

            if (booIncludeMineshaft)
            {
                frmLogForm.UpdateLog("Creating mineshaft");
                Mineshaft.MakeMineshaft(worldDest, bmDest, intFarmSize, intMapSize);
                frmLogForm.UpdateProgress(49);
            }

            if (booIncludeWalls)
            {
                frmLogForm.UpdateLog("Creating walls");
                Walls.MakeWalls(worldDest, intFarmSize, intMapSize, strCityEmblem, strOutsideLights, intWallMaterial);
            }
            frmLogForm.UpdateProgress(51);

            if (booIncludeMoat)
            {
                frmLogForm.UpdateLog("Creating moat");
                Moat.MakeMoat(intFarmSize, intMapSize, strMoatType, booIncludeGuardTowers);
            }
            frmLogForm.UpdateProgress(52);

            if (booIncludeDrawbridges)
            {
                frmLogForm.UpdateLog("Creating drawbridges");
                Drawbridge.MakeDrawbridges(bmDest, intFarmSize, intMapSize, booIncludeMoat, booIncludeWalls, booIncludeItemsInChests, intWallMaterial, strMoatType);
            }
            frmLogForm.UpdateProgress(53);

            if (booIncludeGuardTowers)
            {
                frmLogForm.UpdateLog("Creating guard towers");
                GuardTowers.MakeGuardTowers(bmDest, intFarmSize, intMapSize, booIncludeWalls, strOutsideLights, strTowerAddition, booIncludeItemsInChests, intWallMaterial);
            }
            frmLogForm.UpdateProgress(54);

            if (booIncludeFarms)
            {
                frmLogForm.UpdateLog("Creating farms");
                Farms.MakeFarms(worldDest, bmDest, intFarmSize, intMapSize);
            }
            frmLogForm.UpdateProgress(58);

            if (!booIncludeValuableBlocks)
            {
                cmDest.Save();
                worldDest.Save();
                Chunks.ReplaceValuableBlocks(worldDest, bmDest, intMapSize);
            }
            frmLogForm.UpdateProgress(62);
            #endregion

            #region world settings
            // spawn in a guard tower
            //worldDest.Level.SpawnX = intFarmSize + 5;
            //worldDest.Level.SpawnZ = intFarmSize + 5;
            //worldDest.Level.SpawnY = 82;
            // spawn looking at one of the city entrances
            worldDest.Level.SpawnX = intMapSize / 2;
            worldDest.Level.SpawnZ = intMapSize - (intFarmSize - 10);
            worldDest.Level.SpawnY = 64;
            // spawn in the middle of the city
            //worldDest.Level.SpawnX = intMapSize / 2;
            //worldDest.Level.SpawnZ = (intMapSize / 2) - 1;
            //worldDest.Level.SpawnY = 64;
            // spawn default
            //world.Level.SpawnX = 0;
            //world.Level.SpawnY = 65;
            //world.Level.SpawnZ = 0;
            if (strWorldSeed != "")
            {
                worldDest.Level.RandomSeed = intWorldSeed;
            }

            worldDest.Level.LevelName = strCityName;

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
            MakeHelperChest(bmDest, worldDest.Level.SpawnX + 2, worldDest.Level.SpawnY, worldDest.Level.SpawnZ + 2);
#endif

            frmLogForm.UpdateLog("Resetting lighting");
            Chunks.ResetLighting(worldDest, cmDest, frmLogForm, (int)Math.Pow(intMapSize / 16, 2));

            worldDest.Save();

            frmLogForm.UpdateLog("\r\nCreated the " + strCityName + "!");
            frmLogForm.UpdateLog("It'll be at the end of your MineCraft world list.");
        }
        static string SafeFilename(string strFilename)
        {
            string strUnsafe = @"*/\:?<>|" + '"';
            for (int a = 0; a < strUnsafe.Length; a++)
            {
                strFilename = strFilename.Replace(strUnsafe.Substring(a, 1), "");
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
            tec.Items[4] = BlockHelper.MakeItem((int)BlockType.LADDER, 64);
            tec.Items[5] = BlockHelper.MakeItem((int)BlockType.DIRT, 64);
            tec.Items[6] = BlockHelper.MakeItem((int)BlockType.SAND, 64);
            tec.Items[7] = BlockHelper.MakeItem((int)BlockType.CRAFTING_TABLE, 64);
            tec.Items[8] = BlockHelper.MakeItem((int)BlockType.FURNACE, 64);
            tec.Items[9] = BlockHelper.MakeItem(ItemInfo.Bread.ID, 64);
            tec.Items[10] = BlockHelper.MakeItem((int)BlockType.TORCH, 64);
            tec.Items[11] = BlockHelper.MakeItem((int)BlockType.STONE, 64);
            tec.Items[12] = BlockHelper.MakeItem((int)BlockType.CHEST, 64);
            tec.Items[13] = BlockHelper.MakeItem((int)BlockType.GLASS, 64);
            tec.Items[14] = BlockHelper.MakeItem((int)BlockType.WOOD, 64);
            tec.Items[15] = BlockHelper.MakeItem(ItemInfo.Cookie.ID, 64);
            tec.Items[16] = BlockHelper.MakeItem(ItemInfo.RedstoneDust.ID, 64);
            tec.Items[17] = BlockHelper.MakeItem((int)BlockType.IRON_BLOCK, 64);
            tec.Items[18] = BlockHelper.MakeItem((int)BlockType.DIAMOND_BLOCK, 64);
            tec.Items[19] = BlockHelper.MakeItem((int)BlockType.GOLD_BLOCK, 64);
            bm.SetID(x, y, z, (int)BlockType.CHEST);
            bm.SetTileEntity(x, y, z, tec);
        }
        public int JavaStringHashCode(string strHash)
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