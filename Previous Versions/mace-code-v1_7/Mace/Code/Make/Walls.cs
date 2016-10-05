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
using System.IO;
using Substrate;

namespace Mace
{
    static class Walls
    {
        public static void MakeWalls(BetaWorld world, int intFarmLength, int intMapLength,
                                     string strCityEmblem, string strOutsideLights,
                                     int intWallMaterial)
        {
            // walls
            for (int a = intFarmLength + 6; a <= intFarmLength + 10; a++)
            {
                BlockShapes.MakeHollowLayers(a, intMapLength - a, 1, 72, a, intMapLength - a, intWallMaterial, 0, -1);
                world.Save();
            }
            // outside and inside edges at the top
            BlockShapes.MakeHollowLayers(intFarmLength + 5, intMapLength - (intFarmLength + 5), 72, 73,
                                         intFarmLength + 5, intMapLength - (intFarmLength + 5), intWallMaterial, 0, -1);
            BlockShapes.MakeHollowLayers(intFarmLength + 11, intMapLength - (intFarmLength + 11), 72, 73,
                                         intFarmLength + 11, intMapLength - (intFarmLength + 11), intWallMaterial, 0, -1);
            // alternating blocks on top of the edges
            for (int a = intFarmLength + 6; a <= intMapLength - (intFarmLength + 6); a += 2)
            {
                BlockShapes.MakeBlock(a, 74, intFarmLength + 5, intWallMaterial, 2, 100, -1);
            }
            for (int a = intFarmLength + 12; a <= intMapLength - (intFarmLength + 12); a += 2)
            {
                BlockShapes.MakeBlock(a, 74, intFarmLength + 11, intWallMaterial, 2, 100, -1);
            }
            // ladder
            BlockHelper.MakeLadder((intMapLength / 2) - 5, 64, 72, intFarmLength + 11, 2, intWallMaterial);
            BlockShapes.MakeBlock((intMapLength / 2) - 5, 73, intFarmLength + 11, BlockType.AIR, 2, 100, -1);
            // decorations at the gates
            switch (strOutsideLights)
            {
                case "Fire":
                    // fire above the entrances
                    BlockShapes.MakeBlock((intMapLength / 2) - 1, 69, intFarmLength + 5, BlockType.NETHERRACK, 2, 100, -1);
                    BlockShapes.MakeBlock((intMapLength / 2), 69, intFarmLength + 5, BlockType.NETHERRACK, 2, 100, -1);
                    BlockShapes.MakeBlock((intMapLength / 2) - 1, 70, intFarmLength + 5, BlockType.FIRE, 2, 100, -1);
                    BlockShapes.MakeBlock((intMapLength / 2), 70, intFarmLength + 5, BlockType.FIRE, 2, 100, -1);
                    // fire on the outside walls
                    for (int a = intFarmLength + 8; a < (intMapLength / 2) - 9; a += 4)
                    {
                        BlockShapes.MakeBlock(a, 69, intFarmLength + 5, BlockType.NETHERRACK, 2, 100, -1);
                        BlockShapes.MakeBlock(a, 70, intFarmLength + 5, BlockType.FIRE, 2, 100, -1);
                    }
                    break;
                case "Torches":
                    // torches above the entrances
                    BlockHelper.MakeTorch((intMapLength / 2), 70, intFarmLength + 5, intWallMaterial, 2);
                    BlockHelper.MakeTorch((intMapLength / 2) - 1, 70, intFarmLength + 5, intWallMaterial, 2);
                    // torches on the outside walls
                    for (int a = intFarmLength + 8; a < (intMapLength / 2) - 9; a += 4)
                    {
                        BlockHelper.MakeTorch(a, 70, intFarmLength + 5, intWallMaterial, 2);
                    }
                    break;
                case "None":
                    break;
                default:
                    Debug.Fail("Invalid switch result");
                    break;
            }
            // torches on the inside walls
            for (int a = intFarmLength + 16; a < (intMapLength / 2); a += 4)
            {
                BlockHelper.MakeTorch(a, 69, intFarmLength + 11, intWallMaterial, 2);
            }
            // torches on the wall roofs
            for (int a = intFarmLength + 16; a < (intMapLength / 2); a += 4)
            {
                BlockShapes.MakeBlock(a, 73, intFarmLength + 8, BlockType.TORCH, 2, 100, -1);
            }
            MakeEmblem(intFarmLength, intMapLength, strCityEmblem);
        }
        private static void MakeEmblem(int intFarmLength, int intMapLength, string strCityEmblem)
        {
            if (strCityEmblem.ToLower() != "none")
            {
                int intBlockyBlock = RandomHelper.RandomNumber(BlockType.IRON_BLOCK, BlockType.GOLD_BLOCK,
                                                               BlockType.DIAMOND_BLOCK);
                string[] strEmblem;
                if (strCityEmblem == "Random")
                {
                    string[] strFiles = Directory.GetFiles("Resources", "Emblem*.txt");
                    strCityEmblem = RandomHelper.RandomItemFromArray(strFiles);
                    strEmblem = File.ReadAllLines(strCityEmblem);
                }
                else
                {
                    strEmblem = File.ReadAllLines(Path.Combine("Resources", "Emblem " + strCityEmblem + ".txt"));
                }

                for (int y = 0; y < strEmblem.GetLength(0); y++)
                {
                    strEmblem[y] = strEmblem[y].Replace("  ", " ");
                    strEmblem[y] = strEmblem[y].Replace((char)9, ' '); //tab
                    string[] strLine = strEmblem[y].Split(' ');
                    for (int x = 0; x < strLine.GetLength(0); x++)
                    {
                        string[] strSplit = strLine[x].Split(':');
                        if (strSplit.GetLength(0) == 1)
                        {
                            Array.Resize(ref strSplit, 2);
                        }
                        if (strSplit[0] == "-1")
                        {
                            strSplit[0] = intBlockyBlock.ToString();
                        }
                        BlockShapes.MakeBlock(((intMapLength / 2) - (strLine.GetLength(0) + 5)) + x, 71 - y,
                                              intFarmLength + 5, Convert.ToInt32(strSplit[0]), 2, 100,
                                              Convert.ToInt32(strSplit[1]));
                    }
                    for (int x = strLine.GetLength(0) + 1; x < strLine.GetLength(0) + 5; x++)
                    {
                        BlockShapes.MakeBlock((intMapLength / 2) - (5 + x), 69, intFarmLength + 5, BlockType.AIR, 2, 100, 0);
                        BlockShapes.MakeBlock((intMapLength / 2) - (5 + x), 70, intFarmLength + 5, BlockType.AIR, 2, 100, 0);
                    }
                }
            }
        }
    }
}