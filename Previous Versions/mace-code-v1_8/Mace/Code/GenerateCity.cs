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

namespace Mace
{
    static class GenerateCity
    {
        public static void Generate(frmMace frmLogForm, BetaWorld worldDest, BetaChunkManager cmDest, BlockManager bmDest, int x, int z)
        {
            #region create a city name
            string strStart, strEnd;
            do
            {
                strStart = RandomHelper.RandomFileLine(Path.Combine("Resources", City.CityNamePrefixFilename));
                strEnd = RandomHelper.RandomFileLine(Path.Combine("Resources", City.CityNameSuffixFilename));
                City.Name = "City of " + strStart + strEnd;
            } while (GenerateWorld.lstCityNames.Contains(City.Name) ||
                     strStart.ToLower().Trim() == strEnd.ToLower().Trim() ||
                     (strStart + strEnd).Length > 14);
            GenerateWorld.lstCityNames.Add(City.Name);
            #endregion

            #region determine block sizes
            City.CityLength *= 16; // chunk length
            City.FarmLength = City.HasFarms ? 32 : 8;
            City.MapLength = City.CityLength + (City.FarmLength * 2);
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
#pragma warning disable
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
#pragma warning restore
            #endregion

            #region make the city
            frmLogForm.UpdateLog("Creating the " + City.Name, false, false);
            frmLogForm.UpdateLog("City length in blocks: " + City.MapLength, true, true);
            frmLogForm.UpdateLog("City position in blocks: " + ((x + 30) * 16) + "," + ((z + 30) * 16), true, true);
            frmLogForm.UpdateLog("Theme: " + City.ThemeName, true, true);
            frmLogForm.UpdateLog("Creating underground terrain", true, false);
            Chunks.CreateInitialChunks(cmDest, frmLogForm);
            frmLogForm.UpdateProgress(0.35);
          
            Buildings.structPoint spMineshaftEntrance = new Buildings.structPoint();

            if (City.HasWalls)
            {
                frmLogForm.UpdateLog("Creating walls", true, false);
                Walls.MakeWalls(worldDest, frmLogForm);
            }
            frmLogForm.UpdateProgress(0.45);
            if (City.HasBuildings || City.HasPaths)
            {
                frmLogForm.UpdateLog("Creating paths", true, false);
                int[,] intArea = Paths.MakePaths(worldDest, bmDest);
                frmLogForm.UpdateProgress(0.50);
                if (City.HasBuildings)
                {
                    frmLogForm.UpdateLog("Creating buildings", true, false);
                    spMineshaftEntrance = Buildings.MakeInsideCity(bmDest, worldDest, intArea, frmLogForm);
                    frmLogForm.UpdateProgress(0.55);
                    if (City.HasMineshaft)
                    {
                        frmLogForm.UpdateLog("Creating mineshaft", true, false);
                        Mineshaft.MakeMineshaft(worldDest, bmDest, spMineshaftEntrance, frmLogForm);
                    }
                }
            }
            frmLogForm.UpdateProgress(0.60);

            if (City.HasMoat)
            {
                frmLogForm.UpdateLog("Creating moat", true, false);
                Moat.MakeMoat(frmLogForm);
            }
            frmLogForm.UpdateProgress(0.65);

            if (City.HasDrawbridges)
            {
                frmLogForm.UpdateLog("Creating drawbridges", true, false);
                Drawbridge.MakeDrawbridges(bmDest);
            }
            frmLogForm.UpdateProgress(0.70);

            if (City.HasGuardTowers)
            {
                frmLogForm.UpdateLog("Creating guard towers", true, false);
                GuardTowers.MakeGuardTowers(bmDest, frmLogForm);
            }
            frmLogForm.UpdateProgress(0.75);

            if (City.HasFarms)
            {
                frmLogForm.UpdateLog("Creating farms", true, false);
                Farms.MakeFarms(worldDest, bmDest, frmLogForm);
            }
            frmLogForm.UpdateProgress(0.80);

            if (!City.HasValuableBlocks)
            {
                frmLogForm.UpdateLog("Replacing valuable blocks", true, true);
                cmDest.Save();
                worldDest.Save();
                Chunks.ReplaceValuableBlocks(worldDest, bmDest);
            }
            frmLogForm.UpdateProgress(0.90);
            frmLogForm.UpdateLog("Creating rail data", true, false);
            Chunks.PositionRails(worldDest, bmDest);
            frmLogForm.UpdateProgress(0.95);
            frmLogForm.UpdateLog("Creating position data", true, false);

            Chunks.MoveChunks(worldDest, cmDest, x, z);
            frmLogForm.UpdateProgress(1);
            #endregion
        }
    }
}