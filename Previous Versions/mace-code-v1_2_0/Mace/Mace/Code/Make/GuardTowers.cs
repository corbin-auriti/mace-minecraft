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

namespace Mace
{
    class GuardTowers
    {
        static Random rand = new Random();
        public static void MakeGuardTowers(BlockManager bm, int intFarmSize, int intMapSize, bool booIncludeWalls)
        {
            // remove wall
            BlockShapes.MakeSolidBox(intFarmSize + 5, intFarmSize + 11, 64, 79,
                                     intFarmSize + 5, intFarmSize + 11, (int)BlockType.AIR, 1);
            // add tower
            BlockShapes.MakeHollowBox(intFarmSize + 4, intFarmSize + 12, 63, 80,
                                      intFarmSize + 4, intFarmSize + 12, (int)BlockType.STONE, 1);
            // divide into two rooms
            BlockShapes.MakeSolidBox(intFarmSize + 4, intFarmSize + 12, 59, 71,
                                     intFarmSize + 4, intFarmSize + 12, (int)BlockType.STONE, 1);
            // add openings to the walls
            for (int y = 72; y <= 74; y++)
                for (int x = intFarmSize + 7; x <= intFarmSize + 9; x++)
                    BlockShapes.MakeBlock(x, y, intFarmSize + 12, (int)BlockType.AIR, 2);
            // add blocks on top of the towers
            BlockShapes.MakeHollowLayers(intFarmSize + 4, intFarmSize + 12, 81, 81,
                                         intFarmSize + 4, intFarmSize + 12, (int)BlockType.STONE, 1);
            // alternating top blocks
            for (int x = intFarmSize + 4; x <= intFarmSize + 12; x += 2)
                for (int z = intFarmSize + 4; z <= intFarmSize + 12; z += 2)
                    if (x == intFarmSize + 4 || x == intFarmSize + 12 || z == intFarmSize + 4 || z == intFarmSize + 12)
                        BlockShapes.MakeBlock(x, 82, z, (int)BlockType.STONE, 1);
            // add central columns
            BlockShapes.MakeSolidBox(intFarmSize + 8, intFarmSize + 8, 72, 81,
                                     intFarmSize + 8, intFarmSize + 8, (int)BlockType.STONE, 1);
            BlockHelper.MakeLadder(intFarmSize + 7, 72, 81, intFarmSize + 8, 2);
            BlockHelper.MakeLadder(intFarmSize + 9, 72, 81, intFarmSize + 8, 2);
            // add cobwebs
            // todo: could probably use double-mirroring here
            BlockShapes.MakeBlock(intFarmSize + 5, 79, intFarmSize + 5, (int)BlockType.COBWEB, 1, 30);
            BlockShapes.MakeBlock(intFarmSize + 5, 79, intFarmSize + 11, (int)BlockType.COBWEB, 1, 30);
            BlockShapes.MakeBlock(intFarmSize + 11, 79, intFarmSize + 5, (int)BlockType.COBWEB, 1, 30);
            BlockShapes.MakeBlock(intFarmSize + 11, 79, intFarmSize + 11, (int)BlockType.COBWEB, 1, 30);
            // add chests
            MakeGuardChest(bm, intFarmSize + 11, 72, intFarmSize + 11);
            MakeGuardChest(bm, intMapSize - (intFarmSize + 11), 72, intFarmSize + 11);
            MakeGuardChest(bm, intFarmSize + 11, 72, intMapSize - (intFarmSize + 11));
            MakeGuardChest(bm, intMapSize - (intFarmSize + 11), 72, intMapSize - (intFarmSize + 11));
            // add archery slots
            BlockShapes.MakeSolidBox(intFarmSize + 4, intFarmSize + 4, 73, 76,
                                     intFarmSize + 8, intFarmSize + 8, (int)BlockType.AIR, 2);
            BlockShapes.MakeSolidBox(intFarmSize + 4, intFarmSize + 4, 75, 75,
                                     intFarmSize + 7, intFarmSize + 9, (int)BlockType.AIR, 2);
            if (!booIncludeWalls)
                BlockHelper.MakeLadder(intFarmSize + 13, 64, 71, intFarmSize + 8, 2);
        }
        private static void MakeGuardChest(BlockManager bm, int x, int y, int z)
        {
            TileEntityChest tec = new TileEntityChest();
            for (int a = 0; a < 5; a++)
                tec.Items[a] = BlockHelper.MakeItem(RandomHelper.RandomNumber(ItemInfo.IronSword.ID,
                                                                              ItemInfo.WoodenSword.ID,
                                                                              ItemInfo.StoneSword.ID), 1);
            tec.Items[6] = BlockHelper.MakeItem(ItemInfo.Bow.ID, 1);
            tec.Items[7] = BlockHelper.MakeItem(ItemInfo.Arrow.ID, 64);
            int intArmourStartID = RandomHelper.RandomNumber(ItemInfo.LeatherCap.ID, ItemInfo.ChainHelmet.ID,
                                                             ItemInfo.IronHelmet.ID);
            for (int a = 9; a < 18; a++)
                tec.Items[a] = BlockHelper.MakeItem(intArmourStartID + rand.Next(4), 1); // random armour
            bm.SetID(x, y, z, (int)BlockType.CHEST);
            bm.SetTileEntity(x, y, z, tec);
        }
    }
}
