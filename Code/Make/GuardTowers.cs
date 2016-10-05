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
using System.Linq;
using Substrate;
using Substrate.TileEntities;
using Substrate.Entities;

namespace Mace
{
    static class GuardTowers
    {
        public static void MakeGuardTowers(BlockManager bm, frmMace frmLogForm)
        {
            // remove wall
            BlockShapes.MakeSolidBox(City.edgeLength + 5, City.edgeLength + 11, 64, 79,
                                     City.edgeLength + 5, City.edgeLength + 11, BlockInfo.Air.ID, 1);
            // add tower
            BlockShapes.MakeHollowBox(City.edgeLength + 4, City.edgeLength + 12, 63, 80,
                                      City.edgeLength + 4, City.edgeLength + 12, City.wallMaterialID, 1, City.wallMaterialData);
            // divide into two rooms
            BlockShapes.MakeSolidBoxWithData(City.edgeLength + 4, City.edgeLength + 12, 2, 72,
                                             City.edgeLength + 4, City.edgeLength + 12, 
                                             City.wallMaterialID, 1, City.wallMaterialData);
            BlockShapes.MakeSolidBox(City.edgeLength + 5, City.edgeLength + 11, 64, 67,
                                     City.edgeLength + 5, City.edgeLength + 11, BlockInfo.Air.ID, 1);

            switch (City.outsideLightType)
            {
                case "Fire":
                    BlockShapes.MakeBlock(City.edgeLength + 5, 76, City.edgeLength + 3, BlockInfo.Netherrack.ID, 2, 100, -1);
                    BlockShapes.MakeBlock(City.edgeLength + 5, 77, City.edgeLength + 3, BlockInfo.Fire.ID, 2, 100, -1);
                    BlockShapes.MakeBlock(City.edgeLength + 11, 76, City.edgeLength + 3, BlockInfo.Netherrack.ID, 2, 100, -1);
                    BlockShapes.MakeBlock(City.edgeLength + 11, 77, City.edgeLength + 3, BlockInfo.Fire.ID, 2, 100, -1);
                    break;
                case "Torches":
                    for (int y = 73; y <= 80; y += 7)
                    {
                        BlockHelper.MakeTorch(City.edgeLength + 6, y, City.edgeLength + 3, City.wallMaterialID, 2);
                        BlockHelper.MakeTorch(City.edgeLength + 10, y, City.edgeLength + 3, City.wallMaterialID, 2);
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
                // add torches
                BlockHelper.MakeTorch(City.edgeLength + 6, 79, City.edgeLength + 13, City.wallMaterialID, 2);
                BlockHelper.MakeTorch(City.edgeLength + 10, 79, City.edgeLength + 13, City.wallMaterialID, 2);
                // add torches inside
                BlockHelper.MakeTorch(City.edgeLength + 6, 77, City.edgeLength + 11, City.wallMaterialID, 2);
                BlockHelper.MakeTorch(City.edgeLength + 10, 77, City.edgeLength + 11, City.wallMaterialID, 2);
                BlockHelper.MakeTorch(City.edgeLength + 5, 77, City.edgeLength + 6, City.wallMaterialID, 2);
                BlockHelper.MakeTorch(City.edgeLength + 5, 77, City.edgeLength + 10, City.wallMaterialID, 2);
            }
            // add openings to the walls
            BlockShapes.MakeBlock(City.edgeLength + 7, 73, City.edgeLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            BlockShapes.MakeBlock(City.edgeLength + 9, 73, City.edgeLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            BlockShapes.MakeBlock(City.edgeLength + 7, 74, City.edgeLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            BlockShapes.MakeBlock(City.edgeLength + 9, 74, City.edgeLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            // add blocks on top of the towers
            BlockShapes.MakeHollowLayers(City.edgeLength + 4, City.edgeLength + 12, 81, 81,
                                         City.edgeLength + 4, City.edgeLength + 12,
                                         City.wallMaterialID, 1, City.wallMaterialData);

            // alternating top blocks
            for (int x = City.edgeLength + 4; x <= City.edgeLength + 12; x += 2)
            {
                for (int z = City.edgeLength + 4; z <= City.edgeLength + 12; z += 2)
                {
                    if (x == City.edgeLength + 4 ||
                        x == City.edgeLength + 12 ||
                        z == City.edgeLength + 4 ||
                        z == City.edgeLength + 12)
                    {
                        BlockShapes.MakeBlock(x, 82, z, City.wallMaterialID, 1, 100, City.wallMaterialData);
                    }
                }
            }
            // add central columns
            BlockShapes.MakeSolidBoxWithData(City.edgeLength + 8, City.edgeLength + 8, 73, 82,
                                             City.edgeLength + 8, City.edgeLength + 8, City.wallMaterialID, 1,
                                             City.wallMaterialData);
            BlockHelper.MakeLadder(City.edgeLength + 7, 73, 82, City.edgeLength + 8, 2, City.wallMaterialID);
            BlockHelper.MakeLadder(City.edgeLength + 9, 73, 82, City.edgeLength + 8, 2, City.wallMaterialID);
            // add torches on the roof
            if (City.hasTorchesOnWalkways)
            {
                BlockShapes.MakeBlock(City.edgeLength + 6, 81, City.edgeLength + 6, BlockInfo.Torch.ID, 1, 100, -1);
                BlockShapes.MakeBlock(City.edgeLength + 6, 81, City.edgeLength + 10, BlockInfo.Torch.ID, 2, 100, -1);
                BlockShapes.MakeBlock(City.edgeLength + 10, 81, City.edgeLength + 10, BlockInfo.Torch.ID, 1, 100, -1);
            }
            // add cobwebs
            BlockShapes.MakeBlock(City.edgeLength + 5, 79, City.edgeLength + 5, BlockInfo.Cobweb.ID, 1, 30, -1);
            BlockShapes.MakeBlock(City.edgeLength + 5, 79, City.edgeLength + 11, BlockInfo.Cobweb.ID, 1, 30, -1);
            BlockShapes.MakeBlock(City.edgeLength + 11, 79, City.edgeLength + 5, BlockInfo.Cobweb.ID, 1, 30, -1);
            BlockShapes.MakeBlock(City.edgeLength + 11, 79, City.edgeLength + 11, BlockInfo.Cobweb.ID, 1, 30, -1);
            
            MakeGuardChest(bm, City.edgeLength + 11, 73, City.edgeLength + 11);
            MakeGuardChest(bm, City.mapLength - (City.edgeLength + 11), 73, City.edgeLength + 11);
            MakeGuardChest(bm, City.edgeLength + 11, 73, City.mapLength - (City.edgeLength + 11));
            MakeGuardChest(bm, City.mapLength - (City.edgeLength + 11), 73, City.mapLength - (City.edgeLength + 11));

            // add archery slots
            BlockShapes.MakeSolidBox(City.edgeLength + 4, City.edgeLength + 4, 74, 77,
                                     City.edgeLength + 8, City.edgeLength + 8, BlockInfo.Air.ID, 2);
            BlockShapes.MakeSolidBox(City.edgeLength + 4, City.edgeLength + 4, 76, 76,
                                     City.edgeLength + 7, City.edgeLength + 9, BlockInfo.Air.ID, 2);
            if (!City.hasWalls)
            {
                BlockHelper.MakeLadder(City.edgeLength + 13, 64, 72, City.edgeLength + 8, 2, City.wallMaterialID);
            }
            // include beds
            BlockHelper.MakeBed(City.edgeLength + 5, City.edgeLength + 6, 64, City.edgeLength + 8, City.edgeLength + 8, 2);
            BlockHelper.MakeBed(City.edgeLength + 5, City.edgeLength + 6, 64, City.edgeLength + 10, City.edgeLength + 10, 2);
            BlockHelper.MakeBed(City.edgeLength + 11, City.edgeLength + 10, 64, City.edgeLength + 8, City.edgeLength + 8, 2);
            // make columns to orientate torches
            BlockShapes.MakeSolidBox(City.edgeLength + 5, City.edgeLength + 5, 64, 73,
                                     City.edgeLength + 5, City.edgeLength + 5, BlockInfo.Wood.ID, 2);
            BlockShapes.MakeSolidBox(City.edgeLength + 11, City.edgeLength + 11, 64, 71,
                                     City.edgeLength + 5, City.edgeLength + 5, BlockInfo.Wood.ID, 2);
            BlockShapes.MakeSolidBox(City.edgeLength + 5, City.edgeLength + 5, 64, 71,
                                     City.edgeLength + 11, City.edgeLength + 11, BlockInfo.Wood.ID, 2);
            BlockShapes.MakeSolidBox(City.edgeLength + 11, City.edgeLength + 11, 64, 71,
                                     City.edgeLength + 11, City.edgeLength + 11, BlockInfo.Wood.ID, 2);
            // add ladders
            BlockHelper.MakeLadder(City.edgeLength + 5, 64, 73, City.edgeLength + 6, 2, BlockInfo.Wood.ID);
            // make torches
            if (City.hasTorchesOnWalkways)
            {
                BlockHelper.MakeTorch(City.edgeLength + 10, 66, City.edgeLength + 5, BlockInfo.Wood.ID, 2);
                BlockHelper.MakeTorch(City.edgeLength + 6, 66, City.edgeLength + 11, BlockInfo.Wood.ID, 2);
                BlockHelper.MakeTorch(City.edgeLength + 10, 66, City.edgeLength + 11, BlockInfo.Wood.ID, 2);
            }
            // make columns for real
            BlockShapes.MakeSolidBoxWithData(City.edgeLength + 5, City.edgeLength + 5, 64, 73, City.edgeLength + 5,
                                             City.edgeLength + 5, City.wallMaterialID, 2, City.wallMaterialData);
            BlockShapes.MakeSolidBoxWithData(City.edgeLength + 11, City.edgeLength + 11, 64, 71, City.edgeLength + 5,
                                             City.edgeLength + 5, City.wallMaterialID, 2, City.wallMaterialData);
            BlockShapes.MakeSolidBoxWithData(City.edgeLength + 5, City.edgeLength + 5, 64, 71, City.edgeLength + 11,
                                             City.edgeLength + 11, City.wallMaterialID, 2, City.wallMaterialData);
            BlockShapes.MakeSolidBoxWithData(City.edgeLength + 11, City.edgeLength + 11, 64, 71, City.edgeLength + 11,
                                             City.edgeLength + 11, City.wallMaterialID, 2, City.wallMaterialData);     
            // make cobwebs
            BlockShapes.MakeBlock(City.edgeLength + 11, 67, City.edgeLength + 8, BlockInfo.Cobweb.ID, 2, 75, -1);
            // make doors from the city to the guard tower
            BlockShapes.MakeBlock(City.edgeLength + 11, 65, City.edgeLength + 11, BlockInfo.GoldBlock.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.edgeLength + 11, 64, City.edgeLength + 11, BlockInfo.GoldBlock.ID, 1, 100, -1);
            BlockHelper.MakeDoor(City.edgeLength + 11, 64, City.edgeLength + 12, BlockInfo.GoldBlock.ID, true, 2);
            BlockShapes.MakeBlock(City.edgeLength + 11, 65, City.edgeLength + 11, BlockInfo.Air.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.edgeLength + 11, 64, City.edgeLength + 11, BlockInfo.Air.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.edgeLength + 11, 64, City.edgeLength + 11, BlockInfo.StonePlate.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.edgeLength + 11, 64, City.edgeLength + 13, BlockInfo.StonePlate.ID, 2, 100, -1);
            //BlockShapes.MakeBlock(City.intFarmSize + 13, 64, City.intFarmSize + 11, BlockInfo.Stone.ID_PLATE, 1, 100, -1);
            // add guard tower sign
            BlockHelper.MakeSign(City.edgeLength + 12, 65, City.edgeLength + 13, "~Guard Tower~~", City.wallMaterialID, 1);
            BlockHelper.MakeSign(City.edgeLength + 8, 74, City.edgeLength + 13, "~Guard Tower~~", City.wallMaterialID, 2);
            // make beacon
            frmLogForm.UpdateLog("Creating tower addition: " + City.towersAdditionType, true, true);
            switch (City.towersAdditionType)
            {
                case "Fire beacon":
                    BlockShapes.MakeSolidBoxWithData(City.edgeLength + 8, City.edgeLength + 8, 83, 84,
                                                     City.edgeLength + 8, City.edgeLength + 8,
                                                     City.wallMaterialID, 1, City.wallMaterialData);
                    BlockShapes.MakeSolidBoxWithData(City.edgeLength + 6, City.edgeLength + 10, 85, 85,
                                                     City.edgeLength + 6, City.edgeLength + 10,
                                                     City.wallMaterialID, 1, City.wallMaterialData);
                    BlockShapes.MakeSolidBox(City.edgeLength + 6, City.edgeLength + 10, 86, 86,
                                             City.edgeLength + 6, City.edgeLength + 10, BlockInfo.Netherrack.ID, 1);
                    BlockShapes.MakeSolidBox(City.edgeLength + 6, City.edgeLength + 10, 87, 87,
                                             City.edgeLength + 6, City.edgeLength + 10, BlockInfo.Fire.ID, 1);
                    break;
                case "Flag":
                    BlockShapes.MakeSolidBox(City.edgeLength + 4, City.edgeLength + 4, 83, 91,
                                             City.edgeLength + 12, City.edgeLength + 12, BlockInfo.Fence.ID, 2);
                    BlockShapes.MakeBlock(City.edgeLength + 4, 84, City.edgeLength + 13, BlockInfo.Fence.ID, 2, 100, 0);
                    BlockShapes.MakeBlock(City.edgeLength + 4, 91, City.edgeLength + 13, BlockInfo.Fence.ID, 2, 100, 0);
                    int[] intColours = RNG.ShuffleArray(Enumerable.Range(0, 15).ToArray());
                    // select a random flag file and turn it into an array
                    string[] strFlagLines = File.ReadAllLines(RNG.RandomItemFromArray(Directory.GetFiles("Resources", "Flag_*.txt")));
                    for (int x = 0; x < strFlagLines[0].Length; x++)
                    {
                        for (int y = 0; y < strFlagLines.GetLength(0); y++)
                        {
                            int WoolColourID = Convert.ToInt32(strFlagLines[y].Substring(x, 1));
                            BlockShapes.MakeSolidBoxWithData(City.edgeLength + 4, City.edgeLength + 4,
                                                             90 - y, 90 - y,
                                                             City.edgeLength + 13 + x, City.edgeLength + 13 + x,
                                                             BlockInfo.Wool.ID, 2, intColours[WoolColourID]);
                        }
                    }
                    break;
            }
        }
        private static void MakeGuardChest(BlockManager bm, int x, int y, int z)
        {
            TileEntityChest tec = new TileEntityChest();
            if (City.hasItemsInChests)
            {
                for (int a = 0; a < 5; a++)
                {
                    tec.Items[a] = BlockHelper.MakeItem(RNG.RandomItem(ItemInfo.IronSword.ID,
                                                                                  ItemInfo.WoodenSword.ID,
                                                                                  ItemInfo.StoneSword.ID), 1);
                }
                tec.Items[6] = BlockHelper.MakeItem(ItemInfo.Bow.ID, 1);
                tec.Items[7] = BlockHelper.MakeItem(ItemInfo.Arrow.ID, 64);
                int intArmourStartID = RNG.RandomItem(ItemInfo.LeatherCap.ID,
                                                                 ItemInfo.ChainHelmet.ID,
                                                                 ItemInfo.IronHelmet.ID);
                for (int a = 9; a < 18; a++)
                {
                    // random armour
                    tec.Items[a] = BlockHelper.MakeItem(intArmourStartID + RNG.Next(4), 1);
                }
            }
            //BlockHelper.MakeChest(x, y, z, City.wallMaterialID, tec, 0);
            BlockHelper.MakeChest(x, y, z, City.wallMaterialID, tec, 0);
            //bm.SetID(x, y, z, BlockInfo.Chest.ID);
            //bm.SetTileEntity(x, y, z, tec);
        }
    }
}
