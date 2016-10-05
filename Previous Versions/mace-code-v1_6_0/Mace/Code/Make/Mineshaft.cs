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
    along with this program.  If not, see <http://www.gnu.org/licenses/>
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Substrate;
using Substrate.TileEntities;

namespace Mace
{
    class Mineshaft
    {
        private const int SPLIT_CHANCE = 7;
        private const int MULTIPLIER = 5;
        static Dictionary<string, string> dictResourceIDs =
          MakeDictionaryFromChildNodeAttributes(Path.Combine("Resources", "MineshaftChests.xml"), "ids");
        static Dictionary<string, string> dictResourceAmounts =
          MakeDictionaryFromChildNodeAttributes(Path.Combine("Resources", "MineshaftChests.xml"), "amounts");
        static string[] strResourceNames;
        static int[] intResourceChances;
        static int intBlockStart;

        public static void MakeMineshaft(BetaWorld world, BlockManager bm, int intFarmSize, int intMapSize)
        {
            intBlockStart = intFarmSize + 13;
            int intMineshaftSize = (1 + intMapSize) - (intBlockStart * 2);
            intBlockStart -= 2;
            for (int intLevel = 1; intLevel <= 7; intLevel++)
            {
                MakeLevel(world, bm, intLevel, intMineshaftSize);
            }
            BlockShapes.MakeSolidBox(intMapSize / 2, intMapSize / 2, 3, 64, (intMapSize / 2) + 1, (intMapSize / 2) + 1, (int)BlockType.WOOD, 0);
            BlockHelper.MakeLadder(intMapSize / 2, 4, 63, intMapSize / 2, 0, (int)BlockType.WOOD);
            //BlockShapes.MakeSolidBox(intMapSize / 2, intMapSize / 2, 4, 63, intMapSize / 2, intMapSize / 2, (int)BlockType.AIR, 0);
            bm.SetID(intMapSize / 2, 64, intMapSize / 2, (int)BlockType.TRAPDOOR);
        }

