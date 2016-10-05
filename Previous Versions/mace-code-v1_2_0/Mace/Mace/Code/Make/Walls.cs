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

namespace Mace
{
    class Walls
    {
        public static void MakeWalls(int intFarmSize, int intMapSize)
        {
            // walls
            for (int a = intFarmSize + 6; a <= intFarmSize + 10; a++)
                BlockShapes.MakeHollowLayers(a, intMapSize - a, 58, 71, a, intMapSize - a, (int)BlockType.STONE);
            // outside and inside edges at the top
            BlockShapes.MakeHollowLayers(intFarmSize + 6, intMapSize - (intFarmSize + 6), 72, 72,
                                         intFarmSize + 6, intMapSize - (intFarmSize + 6), (int)BlockType.STONE);
            BlockShapes.MakeHollowLayers(intFarmSize + 10, intMapSize - (intFarmSize + 10), 72, 72,
                                         intFarmSize + 10, intMapSize - (intFarmSize + 10), (int)BlockType.STONE);
            // alternating blocks on top of the edges
            for (int a = intFarmSize + 6; a <= intMapSize - (intFarmSize + 6); a += 2)
                BlockShapes.MakeBlock(a, 73, intFarmSize + 6, (int)BlockType.STONE, 2);
            for (int a = intFarmSize + 10; a <= intMapSize - (intFarmSize + 10); a += 2)
                BlockShapes.MakeBlock(a, 73, intFarmSize + 10, (int)BlockType.STONE, 2);
            // ladder
            BlockHelper.MakeLadder((intMapSize / 2) - 5, 64, 72, intFarmSize + 11, 2);
        }
    }
}
