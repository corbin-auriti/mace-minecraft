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
    class Moat
    {

        public static void MakeMoat(int intFarmSize, int intMapSize, string strMoatType, bool booIncludeGuardTowers)
        {
            switch (strMoatType)
            {
                case "Drop to Bedrock":
                    for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 2, 63, a, intMapSize - a, (int)BlockType.AIR, 0, -1);
                    }
                    break;
                case "Cactus":
                    for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 59, 63, a, intMapSize - a, (int)BlockType.AIR, 0, -1);
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 58, 58, a, intMapSize - a, (int)BlockType.SAND, 0, -1);
                    }
                    for (int a = intFarmSize + 1; a <= intMapSize / 2; a += 2)
                    {
                        BlockShapes.MakeBlock(a, 59, intFarmSize + 1, (int)BlockType.CACTUS, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 59, intFarmSize + 3, (int)BlockType.CACTUS, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 60, intFarmSize + 1, (int)BlockType.CACTUS, 2, 50, -1);
                        BlockShapes.MakeBlock(a, 60, intFarmSize + 3, (int)BlockType.CACTUS, 2, 50, -1);
                    }
                    for (int a = intFarmSize; a <= intMapSize / 2; a += 2)
                    {
                        BlockShapes.MakeBlock(a, 59, intFarmSize, (int)BlockType.CACTUS, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 59, intFarmSize + 2, (int)BlockType.CACTUS, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 59, intFarmSize + 4, (int)BlockType.CACTUS, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 60, intFarmSize, (int)BlockType.CACTUS, 2, 50, -1);
                        BlockShapes.MakeBlock(a, 60, intFarmSize + 2, (int)BlockType.CACTUS, 2, 50, -1);
                        BlockShapes.MakeBlock(a, 60, intFarmSize + 4, (int)BlockType.CACTUS, 2, 50, -1);
                    }
                    if (booIncludeGuardTowers)
                    {
                        for (int a = intFarmSize + 3; a <= intFarmSize + 13; a += 2)
                        {
                            BlockShapes.MakeBlock(a, 59, intFarmSize + 3, (int)BlockType.AIR, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, intFarmSize + 3, (int)BlockType.AIR, 2, 100, -1);
                        }
                    }
                    break;
                case "Lava":
                    for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 62, 63, a, intMapSize - a, (int)BlockType.AIR, 0, -1);
                    }
                    for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 59, 61, a, intMapSize - a, (int)BlockType.LAVA, 0, -1);
                    }
                    break;
                case "Water":
                    for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 59, 63, a, intMapSize - a, (int)BlockType.WATER, 0, -1);
                    }
                    break;
            }
        }
    }
}
