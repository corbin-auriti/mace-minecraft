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
    static class Drawbridge
    {
        public static void MakeDrawbridges(BlockManager bm, int intFarmLength, int intMapLength,
                                           bool booIncludeMoat, bool booIncludeWalls, bool booIncludeItemsInChests,
                                           int intWallMaterial, string strMoatType, string strCityName)
        {
            if (booIncludeWalls)
            {
                // drawbridge
                int intBridgeEnd = booIncludeMoat ? -2 : 5;
                if (strMoatType == "Lava" || strMoatType == "Fire")
                {
                    BlockShapes.MakeSolidBox((intMapLength / 2) - 2, intMapLength / 2, 63, 63,
                                             intFarmLength + intBridgeEnd, intFarmLength + 13, BlockType.STONE, 2);
                }
                else
                {
                    BlockShapes.MakeSolidBox((intMapLength / 2) - 2, intMapLength / 2, 63, 63,
                                             intFarmLength + intBridgeEnd, intFarmLength + 13, BlockType.WOOD_PLANK, 2);
                }
                BlockShapes.MakeSolidBox((intMapLength / 2) - 2, intMapLength / 2, 64, 64,
                                         intFarmLength + intBridgeEnd, intFarmLength + 13, BlockType.AIR, 2);
                // carve out the entrance/exit
                BlockShapes.MakeSolidBox((intMapLength / 2) - 2, intMapLength / 2, 64, 67,
                                         intFarmLength + 6, intFarmLength + 10, BlockType.AIR, 2);
                if (Utils.IsValidSign(strCityName))
                {
                    BlockHelper.MakeSign((intMapLength / 2) - 3, 65, intFarmLength + 5, Utils.ConvertStringToSignText(strCityName.Replace("City of ", "City of~")), intWallMaterial, 2);
                }
                // add the bottom of a portcullis
                BlockShapes.MakeSolidBox((intMapLength / 2) - 2, intMapLength / 2, 67, 67,
                                         intFarmLength + 6, intFarmLength + 6, BlockType.FENCE, 2);
                // add room for murder holes
                BlockShapes.MakeSolidBox((intMapLength / 2) - 2, (intMapLength / 2) + 2, 69, 71,
                                         intFarmLength + 8, intFarmLength + 9, BlockType.AIR, 2);
                BlockShapes.MakeSolidBox(intMapLength / 2, intMapLength / 2, 69, 72,
                                         intFarmLength + 8, intFarmLength + 9, BlockType.AIR, 2);
                BlockHelper.MakeLadder(intMapLength / 2, 69, 72, intFarmLength + 9, 2, intWallMaterial);
                BlockShapes.MakeSolidBox(intMapLength / 2, intMapLength / 2, 72, 72,
                                         intFarmLength + 8, intFarmLength + 8, intWallMaterial, 2);
                // murder holes
                BlockShapes.MakeSolidBox((intMapLength / 2) - 2, (intMapLength / 2) - 2, 68, 68,
                                         intFarmLength + 8, intFarmLength + 8, BlockType.AIR, 2);
                BlockShapes.MakeSolidBox(intMapLength / 2, intMapLength / 2, 68, 68,
                                         intFarmLength + 8, intFarmLength + 8, BlockType.AIR, 2);
                BlockShapes.MakeSolidBox((intMapLength / 2) + 2, (intMapLength / 2) + 2, 68, 68,
                                         intFarmLength + 8, intFarmLength + 8, BlockType.AIR, 2);
                // chests
                BlockShapes.MakeBlock((intMapLength / 2) - 4, 69, intFarmLength + 9, BlockType.GRAVEL, 2, 100, -1);
                BlockShapes.MakeBlock((intMapLength / 2) + 4, 69, intFarmLength + 9, BlockType.GRAVEL, 2, 100, -1);
                BlockShapes.MakeBlock((intMapLength / 2) - 3, 70, intFarmLength + 9, BlockType.AIR, 2, 100, -1);
                BlockShapes.MakeBlock((intMapLength / 2) + 3, 70, intFarmLength + 9, BlockType.AIR, 2, 100, -1);
                TileEntityChest tec = new TileEntityChest();
                if (booIncludeItemsInChests)
                {
                    tec.Items[0] = BlockHelper.MakeItem(ItemInfo.LavaBucket.ID, 1);
                    tec.Items[1] = BlockHelper.MakeItem(ItemInfo.LavaBucket.ID, 1);
                    tec.Items[2] = BlockHelper.MakeItem(ItemInfo.LavaBucket.ID, 1);
                }
                BlockHelper.MakeChest((intMapLength / 2) - 3, 69, intFarmLength + 9, BlockType.GRAVEL, tec, 2);
                // add torches
                BlockHelper.MakeTorch((intMapLength / 2) - 1, 70, intFarmLength + 9, intWallMaterial, 2);
                BlockHelper.MakeTorch((intMapLength / 2) + 1, 70, intFarmLength + 9, intWallMaterial, 2);
                // link to main roads
                //BlockShapes.MakeSolidBox((intMapLength / 2) - 1, (intMapLength / 2) + 1, 63, 63,
                //                         intFarmLength + 11, intFarmLength + 13, BlockType.DOUBLE_SLAB, 0);
            }
            else if (booIncludeMoat)
            {
                BlockShapes.MakeSolidBox((intMapLength / 2) - 2, intMapLength / 2, 63, 63,
                                         intFarmLength - 2, intFarmLength + 6, BlockType.STONE, 2);
            }
        }
    }
}
