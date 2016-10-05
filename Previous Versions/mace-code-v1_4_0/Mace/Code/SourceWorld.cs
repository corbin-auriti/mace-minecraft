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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using Substrate;
using Substrate.TileEntities;
using System.Diagnostics;

namespace Mace
{
    class SourceWorld
    {
        public struct Building
        {
            public string strName;
            public int intSize;
            public int intSourceX;
            public int intSourceZ;
            public string strFrequency;
            public int intID;
        }
        static Building[] AllBuildings = new Building[0];
        static Random rand = new Random();
        static int intHouseNumber;
        static BlockManager bmSource;

        public static void SetupClass()
        {
            intHouseNumber = 0;
            BetaWorld worldSource = BetaWorld.Open("Resources\\Mace");
            bmSource = worldSource.GetBlockManager();
            ReadBuildings();
        }

        public static void ReadBuildings()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"Resources\\Buildings.xml");
            XmlNodeList buildingList = xmlDoc.GetElementsByTagName("building");
            Array.Resize(ref AllBuildings, buildingList.Count);
            int intBuildings = 0;
            foreach (XmlNode xmlNode in buildingList)
            {
                XmlElement buildingElement = (XmlElement)xmlNode;
                AllBuildings[intBuildings].strName = buildingElement.GetElementsByTagName("name")[0].InnerText;
                AllBuildings[intBuildings].intSize = Convert.ToInt32(buildingElement.GetElementsByTagName("size")[0].InnerText);
                AllBuildings[intBuildings].intSourceX = Convert.ToInt32(buildingElement.GetElementsByTagName("source_x")[0].InnerText);
                AllBuildings[intBuildings].intSourceZ = Convert.ToInt32(buildingElement.GetElementsByTagName("source_z")[0].InnerText);
                AllBuildings[intBuildings].strFrequency = buildingElement.GetElementsByTagName("frequency")[0].InnerText;
                AllBuildings[intBuildings].intID = intBuildings;
                intBuildings++;
            }
        }
        public static Building SelectRandomBuilding()
        {
            string[] strFrequencyList = { "very common", "common", "average", "rare", "very rare" };
            int intFrequency = RandomHelper.RandomWeightedNumber(new int[] { 12, 9, 6, 3, 1 });
            int intBuilding = 0;
            do
            {
                intBuilding = rand.Next(AllBuildings.GetLength(0));
            } while (AllBuildings[intBuilding].strFrequency != strFrequencyList[intFrequency]);
            return AllBuildings[intBuilding];
        }
        public static Building GetBuilding(int intID)
        {
            return AllBuildings[intID];
        }
        public static Building GetBuilding(string strName)
        {
            for (int a = 0; a <= AllBuildings.GetUpperBound(0); a++)
            {
                if (AllBuildings[a].strName.ToLower() == strName.ToLower())
                {
                    return AllBuildings[a];
                }
            }
            Debug.Assert(false);
            MessageBox.Show("Error: Could not find a building called " + strName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return AllBuildings[0];
        }

        public static void InsertBuilding(BlockManager bmDest, int intBlockStart, int x1dest, int z1dest, Building bldInsert)
        {
            for (int x = 0; x < bldInsert.intSize; x++)
            {
                for (int z = 0; z < bldInsert.intSize; z++)
                {
                    for (int y = 45; y <= 80; y++)
                    {
                        if (y != 64 || bmDest.GetID(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest) == (int)BlockType.AIR)
                        {
                            int intBlockID = bmSource.GetID(x + bldInsert.intSourceX, y, z + bldInsert.intSourceZ);
                            bmDest.SetID(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest, intBlockID);
                            bmDest.SetData(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest, bmSource.GetData(x + bldInsert.intSourceX, y, z + bldInsert.intSourceZ));
                            switch (intBlockID)
                            {
                                case (int)BlockType.CHEST:
                                case (int)BlockType.FURNACE:
                                case (int)BlockType.MONSTER_SPAWNER:
                                case (int)BlockType.NOTE_BLOCK:
                                case (int)BlockType.JUKEBOX:
                                case (int)BlockType.TRAPDOOR:
                                    bmDest.SetTileEntity(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest,
                                                         bmSource.GetTileEntity(x + bldInsert.intSourceX, y, z + bldInsert.intSourceZ));
                                    break;
                                case (int)BlockType.SIGN_POST:
                                case (int)BlockType.WALL_SIGN:
                                    TileEntitySign tes = (TileEntitySign)bmSource.GetTileEntity(x + bldInsert.intSourceX, y, z + bldInsert.intSourceZ);
                                    if (tes.Text1.StartsWith("[") && tes.Text1.EndsWith("]"))
                                    {
                                        string[] strRandomSign = BlockHelper.TextToSign(SignText(tes.Text1));
                                        tes.Text1 = strRandomSign[0];
                                        tes.Text2 = strRandomSign[1];
                                        tes.Text3 = strRandomSign[2];
                                        tes.Text4 = strRandomSign[3];
                                    }
                                    bmDest.SetTileEntity(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest, tes);
                                    break;
                            }
                        }
                    }
                }
            }
        }
        private static string SignText(string strOverwrite)
        {
            switch (strOverwrite)
            {
                case "[house]":
                    return "|House Number|" + ++intHouseNumber + "|";
                case "[bakery]":
                    return ConvertToSignText(RandomHelper.RandomFileLine("Resources\\Bakery.txt"));
                case "[tavern]":
                    return "|The " + RandomHelper.RandomFileLine("Resources\\TavernStartingWords.txt") +
                           "|" + RandomHelper.RandomFileLine("Resources\\TavernEndingWords.txt") +
                           "|" + RandomHelper.RandomFileLine("Resources\\TavernTypes.txt");
                case "[greenhouse]":
                    return "|Greenhouse||";
                case "[warehouse]":
                    return "|Warehouse||";
                case "[statue1]":
                    return ConvertToSignText(RandomHelper.RandomFileLine("Resources\\Statue1.txt"));
                case "[statue2]":
                    return ConvertToSignText(RandomHelper.RandomFileLine("Resources\\Statue2.txt"));
                case "[gravestone]":
                    return ConvertToSignText(RandomHelper.RandomFileLine("Resources\\Gravestone.txt"));
                case "[graveyard]":
                    return "|Graveyard||";
                case "[crafthut]":
                    return "|Crafting Hut||";
                case "[church]":
                    return "The " + RandomHelper.RandomFileLine("Resources\\ChurchTypes.txt") +
                           "|of the " + RandomHelper.RandomFileLine("Resources\\ChurchStartingWords.txt") +
                           "|" + RandomHelper.RandomFileLine("Resources\\ChurchEndingWords.txt") +
                           "|";
                case "[brothel]":
                    return "|" + RandomHelper.RandomFileLine("Resources\\BrothelStartingWords.txt") +
                           "|" + RandomHelper.RandomFileLine("Resources\\BrothelEndingWords.txt") +
                           "|" + RandomHelper.RandomFileLine("Resources\\BrothelTypes.txt");
                case "[coffin]":
                    return ConvertToSignText(RandomHelper.RandomFileLine("Resources\\Gravestone.txt"));
                case "[library]":
                    return ConvertToSignText(RandomHelper.RandomFileLine("Resources\\Library.txt"));
                default:
                    Debug.Fail("No handler for " + strOverwrite + " signs.");
                    return "|||";
            }
        }
        public static string ConvertToSignText(string strText)
        {
            string[] strSignText = new string[4] { "", "", "", "" };
            strText = strText.Replace("~", "~ ");
            string[] strWords = strText.Split(' ');            
            int intLine = 0;
            for (int a = 0; a < strWords.GetLength(0); a++)
            {
                if (strSignText[intLine].Length + strWords[a].Length + 1 > 14)
                {
                    intLine++;
                    if (intLine > 3)
                    {
                        Debug.Fail("Sign text is too long: " + strText);
                        break;
                    }
                }
                strSignText[intLine] += ' ' + strWords[a];
                strSignText[intLine] = strSignText[intLine].Trim();
                // this is used to force a new line
                if (strSignText[intLine].EndsWith("~"))
                {
                    // get rid of the last letter
                    strSignText[intLine] = strSignText[intLine].Substring(0, strSignText[intLine].Length - 1);
                    intLine++;
                }
            }
            // move the text down if we've only used half the sign
            if (strSignText[2] == "")
            {
                strSignText[2] = strSignText[1];
                strSignText[1] = strSignText[0];
                strSignText[0] = "";
            }
            return String.Join("|", strSignText);
        }    
    }
}
