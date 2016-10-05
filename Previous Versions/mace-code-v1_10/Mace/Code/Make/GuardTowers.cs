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
            BlockShapes.MakeSolidBox(City.EdgeLength + 5, City.EdgeLength + 11, 64, 79,
                                     City.EdgeLength + 5, City.EdgeLength + 11, BlockInfo.Air.ID, 1);
            // add tower
            BlockShapes.MakeHollowBox(City.EdgeLength + 4, City.EdgeLength + 12, 63, 80,
                                      City.EdgeLength + 4, City.EdgeLength + 12, City.WallMaterialID, 1, City.WallMaterialData);
            // divide into two rooms
            BlockShapes.MakeSolidBoxWithData(City.EdgeLength + 4, City.EdgeLength + 12, 2, 72,
                                             City.EdgeLength + 4, City.EdgeLength + 12, 
                                             City.WallMaterialID, 1, City.WallMaterialData);
            BlockShapes.MakeSolidBox(City.EdgeLength + 5, City.EdgeLength + 11, 64, 67,
                                     City.EdgeLength + 5, City.EdgeLength + 11, BlockInfo.Air.ID, 1);

            switch (City.OutsideLightType)
            {
                case "Fire":
                    BlockShapes.MakeBlock(City.EdgeLength + 5, 76, City.EdgeLength + 3, BlockInfo.Netherrack.ID, 2, 100, -1);
                    BlockShapes.MakeBlock(City.EdgeLength + 5, 77, City.EdgeLength + 3, BlockInfo.Fire.ID, 2, 100, -1);
                    BlockShapes.MakeBlock(City.EdgeLength + 11, 76, City.EdgeLength + 3, BlockInfo.Netherrack.ID, 2, 100, -1);
                    BlockShapes.MakeBlock(City.EdgeLength + 11, 77, City.EdgeLength + 3, BlockInfo.Fire.ID, 2, 100, -1);
                    break;
                case "Torches":
                    for (int y = 73; y <= 80; y += 7)
                    {
                        BlockHelper.MakeTorch(City.EdgeLength + 6, y, City.EdgeLength + 3, City.WallMaterialID, 2);
                        BlockHelper.MakeTorch(City.EdgeLength + 10, y, City.EdgeLength + 3, City.WallMaterialID, 2);
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
                // add torches
                BlockHelper.MakeTorch(City.EdgeLength + 6, 79, City.EdgeLength + 13, City.WallMaterialID, 2);
                BlockHelper.MakeTorch(City.EdgeLength + 10, 79, City.EdgeLength + 13, City.WallMaterialID, 2);
                // add torches inside
                BlockHelper.MakeTorch(City.EdgeLength + 6, 77, City.EdgeLength + 11, City.WallMaterialID, 2);
                BlockHelper.MakeTorch(City.EdgeLength + 10, 77, City.EdgeLength + 11, City.WallMaterialID, 2);
                BlockHelper.MakeTorch(City.EdgeLength + 5, 77, City.EdgeLength + 6, City.WallMaterialID, 2);
                BlockHelper.MakeTorch(City.EdgeLength + 5, 77, City.EdgeLength + 10, City.WallMaterialID, 2);
            }
            // add openings to the walls
            BlockShapes.MakeBlock(City.EdgeLength + 7, 73, City.EdgeLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            BlockShapes.MakeBlock(City.EdgeLength + 9, 73, City.EdgeLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            BlockShapes.MakeBlock(City.EdgeLength + 7, 74, City.EdgeLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            BlockShapes.MakeBlock(City.EdgeLength + 9, 74, City.EdgeLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            // add blocks on top of the towers
            BlockShapes.MakeHollowLayers(City.EdgeLength + 4, City.EdgeLength + 12, 81, 81,
                                         City.EdgeLength + 4, City.EdgeLength + 12,
                                         City.WallMaterialID, 1, City.WallMaterialData);

            // alternating top blocks
            for (int x = City.EdgeLength + 4; x <= City.EdgeLength + 12; x += 2)
            {
                for (int z = City.EdgeLength + 4; z <= City.EdgeLength + 12; z += 2)
                {
                    if (x == City.EdgeLength + 4 ||
                        x == City.EdgeLength + 12 ||
                        z == City.EdgeLength + 4 ||
                        z == City.EdgeLength + 12)
                    {
                        BlockShapes.MakeBlock(x, 82, z, City.WallMaterialID, 1, 100, City.WallMaterialData);
                    }
                }
            }
            // add central columns
            BlockShapes.MakeSolidBoxWithData(City.EdgeLength + 8, City.EdgeLength + 8, 73, 82,
                                             City.EdgeLength + 8, City.EdgeLength + 8, City.WallMaterialID, 1,
                                             City.WallMaterialData);
            BlockHelper.MakeLadder(City.EdgeLength + 7, 73, 82, City.EdgeLength + 8, 2, City.WallMaterialID);
            BlockHelper.MakeLadder(City.EdgeLength + 9, 73, 82, City.EdgeLength + 8, 2, City.WallMaterialID);
            // add torches on the roof
            if (City.HasTorchesOnWalkways)
            {
                BlockShapes.MakeBlock(City.EdgeLength + 6, 81, City.EdgeLength + 6, BlockInfo.Torch.ID, 1, 100, -1);
                BlockShapes.MakeBlock(City.EdgeLength + 6, 81, City.EdgeLength + 10, BlockInfo.Torch.ID, 2, 100, -1);
                BlockShapes.MakeBlock(City.EdgeLength + 10, 81, City.EdgeLength + 10, BlockInfo.Torch.ID, 1, 100, -1);
            }
            // add cobwebs
            BlockShapes.MakeBlock(City.EdgeLength + 5, 79, City.EdgeLength + 5, BlockInfo.Cobweb.ID, 1, 30, -1);
            BlockShapes.MakeBlock(City.EdgeLength + 5, 79, City.EdgeLength + 11, BlockInfo.Cobweb.ID, 1, 30, -1);
            BlockShapes.MakeBlock(City.EdgeLength + 11, 79, City.EdgeLength + 5, BlockInfo.Cobweb.ID, 1, 30, -1);
            BlockShapes.MakeBlock(City.EdgeLength + 11, 79, City.EdgeLength + 11, BlockInfo.Cobweb.ID, 1, 30, -1);
            
            // add chests
            MakeGuardChest(bm, City.EdgeLength + 11, 73, City.EdgeLength + 11);
            MakeGuardChest(bm, City.MapLength - (City.EdgeLength + 11), 73, City.EdgeLength + 11);
            MakeGuardChest(bm, City.EdgeLength + 11, 73, City.MapLength - (City.EdgeLength + 11));
            MakeGuardChest(bm, City.MapLength - (City.EdgeLength + 11), 73, City.MapLength - (City.EdgeLength + 11));

            // add archery slots
            BlockShapes.MakeSolidBox(City.EdgeLength + 4, City.EdgeLength + 4, 74, 77,
                                     City.EdgeLength + 8, City.EdgeLength + 8, BlockInfo.Air.ID, 2);
            BlockShapes.MakeSolidBox(City.EdgeLength + 4, City.EdgeLength + 4, 76, 76,
                                     City.EdgeLength + 7, City.EdgeLength + 9, BlockInfo.Air.ID, 2);
            if (!City.HasWalls)
            {
                BlockHelper.MakeLadder(City.EdgeLength + 13, 64, 72, City.EdgeLength + 8, 2, City.WallMaterialID);
            }
            // include beds
            BlockHelper.MakeBed(City.EdgeLength + 5, City.EdgeLength + 6, 64, City.EdgeLength + 8, City.EdgeLength + 8, 2);
            BlockHelper.MakeBed(City.EdgeLength + 5, City.EdgeLength + 6, 64, City.EdgeLength + 10, City.EdgeLength + 10, 2);
            BlockHelper.MakeBed(City.EdgeLength + 11, City.EdgeLength + 10, 64, City.EdgeLength + 8, City.EdgeLength + 8, 2);
            // make columns to orientate torches
            BlockShapes.MakeSolidBox(City.EdgeLength + 5, City.EdgeLength + 5, 64, 73,
                                     City.EdgeLength + 5, City.EdgeLength + 5, BlockInfo.Wood.ID, 2);
            BlockShapes.MakeSolidBox(City.EdgeLength + 11, City.EdgeLength + 11, 64, 71,
                                     City.EdgeLength + 5, City.EdgeLength + 5, BlockInfo.Wood.ID, 2);
            BlockShapes.MakeSolidBox(City.EdgeLength + 5, City.EdgeLength + 5, 64, 71,
                                     City.EdgeLength + 11, City.EdgeLength + 11, BlockInfo.Wood.ID, 2);
            BlockShapes.MakeSolidBox(City.EdgeLength + 11, City.EdgeLength + 11, 64, 71,
                                     City.EdgeLength + 11, City.EdgeLength + 11, BlockInfo.Wood.ID, 2);
            // add ladders
            BlockHelper.MakeLadder(City.EdgeLength + 5, 64, 73, City.EdgeLength + 6, 2, BlockInfo.Wood.ID);
            // make torches
            if (City.HasTorchesOnWalkways)
            {
                BlockHelper.MakeTorch(City.EdgeLength + 10, 66, City.EdgeLength + 5, BlockInfo.Wood.ID, 2);
                BlockHelper.MakeTorch(City.EdgeLength + 6, 66, City.EdgeLength + 11, BlockInfo.Wood.ID, 2);
                BlockHelper.MakeTorch(City.EdgeLength + 10, 66, City.EdgeLength + 11, BlockInfo.Wood.ID, 2);
            }
            // make columns for real
            BlockShapes.MakeSolidBoxWithData(City.EdgeLength + 5, City.EdgeLength + 5, 64, 73, City.EdgeLength + 5,
                                             City.EdgeLength + 5, City.WallMaterialID, 2, City.WallMaterialData);
            BlockShapes.MakeSolidBoxWithData(City.EdgeLength + 11, City.EdgeLength + 11, 64, 71, City.EdgeLength + 5,
                                             City.EdgeLength + 5, City.WallMaterialID, 2, City.WallMaterialData);
            BlockShapes.MakeSolidBoxWithData(City.EdgeLength + 5, City.EdgeLength + 5, 64, 71, City.EdgeLength + 11,
                                             City.EdgeLength + 11, City.WallMaterialID, 2, City.WallMaterialData);
            BlockShapes.MakeSolidBoxWithData(City.EdgeLength + 11, City.EdgeLength + 11, 64, 71, City.EdgeLength + 11,
                                             City.EdgeLength + 11, City.WallMaterialID, 2, City.WallMaterialData);     
            // make cobwebs
            BlockShapes.MakeBlock(City.EdgeLength + 11, 67, City.EdgeLength + 8, BlockInfo.Cobweb.ID, 2, 75, -1);
            // make doors from the city to the guard tower
            BlockShapes.MakeBlock(City.EdgeLength + 11, 65, City.EdgeLength + 11, BlockInfo.GoldBlock.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.EdgeLength + 11, 64, City.EdgeLength + 11, BlockInfo.GoldBlock.ID, 1, 100, -1);
            BlockHelper.MakeDoor(City.EdgeLength + 11, 64, City.EdgeLength + 12, BlockInfo.GoldBlock.ID, true, 2);
            BlockShapes.MakeBlock(City.EdgeLength + 11, 65, City.EdgeLength + 11, BlockInfo.Air.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.EdgeLength + 11, 64, City.EdgeLength + 11, BlockInfo.Air.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.EdgeLength + 11, 64, City.EdgeLength + 11, BlockInfo.StonePlate.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.EdgeLength + 11, 64, City.EdgeLength + 13, BlockInfo.StonePlate.ID, 2, 100, -1);
            //BlockShapes.MakeBlock(City.intFarmSize + 13, 64, City.intFarmSize + 11, BlockInfo.Stone.ID_PLATE, 1, 100, -1);
            // add guard tower sign
            BlockHelper.MakeSign(City.EdgeLength + 12, 65, City.EdgeLength + 13, "~Guard Tower~~", City.WallMaterialID, 1);
            BlockHelper.MakeSign(City.EdgeLength + 8, 74, City.EdgeLength + 13, "~Guard Tower~~", City.WallMaterialID, 2);
            // make beacon
            frmLogForm.UpdateLog("Creating tower addition: " + City.TowersAdditionType, true, true);
            switch (City.TowersAdditionType)
            {
                case "Fire beacon":
                    BlockShapes.MakeSolidBoxWithData(City.EdgeLength + 8, City.EdgeLength + 8, 83, 84,
                                                     City.EdgeLength + 8, City.EdgeLength + 8,
                                                     City.WallMaterialID, 1, City.WallMaterialData);
                    BlockShapes.MakeSolidBoxWithData(City.EdgeLength + 6, City.EdgeLength + 10, 85, 85,
                                                     City.EdgeLength + 6, City.EdgeLength + 10,
                                                     City.WallMaterialID, 1, City.WallMaterialData);
                    BlockShapes.MakeSolidBox(City.EdgeLength + 6, City.EdgeLength + 10, 86, 86,
                                             City.EdgeLength + 6, City.EdgeLength + 10, BlockInfo.Netherrack.ID, 1);
                    BlockShapes.MakeSolidBox(City.EdgeLength + 6, City.EdgeLength + 10, 87, 87,
                                             City.EdgeLength + 6, City.EdgeLength + 10, BlockInfo.Fire.ID, 1);
                    break;
                case "Flag":
                    BlockShapes.MakeSolidBox(City.EdgeLength + 4, City.EdgeLength + 4, 83, 91,
                                             City.EdgeLength + 12, City.EdgeLength + 12, BlockInfo.Fence.ID, 2);
                    BlockShapes.MakeBlock(City.EdgeLength + 4, 84, City.EdgeLength + 13, BlockInfo.Fence.ID, 2, 100, 0);
                    BlockShapes.MakeBlock(City.EdgeLength + 4, 91, City.EdgeLength + 13, BlockInfo.Fence.ID, 2, 100, 0);
                    int[] intColours = RNG.ShuffleArray(Enumerable.Range(0, 15).ToArray());
                    // select a random flag file and turn it into an array
                    string[] strFlagLines = File.ReadAllLines(RNG.RandomItemFromArray(Directory.GetFiles("Resources", "Flag_*.txt")));
                    for (int x = 0; x < strFlagLines[0].Length; x++)
                    {
                        for (int y = 0; y < strFlagLines.GetLength(0); y++)
                        {
                            int WoolColourID = Convert.ToInt32(strFlagLines[y].Substring(x, 1));
                            BlockShapes.MakeSolidBoxWithData(City.EdgeLength + 4, City.EdgeLength + 4,
                                                             90 - y, 90 - y,
                                                             City.EdgeLength + 13 + x, City.EdgeLength + 13 + x,
                                                             BlockInfo.Wool.ID, 2, intColours[WoolColourID]);
                        }
                    }
                    break;
            }
        }
        private static void MakeGuardChest(BlockManager bm, int x, int y, int z)
        {
            TileEntityChest tec = new TileEntityChest();
            if (City.HasItemsInChests)
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
            bm.SetID(x, y, z, BlockInfo.Chest.ID);
            bm.SetTileEntity(x, y, z, tec);
        }
    }
}
