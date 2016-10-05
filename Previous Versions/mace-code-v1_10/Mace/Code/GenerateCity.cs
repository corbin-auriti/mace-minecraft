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
        public static void Generate(frmMace frmLogForm, BetaWorld worldDest, BetaChunkManager cmDest, BlockManager bmDest,
                                    int x, int z, bool booExportSchematics, string strUndergroundOres)
        {
            #region create a city name
            string strStart, strEnd;
            do
            {
                strStart = RNG.RandomFileLine(Path.Combine("Resources", City.CityNamePrefixFilename));
                strEnd = RNG.RandomFileLine(Path.Combine("Resources", City.CityNameSuffixFilename));
                City.Name = City.CityNamePrefix + strStart + strEnd;
            } while (GenerateWorld.lstCityNames.Contains(City.Name) ||
                     strStart.ToLower().Trim() == strEnd.ToLower().Trim() ||
                     (strStart + strEnd).Length > 14);
            GenerateWorld.lstCityNames.Add(City.Name);
            #endregion

            #region determine block sizes
            City.CityLength *= 16;
            City.FarmLength *= 16;
            City.EdgeLength = 8;
            City.MapLength = City.CityLength + (City.EdgeLength * 2);
            #endregion

            #region setup classes
            BlockShapes.SetupClass(bmDest);
            BlockHelper.SetupClass(bmDest);
            NoticeBoard.SetupClass();
            if (!SourceWorld.SetupClass(worldDest))
            {
                return;
            }
            #endregion
            
            #region determine random options
            switch (City.WallMaterialID)
            {
                case BlockType.WOOD_PLANK:
                case BlockType.WOOD:
                case BlockType.LEAVES:
                case BlockType.VINES:
                case BlockType.WOOL:
                case BlockType.BOOKSHELF:
                    switch (City.OutsideLightType)
                    {
                        case "Fire":
                            frmLogForm.UpdateLog("Fixing fire-spreading combination", true, true);
                            City.OutsideLightType = "Torches";
                            break;
                    }
                    switch (City.MoatType)
                    {
                        case "Fire":
                        case "Lava":
                            frmLogForm.UpdateLog("Fixing fire-spreading combination", true, true);
                            City.MoatType = "Water";
                            break;
                    }
                    break;
            }
            switch (City.PathType.ToLower().Trim())
            {
                case "stone_raised":
                    City.PathBlockID = BlockInfo.DoubleSlab.ID;
                    City.PathBlockData = 0;
                    City.PathAlternativeBlockID = BlockInfo.Slab.ID;
                    City.PathAlternativeBlockData = 0;
                    City.PathExtends = 2;
                    break;
                case "sandstone_raised":
                    City.PathBlockID = BlockInfo.Sandstone.ID;
                    City.PathBlockData = 0;
                    City.PathAlternativeBlockID = BlockInfo.Slab.ID;
                    City.PathAlternativeBlockData = 1;
                    City.PathExtends = 2;
                    break;
                case "woodplanks_raised":
                    City.PathBlockID = BlockInfo.WoodPlank.ID;
                    City.PathBlockData = 0;
                    City.PathAlternativeBlockID = BlockInfo.Slab.ID;
                    City.PathAlternativeBlockData = 2;
                    City.PathExtends = 2;
                    break;
                case "cobblestone_raised":
                    City.PathBlockID = BlockInfo.Cobblestone.ID;
                    City.PathBlockData = 0;
                    City.PathAlternativeBlockID = BlockInfo.Slab.ID;
                    City.PathAlternativeBlockData = 3;
                    City.PathExtends = 2;
                    break;
                case "brick_raised":
                    City.PathBlockID = BlockInfo.BrickBlock.ID;
                    City.PathBlockData = 0;
                    City.PathAlternativeBlockID = BlockInfo.Slab.ID;
                    City.PathAlternativeBlockData = 4;
                    City.PathExtends = 2;
                    break;
                case "stonebrick_raised":
                    City.PathBlockID = BlockInfo.StoneBrick.ID;
                    City.PathBlockData = 0;
                    City.PathAlternativeBlockID = BlockInfo.Slab.ID;
                    City.PathAlternativeBlockData = 5;
                    City.PathExtends = 2;
                    break;
                case "stonebrick":
                    City.PathBlockID = BlockInfo.StoneBrick.ID;
                    City.PathBlockData = 0;
                    City.PathAlternativeBlockID = 0;
                    City.PathAlternativeBlockData = 0;
                    City.PathExtends = 1;
                    break;
                case "sandstone":
                    City.PathBlockID = BlockInfo.Sandstone.ID;
                    City.PathBlockData = 0;
                    City.PathAlternativeBlockID = 0;
                    City.PathAlternativeBlockData = 0;
                    City.PathExtends = 1;
                    break;
                case "stone":
                    City.PathBlockID = BlockInfo.Stone.ID;
                    City.PathBlockData = 0;
                    City.PathAlternativeBlockID = 0;
                    City.PathAlternativeBlockData = 0;
                    City.PathExtends = 1;
                    break;
                case "wood":
                    City.PathBlockID = BlockInfo.Wood.ID;
                    City.PathBlockData = RNG.Next(0, 2);
                    City.PathAlternativeBlockID = 0;
                    City.PathAlternativeBlockData = 0;
                    City.PathExtends = 1;
                    break;
            }
            #endregion

            #region make the city
            frmLogForm.UpdateLog("Creating the " + City.Name, false, false);
            frmLogForm.UpdateLog("City length in blocks: " + City.MapLength, true, true);
            frmLogForm.UpdateLog("Edge length in blocks: " + City.EdgeLength, true, true);
            frmLogForm.UpdateLog("Farm length in blocks: " + City.FarmLength, true, true);
            frmLogForm.UpdateLog("City position in blocks: " + ((x + Chunks.CITY_RELOCATION_CHUNKS) * 16) + "," +
                                 ((z + Chunks.CITY_RELOCATION_CHUNKS) * 16), true, true);
            frmLogForm.UpdateLog("Theme: " + City.ThemeName, true, true);
            frmLogForm.UpdateLog("Creating underground terrain", true, false);
            Chunks.CreateInitialChunks(cmDest, frmLogForm, strUndergroundOres);
            frmLogForm.UpdateProgress(0.21);
         
            Buildings.structPoint spMineshaftEntrance = new Buildings.structPoint();

            if (City.HasWalls)
            {
                frmLogForm.UpdateLog("Creating walls", true, false);
                Walls.MakeWalls(worldDest, frmLogForm);
            }
            frmLogForm.UpdateProgress(0.24);

            if (City.HasDrawbridges)
            {
                frmLogForm.UpdateLog("Creating entrances", true, false);
                Entrances.MakeEntrances(bmDest);
            }
            frmLogForm.UpdateProgress(0.27);

            if (City.HasMoat)
            {
                frmLogForm.UpdateLog("Creating moat", true, false);
                Moat.MakeMoat(frmLogForm, bmDest);
            }
            frmLogForm.UpdateProgress(0.30);

            if (City.HasBuildings || City.HasPaths)
            {
                frmLogForm.UpdateLog("Creating paths", true, false);
                frmLogForm.UpdateLog("Path type: " + City.PathType, true, true);
                int[,] intArea = Paths.MakePaths(worldDest, bmDest);
                frmLogForm.UpdateProgress(0.33);
                if (City.HasBuildings)
                {
                    frmLogForm.UpdateLog("Creating buildings", true, false);
                    spMineshaftEntrance = Buildings.MakeInsideCity(bmDest, worldDest, intArea, frmLogForm);
                    frmLogForm.UpdateProgress(0.36);
                    if (City.HasMineshaft)
                    {
                        frmLogForm.UpdateLog("Creating mineshaft", true, false);
                        Mineshaft.MakeMineshaft(worldDest, bmDest, spMineshaftEntrance, frmLogForm);
                    }
                }
            }
            frmLogForm.UpdateProgress(0.39);

            if (City.HasGuardTowers)
            {
                frmLogForm.UpdateLog("Creating guard towers", true, false);
                GuardTowers.MakeGuardTowers(bmDest, frmLogForm);
            }
            frmLogForm.UpdateProgress(0.42);

            if (City.HasFarms)
            {
                frmLogForm.UpdateLog("Creating farms", true, false);
                Farms2.MakeFarms(worldDest, bmDest);
            }
            frmLogForm.UpdateProgress(0.45);

            if (City.HasFlowers)
            {
                frmLogForm.UpdateLog("Creating flowers", true, false);
                Flowers.MakeFlowers(worldDest, bmDest);
            }
            frmLogForm.UpdateProgress(0.46);

            if (!City.HasValuableBlocks)
            {
                frmLogForm.UpdateLog("Replacing valuable blocks", true, true);
                cmDest.Save();
                worldDest.Save();
                Chunks.ReplaceValuableBlocks(worldDest, bmDest);
            }
            frmLogForm.UpdateProgress(0.48);

            frmLogForm.UpdateLog("Creating rail data", true, false);
            Chunks.PositionRails(worldDest, bmDest);
            frmLogForm.UpdateProgress(0.50);            

            frmLogForm.UpdateLog("Creating lighting data", true, false);
            Chunks.ResetLighting(worldDest, cmDest, frmLogForm);
            frmLogForm.UpdateProgress(0.95);
            #endregion

            #region export schematic
            if (booExportSchematics)
            {
                frmLogForm.UpdateLog("Creating schematic in world folder", true, false);
                AlphaBlockCollection abcExport = new AlphaBlockCollection(City.MapLength, 128, City.MapLength + City.FarmLength);
                for (int xBlock = 0; xBlock < City.MapLength; xBlock++)
                {
                    for (int zBlock = -City.FarmLength; zBlock < City.MapLength; zBlock++)
                    {
                        for (int y = 0; y < 128; y++)
                        {
                            abcExport.SetBlock(xBlock, y, City.FarmLength + zBlock, bmDest.GetBlock(xBlock, y, zBlock));
                        }
                    }
                }
                Schematic CitySchematic = new Schematic(City.MapLength, 128, City.MapLength + City.FarmLength);
                CitySchematic.Blocks = abcExport;
                CitySchematic.Export(worldDest.Path + "\\" + City.Name + ".schematic");
            }
            #endregion

            #region positioning
            frmLogForm.UpdateLog("Creating position data", true, false);
            Chunks.MoveChunks(worldDest, cmDest, x, z);
            frmLogForm.UpdateProgress(1);
            #endregion
        }
    }
}