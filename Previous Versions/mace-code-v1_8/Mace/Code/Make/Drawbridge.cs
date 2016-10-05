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
        public static void MakeDrawbridges(BlockManager bm)
        {
            if (City.HasWalls)
            {
                // drawbridge
                int intBridgeEnd = City.HasMoat ? -2 : 5;
                if (City.MoatType == "Lava" || City.MoatType == "Fire")
                {
                    BlockShapes.MakeSolidBox((City.MapLength / 2) - 2, City.MapLength / 2, 63, 63,
                                             City.FarmLength + intBridgeEnd, City.FarmLength + 13, BlockInfo.Stone.ID, 2);
                }
                else
                {
                    BlockShapes.MakeSolidBox((City.MapLength / 2) - 2, City.MapLength / 2, 63, 63,
                                             City.FarmLength + intBridgeEnd, City.FarmLength + 13, BlockInfo.WoodPlank.ID, 2);
                }
                BlockShapes.MakeSolidBox((City.MapLength / 2) - 2, City.MapLength / 2, 64, 64,
                                         City.FarmLength + intBridgeEnd, City.FarmLength + 13, BlockInfo.Air.ID, 2);
                // carve out the entrance/exit
                BlockShapes.MakeSolidBox((City.MapLength / 2) - 2, City.MapLength / 2, 64, 67,
                                         City.FarmLength + intBridgeEnd, City.FarmLength + 10, BlockInfo.Air.ID, 2);
                if (Utils.IsValidSign(City.Name))
                {
                    BlockHelper.MakeSign((City.MapLength / 2) - 3, 65, City.FarmLength + 5,
                                         Utils.ConvertStringToSignText(City.Name.Replace("City of ", "City of~")),
                                         City.WallMaterialID, 2);
                }
                // add the bottom of a portcullis
                BlockShapes.MakeSolidBox((City.MapLength / 2) - 2, City.MapLength / 2, 67, 67,
                                         City.FarmLength + 6, City.FarmLength + 6, BlockInfo.Fence.ID, 2);
                // add room for murder holes
                BlockShapes.MakeSolidBox((City.MapLength / 2) - 2, (City.MapLength / 2) + 2, 69, 71,
                                         City.FarmLength + 8, City.FarmLength + 9, BlockInfo.Air.ID, 2);
                BlockShapes.MakeSolidBox(City.MapLength / 2, City.MapLength / 2, 69, 72,
                                         City.FarmLength + 8, City.FarmLength + 9, BlockInfo.Air.ID, 2);
                BlockHelper.MakeLadder(City.MapLength / 2, 69, 72, City.FarmLength + 9, 2, City.WallMaterialID);
                BlockShapes.MakeSolidBoxWithData(City.MapLength / 2, City.MapLength / 2, 72, 72,
                                                 City.FarmLength + 8, City.FarmLength + 8,
                                                 City.WallMaterialID, 2, City.WallMaterialData);
                // murder holes
                BlockShapes.MakeSolidBox((City.MapLength / 2) - 2, (City.MapLength / 2) - 2, 68, 68,
                                         City.FarmLength + 8, City.FarmLength + 8, BlockInfo.Air.ID, 2);
                BlockShapes.MakeSolidBox(City.MapLength / 2, City.MapLength / 2, 68, 68,
                                         City.FarmLength + 8, City.FarmLength + 8, BlockInfo.Air.ID, 2);
                BlockShapes.MakeSolidBox((City.MapLength / 2) + 2, (City.MapLength / 2) + 2, 68, 68,
                                         City.FarmLength + 8, City.FarmLength + 8, BlockInfo.Air.ID, 2);
                // chests
                BlockShapes.MakeBlock((City.MapLength / 2) - 4, 69, City.FarmLength + 9, BlockInfo.Gravel.ID, 2, 100, -1);
                BlockShapes.MakeBlock((City.MapLength / 2) + 4, 69, City.FarmLength + 9, BlockInfo.Gravel.ID, 2, 100, -1);
                BlockShapes.MakeBlock((City.MapLength / 2) - 3, 70, City.FarmLength + 9, BlockInfo.Air.ID, 2, 100, -1);
                BlockShapes.MakeBlock((City.MapLength / 2) + 3, 70, City.FarmLength + 9, BlockInfo.Air.ID, 2, 100, -1);
                TileEntityChest tec = new TileEntityChest();
                if (City.HasItemsInChests)
                {
                    tec.Items[0] = BlockHelper.MakeItem(ItemInfo.LavaBucket.ID, 1);
                    tec.Items[1] = BlockHelper.MakeItem(ItemInfo.LavaBucket.ID, 1);
                    tec.Items[2] = BlockHelper.MakeItem(ItemInfo.LavaBucket.ID, 1);
                }
                BlockHelper.MakeChest((City.MapLength / 2) - 3, 69, City.FarmLength + 9, BlockInfo.Gravel.ID, tec, 2);
                // add torches
                BlockHelper.MakeTorch((City.MapLength / 2) - 1, 70, City.FarmLength + 9, City.WallMaterialID, 2);
                BlockHelper.MakeTorch((City.MapLength / 2) + 1, 70, City.FarmLength + 9, City.WallMaterialID, 2);
                // link to main roads
                //BlockShapes.MakeSolidBox((City.intMapLength / 2) - 1, (City.intMapLength / 2) + 1, 63, 63,
                //                         City.intFarmSize + 11, City.intFarmSize + 13, BlockType.DOUBLE_SLAB, 0);
            }
            else if (City.HasMoat)
            {
                BlockShapes.MakeSolidBox((City.MapLength / 2) - 2, City.MapLength / 2, 63, 63,
                                         City.FarmLength - 2, City.FarmLength + 6, BlockInfo.Stone.ID, 2);
            }
        }
    }
}
