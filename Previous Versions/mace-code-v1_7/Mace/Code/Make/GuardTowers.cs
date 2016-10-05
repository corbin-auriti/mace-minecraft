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
using Substrate;
using Substrate.TileEntities;
using Substrate.Entities;

namespace Mace
{
    static class GuardTowers
    {
        public static void MakeGuardTowers(BlockManager bm, int intFarmLength, int intMapLength, bool booIncludeWalls,
                                           string strOutsideLights, string strTowerAddition,
                                           bool booIncludeItemsInChests, int intWallMaterial)
        {
            // remove wall
            BlockShapes.MakeSolidBox(intFarmLength + 5, intFarmLength + 11, 64, 79,
                                     intFarmLength + 5, intFarmLength + 11, BlockType.AIR, 1);
            // add tower
            BlockShapes.MakeHollowBox(intFarmLength + 4, intFarmLength + 12, 63, 80,
                                      intFarmLength + 4, intFarmLength + 12, intWallMaterial, 1, -1);
            // divide into two rooms
            BlockShapes.MakeSolidBox(intFarmLength + 4, intFarmLength + 12, 2, 72,
                                     intFarmLength + 4, intFarmLength + 12, intWallMaterial, 1);
            BlockShapes.MakeSolidBox(intFarmLength + 5, intFarmLength + 11, 64, 67,
                                     intFarmLength + 5, intFarmLength + 11, BlockType.AIR, 1);
            switch (strOutsideLights)
            {
                case "Fire":
                    BlockShapes.MakeBlock(intFarmLength + 5, 76, intFarmLength + 3, BlockType.NETHERRACK, 2, 100, -1);
                    BlockShapes.MakeBlock(intFarmLength + 5, 77, intFarmLength + 3, BlockType.FIRE, 2, 100, -1);
                    BlockShapes.MakeBlock(intFarmLength + 11, 76, intFarmLength + 3, BlockType.NETHERRACK, 2, 100, -1);
                    BlockShapes.MakeBlock(intFarmLength + 11, 77, intFarmLength + 3, BlockType.FIRE, 2, 100, -1);
                    break;
                case "Torches":
                    for (int y = 73; y <= 80; y += 7)
                    {
                        BlockHelper.MakeTorch(intFarmLength + 6, y, intFarmLength + 3, intWallMaterial, 2);
                        BlockHelper.MakeTorch(intFarmLength + 10, y, intFarmLength + 3, intWallMaterial, 2);
                    }
                    break;
                case "None":
                    break;
                default:
                    Debug.Fail("Invalid switch result");
                    break;
            }
            // add torches
            BlockHelper.MakeTorch(intFarmLength + 6, 79, intFarmLength + 13, intWallMaterial, 2);
            BlockHelper.MakeTorch(intFarmLength + 10, 79, intFarmLength + 13, intWallMaterial, 2);
            // add torches inside
            BlockHelper.MakeTorch(intFarmLength + 6, 77, intFarmLength + 11, intWallMaterial, 2);
            BlockHelper.MakeTorch(intFarmLength + 10, 77, intFarmLength + 11, intWallMaterial, 2);
            BlockHelper.MakeTorch(intFarmLength + 5, 77, intFarmLength + 6, intWallMaterial, 2);
            BlockHelper.MakeTorch(intFarmLength + 5, 77, intFarmLength + 10, intWallMaterial, 2);
            // add openings to the walls
            BlockShapes.MakeBlock(intFarmLength + 7, 73, intFarmLength + 12, BlockType.AIR, 2, 100, -1);
            BlockShapes.MakeBlock(intFarmLength + 9, 73, intFarmLength + 12, BlockType.AIR, 2, 100, -1);
            BlockShapes.MakeBlock(intFarmLength + 7, 74, intFarmLength + 12, BlockType.AIR, 2, 100, -1);
            BlockShapes.MakeBlock(intFarmLength + 9, 74, intFarmLength + 12, BlockType.AIR, 2, 100, -1);
            // add blocks on top of the towers
            BlockShapes.MakeHollowLayers(intFarmLength + 4, intFarmLength + 12, 81, 81,
                                         intFarmLength + 4, intFarmLength + 12, intWallMaterial, 1, -1);
            // alternating top blocks
            for (int x = intFarmLength + 4; x <= intFarmLength + 12; x += 2)
            {
                for (int z = intFarmLength + 4; z <= intFarmLength + 12; z += 2)
                {
                    if (x == intFarmLength + 4 || x == intFarmLength + 12 || z == intFarmLength + 4 || z == intFarmLength + 12)
                    {
                        BlockShapes.MakeBlock(x, 82, z, intWallMaterial, 1, 100, -1);
                    }
                }
            }
            // add central columns
            BlockShapes.MakeSolidBox(intFarmLength + 8, intFarmLength + 8, 73, 82,
                                     intFarmLength + 8, intFarmLength + 8, intWallMaterial, 1);
            BlockHelper.MakeLadder(intFarmLength + 7, 73, 82, intFarmLength + 8, 2, intWallMaterial);
            BlockHelper.MakeLadder(intFarmLength + 9, 73, 82, intFarmLength + 8, 2, intWallMaterial);
            // add torches on the roof
            BlockShapes.MakeBlock(intFarmLength + 6, 81, intFarmLength + 6, BlockType.TORCH, 1, 100, -1);
            BlockShapes.MakeBlock(intFarmLength + 6, 81, intFarmLength + 10, BlockType.TORCH, 2, 100, -1);
            BlockShapes.MakeBlock(intFarmLength + 10, 81, intFarmLength + 10, BlockType.TORCH, 1, 100, -1);
            // add cobwebs
            BlockShapes.MakeBlock(intFarmLength + 5, 79, intFarmLength + 5, BlockType.COBWEB, 1, 30, -1);
            BlockShapes.MakeBlock(intFarmLength + 5, 79, intFarmLength + 11, BlockType.COBWEB, 1, 30, -1);
            BlockShapes.MakeBlock(intFarmLength + 11, 79, intFarmLength + 5, BlockType.COBWEB, 1, 30, -1);
            BlockShapes.MakeBlock(intFarmLength + 11, 79, intFarmLength + 11, BlockType.COBWEB, 1, 30, -1);
            // add chests
            MakeGuardChest(bm, intFarmLength + 11, 73, intFarmLength + 11, booIncludeItemsInChests);
            MakeGuardChest(bm, intMapLength - (intFarmLength + 11), 73, intFarmLength + 11, booIncludeItemsInChests);
            MakeGuardChest(bm, intFarmLength + 11, 73, intMapLength - (intFarmLength + 11), booIncludeItemsInChests);
            MakeGuardChest(bm, intMapLength - (intFarmLength + 11), 73, intMapLength - (intFarmLength + 11),
                           booIncludeItemsInChests);
            // add archery slots
            BlockShapes.MakeSolidBox(intFarmLength + 4, intFarmLength + 4, 74, 77,
                                     intFarmLength + 8, intFarmLength + 8, BlockType.AIR, 2);
            BlockShapes.MakeSolidBox(intFarmLength + 4, intFarmLength + 4, 76, 76,
                                     intFarmLength + 7, intFarmLength + 9, BlockType.AIR, 2);
            if (!booIncludeWalls)
            {
                BlockHelper.MakeLadder(intFarmLength + 13, 64, 72, intFarmLength + 8, 2, intWallMaterial);
            }
            // include beds
            BlockHelper.MakeBed(intFarmLength + 5, intFarmLength + 6, 64, intFarmLength + 8, intFarmLength + 8, 2);
            BlockHelper.MakeBed(intFarmLength + 5, intFarmLength + 6, 64, intFarmLength + 10, intFarmLength + 10, 2);
            BlockHelper.MakeBed(intFarmLength + 11, intFarmLength + 10, 64, intFarmLength + 8, intFarmLength + 8, 2);
            // make columns to orientate torches
            BlockShapes.MakeSolidBox(intFarmLength + 5, intFarmLength + 5, 64, 73, intFarmLength + 5, intFarmLength + 5,
                                     BlockType.WOOD, 2);
            BlockShapes.MakeSolidBox(intFarmLength + 11, intFarmLength + 11, 64, 71, intFarmLength + 5, intFarmLength + 5,
                                     BlockType.WOOD, 2);
            BlockShapes.MakeSolidBox(intFarmLength + 5, intFarmLength + 5, 64, 71, intFarmLength + 11, intFarmLength + 11,
                                     BlockType.WOOD, 2);
            BlockShapes.MakeSolidBox(intFarmLength + 11, intFarmLength + 11, 64, 71, intFarmLength + 11, intFarmLength + 11,
                                     BlockType.WOOD, 2);
            // add ladders
            BlockHelper.MakeLadder(intFarmLength + 5, 64, 73, intFarmLength + 6, 2, BlockType.WOOD);
            // make torches
            BlockHelper.MakeTorch(intFarmLength + 10, 66, intFarmLength + 5, BlockType.WOOD, 2);
            BlockHelper.MakeTorch(intFarmLength + 6, 66, intFarmLength + 11, BlockType.WOOD, 2);
            BlockHelper.MakeTorch(intFarmLength + 10, 66, intFarmLength + 11, BlockType.WOOD, 2);
            // make columns for real
            BlockShapes.MakeSolidBox(intFarmLength + 5, intFarmLength + 5, 64, 73, intFarmLength + 5, intFarmLength + 5,
                                     intWallMaterial, 2);
            BlockShapes.MakeSolidBox(intFarmLength + 11, intFarmLength + 11, 64, 71, intFarmLength + 5, intFarmLength + 5,
                                     intWallMaterial, 2);
            BlockShapes.MakeSolidBox(intFarmLength + 5, intFarmLength + 5, 64, 71, intFarmLength + 11, intFarmLength + 11,
                                     intWallMaterial, 2);
            BlockShapes.MakeSolidBox(intFarmLength + 11, intFarmLength + 11, 64, 71, intFarmLength + 11, intFarmLength + 11,
                                     intWallMaterial, 2);     
            // make cobwebs
            BlockShapes.MakeBlock(intFarmLength + 11, 67, intFarmLength + 8, BlockType.COBWEB, 2, 75, -1);
            // make doors from the city to the guard tower
            BlockShapes.MakeBlock(intFarmLength + 11, 65, intFarmLength + 11, BlockType.GOLD_BLOCK, 1, 100, -1);
            BlockShapes.MakeBlock(intFarmLength + 11, 64, intFarmLength + 11, BlockType.GOLD_BLOCK, 1, 100, -1);
            BlockHelper.MakeDoor(intFarmLength + 11, 64, intFarmLength + 12, BlockType.GOLD_BLOCK, true, 2);
            BlockShapes.MakeBlock(intFarmLength + 11, 65, intFarmLength + 11, BlockType.AIR, 1, 100, -1);
            BlockShapes.MakeBlock(intFarmLength + 11, 64, intFarmLength + 11, BlockType.AIR, 1, 100, -1);
            BlockShapes.MakeBlock(intFarmLength + 11, 64, intFarmLength + 11, BlockType.STONE_PLATE, 1, 100, -1);
            BlockShapes.MakeBlock(intFarmLength + 11, 64, intFarmLength + 13, BlockType.STONE_PLATE, 2, 100, -1);
            //BlockShapes.MakeBlock(intFarmLength + 13, 64, intFarmLength + 11, BlockType.STONE_PLATE, 1, 100, -1);
            // add guard tower sign
            BlockHelper.MakeSign(intFarmLength + 12, 65, intFarmLength + 13, "~Guard Tower~~", intWallMaterial, 1);
            BlockHelper.MakeSign(intFarmLength + 8, 74, intFarmLength + 13, "~Guard Tower~~", intWallMaterial, 2);
            // make beacon
            switch (strTowerAddition)
            {
                case "Fire beacon":
                    BlockShapes.MakeSolidBox(intFarmLength + 8, intFarmLength + 8, 83, 84,
                                             intFarmLength + 8, intFarmLength + 8, intWallMaterial, 1);
                    BlockShapes.MakeSolidBox(intFarmLength + 6, intFarmLength + 10, 85, 85,
                                             intFarmLength + 6, intFarmLength + 10, intWallMaterial, 1);
                    BlockShapes.MakeSolidBox(intFarmLength + 6, intFarmLength + 10, 86, 86,
                                             intFarmLength + 6, intFarmLength + 10, BlockType.NETHERRACK, 1);
                    BlockShapes.MakeSolidBox(intFarmLength + 6, intFarmLength + 10, 87, 87,
                                             intFarmLength + 6, intFarmLength + 10, BlockType.FIRE, 1);
                    break;
                case "Flag":
                    BlockShapes.MakeSolidBox(intFarmLength + 4, intFarmLength + 4, 83, 91,
                                             intFarmLength + 12, intFarmLength + 12, BlockType.FENCE, 2);
                    BlockShapes.MakeBlock(intFarmLength + 4, 84, intFarmLength + 13, BlockType.FENCE, 2, 100, 0);
                    BlockShapes.MakeBlock(intFarmLength + 4, 91, intFarmLength + 13, BlockType.FENCE, 2, 100, 0);
                    int[] intColours = Utils.ShuffleArray(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
                    switch (RandomHelper.Next(7))
                    {
                        case 0: //2vert
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 90,
                                intFarmLength + 13, intFarmLength + 17, BlockType.WOOL, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 90,
                                intFarmLength + 18, intFarmLength + 22, BlockType.WOOL, 2, intColours[1]);
                            break;
                        case 1: //3vert
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 90,
                                intFarmLength + 13, intFarmLength + 15, BlockType.WOOL, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 90,
                                intFarmLength + 16, intFarmLength + 18, BlockType.WOOL, 2, intColours[1]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 90,
                                intFarmLength + 19, intFarmLength + 21, BlockType.WOOL, 2, intColours[2]);
                            break;
                        case 2: //4vert
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 90,
                                intFarmLength + 13, intFarmLength + 14, BlockType.WOOL, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 90,
                                intFarmLength + 15, intFarmLength + 16, BlockType.WOOL, 2, intColours[1]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 90,
                                intFarmLength + 17, intFarmLength + 18, BlockType.WOOL, 2, intColours[2]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 90,
                                intFarmLength + 19, intFarmLength + 20, BlockType.WOOL, 2, intColours[3]);
                            break;
                        case 3: //2horiz
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 87,
                                intFarmLength + 13, intFarmLength + 22, BlockType.WOOL, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 88, 90,
                                intFarmLength + 13, intFarmLength + 22, BlockType.WOOL, 2, intColours[1]);
                            break;
                        case 4: //3horiz
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 86,
                                intFarmLength + 13, intFarmLength + 22, BlockType.WOOL, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 87, 88,
                                intFarmLength + 13, intFarmLength + 22, BlockType.WOOL, 2, intColours[1]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 89, 90,
                                intFarmLength + 13, intFarmLength + 22, BlockType.WOOL, 2, intColours[2]);
                            break;
                        case 5: //quarters
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 87,
                                intFarmLength + 13, intFarmLength + 17, BlockType.WOOL, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 87,
                                intFarmLength + 18, intFarmLength + 22, BlockType.WOOL, 2, intColours[1]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 88, 90,
                                intFarmLength + 13, intFarmLength + 17, BlockType.WOOL, 2, intColours[2]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 88, 90,
                                intFarmLength + 18, intFarmLength + 22, BlockType.WOOL, 2, intColours[3]);
                            break;
                        case 6: //cross
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 90,
                                intFarmLength + 13, intFarmLength + 22, BlockType.WOOL, 2, intColours[0]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 87, 88,
                                intFarmLength + 13, intFarmLength + 22, BlockType.WOOL, 2, intColours[1]);
                            BlockShapes.MakeSolidBoxWithData(intFarmLength + 4, intFarmLength + 4, 85, 90,
                                intFarmLength + 17, intFarmLength + 18, BlockType.WOOL, 2, intColours[1]);
                            break;
                        default:
                            Debug.Fail("Invalid switch result");
                            break;
                    }
                    break;
            }
        }
        private static void MakeGuardChest(BlockManager bm, int x, int y, int z, bool booIncludeItemsInChests)
        {
            TileEntityChest tec = new TileEntityChest();
            if (booIncludeItemsInChests)
            {
                for (int a = 0; a < 5; a++)
                {
                    tec.Items[a] = BlockHelper.MakeItem(RandomHelper.RandomNumber(ItemInfo.IronSword.ID,
                                                                                  ItemInfo.WoodenSword.ID,
                                                                                  ItemInfo.StoneSword.ID), 1);
                }
                tec.Items[6] = BlockHelper.MakeItem(ItemInfo.Bow.ID, 1);
                tec.Items[7] = BlockHelper.MakeItem(ItemInfo.Arrow.ID, 64);
                int intArmourStartID = RandomHelper.RandomNumber(ItemInfo.LeatherCap.ID, ItemInfo.ChainHelmet.ID,
                                                                 ItemInfo.IronHelmet.ID);
                for (int a = 9; a < 18; a++)
                {
                    // random armour
                    tec.Items[a] = BlockHelper.MakeItem(intArmourStartID + RandomHelper.Next(4), 1);
                }
            }
            bm.SetID(x, y, z, BlockType.CHEST);
            bm.SetTileEntity(x, y, z, tec);
        }
    }
}
