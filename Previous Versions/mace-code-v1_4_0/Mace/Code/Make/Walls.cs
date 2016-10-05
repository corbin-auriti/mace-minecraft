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
        static Random rand = new Random();

        public static void MakeWalls(BetaWorld world, int intFarmSize, int intMapSize,
                                     string strCityEmblem, string strOutsideLights)
        {
            // walls
            for (int a = intFarmSize + 6; a <= intFarmSize + 10; a++)
            {
                BlockShapes.MakeHollowLayers(a, intMapSize - a, 58, 72, a, intMapSize - a, (int)BlockType.STONE);
                world.Save();
            }
            // outside and inside edges at the top
            BlockShapes.MakeHollowLayers(intFarmSize + 5, intMapSize - (intFarmSize + 5), 72, 73,
                                         intFarmSize + 5, intMapSize - (intFarmSize + 5), (int)BlockType.STONE);
            BlockShapes.MakeHollowLayers(intFarmSize + 11, intMapSize - (intFarmSize + 11), 72, 73,
                                         intFarmSize + 11, intMapSize - (intFarmSize + 11), (int)BlockType.STONE);
            // alternating blocks on top of the edges
            for (int a = intFarmSize + 6; a <= intMapSize - (intFarmSize + 6); a += 2)
            {
                BlockShapes.MakeBlock(a, 74, intFarmSize + 5, (int)BlockType.STONE, 2);
            }
            for (int a = intFarmSize + 10; a <= intMapSize - (intFarmSize + 10); a += 2)
            {
                BlockShapes.MakeBlock(a, 74, intFarmSize + 11, (int)BlockType.STONE, 2);
            }
            // ladder
            BlockHelper.MakeLadder((intMapSize / 2) - 5, 64, 72, intFarmSize + 11, 2);
            BlockShapes.MakeBlock((intMapSize / 2) - 5, 73, intFarmSize + 11, (int)BlockType.AIR, 2);
            // decorations at the gates
            MakeEmblem(intFarmSize, intMapSize, strCityEmblem);
            switch (strOutsideLights)
            {
                case "Fire":
                    // fire above the entrances
                    BlockShapes.MakeBlock((intMapSize / 2) - 1, 69, intFarmSize + 5, (int)BlockType.NETHERRACK, 2);
                    BlockShapes.MakeBlock((intMapSize / 2), 69, intFarmSize + 5, (int)BlockType.NETHERRACK, 2);
                    BlockShapes.MakeBlock((intMapSize / 2) - 1, 70, intFarmSize + 5, (int)BlockType.FIRE, 2);
                    BlockShapes.MakeBlock((intMapSize / 2), 70, intFarmSize + 5, (int)BlockType.FIRE, 2);
                    // fire on the outside walls
                    for (int a = intFarmSize + 8; a < (intMapSize / 2) - 13; a += 4)
                    {
                        BlockShapes.MakeBlock(a, 69, intFarmSize + 5, (int)BlockType.NETHERRACK, 2);
                        BlockShapes.MakeBlock(a, 70, intFarmSize + 5, (int)BlockType.FIRE, 2);
                    }
                    break;
                case "Torches":
                    // torches above the entrances
                    BlockHelper.MakeTorch((intMapSize / 2), 70, intFarmSize + 5, (int)BlockType.STONE, 2);
                    BlockHelper.MakeTorch((intMapSize / 2) - 1, 70, intFarmSize + 5, (int)BlockType.STONE, 2);
                    // torches on the outside walls
                    for (int a = intFarmSize + 8; a < (intMapSize / 2) - 13; a += 4)
                    {
                        BlockHelper.MakeTorch(a, 70, intFarmSize + 5, (int)BlockType.STONE, 2);
                    }
                    break;
            }
            // torches on the inside walls
            for (int a = intFarmSize + 16; a < (intMapSize / 2); a += 4)
            {
                BlockHelper.MakeTorch(a, 69, intFarmSize + 11, (int)BlockType.STONE, 2);
            }
            // torches on the wall roofs
            for (int a = intFarmSize + 16; a < (intMapSize / 2); a += 4)
            {
                BlockShapes.MakeBlock(a, 73, intFarmSize + 8, (int)BlockType.TORCH, 2);
            }
        }
        private static void MakeEmblem(int intFarmSize, int intMapSize, string strCityEmblem)
        {
            if (strCityEmblem == "Random")
            {
                strCityEmblem = RandomHelper.RandomString("Cross", "England Flag", "Pride Flag", "Shield", "Yin and Yang");
            }

            switch (strCityEmblem)
            {
                case "None":
                    break;
                case "Cross":
                    int intCrossBlock = (int)BlockType.IRON_BLOCK;
                    switch(rand.Next(3))
                    {
                        case 0:
                            intCrossBlock = (int)BlockType.IRON_BLOCK;
                            break;
                        case 1:
                            intCrossBlock = (int)BlockType.GOLD_BLOCK;
                            break;
                        case 2:
                            intCrossBlock = (int)BlockType.DIAMOND_BLOCK;
                            break;
                    }
                    for (int y = 64; y <= 71; y++)
                    {
                        BlockShapes.MakeBlock((intMapSize / 2) - 8, y, intFarmSize + 5, intCrossBlock, 2);
                        BlockShapes.MakeBlock((intMapSize / 2) - 7, y, intFarmSize + 5, intCrossBlock, 2);
                    }
                    for (int x = (intMapSize / 2) - 10; x <= (intMapSize / 2) - 5; x++)
                    {
                        BlockShapes.MakeBlock(x, 69, intFarmSize + 5, intCrossBlock, 2);
                        BlockShapes.MakeBlock(x, 68, intFarmSize + 5, intCrossBlock, 2);
                    }
                    break;
                case "England Flag":
                    for (int x = (intMapSize / 2) - 11; x <= (intMapSize / 2) - 5; x++)
                    {
                        for (int y = 66; y <= 70; y++)
                        {
                            int intBlockData = (int)WoolColor.WHITE;
                            if (y == 68 || x == (intMapSize / 2) - 8)
                            {
                                intBlockData = (int)WoolColor.RED;
                            }
                            BlockShapes.MakeBlock(x, y, intFarmSize + 5, (int)BlockType.WOOL, 2, 100, intBlockData);
                        }
                    }
                    break;
                case "Pride Flag":
                    for (int y = 66; y <= 71; y++)
                    {
                        int intWoolID = 0;
                        switch (y)
                        {
                            case 71:
                                intWoolID = (int)WoolColor.RED;
                                break;
                            case 70:
                                intWoolID = (int)WoolColor.ORANGE;
                                break;
                            case 69:
                                intWoolID = (int)WoolColor.YELLOW;
                                break;
                            case 68:
                                intWoolID = (int)WoolColor.LIGHT_GREEN;
                                break;
                            case 67:
                                intWoolID = (int)WoolColor.BLUE;
                                break;
                            case 66:
                                intWoolID = (int)WoolColor.PURPLE;
                                break;
                        }
                        for (int x = (intMapSize / 2) - 13; x <= (intMapSize / 2) - 5; x++)
                        {
                            BlockShapes.MakeBlock(x, y, intFarmSize + 5, (int)BlockType.WOOL, 2, 100, intWoolID);
                        }
                    }
                    break;
                case "Shield":
                    for (int x = (intMapSize / 2) - 10; x <= (intMapSize / 2) - 6; x++)
                    {
                        for (int y = 66; y <= 70; y++)
                        {
                            if (x == (intMapSize / 2) - 8 || y == 68)
                            {
                                BlockShapes.MakeBlock(x, y, intFarmSize + 5, (int)BlockType.GOLD_BLOCK, 2);
                            }
                            else if ((y >= 69 && x >= (intMapSize / 2) - 7) || (y <= 67 && x <= (intMapSize / 2) - 8))
                            {
                                BlockShapes.MakeBlock(x, y, intFarmSize + 5, (int)BlockType.DIAMOND_BLOCK, 2);
                            }
                            else
                            {
                                BlockShapes.MakeBlock(x, y, intFarmSize + 5, (int)BlockType.IRON_BLOCK, 2);
                            }
                        }
                    }
                    BlockShapes.MakeBlock((intMapSize / 2) - 8, 65, intFarmSize + 5, (int)BlockType.GOLD_BLOCK, 2);
                    BlockShapes.MakeBlock((intMapSize / 2) - 10, 66, intFarmSize + 5, (int)BlockType.AIR, 2);
                    BlockShapes.MakeBlock((intMapSize / 2) - 6, 66, intFarmSize + 5, (int)BlockType.AIR, 2);
                    break;
                case "Yin and Yang":
                    string[] strYinYang = { " WWWB ", "WWBWBB", "WWWWBB", "WWBBBB", "WWBWBB", " WBBB " };
                    for (int y = 0; y <= 5; y++)
                    {
                        for (int z = 0; z <= 5; z++)
                        {
                            switch (strYinYang[y][z])
                            {
                                case 'W':
                                    BlockShapes.MakeBlock(((intMapSize / 2) - 10) + z, 70 - y, intFarmSize + 5, (int)BlockType.WOOL, 2, 100, (int)WoolColor.WHITE);
                                    break;
                                case 'B':
                                    BlockShapes.MakeBlock(((intMapSize / 2) - 10) + z, 70 - y, intFarmSize + 5, (int)BlockType.WOOL, 2, 100, (int)WoolColor.BLACK);
                                    break;
                            }
                        }
                    }
                    break;
            }
        }
    }
}
