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
using Substrate.Entities;

namespace Mace
{
    static class Walls
    {
        public static void MakeWalls(AnvilWorld world, frmMace frmLogForm)
        {
            // walls
            for (int a = City.edgeLength + 6; a <= City.edgeLength + 10; a++)
            {
                BlockShapes.MakeHollowLayers(a, City.mapLength - a, 1, 72, a, City.mapLength - a,
                                             City.wallMaterialID, 0, City.wallMaterialData);
                world.Save();
            }
            // outside and inside edges at the top
            BlockShapes.MakeHollowLayers(City.edgeLength + 5, City.mapLength - (City.edgeLength + 5), 72, 73,
                                         City.edgeLength + 5, City.mapLength - (City.edgeLength + 5),
                                         City.wallMaterialID, 0, City.wallMaterialData);
            BlockShapes.MakeHollowLayers(City.edgeLength + 11, City.mapLength - (City.edgeLength + 11), 72, 73,
                                         City.edgeLength + 11, City.mapLength - (City.edgeLength + 11),
                                         City.wallMaterialID, 0, City.wallMaterialData);
            // alternating blocks on top of the edges
            for (int a = City.edgeLength + 6; a <= City.mapLength - (City.edgeLength + 6); a += 2)
            {
                BlockShapes.MakeBlock(a, 74, City.edgeLength + 5, City.wallMaterialID, 2, 100, City.wallMaterialData);
            }
            for (int a = City.edgeLength + 12; a <= City.mapLength - (City.edgeLength + 12); a += 2)
            {
                BlockShapes.MakeBlock(a, 74, City.edgeLength + 11, City.wallMaterialID, 2, 100, City.wallMaterialData);
            }
            // ladder
            BlockHelper.MakeLadder((City.mapLength / 2) - 5, 64, 72, City.edgeLength + 11, 2, City.wallMaterialID);

            BlockShapes.MakeSolidBox((City.mapLength / 2) - 5, (City.mapLength / 2) + 5,
                                     65, 74,
                                     City.edgeLength + 11, City.edgeLength + 11,
                                     BlockInfo.Air.ID, 2);

            // decorations at the gates
            frmLogForm.UpdateLog("Creating wall lights: " + City.outsideLightType, true, true);
            switch (City.outsideLightType)
            {
                case "Fire":
                    // fire above the entrances
                    BlockShapes.MakeBlock((City.mapLength / 2) - 1, 69, City.edgeLength + 5, BlockInfo.Netherrack.ID, 2, 100, -1);
                    BlockShapes.MakeBlock((City.mapLength / 2), 69, City.edgeLength + 5, BlockInfo.Netherrack.ID, 2, 100, -1);
                    BlockShapes.MakeBlock((City.mapLength / 2) - 1, 70, City.edgeLength + 5, BlockInfo.Fire.ID, 2, 100, -1);
                    BlockShapes.MakeBlock((City.mapLength / 2), 70, City.edgeLength + 5, BlockInfo.Fire.ID, 2, 100, -1);
                    // fire on the outside walls
                    for (int a = City.edgeLength + 8; a < (City.mapLength / 2) - 9; a += 4)
                    {
                        BlockShapes.MakeBlock(a, 69, City.edgeLength + 5, BlockInfo.Netherrack.ID, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 70, City.edgeLength + 5, BlockInfo.Fire.ID, 2, 100, -1);
                    }
                    break;
                case "Torches":
                    // torches above the entrances
                    BlockHelper.MakeTorch((City.mapLength / 2), 70, City.edgeLength + 5, City.wallMaterialID, 2);
                    BlockHelper.MakeTorch((City.mapLength / 2) - 1, 70, City.edgeLength + 5, City.wallMaterialID, 2);
                    // torches on the outside walls
                    for (int a = City.edgeLength + 8; a < (City.mapLength / 2) - 9; a += 4)
                    {
                        BlockHelper.MakeTorch(a, 70, City.edgeLength + 5, City.wallMaterialID, 2);
                    }
                    break;
                case "None":
                case "":
                    break;
                default:
                    Debug.Fail("Invalid switch result");
                    break;
            }
            if (City.hasTorchesOnWalkways)
            {
                // torches on the inside walls
                for (int a = City.edgeLength + 16; a < (City.mapLength / 2); a += 4)
                {
                    BlockHelper.MakeTorch(a, 69, City.edgeLength + 11, City.wallMaterialID, 2);
                }
                // torches on the wall roofs
                for (int a = City.edgeLength + 16; a < (City.mapLength / 2); a += 4)
                {
                    BlockShapes.MakeBlock(a, 73, City.edgeLength + 8, BlockInfo.Torch.ID, 2, 100, -1);
                }
            }

            
            for (int a = City.edgeLength + 16; a < (City.mapLength / 2); a += 24)
            {
                switch (City.npcs)
                {
                    case "Ghostdancer's NPCs":
                        EntityVillager eVillager;
                        eVillager = new EntityVillager(new TypedEntity("GKnight"));
                        eVillager.Health = 20;
                        BlockShapes.MakeEntity(a, 73, City.edgeLength + 8, eVillager, 2);
                        break;
                    case "Minecraft Villagers":
                        EntityMob eMob;
                        eMob = new EntityMob(new TypedEntity("VillagerGolem"));
                        eMob.Health = 100;
                        BlockShapes.MakeEntity(a, 73, City.edgeLength + 8, eMob, 2);
                        break;
                }                    
            }

            frmLogForm.UpdateLog("Creating wall emblems: " + City.cityEmblemType, true, true);
            MakeEmblem();
        }
        private static void MakeEmblem()
        {
            if (City.cityEmblemType.ToLower() != "none")
            {
                int intBlockyBlock = RNG.RandomItem(BlockInfo.GoldBlock.ID, BlockInfo.IronBlock.ID, BlockInfo.DiamondBlock.ID);
                string[] strEmblem;
                strEmblem = File.ReadAllLines(Path.Combine("Resources", "Emblem " + City.cityEmblemType + ".txt"));

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
                        BlockShapes.MakeBlock(((City.mapLength / 2) - (strLine.GetLength(0) + 6)) + x, 71 - y,
                                              City.edgeLength + 5, Convert.ToInt32(strSplit[0]), 2, 100,
                                              Convert.ToInt32(strSplit[1]));
                    }
                    for (int x = strLine.GetLength(0) + 1; x < strLine.GetLength(0) + 5; x++)
                    {
                        BlockShapes.MakeBlock((City.mapLength / 2) - (6 + x), 69,
                                              City.edgeLength + 5, BlockInfo.Air.ID, 2, 100, 0);
                        BlockShapes.MakeBlock((City.mapLength / 2) - (6 + x), 70,
                                              City.edgeLength + 5, BlockInfo.Air.ID, 2, 100, 0);
                    }
                }
            }
        }
    }
}