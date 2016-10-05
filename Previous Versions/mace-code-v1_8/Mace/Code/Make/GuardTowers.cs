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
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Diagnostics;
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
            BlockShapes.MakeSolidBox(City.FarmLength + 5, City.FarmLength + 11, 64, 79,
                                     City.FarmLength + 5, City.FarmLength + 11, BlockInfo.Air.ID, 1);
            // add tower
            BlockShapes.MakeHollowBox(City.FarmLength + 4, City.FarmLength + 12, 63, 80,
                                      City.FarmLength + 4, City.FarmLength + 12, City.WallMaterialID, 1, City.WallMaterialData);
            // divide into two rooms
            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 12, 2, 72,
                                             City.FarmLength + 4, City.FarmLength + 12, 
                                             City.WallMaterialID, 1, City.WallMaterialData);
            BlockShapes.MakeSolidBox(City.FarmLength + 5, City.FarmLength + 11, 64, 67,
                                     City.FarmLength + 5, City.FarmLength + 11, BlockInfo.Air.ID, 1);

            switch (City.OutsideLightType)
            {
                case "Fire":
                    BlockShapes.MakeBlock(City.FarmLength + 5, 76, City.FarmLength + 3, BlockInfo.Netherrack.ID, 2, 100, -1);
                    BlockShapes.MakeBlock(City.FarmLength + 5, 77, City.FarmLength + 3, BlockInfo.Fire.ID, 2, 100, -1);
                    BlockShapes.MakeBlock(City.FarmLength + 11, 76, City.FarmLength + 3, BlockInfo.Netherrack.ID, 2, 100, -1);
                    BlockShapes.MakeBlock(City.FarmLength + 11, 77, City.FarmLength + 3, BlockInfo.Fire.ID, 2, 100, -1);
                    break;
                case "Torches":
                    for (int y = 73; y <= 80; y += 7)
                    {
                        BlockHelper.MakeTorch(City.FarmLength + 6, y, City.FarmLength + 3, City.WallMaterialID, 2);
                        BlockHelper.MakeTorch(City.FarmLength + 10, y, City.FarmLength + 3, City.WallMaterialID, 2);
                    }
                    break;
                case "None":
                    break;
                default:
                    Debug.Fail("Invalid switch result");
                    break;
            }
            // add torches
            BlockHelper.MakeTorch(City.FarmLength + 6, 79, City.FarmLength + 13, City.WallMaterialID, 2);
            BlockHelper.MakeTorch(City.FarmLength + 10, 79, City.FarmLength + 13, City.WallMaterialID, 2);
            // add torches inside
            BlockHelper.MakeTorch(City.FarmLength + 6, 77, City.FarmLength + 11, City.WallMaterialID, 2);
            BlockHelper.MakeTorch(City.FarmLength + 10, 77, City.FarmLength + 11, City.WallMaterialID, 2);
            BlockHelper.MakeTorch(City.FarmLength + 5, 77, City.FarmLength + 6, City.WallMaterialID, 2);
            BlockHelper.MakeTorch(City.FarmLength + 5, 77, City.FarmLength + 10, City.WallMaterialID, 2);
            // add openings to the walls
            BlockShapes.MakeBlock(City.FarmLength + 7, 73, City.FarmLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            BlockShapes.MakeBlock(City.FarmLength + 9, 73, City.FarmLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            BlockShapes.MakeBlock(City.FarmLength + 7, 74, City.FarmLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            BlockShapes.MakeBlock(City.FarmLength + 9, 74, City.FarmLength + 12, BlockInfo.Air.ID, 2, 100, -1);
            // add blocks on top of the towers
            BlockShapes.MakeHollowLayers(City.FarmLength + 4, City.FarmLength + 12, 81, 81,
                                         City.FarmLength + 4, City.FarmLength + 12,
                                         City.WallMaterialID, 1, City.WallMaterialData);

            // alternating top blocks
            for (int x = City.FarmLength + 4; x <= City.FarmLength + 12; x += 2)
            {
                for (int z = City.FarmLength + 4; z <= City.FarmLength + 12; z += 2)
                {
                    if (x == City.FarmLength + 4 ||
                        x == City.FarmLength + 12 ||
                        z == City.FarmLength + 4 ||
                        z == City.FarmLength + 12)
                    {
                        BlockShapes.MakeBlock(x, 82, z, City.WallMaterialID, 1, 100, City.WallMaterialData);
                    }
                }
            }
            // add central columns
            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 8, City.FarmLength + 8, 73, 82,
                                             City.FarmLength + 8, City.FarmLength + 8, City.WallMaterialID, 1,
                                             City.WallMaterialData);
            BlockHelper.MakeLadder(City.FarmLength + 7, 73, 82, City.FarmLength + 8, 2, City.WallMaterialID);
            BlockHelper.MakeLadder(City.FarmLength + 9, 73, 82, City.FarmLength + 8, 2, City.WallMaterialID);
            // add torches on the roof
            BlockShapes.MakeBlock(City.FarmLength + 6, 81, City.FarmLength + 6, BlockInfo.Torch.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.FarmLength + 6, 81, City.FarmLength + 10, BlockInfo.Torch.ID, 2, 100, -1);
            BlockShapes.MakeBlock(City.FarmLength + 10, 81, City.FarmLength + 10, BlockInfo.Torch.ID, 1, 100, -1);
            // add cobwebs
            BlockShapes.MakeBlock(City.FarmLength + 5, 79, City.FarmLength + 5, BlockInfo.Cobweb.ID, 1, 30, -1);
            BlockShapes.MakeBlock(City.FarmLength + 5, 79, City.FarmLength + 11, BlockInfo.Cobweb.ID, 1, 30, -1);
            BlockShapes.MakeBlock(City.FarmLength + 11, 79, City.FarmLength + 5, BlockInfo.Cobweb.ID, 1, 30, -1);
            BlockShapes.MakeBlock(City.FarmLength + 11, 79, City.FarmLength + 11, BlockInfo.Cobweb.ID, 1, 30, -1);
            
            // add chests
            MakeGuardChest(bm, City.FarmLength + 11, 73, City.FarmLength + 11);
            MakeGuardChest(bm, City.MapLength - (City.FarmLength + 11), 73, City.FarmLength + 11);
            MakeGuardChest(bm, City.FarmLength + 11, 73, City.MapLength - (City.FarmLength + 11));
            MakeGuardChest(bm, City.MapLength - (City.FarmLength + 11), 73, City.MapLength - (City.FarmLength + 11));

            // add archery slots
            BlockShapes.MakeSolidBox(City.FarmLength + 4, City.FarmLength + 4, 74, 77,
                                     City.FarmLength + 8, City.FarmLength + 8, BlockInfo.Air.ID, 2);
            BlockShapes.MakeSolidBox(City.FarmLength + 4, City.FarmLength + 4, 76, 76,
                                     City.FarmLength + 7, City.FarmLength + 9, BlockInfo.Air.ID, 2);
            if (!City.HasWalls)
            {
                BlockHelper.MakeLadder(City.FarmLength + 13, 64, 72, City.FarmLength + 8, 2, City.WallMaterialID);
            }
            // include beds
            BlockHelper.MakeBed(City.FarmLength + 5, City.FarmLength + 6, 64, City.FarmLength + 8, City.FarmLength + 8, 2);
            BlockHelper.MakeBed(City.FarmLength + 5, City.FarmLength + 6, 64, City.FarmLength + 10, City.FarmLength + 10, 2);
            BlockHelper.MakeBed(City.FarmLength + 11, City.FarmLength + 10, 64, City.FarmLength + 8, City.FarmLength + 8, 2);
            // make columns to orientate torches
            BlockShapes.MakeSolidBox(City.FarmLength + 5, City.FarmLength + 5, 64, 73,
                                     City.FarmLength + 5, City.FarmLength + 5, BlockInfo.Wood.ID, 2);
            BlockShapes.MakeSolidBox(City.FarmLength + 11, City.FarmLength + 11, 64, 71,
                                     City.FarmLength + 5, City.FarmLength + 5, BlockInfo.Wood.ID, 2);
            BlockShapes.MakeSolidBox(City.FarmLength + 5, City.FarmLength + 5, 64, 71,
                                     City.FarmLength + 11, City.FarmLength + 11, BlockInfo.Wood.ID, 2);
            BlockShapes.MakeSolidBox(City.FarmLength + 11, City.FarmLength + 11, 64, 71,
                                     City.FarmLength + 11, City.FarmLength + 11, BlockInfo.Wood.ID, 2);
            // add ladders
            BlockHelper.MakeLadder(City.FarmLength + 5, 64, 73, City.FarmLength + 6, 2, BlockInfo.Wood.ID);
            // make torches
            BlockHelper.MakeTorch(City.FarmLength + 10, 66, City.FarmLength + 5, BlockInfo.Wood.ID, 2);
            BlockHelper.MakeTorch(City.FarmLength + 6, 66, City.FarmLength + 11, BlockInfo.Wood.ID, 2);
            BlockHelper.MakeTorch(City.FarmLength + 10, 66, City.FarmLength + 11, BlockInfo.Wood.ID, 2);
            // make columns for real
            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 5, City.FarmLength + 5, 64, 73, City.FarmLength + 5,
                                             City.FarmLength + 5, City.WallMaterialID, 2, City.WallMaterialData);
            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 11, City.FarmLength + 11, 64, 71, City.FarmLength + 5,
                                             City.FarmLength + 5, City.WallMaterialID, 2, City.WallMaterialData);
            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 5, City.FarmLength + 5, 64, 71, City.FarmLength + 11,
                                             City.FarmLength + 11, City.WallMaterialID, 2, City.WallMaterialData);
            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 11, City.FarmLength + 11, 64, 71, City.FarmLength + 11,
                                             City.FarmLength + 11, City.WallMaterialID, 2, City.WallMaterialData);     
            // make cobwebs
            BlockShapes.MakeBlock(City.FarmLength + 11, 67, City.FarmLength + 8, BlockInfo.Cobweb.ID, 2, 75, -1);
            // make doors from the city to the guard tower
            BlockShapes.MakeBlock(City.FarmLength + 11, 65, City.FarmLength + 11, BlockInfo.GoldBlock.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.FarmLength + 11, 64, City.FarmLength + 11, BlockInfo.GoldBlock.ID, 1, 100, -1);
            BlockHelper.MakeDoor(City.FarmLength + 11, 64, City.FarmLength + 12, BlockInfo.GoldBlock.ID, true, 2);
            BlockShapes.MakeBlock(City.FarmLength + 11, 65, City.FarmLength + 11, BlockInfo.Air.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.FarmLength + 11, 64, City.FarmLength + 11, BlockInfo.Air.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.FarmLength + 11, 64, City.FarmLength + 11, BlockInfo.StonePlate.ID, 1, 100, -1);
            BlockShapes.MakeBlock(City.FarmLength + 11, 64, City.FarmLength + 13, BlockInfo.StonePlate.ID, 2, 100, -1);
            //BlockShapes.MakeBlock(City.intFarmSize + 13, 64, City.intFarmSize + 11, BlockInfo.Stone.ID_PLATE, 1, 100, -1);
            // add guard tower sign
            BlockHelper.MakeSign(City.FarmLength + 12, 65, City.FarmLength + 13, "~Guard Tower~~", City.WallMaterialID, 1);
            BlockHelper.MakeSign(City.FarmLength + 8, 74, City.FarmLength + 13, "~Guard Tower~~", City.WallMaterialID, 2);
            // make beacon
            frmLogForm.UpdateLog("Creating tower addition: " + City.TowersAdditionType, true, true);
            switch (City.TowersAdditionType)
            {
                case "Fire beacon":
                    BlockShapes.MakeSolidBoxWithData(City.FarmLength + 8, City.FarmLength + 8, 83, 84,
                                                     City.FarmLength + 8, City.FarmLength + 8,
                                                     City.WallMaterialID, 1, City.WallMaterialData);
                    BlockShapes.MakeSolidBoxWithData(City.FarmLength + 6, City.FarmLength + 10, 85, 85,
                                                     City.FarmLength + 6, City.FarmLength + 10,
                                                     City.WallMaterialID, 1, City.WallMaterialData);
                    BlockShapes.MakeSolidBox(City.FarmLength + 6, City.FarmLength + 10, 86, 86,
                                             City.FarmLength + 6, City.FarmLength + 10, BlockInfo.Netherrack.ID, 1);
                    BlockShapes.MakeSolidBox(City.FarmLength + 6, City.FarmLength + 10, 87, 87,
                                             City.FarmLength + 6, City.FarmLength + 10, BlockInfo.Fire.ID, 1);
                    break;
                case "Flag":
                    BlockShapes.MakeSolidBox(City.FarmLength + 4, City.FarmLength + 4, 83, 91,
                                             City.FarmLength + 12, City.FarmLength + 12, BlockInfo.Fence.ID, 2);
                    BlockShapes.MakeBlock(City.FarmLength + 4, 84, City.FarmLength + 13, BlockInfo.Fence.ID, 2, 100, 0);
                    BlockShapes.MakeBlock(City.FarmLength + 4, 91, City.FarmLength + 13, BlockInfo.Fence.ID, 2, 100, 0);
                    int[] intColours = RandomHelper.ShuffleArray(Enumerable.Range(0, 15).ToArray());
                    switch (RandomHelper.Next(8))
                    {
                        case 0: //2vert
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 13, City.FarmLength + 17, BlockInfo.Wool.ID, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 18, City.FarmLength + 22, BlockInfo.Wool.ID, 2, intColours[1]);
                            break;
                        case 1: //3vert
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 13, City.FarmLength + 15, BlockInfo.Wool.ID, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 16, City.FarmLength + 18, BlockInfo.Wool.ID, 2, intColours[1]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 19, City.FarmLength + 21, BlockInfo.Wool.ID, 2, intColours[2]);
                            break;
                        case 2: //4vert
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 13, City.FarmLength + 14, BlockInfo.Wool.ID, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 15, City.FarmLength + 16, BlockInfo.Wool.ID, 2, intColours[1]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 17, City.FarmLength + 18, BlockInfo.Wool.ID, 2, intColours[2]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 19, City.FarmLength + 20, BlockInfo.Wool.ID, 2, intColours[3]);
                            break;
                        case 3: //2horiz
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 87,
                                City.FarmLength + 13, City.FarmLength + 22, BlockInfo.Wool.ID, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 88, 90,
                                City.FarmLength + 13, City.FarmLength + 22, BlockInfo.Wool.ID, 2, intColours[1]);
                            break;
                        case 4: //3horiz
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 86,
                                City.FarmLength + 13, City.FarmLength + 22, BlockInfo.Wool.ID, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 87, 88,
                                City.FarmLength + 13, City.FarmLength + 22, BlockInfo.Wool.ID, 2, intColours[1]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 89, 90,
                                City.FarmLength + 13, City.FarmLength + 22, BlockInfo.Wool.ID, 2, intColours[2]);
                            break;
                        case 5: //quarters
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 87,
                                City.FarmLength + 13, City.FarmLength + 17, BlockInfo.Wool.ID, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 87,
                                City.FarmLength + 18, City.FarmLength + 22, BlockInfo.Wool.ID, 2, intColours[1]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 88, 90,
                                City.FarmLength + 13, City.FarmLength + 17, BlockInfo.Wool.ID, 2, intColours[2]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 88, 90,
                                City.FarmLength + 18, City.FarmLength + 22, BlockInfo.Wool.ID, 2, intColours[3]);
                            break;
                        case 6: //cross
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 13, City.FarmLength + 22, BlockInfo.Wool.ID, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 87, 88,
                                City.FarmLength + 13, City.FarmLength + 22, BlockInfo.Wool.ID, 2, intColours[1]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 17, City.FarmLength + 18, BlockInfo.Wool.ID, 2, intColours[1]);
                            break;
                        default: // inside "circle"
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 85, 90,
                                City.FarmLength + 13, City.FarmLength + 22, BlockInfo.Wool.ID, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 86, 89,
                                City.FarmLength + 17, City.FarmLength + 18, BlockInfo.Wool.ID, 2, intColours[1]);
                            BlockShapes.MakeSolidBoxWithData(City.FarmLength + 4, City.FarmLength + 4, 87, 88,
                                City.FarmLength + 16, City.FarmLength + 19, BlockInfo.Wool.ID, 2, intColours[1]);
                            break;
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
                    tec.Items[a] = BlockHelper.MakeItem(RandomHelper.RandomNumber(ItemInfo.IronSword.ID,
                                                                                  ItemInfo.WoodenSword.ID,
                                                                                  ItemInfo.StoneSword.ID), 1);
                }
                tec.Items[6] = BlockHelper.MakeItem(ItemInfo.Bow.ID, 1);
                tec.Items[7] = BlockHelper.MakeItem(ItemInfo.Arrow.ID, 64);
                int intArmourStartID = RandomHelper.RandomNumber(ItemInfo.LeatherCap.ID,
                                                                 ItemInfo.ChainHelmet.ID,
                                                                 ItemInfo.IronHelmet.ID);
                for (int a = 9; a < 18; a++)
                {
                    // random armour
                    tec.Items[a] = BlockHelper.MakeItem(intArmourStartID + RandomHelper.Next(4), 1);
                }
            }
            bm.SetID(x, y, z, BlockInfo.Chest.ID);
            bm.SetTileEntity(x, y, z, tec);
        }
    }
}
