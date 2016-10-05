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

namespace Mace
{
    static class Moat
    {
        public static void MakeMoat(int intFarmLength, int intMapLength, string strMoatType, bool booIncludeGuardTowers)
        {
            switch (strMoatType)
            {
                case "Drop to Bedrock":
                    for (int a = intFarmLength - 1; a <= intFarmLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapLength - a, 2, 63, a, intMapLength - a, BlockType.AIR, 0, -1);
                    }
                    break;
                case "Cactus":
                    for (int a = intFarmLength - 1; a <= intFarmLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapLength - a, 59, 63, a, intMapLength - a, BlockType.AIR, 0, -1);
                        BlockShapes.MakeHollowLayers(a, intMapLength - a, 58, 58, a, intMapLength - a, BlockType.SAND, 0, -1);
                    }
                    for (int a = intFarmLength + 1; a <= intMapLength / 2; a += 2)
                    {
                        BlockShapes.MakeBlock(a, 59, intFarmLength + 1, BlockType.CACTUS, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 59, intFarmLength + 3, BlockType.CACTUS, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 60, intFarmLength + 1, BlockType.CACTUS, 2, 50, -1);
                        BlockShapes.MakeBlock(a, 60, intFarmLength + 3, BlockType.CACTUS, 2, 50, -1);
                    }
                    for (int a = intFarmLength; a <= intMapLength / 2; a += 2)
                    {
                        BlockShapes.MakeBlock(a, 59, intFarmLength, BlockType.CACTUS, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 59, intFarmLength + 2, BlockType.CACTUS, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 59, intFarmLength + 4, BlockType.CACTUS, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 60, intFarmLength, BlockType.CACTUS, 2, 50, -1);
                        BlockShapes.MakeBlock(a, 60, intFarmLength + 2, BlockType.CACTUS, 2, 50, -1);
                        BlockShapes.MakeBlock(a, 60, intFarmLength + 4, BlockType.CACTUS, 2, 50, -1);
                    }
                    if (booIncludeGuardTowers)
                    {
                        for (int a = intFarmLength + 3; a <= intFarmLength + 13; a += 2)
                        {
                            BlockShapes.MakeBlock(a, 59, intFarmLength + 3, BlockType.AIR, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, intFarmLength + 3, BlockType.AIR, 2, 100, -1);
                        }
                    }
                    break;
                case "Lava":
                    for (int a = intFarmLength - 1; a <= intFarmLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapLength - a, 59, 61, a, intMapLength - a, BlockType.LAVA, 0, -1);
                        BlockShapes.MakeHollowLayers(a, intMapLength - a, 62, 63, a, intMapLength - a, BlockType.AIR, 0, -1);
                    }
                    break;
                case "Fire":
                    for (int a = intFarmLength - 1; a <= intFarmLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapLength - a, 59, 59, a, intMapLength - a,
                                                     BlockType.NETHERRACK, 0, -1);
                        BlockShapes.MakeHollowLayers(a, intMapLength - a, 60, 60, a, intMapLength - a, BlockType.FIRE, 0, -1);
                        BlockShapes.MakeHollowLayers(a, intMapLength - a, 61, 63, a, intMapLength - a, BlockType.AIR, 0, -1);
                    }
                    break;
                case "Water":
                    for (int a = intFarmLength - 1; a <= intFarmLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapLength - a, 59, 63, a, intMapLength - a, BlockType.WATER, 0, -1);
                    }
                    break;
                default:
                    Debug.Fail("Invalid switch result");
                    break;
            }
        }
    }
}
