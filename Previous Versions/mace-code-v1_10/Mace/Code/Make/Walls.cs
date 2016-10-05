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
using System.Diagnostics;
using System.IO;
using Substrate;

namespace Mace
{
    static class Walls
    {
        public static void MakeWalls(BetaWorld world, frmMace frmLogForm)
        {
            // walls
            for (int a = City.EdgeLength + 6; a <= City.EdgeLength + 10; a++)
            {
                BlockShapes.MakeHollowLayers(a, City.MapLength - a, 1, 72, a, City.MapLength - a,
                                             City.WallMaterialID, 0, City.WallMaterialData);
                world.Save();
            }
            // outside and inside edges at the top
            BlockShapes.MakeHollowLayers(City.EdgeLength + 5, City.MapLength - (City.EdgeLength + 5), 72, 73,
                                         City.EdgeLength + 5, City.MapLength - (City.EdgeLength + 5),
                                         City.WallMaterialID, 0, City.WallMaterialData);
            BlockShapes.MakeHollowLayers(City.EdgeLength + 11, City.MapLength - (City.EdgeLength + 11), 72, 73,
                                         City.EdgeLength + 11, City.MapLength - (City.EdgeLength + 11),
                                         City.WallMaterialID, 0, City.WallMaterialData);
            // alternating blocks on top of the edges
            for (int a = City.EdgeLength + 6; a <= City.MapLength - (City.EdgeLength + 6); a += 2)
            {
                BlockShapes.MakeBlock(a, 74, City.EdgeLength + 5, City.WallMaterialID, 2, 100, City.WallMaterialData);
            }
            for (int a = City.EdgeLength + 12; a <= City.MapLength - (City.EdgeLength + 12); a += 2)
            {
                BlockShapes.MakeBlock(a, 74, City.EdgeLength + 11, City.WallMaterialID, 2, 100, City.WallMaterialData);
            }
            // ladder
            BlockHelper.MakeLadder((City.MapLength / 2) - 5, 64, 72, City.EdgeLength + 11, 2, City.WallMaterialID);
            BlockShapes.MakeBlock((City.MapLength / 2) - 5, 73, City.EdgeLength + 11, BlockInfo.Air.ID, 2, 100, -1);
            // decorations at the gates
            frmLogForm.UpdateLog("Creating wall lights: " + City.OutsideLightType, true, true);
            switch (City.OutsideLightType)
            {
                case "Fire":
                    // fire above the entrances
                    BlockShapes.MakeBlock((City.MapLength / 2) - 1, 69, City.EdgeLength + 5, BlockInfo.Netherrack.ID, 2, 100, -1);
                    BlockShapes.MakeBlock((City.MapLength / 2), 69, City.EdgeLength + 5, BlockInfo.Netherrack.ID, 2, 100, -1);
                    BlockShapes.MakeBlock((City.MapLength / 2) - 1, 70, City.EdgeLength + 5, BlockInfo.Fire.ID, 2, 100, -1);
                    BlockShapes.MakeBlock((City.MapLength / 2), 70, City.EdgeLength + 5, BlockInfo.Fire.ID, 2, 100, -1);
                    // fire on the outside walls
                    for (int a = City.EdgeLength + 8; a < (City.MapLength / 2) - 9; a += 4)
                    {
                        BlockShapes.MakeBlock(a, 69, City.EdgeLength + 5, BlockInfo.Netherrack.ID, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 70, City.EdgeLength + 5, BlockInfo.Fire.ID, 2, 100, -1);
                    }
                    break;
                case "Torches":
                    // torches above the entrances
                    BlockHelper.MakeTorch((City.MapLength / 2), 70, City.EdgeLength + 5, City.WallMaterialID, 2);
                    BlockHelper.MakeTorch((City.MapLength / 2) - 1, 70, City.EdgeLength + 5, City.WallMaterialID, 2);
                    // torches on the outside walls
                    for (int a = City.EdgeLength + 8; a < (City.MapLength / 2) - 9; a += 4)
                    {
                        BlockHelper.MakeTorch(a, 70, City.EdgeLength + 5, City.WallMaterialID, 2);
                    }
                    break;
                case "None":
                case "":
                    break;
                default:
                    Debug.Fail("Invalid switch result");
                    break;
            }
            if (City.HasTorchesOnWalkways)
            {
                // torches on the inside walls
                for (int a = City.EdgeLength + 16; a < (City.MapLength / 2); a += 4)
                {
                    BlockHelper.MakeTorch(a, 69, City.EdgeLength + 11, City.WallMaterialID, 2);
                }
                // torches on the wall roofs
                for (int a = City.EdgeLength + 16; a < (City.MapLength / 2); a += 4)
                {
                    BlockShapes.MakeBlock(a, 73, City.EdgeLength + 8, BlockInfo.Torch.ID, 2, 100, -1);
                }
            }
            frmLogForm.UpdateLog("Creating wall emblems: " + City.CityEmblemType, true, true);
            MakeEmblem();
        }
        private static void MakeEmblem()
        {
            if (City.CityEmblemType.ToLower() != "none")
            {
                int intBlockyBlock = RNG.RandomItem(BlockInfo.GoldBlock.ID, BlockInfo.IronBlock.ID, BlockInfo.DiamondBlock.ID);
                string[] strEmblem;
                strEmblem = File.ReadAllLines(Path.Combine("Resources", "Emblem " + City.CityEmblemType + ".txt"));

                for (int y = 0; y < strEmblem.GetLength(0); y++)
                {
                    strEmblem[y] = strEmblem[y].Replace("  ", " ");
                    strEmblem[y] = strEmblem[y].Replace("\t", " "); //tab
                    string[] strLine = strEmblem[y].Split(' ');
                    for (int x = 0; x < strLine.GetLength(0); x++)
                    {
                        string[] strSplit = strLine[x].Split(':');
                        if (strSplit.GetLength(0) == 1)
                        {
                            Array.Resize(ref strSplit, 2);
                        }
                        if (strSplit[0] == "-1")
                        {
                            strSplit[0] = intBlockyBlock.ToString();
                        }
                        BlockShapes.MakeBlock(((City.MapLength / 2) - (strLine.GetLength(0) + 6)) + x, 71 - y,
                                              City.EdgeLength + 5, Convert.ToInt32(strSplit[0]), 2, 100,
                                              Convert.ToInt32(strSplit[1]));
                    }
                    for (int x = strLine.GetLength(0) + 1; x < strLine.GetLength(0) + 5; x++)
                    {
                        BlockShapes.MakeBlock((City.MapLength / 2) - (6 + x), 69,
                                              City.EdgeLength + 5, BlockInfo.Air.ID, 2, 100, 0);
                        BlockShapes.MakeBlock((City.MapLength / 2) - (6 + x), 70,
                                              City.EdgeLength + 5, BlockInfo.Air.ID, 2, 100, 0);
                    }
                }
            }
        }
    }
}