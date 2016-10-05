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
using System.Diagnostics;
using System.IO;
using Substrate;
using Substrate.TileEntities;

namespace Mace
{
    class GenerateCity
    {
        public void Generate(frmMace frmLogForm, bool booIncludeFarms, bool booIncludeMoat, bool booIncludeWalls,
                             bool booIncludeDrawbridges, bool booIncludeGuardTowers, bool booIncludeNoticeboard,
                             bool booIncludeBuildings, bool booIncludeSewers, string strCitySize, string strMoatLiquid)
        {            
            string strFolder, strCityName;
            do
            {
                strCityName = "City of " + RandomHelper.RandomFileLine("CityStartingWords.txt")
                                         + RandomHelper.RandomFileLine("CityEndingWords.txt");
                strFolder = Environment.GetEnvironmentVariable("APPDATA") + @"\.minecraft\saves\" + strCityName + @"\";
            } while (Directory.Exists(strFolder));

            Directory.CreateDirectory(strFolder);
            BetaWorld world = BetaWorld.Create(@strFolder);

            int intFarmSize = 28;
            if (!booIncludeFarms)
                intFarmSize = 2;
            int intPlotSize = 12;
            int intPlots = 15;
            Random rand = new Random();

            switch (strCitySize)
            {
                case "Random":
                    intPlots = rand.Next(10, 20);
                    break;
                case "Very small":
                    intPlots = 6;
                    break;
                case "Small":
                    intPlots = 10;
                    break;
                case "Medium":
                    intPlots = 15;
                    break;
                case "Large":
                    intPlots = 20;
                    break;
                case "Very large":
                    intPlots = 23;
                    break;
                default:
                    Debug.Assert(false);
                    break;                    
            }
            int intMapSize = (intPlots * intPlotSize) + (intFarmSize * 2);

            ChunkManager cm = world.GetChunkManager();
            frmLogForm.UpdateLog("Creating chunks");
            Chunks.MakeChunks(cm, -1, 2 + (intMapSize / 16), frmLogForm);
            frmLogForm.UpdateProgress(34);

            BlockManager bm = world.GetBlockManager();
            bm.AutoLight = false;

            BlockShapes.SetupClass(bm, intMapSize);
            BlockHelper.SetupClass(bm, intMapSize);

            bool[,] booSewerEntrances;
            if (booIncludeSewers)
            {
                frmLogForm.UpdateLog("Creating sewers");
                booSewerEntrances = Sewers.MakeSewers(intFarmSize, intMapSize, intPlotSize);
            }
            else
            {
                booSewerEntrances = new bool[2 + ((intMapSize - ((intFarmSize + 16) * 2)) / intPlotSize),
                                             2 + ((intMapSize - ((intFarmSize + 16) * 2)) / intPlotSize)];
            }
            frmLogForm.UpdateProgress(35);
            if (booIncludeBuildings)
            {
                frmLogForm.UpdateLog("Creating plots");
                Plots.MakeBuildings(bm, booSewerEntrances, intFarmSize, intMapSize, intPlotSize);
            }
            frmLogForm.UpdateProgress(36);
            if (booIncludeWalls)
            {
                frmLogForm.UpdateLog("Creating walls");
                Walls.MakeWalls(intFarmSize, intMapSize);
            }
            frmLogForm.UpdateProgress(37);
            if (booIncludeMoat)
            {
                frmLogForm.UpdateLog("Creating moat");
                Moat.MakeMoat(intFarmSize, intMapSize, strMoatLiquid);
            }
            frmLogForm.UpdateProgress(38);
            if (booIncludeDrawbridges)
            {
                frmLogForm.UpdateLog("Creating drawbridges");
                Drawbridge.MakeDrawbridges(bm, intFarmSize, intMapSize, booIncludeMoat, booIncludeWalls);
            }
            frmLogForm.UpdateProgress(39);
            if (booIncludeGuardTowers)
            {
                frmLogForm.UpdateLog("Creating guard towers");
                GuardTowers.MakeGuardTowers(bm, intFarmSize, intMapSize, booIncludeWalls);
            }
            frmLogForm.UpdateProgress(40);
            if (booIncludeWalls && booIncludeNoticeboard)
            {
                frmLogForm.UpdateLog("Creating noticeboard");
                NoticeBoard.MakeNoticeBoard(bm, intFarmSize, intMapSize);
            }
            frmLogForm.UpdateProgress(41);
            if (booIncludeFarms)
            {
                frmLogForm.UpdateLog("Creating farms");
                Farms.MakeFarms(bm, intFarmSize, intMapSize);
            }
            frmLogForm.UpdateProgress(42);

            world.Level.LevelName = strCityName;
            // spawn in a guard tower
            //world.Level.SpawnX = intFarmSize + 5;
            //world.Level.SpawnZ = intFarmSize + 5;
            //world.Level.SpawnY = 74;
            // spawn looking at one of the city entrances
            world.Level.SpawnX = intMapSize / 2;
            world.Level.SpawnZ = intMapSize - 21;
            world.Level.SpawnY = 64;
            if (rand.NextDouble() < 0.1)
            {
                world.Level.IsRaining = true;
                if (rand.NextDouble() < 0.25)
                    world.Level.IsThundering = true;
            }

            //MakeHelperChest(bm, world.Level.SpawnX + 2, world.Level.SpawnY, world.Level.SpawnZ + 2);

            frmLogForm.UpdateLog("Resetting lighting");
            Chunks.ResetLighting(cm, frmLogForm, (int)Math.Pow(3 + (intMapSize / 16), 2));
            world.Save();

            frmLogForm.UpdateLog("\r\nCreated the " + strCityName + "!");
            frmLogForm.UpdateLog("It'll be at the end of your MineCraft world list.");
        }
        private static void MakeHelperChest(BlockManager bm, int x, int y, int z)
        {
            TileEntityChest tec = new TileEntityChest();
            tec.Items[0] = BlockHelper.MakeItem(ItemInfo.DiamondSword.ID);
            tec.Items[1] = BlockHelper.MakeItem(ItemInfo.DiamondPickaxe.ID);
            tec.Items[2] = BlockHelper.MakeItem(ItemInfo.DiamondShovel.ID);
            tec.Items[3] = BlockHelper.MakeItem(ItemInfo.DiamondPickaxe.ID);
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
            bm.SetID(x, y, z, (int)BlockType.CHEST);
            bm.SetTileEntity(x, y, z, tec);
        }
    }
}