        private static void MakeLevel(BetaWorld world, BlockManager bm, int intDepth, int intMineshaftSize)
        {
            strResourceNames = ValueFromXMLElement(Path.Combine("Resources", "MineshaftChests.xml"), "level" + intDepth, "names").Split(',');
            intResourceChances = StringArrayToIntArray(
               ValueFromXMLElement(Path.Combine("Resources", "MineshaftChests.xml"), "level" + intDepth, "chances").Split(','));
            int intTorchChance = Convert.ToInt32(ValueFromXMLElement(Path.Combine("Resources", "MineshaftChests.xml"), "level" + intDepth, "torch_chance"));

            int[,] intMap = new int[intMineshaftSize, intMineshaftSize];
            int intXPosOriginal = intMap.GetLength(0) / 2;
            int intZPosOriginal = intMap.GetLength(1) / 2;
            int[,] intArea = new int[intMap.GetLength(0) / MULTIPLIER, intMap.GetLength(1) / MULTIPLIER];
            int intXPos = intXPosOriginal / MULTIPLIER;
            int intZPos = intZPosOriginal / MULTIPLIER;
            intArea[intXPos, intZPos] = 1;
            CreateRouteXPlus(intArea, intXPos + 1, intZPos, 0);
            CreateRouteZPlus(intArea, intXPos, intZPos + 1, 1);
            CreateRouteXMinus(intArea, intXPos - 1, intZPos, 2);
            CreateRouteZMinus(intArea, intXPos, intZPos - 1, 3);
            int intOffsetX = (intXPosOriginal - (intXPos * MULTIPLIER)) - 2;
            int intOffsetZ = (intZPosOriginal - (intZPos * MULTIPLIER)) - 2;
            int intGhostX = 0, intGhostZ = 0;
            bool booGhost = false;
            if (intDepth == 7)
            {
                for (int x = 1; x < intArea.GetLength(0) - 1 && !booGhost; x++)
                {
                    for (int z = 1; z < intArea.GetLength(1) - 1 && !booGhost; z++)
                    {
                        if (intArea[x, z] == 1 && intArea[x + 1, z] == 0 && intArea[x - 1, z] == 0 && intArea[x, z + 1] == 0)
                        {
                            intArea[x, z] = 9;
                            intGhostX = (x * MULTIPLIER) + intOffsetX;
                            intGhostZ = (z * MULTIPLIER) + intOffsetZ;
                            booGhost = true;
                        }
                    }
                }
            }
            for (int x = 4; x < intMap.GetLength(0) - 4; x++)
            {
                for (int z = 4; z < intMap.GetLength(1) - 4; z++)
                {
                    if (intArea.GetLength(0) > x / MULTIPLIER && intArea.GetLength(1) > z / MULTIPLIER)
                    {
                        if (intMap[x + intOffsetX, z + intOffsetZ] < 4)
                        {
                            intMap[x + intOffsetX, z + intOffsetZ] = intArea[x / MULTIPLIER, z / MULTIPLIER];
                        }
                    }
                    if ((x + 3) % 5 == 0 && (z + 3) % 5 == 0 && intArea[x / MULTIPLIER, z / MULTIPLIER] == 1)
                    {
                        if (intArea[(x / MULTIPLIER) + 1, z / MULTIPLIER] == 1)
                        {
                            for (int x2 = 0; x2 < 5; x2++)
                            {
                                if (x2 == 1 || x2 == 3)
                                {
                                    intMap[x + intOffsetX + 3, z + intOffsetZ + x2 - 2] = 8;
                                    intMap[x + intOffsetX + 2, z + intOffsetZ + x2 - 2] = 8;
                                }
                                else
                                {
                                    intMap[x + intOffsetX + 3, z + intOffsetZ + x2 - 2] = 5;
                                    intMap[x + intOffsetX + 2, z + intOffsetZ + x2 - 2] = 5;
                                }
                            }
                            for (int x2 = 0; x2 <= 5; x2++)
                            {
                                if (intMap[x + intOffsetX + x2, z + intOffsetZ] == 5)
                                {
                                    intMap[x + intOffsetX + x2, z + intOffsetZ] = 4;
                                }
                                else
                                {
                                    intMap[x + intOffsetX + x2, z + intOffsetZ] = 7;
                                }
                            }
                        }
                        if (intArea[x / MULTIPLIER, (z / MULTIPLIER) + 1] == 1)
                        {
                            for (int z2 = 0; z2 < 5; z2++)
                            {
                                if (z2 == 1 || z2 == 3)
                                {
                                    intMap[x + intOffsetX + z2 - 2, z + intOffsetZ + 3] = 8;
                                    intMap[x + intOffsetX + z2 - 2, z + intOffsetZ + 2] = 8;
                                }
                                else
                                {
                                    intMap[x + intOffsetX + z2 - 2, z + intOffsetZ + 3] = 5;
                                    intMap[x + intOffsetX + z2 - 2, z + intOffsetZ + 2] = 5;
                                }
                            }
                            for (int z2 = 0; z2 <= 5; z2++)
                            {
                                if (intMap[x + intOffsetX, z + intOffsetZ + z2] == 5)
                                {
                                    intMap[x + intOffsetX, z + intOffsetZ + z2] = 4;
                                }
                                else
                                {
                                    intMap[x + intOffsetX, z + intOffsetZ + z2] = 7;
                                }
                            }
                        }
                        if (intArea[x / MULTIPLIER, z / MULTIPLIER] == 1)
                        {
                            MakeChestAndOrTorch(intArea, intMap, (x - 3) / MULTIPLIER, z / MULTIPLIER, x + intOffsetX - 2, z + intOffsetZ);
                            MakeChestAndOrTorch(intArea, intMap, (x + 3) / MULTIPLIER, z / MULTIPLIER, x + intOffsetX + 2, z + intOffsetZ);
                            MakeChestAndOrTorch(intArea, intMap, x / MULTIPLIER, (z - 3) / MULTIPLIER, x + intOffsetX, z + intOffsetZ - 2);
                            MakeChestAndOrTorch(intArea, intMap, x / MULTIPLIER, (z + 3) / MULTIPLIER, x + intOffsetX, z + intOffsetZ + 2);
                        }
                    }
                }
            }
            intMap[intXPosOriginal, intZPosOriginal] = 3;
            int intSupportMaterial = RandomHelper.RandomNumber((int)BlockType.WOOD, (int)BlockType.WOOD_PLANK, (int)BlockType.FENCE);
            intSupportMaterial = (int)BlockType.WOOD_PLANK;
            for (int x = 0; x < intMap.GetLength(0); x++)
            {
                for (int z = 0; z < intMap.GetLength(1); z++)
                {
                    switch(intMap[x, z])
                    {
                        case 0:
                            break;
                        case 1:
                            for (int y = 39 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.AIR);
                            }
                            break;
                        case 9:
                            for (int y = 39 - (5 * intDepth); y <= 41 - (5 * (intDepth - 1)); y++)
                            {
                                bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.AIR);
                            }
                            break;
                        case 2:
                            break;
                        case 4:
                            for (int y = 38 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                if (y == 38 - (5 * intDepth))
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.GRAVEL);
                                }
                                else if (y == 39 - (5 * intDepth))
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.RAILS);
                                }
                                else if (y == 40 - (5 * intDepth))
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.AIR);
                                }
                                else
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, intSupportMaterial);
                                }
                            }
                            break;
                        case 5:
                            for (int y = 39 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                bm.SetID(x + intBlockStart, y, z + intBlockStart, intSupportMaterial);
                            }
                            break;
                        case 6:
                            for (int y = 39 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                if (y == 39 - (5 * intDepth) &&
                                    RandomHelper.NextDouble() > 0.9)
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.CHEST);
                                    MakeChestItems(bm, x + intBlockStart, y, z + intBlockStart);
                                }
                                else if (y == 41 - (5 * intDepth) &&
                                         RandomHelper.NextDouble() < (double)intTorchChance / 100)
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.TORCH);
                                    if (intMap[x - 1, z] == 0)
                                    {
                                        bm.SetData(x + intBlockStart, y, z + intBlockStart, 1);
                                    }
                                    else if (intMap[x + 1, z] == 0)
                                    {
                                        bm.SetData(x + intBlockStart, y, z + intBlockStart, 2);
                                    }
                                    else if (intMap[x, z - 1] == 0)
                                    {
                                        bm.SetData(x + intBlockStart, y, z + intBlockStart, 3);
                                    }
                                    else
                                    {
                                        bm.SetData(x + intBlockStart, y, z + intBlockStart, 4);
                                    }
                                }
                                else
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.AIR);
                                }
                            }
                            break;
                        case 7:
                        case 3:
                            for (int y = 38 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                if (y == 38 - (5 * intDepth))
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.GRAVEL);
                                }
                                else if (y == 39 - (5 * intDepth))
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.RAILS);
                                }
                                else
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.AIR);
                                }
                            }
                            break;
                        case 8:
                            for (int y = 39 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                if (y == 41 - (5 * intDepth))
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, intSupportMaterial);
                                }
                                else
                                {
                                    bm.SetID(x + intBlockStart, y, z + intBlockStart, (int)BlockType.AIR);
                                }
                            }
                            break;
                    }
                }
            }
            for (int x = 0; x < intMap.GetLength(0); x++)
            {
                for (int z = 0; z < intMap.GetLength(1); z++)
                {
                    switch (intMap[x, z])
                    {
                        case 3:
                        case 4:
                        case 7:
                            BlockHelper.MakeRail(x + intBlockStart, 39 - (5 * intDepth), z + intBlockStart);
                            break;
                    }
                }
            }
            if (booGhost)
            {
                BlockShapes.MakeSolidBox(intBlockStart + intGhostX - 1, intBlockStart + intGhostX + 5,
                                         39 - (5 * intDepth), 46 - (5 * intDepth),
                                         intBlockStart + intGhostZ - 1, intBlockStart + intGhostZ + 5, (int)BlockType.AIR, 0);
                for (int x = intBlockStart + intGhostX; x <= intBlockStart + intGhostX + 4; x++)
                {
                    for (int y = 39 - (5 * intDepth); y <= 44 - (5 * intDepth); y++)
                    {
                        for (int z = intBlockStart + intGhostZ + 3; z <= intBlockStart + intGhostZ + 4; z++)
                        {
                            bm.SetID(x, y, z, (int)BlockType.WOOL);
                            bm.SetData(x, y, z, (int)WoolColor.WHITE);
                        }
                    }
                }
                bm.SetID(intBlockStart + intGhostX + 0, 44 - (5 * intDepth), intBlockStart + intGhostZ + 3, (int)BlockType.TORCH);
                bm.SetID(intBlockStart + intGhostX + 0, 44 - (5 * intDepth), intBlockStart + intGhostZ + 4, (int)BlockType.TORCH);
                bm.SetID(intBlockStart + intGhostX + 4, 44 - (5 * intDepth), intBlockStart + intGhostZ + 3, (int)BlockType.TORCH);
                bm.SetID(intBlockStart + intGhostX + 4, 44 - (5 * intDepth), intBlockStart + intGhostZ + 4, (int)BlockType.TORCH);
                bm.SetData(intBlockStart + intGhostX + 0, 44 - (5 * intDepth), intBlockStart + intGhostZ + 3, (int)TorchOrientation.FLOOR);
                bm.SetData(intBlockStart + intGhostX + 0, 44 - (5 * intDepth), intBlockStart + intGhostZ + 4, (int)TorchOrientation.FLOOR);
                bm.SetData(intBlockStart + intGhostX + 4, 44 - (5 * intDepth), intBlockStart + intGhostZ + 3, (int)TorchOrientation.FLOOR);
                bm.SetData(intBlockStart + intGhostX + 4, 44 - (5 * intDepth), intBlockStart + intGhostZ + 4, (int)TorchOrientation.FLOOR);
                bm.SetID(intBlockStart + intGhostX + 1, 43 - (5 * intDepth), intBlockStart + intGhostZ + 3, (int)BlockType.OBSIDIAN);
                bm.SetID(intBlockStart + intGhostX + 3, 43 - (5 * intDepth), intBlockStart + intGhostZ + 3, (int)BlockType.OBSIDIAN);
                bm.SetID(intBlockStart + intGhostX + 1, 39 - (5 * intDepth), intBlockStart + intGhostZ + 2, (int)BlockType.TORCH);
                bm.SetData(intBlockStart + intGhostX + 1, 39 - (5 * intDepth), intBlockStart + intGhostZ + 2, (int)TorchOrientation.FLOOR);
                bm.SetID(intBlockStart + intGhostX + 2, 39 - (5 * intDepth), intBlockStart + intGhostZ + 2, (int)BlockType.SIGN_POST);
                bm.SetData(intBlockStart + intGhostX + 2, 39 - (5 * intDepth), intBlockStart + intGhostZ + 2, (int)SignPostOrientation.EAST);
                BlockHelper.MakeSignPost(intBlockStart + intGhostX + 2, 39 - (5 * intDepth), intBlockStart + intGhostZ + 2,
                                         "Ghostdancer:|Victim of the|Creeper Rush|of '09");
                bm.SetID(intBlockStart + intGhostX + 3, 39 - (5 * intDepth), intBlockStart + intGhostZ + 2, (int)BlockType.TORCH);
                bm.SetData(intBlockStart + intGhostX + 3, 39 - (5 * intDepth), intBlockStart + intGhostZ + 2, (int)TorchOrientation.FLOOR);
                
            }
            world.Save();
            //File.WriteAllText("c:\\output.txt", TwoDimensionalArrayToString(intMap));
        }
        private static void MakeChestAndOrTorch(int[,] intArea, int[,] intMap,
                                                int intAreaX, int intAreaZ, int intMapX, int intMapZ)
        {
            if (intArea[intAreaX, intAreaZ] == 0)
            {
                intMap[intMapX, intMapZ] = 6;
            }
        }
        private static void MakeChestItems(BlockManager bm, int x, int y, int z)
        {
            string strResource = strResourceNames[RandomHelper.RandomWeightedNumber(intResourceChances)];
            string strAmount;
            dictResourceAmounts.TryGetValue(strResource.ToLower(), out strAmount);
            strAmount = strAmount ?? "1,1";
            int intAmount = RandomHelper.Next(Convert.ToInt32(strAmount.Split(',')[0]),
                                              Convert.ToInt32(strAmount.Split(',')[1]) + 1);
            string strBlockID;
            dictResourceIDs.TryGetValue(strResource.ToLower(), out strBlockID);
            TileEntityChest tec = new TileEntityChest();
            tec.Items[0] = BlockHelper.MakeItem(Convert.ToInt32(strBlockID), intAmount);
            bm.SetTileEntity(x, y, z, tec);
        }
        private static void CreateRouteXMinus(int[,] intArea, int intXPos, int intZPos, int intInvalidDirection)
        {
            if (intXPos > 2)
            {
                intArea[intXPos, intZPos] = 1;
                int intLength = RandomHelper.Next(intXPos / 2, intXPos + 1);
                for (int X = intXPos - 1; X > intXPos - intLength; X--)
                {
                    if (IsZeros(intArea, X - 1, intZPos, X, intZPos + 1))
                    {
                        intArea[X, intZPos] = 1;
                        if (RandomHelper.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 3)
                        {
                            CreateRouteZPlus(intArea, X, intZPos, intInvalidDirection);
                        }
                        if (RandomHelper.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 1)
                        {
                            CreateRouteZMinus(intArea, X, intZPos, intInvalidDirection);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        private static void CreateRouteXPlus(int[,] intArea, int intXPos, int intZPos, int intInvalidDirection)
        {
            if (intArea.GetLength(0) - intXPos > 2)
            {
                intArea[intXPos, intZPos] = 1;
                int intLength = RandomHelper.Next((intArea.GetLength(0) - intXPos) / 2, intArea.GetLength(0) - intXPos);
                for (int X = intXPos + 1; X < intXPos + intLength; X++)
                {
                    if (IsZeros(intArea, X, intZPos - 1, X + 1, intZPos + 1))
                    {
                        intArea[X, intZPos] = 1;
                        if (RandomHelper.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 3)
                        {
                            CreateRouteZPlus(intArea, X, intZPos, intInvalidDirection);
                        }
                        if (RandomHelper.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 1)
                        {
                            CreateRouteZMinus(intArea, X, intZPos, intInvalidDirection);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        private static void CreateRouteZMinus(int[,] intArea, int intXPos, int intZPos, int intInvalidDirection)
        {
            if (intZPos > 2)
            {
                intArea[intXPos, intZPos] = 1;
                int intLength = RandomHelper.Next(intZPos / 2, intZPos + 1);
                for (int Z = intZPos - 1; Z > intZPos - intLength; Z--)
                {
                    if (IsZeros(intArea, intXPos - 1, Z - 1, intXPos + 1, Z))
                    {
                        intArea[intXPos, Z] = 1;
                        if (RandomHelper.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 2)
                        {
                            CreateRouteXPlus(intArea, intXPos, Z, intInvalidDirection);
                        }
                        if (RandomHelper.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 0)
                        {
                            CreateRouteXMinus(intArea, intXPos, Z, intInvalidDirection);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        private static void CreateRouteZPlus(int[,] intArea, int intXPos, int intZPos, int intInvalidDirection)
        {
            if (intArea.GetLength(1) - intZPos > 2)
            {
                intArea[intXPos, intZPos] = 1;
                int intLength = RandomHelper.Next((intArea.GetLength(1) - intZPos) / 2, intArea.GetLength(1) - intZPos);
                for (int Z = intZPos + 1; Z < intZPos + intLength; Z++)
                {
                    if (IsZeros(intArea, intXPos - 1, Z, intXPos + 1, Z + 1))
                    {
                        intArea[intXPos, Z] = 1;
                        if (RandomHelper.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 2)
                        {
                            CreateRouteXPlus(intArea, intXPos, Z, intInvalidDirection);
                        }
                        if (RandomHelper.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 0)
                        {
                            CreateRouteXMinus(intArea, intXPos, Z, intInvalidDirection);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        public static Dictionary<string, string> MakeDictionaryFromChildNodeAttributes(string strFilenameXML, string strRootNode)
        {
            Dictionary<string, string> dictFriendlyNames = new Dictionary<string, string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFilenameXML);
            foreach (XmlNode xmlRootNode in xmlDoc.GetElementsByTagName(strRootNode))
            {
                foreach (XmlNode xmlChildNode in xmlRootNode)
                {
                    dictFriendlyNames.Add(xmlChildNode.Name, xmlChildNode.Attributes[0].InnerText);
                }
            }
            return dictFriendlyNames;
        }      
        private static bool IsZeros(int[,] intArray, int x1, int z1, int x2, int z2)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int z = z1; z <= z2; z++)
                {
                    if (intArray[x, z] > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static string TwoDimensionalArrayToString(int[,] intArray)
        {
            string[] strOutputChars = { "0", "\\", "*", "!", "=", "W", "C", "S", "P", "G" };
            string strOutput = "";
            for (int x = 0; x < intArray.GetLength(0); x++)
            {
                for (int z = 0; z < intArray.GetLength(1); z++)
                {                    
                    strOutput += strOutputChars[intArray[x, z]];
                }
                strOutput += "\r\n";
            }
            return strOutput;
        }
        private static int[,] EnlargeTwoDimensionalArray(int[,] intArea, int intMultiplier)
        {
            int[,] intReturn = new int[intArea.GetLength(0) * intMultiplier, intArea.GetLength(1) * intMultiplier];
            for (int x = 0; x < intReturn.GetLength(0); x++)
            {
                for (int z = 0; z < intReturn.GetLength(1); z++)
                {
                    intReturn[x, z] = intArea[x / intMultiplier, z / intMultiplier];
                }
            }
            return intReturn;
        }
        private static string ValueFromXMLElement(string strFilenameXML, string strParentNode, string strChildNode)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFilenameXML);
            foreach (XmlNode xmlRootNode in xmlDoc.GetElementsByTagName(strParentNode))
            {
                foreach (XmlNode xmlChildNode in xmlRootNode)
                {
                    if (xmlChildNode.Name == strChildNode)
                    {
                        return xmlChildNode.InnerText;
                    }
                }
            }
            return "";
        }
        private static int[] StringArrayToIntArray(string[] strOriginal)
        {
            int[] intNumbers = new int[strOriginal.Length];
            for (int a = 0; a < strOriginal.Length; a++)
            {
                int.TryParse(strOriginal[a], out intNumbers[a]);
            }
            return intNumbers;
        }
    }
}
