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
using Substrate;
using Substrate.TileEntities;
using Substrate.Entities;

namespace Mace
{
    class GuardTowers
    {

        public static void MakeGuardTowers(BlockManager bm, int intFarmSize, int intMapSize,
                                           bool booIncludeWalls, string strOutsideLights, string strFireBeacons)
        {
            // remove wall
            BlockShapes.MakeSolidBox(intFarmSize + 5, intFarmSize + 11, 64, 79,
                                     intFarmSize + 5, intFarmSize + 11, (int)BlockType.AIR, 1);
            // add tower
            BlockShapes.MakeHollowBox(intFarmSize + 4, intFarmSize + 12, 63, 80,
                                      intFarmSize + 4, intFarmSize + 12, (int)BlockType.STONE, 1, -1);
            // divide into two rooms
            BlockShapes.MakeSolidBox(intFarmSize + 4, intFarmSize + 12, 2, 72,
                                     intFarmSize + 4, intFarmSize + 12, (int)BlockType.STONE, 1);
            BlockShapes.MakeSolidBox(intFarmSize + 5, intFarmSize + 11, 64, 67,
                                     intFarmSize + 5, intFarmSize + 11, (int)BlockType.AIR, 1);
            switch (strOutsideLights)
            {
                case "Fire":
                    for (int y = 72; y <= 79; y += 7)
                    {
                        BlockShapes.MakeBlock(intFarmSize + 6, y, intFarmSize + 3, (int)BlockType.NETHERRACK, 2, 100, -1);
                        BlockShapes.MakeBlock(intFarmSize + 6, y + 1, intFarmSize + 3, (int)BlockType.FIRE, 2, 100, -1);
                        BlockShapes.MakeBlock(intFarmSize + 10, y, intFarmSize + 3, (int)BlockType.NETHERRACK, 2, 100, -1);
                        BlockShapes.MakeBlock(intFarmSize + 10, y + 1, intFarmSize + 3, (int)BlockType.FIRE, 2, 100, -1);
                    }
                    break;
                case "Torches":
                    for (int y = 73; y <= 80; y += 7)
                    {
                        BlockHelper.MakeTorch(intFarmSize + 6, y, intFarmSize + 3, (int)BlockType.STONE, 2);
                        BlockHelper.MakeTorch(intFarmSize + 10, y, intFarmSize + 3, (int)BlockType.STONE, 2);
                    }
                    break;
            }
            // add torches
            BlockHelper.MakeTorch(intFarmSize + 6, 79, intFarmSize + 13, (int)BlockType.STONE, 2);
            BlockHelper.MakeTorch(intFarmSize + 10, 79, intFarmSize + 13, (int)BlockType.STONE, 2);
            // add torches inside
            BlockHelper.MakeTorch(intFarmSize + 6, 77, intFarmSize + 11, (int)BlockType.STONE, 2);
            BlockHelper.MakeTorch(intFarmSize + 10, 77, intFarmSize + 11, (int)BlockType.STONE, 2);
            BlockHelper.MakeTorch(intFarmSize + 5, 77, intFarmSize + 6, (int)BlockType.STONE, 2);
            BlockHelper.MakeTorch(intFarmSize + 5, 77, intFarmSize + 10, (int)BlockType.STONE, 2);
            // add openings to the walls
            BlockShapes.MakeBlock(intFarmSize + 7, 73, intFarmSize + 12, (int)BlockType.AIR, 2, 100, -1);
            BlockShapes.MakeBlock(intFarmSize + 9, 73, intFarmSize + 12, (int)BlockType.AIR, 2, 100, -1);
            BlockShapes.MakeBlock(intFarmSize + 7, 74, intFarmSize + 12, (int)BlockType.AIR, 2, 100, -1);
            BlockShapes.MakeBlock(intFarmSize + 9, 74, intFarmSize + 12, (int)BlockType.AIR, 2, 100, -1);
            // add blocks on top of the towers
            BlockShapes.MakeHollowLayers(intFarmSize + 4, intFarmSize + 12, 81, 81,
                                         intFarmSize + 4, intFarmSize + 12, (int)BlockType.STONE, 1, -1);
            // alternating top blocks
            for (int x = intFarmSize + 4; x <= intFarmSize + 12; x += 2)
            {
                for (int z = intFarmSize + 4; z <= intFarmSize + 12; z += 2)
                {
                    if (x == intFarmSize + 4 || x == intFarmSize + 12 || z == intFarmSize + 4 || z == intFarmSize + 12)
                    {
                        BlockShapes.MakeBlock(x, 82, z, (int)BlockType.STONE, 1, 100, -1);
                    }
                }
            }
            // add central columns
            BlockShapes.MakeSolidBox(intFarmSize + 8, intFarmSize + 8, 73, 82,
                                     intFarmSize + 8, intFarmSize + 8, (int)BlockType.STONE, 1);
            BlockHelper.MakeLadder(intFarmSize + 7, 73, 82, intFarmSize + 8, 2, (int)BlockType.STONE);
            BlockHelper.MakeLadder(intFarmSize + 9, 73, 82, intFarmSize + 8, 2, (int)BlockType.STONE);
            // add torches on the roof
            BlockShapes.MakeBlock(intFarmSize + 6, 81, intFarmSize + 6, (int)BlockType.TORCH, 1, 100, -1);
            BlockShapes.MakeBlock(intFarmSize + 6, 81, intFarmSize + 10, (int)BlockType.TORCH, 2, 100, -1);
            BlockShapes.MakeBlock(intFarmSize + 10, 81, intFarmSize + 10, (int)BlockType.TORCH, 1, 100, -1);
            // add cobwebs
            BlockShapes.MakeBlock(intFarmSize + 5, 79, intFarmSize + 5, (int)BlockType.COBWEB, 1, 30, -1);
            BlockShapes.MakeBlock(intFarmSize + 5, 79, intFarmSize + 11, (int)BlockType.COBWEB, 1, 30, -1);
            BlockShapes.MakeBlock(intFarmSize + 11, 79, intFarmSize + 5, (int)BlockType.COBWEB, 1, 30, -1);
            BlockShapes.MakeBlock(intFarmSize + 11, 79, intFarmSize + 11, (int)BlockType.COBWEB, 1, 30, -1);
            // add chests
            MakeGuardChest(bm, intFarmSize + 11, 73, intFarmSize + 11);
            MakeGuardChest(bm, intMapSize - (intFarmSize + 11), 73, intFarmSize + 11);
            MakeGuardChest(bm, intFarmSize + 11, 73, intMapSize - (intFarmSize + 11));
            MakeGuardChest(bm, intMapSize - (intFarmSize + 11), 73, intMapSize - (intFarmSize + 11));
            // add archery slots
            BlockShapes.MakeSolidBox(intFarmSize + 4, intFarmSize + 4, 74, 77,
                                     intFarmSize + 8, intFarmSize + 8, (int)BlockType.AIR, 2);
            BlockShapes.MakeSolidBox(intFarmSize + 4, intFarmSize + 4, 76, 76,
                                     intFarmSize + 7, intFarmSize + 9, (int)BlockType.AIR, 2);
            if (!booIncludeWalls)
            {
                BlockHelper.MakeLadder(intFarmSize + 13, 64, 72, intFarmSize + 8, 2, (int)BlockType.STONE);
            }
            // include beds
            BlockHelper.MakeBed(intFarmSize + 5, intFarmSize + 6, 64, intFarmSize + 8, intFarmSize + 8, 2);
            BlockHelper.MakeBed(intFarmSize + 5, intFarmSize + 6, 64, intFarmSize + 10, intFarmSize + 10, 2);
            BlockHelper.MakeBed(intFarmSize + 11, intFarmSize + 10, 64, intFarmSize + 8, intFarmSize + 8, 2);
            // make columns to orientate torches
            BlockShapes.MakeSolidBox(intFarmSize + 5, intFarmSize + 5, 64, 73, intFarmSize + 5, intFarmSize + 5, (int)BlockType.WOOD, 2);
            BlockShapes.MakeSolidBox(intFarmSize + 11, intFarmSize + 11, 64, 71, intFarmSize + 5, intFarmSize + 5, (int)BlockType.WOOD, 2);
            BlockShapes.MakeSolidBox(intFarmSize + 5, intFarmSize + 5, 64, 71, intFarmSize + 11, intFarmSize + 11, (int)BlockType.WOOD, 2);
            BlockShapes.MakeSolidBox(intFarmSize + 11, intFarmSize + 11, 64, 71, intFarmSize + 11, intFarmSize + 11, (int)BlockType.WOOD, 2);
            // add ladders
            BlockHelper.MakeLadder(intFarmSize + 5, 64, 73, intFarmSize + 6, 2, (int)BlockType.WOOD);
            // make torches
            BlockHelper.MakeTorch(intFarmSize + 10, 66, intFarmSize + 5, (int)BlockType.WOOD, 2);
            BlockHelper.MakeTorch(intFarmSize + 6, 66, intFarmSize + 11, (int)BlockType.WOOD, 2);
            BlockHelper.MakeTorch(intFarmSize + 10, 66, intFarmSize + 11, (int)BlockType.WOOD, 2);
            // make columns for real
            BlockShapes.MakeSolidBox(intFarmSize + 5, intFarmSize + 5, 64, 73, intFarmSize + 5, intFarmSize + 5, (int)BlockType.STONE, 2);
            BlockShapes.MakeSolidBox(intFarmSize + 11, intFarmSize + 11, 64, 71, intFarmSize + 5, intFarmSize + 5, (int)BlockType.STONE, 2);
            BlockShapes.MakeSolidBox(intFarmSize + 5, intFarmSize + 5, 64, 71, intFarmSize + 11, intFarmSize + 11, (int)BlockType.STONE, 2);
            BlockShapes.MakeSolidBox(intFarmSize + 11, intFarmSize + 11, 64, 71, intFarmSize + 11, intFarmSize + 11, (int)BlockType.STONE, 2);     
            // make cobwebs
            BlockShapes.MakeBlock(intFarmSize + 11, 67, intFarmSize + 8, (int)BlockType.COBWEB, 2, 75, -1);
            // make doors from the city to the guard tower
            BlockShapes.MakeBlock(intFarmSize + 11, 65, intFarmSize + 11, (int)BlockType.GOLD_BLOCK, 1, 100, -1);
            BlockShapes.MakeBlock(intFarmSize + 11, 64, intFarmSize + 11, (int)BlockType.GOLD_BLOCK, 1, 100, -1);
            BlockHelper.MakeDoor(intFarmSize + 11, 64, intFarmSize + 12, (int)BlockType.GOLD_BLOCK, true, 2);
            BlockShapes.MakeBlock(intFarmSize + 11, 65, intFarmSize + 11, (int)BlockType.AIR, 1, 100, -1);
            BlockShapes.MakeBlock(intFarmSize + 11, 64, intFarmSize + 11, (int)BlockType.AIR, 1, 100, -1);
            BlockShapes.MakeBlock(intFarmSize + 11, 64, intFarmSize + 11, (int)BlockType.STONE_PLATE, 1, 100, -1);
            BlockShapes.MakeBlock(intFarmSize + 11, 64, intFarmSize + 13, (int)BlockType.STONE_PLATE, 2, 100, -1);
            // make beacon
            if (strFireBeacons == "Yes")
            {
                BlockShapes.MakeSolidBox(intFarmSize + 8, intFarmSize + 8, 83, 84,
                                         intFarmSize + 8, intFarmSize + 8, (int)BlockType.STONE, 1);
                BlockShapes.MakeSolidBox(intFarmSize + 6, intFarmSize + 10, 85, 85, intFarmSize + 6, intFarmSize + 10, (int)BlockType.STONE, 1);
                BlockShapes.MakeSolidBox(intFarmSize + 6, intFarmSize + 10, 86, 86, intFarmSize + 6, intFarmSize + 10, (int)BlockType.NETHERRACK, 1);
                BlockShapes.MakeSolidBox(intFarmSize + 6, intFarmSize + 10, 87, 87, intFarmSize + 6, intFarmSize + 10, (int)BlockType.FIRE, 1);
            }
        }
        private static void MakeGuardChest(BlockManager bm, int x, int y, int z)
        {
            TileEntityChest tec = new TileEntityChest();
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
                tec.Items[a] = BlockHelper.MakeItem(intArmourStartID + RandomHelper.Next(4), 1); // random armour
            }
            bm.SetID(x, y, z, (int)BlockType.CHEST);
            bm.SetTileEntity(x, y, z, tec);
        }
    }
}
