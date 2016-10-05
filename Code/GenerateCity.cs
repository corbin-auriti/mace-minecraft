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
    along with this program.  If not, see <http://www.gnu.org/licenses/>
*/

using System;
using System.IO;
using Substrate;
using Substrate.ImportExport;

namespace Mace
{
    static class GenerateCity
    {
        public static bool Generate(frmMace frmLogForm, AnvilWorld worldDest, RegionChunkManager cmDest, BlockManager bmDest,
                                    int x, int z, bool booExportSchematics, string strUndergroundOres)
        {
            #region create a city name
            string strStart, strEnd;
            do
            {
                strStart = RNG.RandomFileLine(Path.Combine("Resources", City.cityNamePrefixFilename));
                strEnd = RNG.RandomFileLine(Path.Combine("Resources", City.cityNameSuffixFilename));
                City.name = City.cityNamePrefix + strStart + strEnd;
            } while (GenerateWorld.lstCityNames.Contains(City.name) ||
                     strStart.ToLower().Trim() == strEnd.ToLower().Trim() ||
                     (strStart + strEnd).Length > 14);
            GenerateWorld.lstCityNames.Add(City.name);
            #endregion

            #region determine block sizes
            City.cityLength *= 16;
            City.farmLength *= 16;
            City.edgeLength = 8;
            City.mapLength = City.cityLength + (City.edgeLength * 2);
            #endregion

            #region setup classes
            BlockShapes.SetupClass(bmDest, cmDest);
            BlockHelper.SetupClass(bmDest);
            NoticeBoard.SetupClass();
            if (!SourceWorld.SetupClass(worldDest))
            {
                return false;
            }
            #endregion
            
            #region determine random options
            switch (City.wallMaterialID)
            {
                case BlockType.ICE:
                    City.hasTorchesOnWalkways = false;
                    City.outsideLightType = "None";
                    switch (City.moatType)
                    {
                        case "Fire":
                        case "Lava":
                            frmLogForm.UpdateLog("Fixing disaster configuration", true, true);
                            City.moatType = "Water";
                            break;
                    }
                    break;
                case BlockType.WOOD_PLANK:
                case BlockType.WOOD:
                case BlockType.LEAVES:
                case BlockType.VINES:
                case BlockType.WOOL:
                case BlockType.BOOKSHELF:                
                    switch (City.outsideLightType)
                    {
                        case "Fire":
                            frmLogForm.UpdateLog("Fixing fire-spreading combination", true, true);
                            City.outsideLightType = "Torches";
                            break;
                    }
                    switch (City.moatType)
                    {
                        case "Fire":
                        case "Lava":
                            frmLogForm.UpdateLog("Fixing fire-spreading combination", true, true);
                            City.moatType = "Water";
                            break;
                    }
                    break;
            }
            switch (City.pathType.ToLower().Trim())
            {
                case "stone_raised":
                    City.pathBlockID = BlockInfo.DoubleStoneSlab.ID;
                    City.pathBlockData = 0;
                    City.pathAlternativeBlockID = BlockInfo.StoneSlab.ID;
                    City.pathAlternativeBlockData = (int)SlabType.STONE;
                    City.pathExtends = 2;
                    break;
                case "sandstone_raised":
                    City.pathBlockID = BlockInfo.Sandstone.ID;
                    City.pathBlockData = 0;
                    City.pathAlternativeBlockID = BlockInfo.StoneSlab.ID;
                    City.pathAlternativeBlockData = (int)SlabType.SANDSTONE;
                    City.pathExtends = 2;
                    break;
                case "wood_planks_raised":
                case "woodplanks_raised":
                    City.pathBlockID = BlockInfo.WoodPlank.ID;
                    City.pathBlockData = 0;
                    City.pathAlternativeBlockID = BlockInfo.StoneSlab.ID;
                    City.pathAlternativeBlockData = (int)SlabType.WOOD;
                    City.pathExtends = 2;
                    break;
                case "cobblestone_raised":
                    City.pathBlockID = BlockInfo.Cobblestone.ID;
                    City.pathBlockData = 0;
                    City.pathAlternativeBlockID = BlockInfo.StoneSlab.ID;
                    City.pathAlternativeBlockData = (int)SlabType.COBBLESTONE;
                    City.pathExtends = 2;
                    break;
                case "brick_raised":
                    City.pathBlockID = BlockInfo.BrickBlock.ID;
                    City.pathBlockData = 0;
                    City.pathAlternativeBlockID = BlockInfo.StoneSlab.ID;
                    City.pathAlternativeBlockData = (int)SlabType.BRICK;
                    City.pathExtends = 2;
                    break;
                case "stonebrick_raised":
                    City.pathBlockID = BlockInfo.StoneBrick.ID;
                    City.pathBlockData = 0;
                    City.pathAlternativeBlockID = BlockInfo.StoneSlab.ID;
                    City.pathAlternativeBlockData = (int)SlabType.STONE_BRICK;
                    City.pathExtends = 2;
                    break;
                case "stonebrick":
                    City.pathBlockID = BlockInfo.StoneBrick.ID;
                    City.pathBlockData = 0;
                    City.pathAlternativeBlockID = 0;
                    City.pathAlternativeBlockData = 0;
                    City.pathExtends = 1;
                    break;
                case "sandstone":
                    City.pathBlockID = BlockInfo.Sandstone.ID;
                    City.pathBlockData = 0;
                    City.pathAlternativeBlockID = 0;
                    City.pathAlternativeBlockData = 0;
                    City.pathExtends = 1;
                    break;
                case "stone":
                    City.pathBlockID = BlockInfo.Stone.ID;
                    City.pathBlockData = 0;
                    City.pathAlternativeBlockID = 0;
                    City.pathAlternativeBlockData = 0;
                    City.pathExtends = 1;
                    break;
                case "wood":
                    City.pathBlockID = BlockInfo.Wood.ID;
                    City.pathBlockData = RNG.Next(0, 2);
                    City.pathAlternativeBlockID = 0;
                    City.pathAlternativeBlockData = 0;
                    City.pathExtends = 1;
                    break;
            }
            #endregion

            #region make the city
            frmLogForm.UpdateLog("Creating the " + City.name, false, false);
            frmLogForm.UpdateLog("City length in blocks: " + City.mapLength, true, true);
            frmLogForm.UpdateLog("Edge length in blocks: " + City.edgeLength, true, true);
            frmLogForm.UpdateLog("Farm length in blocks: " + City.farmLength, true, true);
            frmLogForm.UpdateLog("City position in blocks: " + ((x + Chunks.CITY_RELOCATION_CHUNKS) * 16) + "," +
                                 ((z + Chunks.CITY_RELOCATION_CHUNKS) * 16), true, true);
            frmLogForm.UpdateLog("Theme: " + City.themeName, true, true);
            frmLogForm.UpdateLog("Creating underground terrain", true, false);
            Chunks.CreateInitialChunks(cmDest, frmLogForm, strUndergroundOres);
            frmLogForm.UpdateProgress(0.21);
            if (CheckCancelled(worldDest, frmLogForm)) return false;
         
            Buildings.structPoint spMineshaftEntrance = new Buildings.structPoint();

            if (City.hasWalls)
            {
                frmLogForm.UpdateLog("Creating walls", true, false);
                Walls.MakeWalls(worldDest, frmLogForm);
            }
            frmLogForm.UpdateProgress(0.24);
            if (CheckCancelled(worldDest, frmLogForm)) return false;

            if (City.hasDrawbridges)
            {
                frmLogForm.UpdateLog("Creating entrances", true, false);
                Entrances.MakeEntrances(bmDest);
            }
            frmLogForm.UpdateProgress(0.27);
            if (CheckCancelled(worldDest, frmLogForm)) return false;

            if (City.hasMoat)
            {
                frmLogForm.UpdateLog("Creating moat", true, false);
                Moat.MakeMoat(frmLogForm, bmDest);
            }
            frmLogForm.UpdateProgress(0.30);
            if (CheckCancelled(worldDest, frmLogForm)) return false;

            if (City.hasBuildings || City.hasPaths)
            {
                frmLogForm.UpdateLog("Creating paths", true, false);
                frmLogForm.UpdateLog("Path type: " + City.pathType, true, true);
                int[,] intArea = Paths.MakePaths(worldDest, bmDest);
                frmLogForm.UpdateProgress(0.33);
                if (City.hasBuildings)
                {
                    frmLogForm.UpdateLog("Creating buildings", true, false);
                    spMineshaftEntrance = Buildings.MakeInsideCity(bmDest, worldDest, intArea, frmLogForm);
                    frmLogForm.UpdateProgress(0.36);
                    if (City.hasMineshaft)
                    {
                        frmLogForm.UpdateLog("Creating mineshaft", true, false);
                        Mineshaft.MakeMineshaft(worldDest, bmDest, spMineshaftEntrance, frmLogForm);
                    }
                }
            }
            frmLogForm.UpdateProgress(0.39);
            if (CheckCancelled(worldDest, frmLogForm)) return false;

            if (City.hasGuardTowers)
            {
                frmLogForm.UpdateLog("Creating guard towers", true, false);
                GuardTowers.MakeGuardTowers(bmDest, frmLogForm);
            }
            frmLogForm.UpdateProgress(0.42);
            if (CheckCancelled(worldDest, frmLogForm)) return false;

            if (City.hasFarms)
            {
                frmLogForm.UpdateLog("Creating farms", true, false);
                Farms2.MakeFarms(worldDest, bmDest);
            }
            frmLogForm.UpdateProgress(0.45);
            if (CheckCancelled(worldDest, frmLogForm)) return false;

            if (City.hasFlowers)
            {
                frmLogForm.UpdateLog("Creating flowers", true, false);
                Flowers.MakeFlowers(worldDest, bmDest);
            }
            frmLogForm.UpdateProgress(0.46);
            if (CheckCancelled(worldDest, frmLogForm)) return false;

            if (!City.hasValuableBlocks)
            {
                frmLogForm.UpdateLog("Replacing valuable blocks", true, true);
                cmDest.Save();
                worldDest.Save();
                Chunks.ReplaceValuableBlocks(worldDest, bmDest);
            }
            frmLogForm.UpdateProgress(0.48);
            if (CheckCancelled(worldDest, frmLogForm)) return false;

            frmLogForm.UpdateLog("Creating rail data", true, false);
            Chunks.PositionRails(worldDest, bmDest);
            frmLogForm.UpdateProgress(0.50);
            if (CheckCancelled(worldDest, frmLogForm)) return false;

            frmLogForm.UpdateLog("Creating lighting data", true, false);
            Chunks.ResetLighting(worldDest, cmDest, frmLogForm);
            frmLogForm.UpdateProgress(0.95);
            if (CheckCancelled(worldDest, frmLogForm)) return false;
            #endregion

            #region export schematic
            if (booExportSchematics)
            {
                frmLogForm.UpdateLog("Creating schematic in world folder", true, false);
                AlphaBlockCollection abcExport = new AlphaBlockCollection(City.mapLength, 128, City.mapLength + City.farmLength);
                for (int xBlock = 0; xBlock < City.mapLength; xBlock++)
                {
                    for (int zBlock = -City.farmLength; zBlock < City.mapLength; zBlock++)
                    {
                        for (int y = 0; y < 128; y++)
                        {
                            abcExport.SetBlock(xBlock, y, City.farmLength + zBlock, bmDest.GetBlock(xBlock, y, zBlock));
                        }
                    }
                }
                Schematic CitySchematic = new Schematic(City.mapLength, 128, City.mapLength + City.farmLength);
                CitySchematic.Blocks = abcExport;
                CitySchematic.Export(worldDest.Path + "\\" + City.name + ".schematic");
            }
            #endregion

            #region positioning
            frmLogForm.UpdateLog("Creating position data", true, false);
            Chunks.MoveChunks(worldDest, cmDest, x, z);
            frmLogForm.UpdateProgress(1);
            #endregion
            return true;
        }
        private static bool CheckCancelled(AnvilWorld worldDest, frmMace frmLogForm)
        {
            if (City.stop)
            {
                worldDest.Level.LevelName = "Generation cancelled. Please delete me.";
                worldDest.Save();
                return true;
            }
            return false;
        }
    }
}