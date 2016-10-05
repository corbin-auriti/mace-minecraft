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
        static Random rand = new Random();

        public static void MakeMoat(int intFarmSize, int intMapSize, string strMoatType, bool booIncludeGuardTowers)
        {
            if (strMoatType == "Random")
            {
                int intRand = rand.Next(100);
                if (intRand >= 90)
                {
                    strMoatType = "Drop to Bedrock";
                }
                else if (intRand >= 80)
                {
                    strMoatType = "Cactus";
                }
                else if (intRand >= 50)
                {
                    strMoatType = "Lava";
                }
                else
                {
                    strMoatType = "Water";
                }
            }
            switch (strMoatType)
            {
                case "Drop to Bedrock":
                    for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 2, 63, a, intMapSize - a, (int)BlockType.AIR);
                    }
                    break;
                case "Cactus":
                    for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 59, 63, a, intMapSize - a, (int)BlockType.AIR);
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 58, 58, a, intMapSize - a, (int)BlockType.SAND);
                    }
                    for (int a = intFarmSize + 1; a <= intMapSize / 2; a += 2)
                    {
                        BlockShapes.MakeBlock(a, 59, intFarmSize + 1, (int)BlockType.CACTUS, 2);
                        BlockShapes.MakeBlock(a, 59, intFarmSize + 3, (int)BlockType.CACTUS, 2);
                        BlockShapes.MakeBlock(a, 60, intFarmSize + 1, (int)BlockType.CACTUS, 2, 50);
                        BlockShapes.MakeBlock(a, 60, intFarmSize + 3, (int)BlockType.CACTUS, 2, 50);
                    }
                    for (int a = intFarmSize; a <= intMapSize / 2; a += 2)
                    {
                        BlockShapes.MakeBlock(a, 59, intFarmSize, (int)BlockType.CACTUS, 2);
                        BlockShapes.MakeBlock(a, 59, intFarmSize + 2, (int)BlockType.CACTUS, 2);
                        BlockShapes.MakeBlock(a, 59, intFarmSize + 4, (int)BlockType.CACTUS, 2);
                        BlockShapes.MakeBlock(a, 60, intFarmSize, (int)BlockType.CACTUS, 2, 50);
                        BlockShapes.MakeBlock(a, 60, intFarmSize + 2, (int)BlockType.CACTUS, 2, 50);
                        BlockShapes.MakeBlock(a, 60, intFarmSize + 4, (int)BlockType.CACTUS, 2, 50);
                    }
                    if (booIncludeGuardTowers)
                    {
                        for (int a = intFarmSize + 3; a <= intFarmSize + 13; a += 2)
                        {
                            BlockShapes.MakeBlock(a, 59, intFarmSize + 3, (int)BlockType.AIR, 2);
                            BlockShapes.MakeBlock(a, 60, intFarmSize + 3, (int)BlockType.AIR, 2);
                        }
                    }
                    break;
                case "Lava":
                    for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 62, 63, a, intMapSize - a, (int)BlockType.AIR);
                    }
                    for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 59, 61, a, intMapSize - a, (int)BlockType.LAVA);
                    }
                    break;
                case "Water":
                    for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, intMapSize - a, 59, 63, a, intMapSize - a, (int)BlockType.WATER);
                    }
                    break;
            }
        }
    }
}
