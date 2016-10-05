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
using Substrate.Entities;
using Substrate.TileEntities;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Mace
{
    class SourceWorld
    {
        public struct Building
        {
            public string strName;
            public int intSize;
            public int intSourceX;
            public int intSourceStartY;
            public int intSourceZ;
            public string strFrequency;
            public int intID;
            public bool booUnique;
        }
        static Building[] AllBuildings = new Building[0];
        static EntityPainting[] AllEntityPainting = new EntityPainting[0];
        
        static int intHouseNumber;
        static BlockManager bmSource;
        static ChunkManager cmSource;
        static ChunkManager cmDest;
        static List<string> lstCitySigns = new List<string>();
        static List<string> lstInstanceSigns = new List<string>();
        static bool booIncludeItemsInChests;

        public static bool SetupClass(BetaWorld worldDest, bool booIncludeItemsInChestsOriginal)
        {
            AllBuildings = new Building[0];
            intHouseNumber = 0;
            BetaWorld worldSource = BetaWorld.Open(Path.Combine("Resources", "Mace"));
            bmSource = worldSource.GetBlockManager();
            cmSource = worldSource.GetChunkManager();
            cmDest = worldDest.GetChunkManager();
            lstCitySigns.Clear();
            lstInstanceSigns.Clear();            
            booIncludeItemsInChests = booIncludeItemsInChestsOriginal;
            return ReadBuildings();
        }
        private static bool ReadBuildings()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path.Combine("Resources", "Buildings.xml"));
            XmlNodeList buildingList = xmlDoc.GetElementsByTagName("building");
            Array.Resize(ref AllBuildings, buildingList.Count);
            int intBuildings = 0;
            bool booFrequencyAboveRare = false;
            foreach (XmlNode xmlNode in buildingList)
            {
                XmlElement buildingElement = (XmlElement)xmlNode;
                AllBuildings[intBuildings].strName = buildingElement.GetElementsByTagName("name")[0].InnerText;
                AllBuildings[intBuildings].intSize = Convert.ToInt32(buildingElement.GetElementsByTagName("size")[0].InnerText);
                AllBuildings[intBuildings].intSourceX = Convert.ToInt32(buildingElement.GetElementsByTagName("source_x")[0].InnerText);
                AllBuildings[intBuildings].intSourceZ = Convert.ToInt32(buildingElement.GetElementsByTagName("source_z")[0].InnerText);
                AllBuildings[intBuildings].strFrequency = buildingElement.GetElementsByTagName("frequency")[0].InnerText;
                switch (AllBuildings[intBuildings].strFrequency.ToLower())
                {
                    case "very common":
                    case "common":
                        booFrequencyAboveRare = true;
                        break;
                }
                if (buildingElement.GetElementsByTagName("source_start_y")[0] != null)
                {
                    AllBuildings[intBuildings].intSourceStartY = Convert.ToInt32(buildingElement.GetElementsByTagName("source_start_y")[0].InnerText);
                }
                else
                {
                    AllBuildings[intBuildings].intSourceStartY = 63;
                }
                if (buildingElement.GetElementsByTagName("unique")[0] != null)
                {
                    AllBuildings[intBuildings].booUnique = (buildingElement.GetElementsByTagName("unique")[0].InnerText.ToLower() == "yes" ||
                                                            buildingElement.GetElementsByTagName("unique")[0].InnerText.ToLower() == "true");
                }
                AllBuildings[intBuildings].intID = intBuildings;
                intBuildings++;
            }
            ReadPaintings();
            if (!booFrequencyAboveRare)
            {
                MessageBox.Show("No buildings have a frequency of \"very common\" or \"common\". Stopping.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        
        private static void ReadPaintings()
        {
            foreach (ChunkRef chunk in cmSource)
            {
                foreach (EntityPainting entPainting in chunk.Entities.FindAll("Painting"))
                {
                    Array.Resize(ref AllEntityPainting, AllEntityPainting.Length + 1);
                    AllEntityPainting[AllEntityPainting.GetLength(0) - 1] = entPainting;
                }
            }
        }
        public static Building SelectRandomBuilding()
        {
            string[] strFrequencyList = { "very common", "common", "average", "rare", "very rare" };
            int intFrequency = RandomHelper.RandomWeightedNumber(new int[] { 7, 6, 5, 4, 3 });
            int intBuilding = 0;
            do
            {
                intBuilding = RandomHelper.Next(AllBuildings.GetLength(0));
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
        public static void InsertBuilding(BlockManager bmDest, int[,] intArea, int intBlockStart, int x1dest, int z1dest, Building bldInsert)
        {
            lstInstanceSigns.Clear();
            int intHighestSourceY = 0;
            int intSourceX = 0, intSourceZ = 0;
            int intRotate = -1;
            for (int intDistance = 0; intRotate == -1 && intDistance < 10; intDistance++)
            {
                for (int intCheck = 0; intRotate == -1 && intCheck <= bldInsert.intSize; intCheck++)
                {
                    if (CheckArea(intArea, x1dest + intCheck, z1dest - intDistance) == 1)
                    {
                        intRotate = 0;
                    }
                    else if (CheckArea(intArea, x1dest - intDistance, z1dest + intCheck) == 1)
                    {
                        intRotate = 1;
                    }
                    else if (CheckArea(intArea, x1dest + bldInsert.intSize + intDistance, z1dest + intCheck) == 1)
                    {
                        intRotate = 2;
                    }
                    else if (CheckArea(intArea, x1dest + intCheck, z1dest + bldInsert.intSize + intDistance) == 1)
                    {
                        intRotate = 3;
                    }
                }
            }
            if (intRotate == -1)
            {
                intRotate = RandomHelper.Next(4);
            }
            for (int x = 0; x < bldInsert.intSize; x++)
            {
                for (int z = 0; z < bldInsert.intSize; z++)
                {
                    switch (intRotate)
                    {
                        case 0:
                            intSourceX = x;
                            intSourceZ = z;
                            break;
                        case 1:
                            intSourceX = (bldInsert.intSize - 1) - z;
                            intSourceZ = x;
                            break;
                        case 2:
                            intSourceX = z;
                            intSourceZ = (bldInsert.intSize - 1) - x;
                            break;
                        case 3:
                            intSourceX = (bldInsert.intSize - 1) - x;
                            intSourceZ = (bldInsert.intSize - 1) - z;
                            break;
                    }

                    int intSourceEndY;
                    for (intSourceEndY = 128; intSourceEndY > 64; intSourceEndY--)
                    {
                        if (bmSource.GetID(intSourceX + bldInsert.intSourceX, intSourceEndY, intSourceZ + bldInsert.intSourceZ) != (int)BlockType.AIR)
                        {
                            break;
                        }
                    }
                    if (intSourceEndY > intHighestSourceY)
                    {
                        intHighestSourceY = intSourceEndY;
                    }
                    for (int y = bldInsert.intSourceStartY; y <= intSourceEndY; y++)
                    {
                        if (y != 64 || bmDest.GetID(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest) == (int)BlockType.AIR)
                        {
                            int intBlockID = bmSource.GetID(intSourceX + bldInsert.intSourceX, y, intSourceZ + bldInsert.intSourceZ);
                            int intBlockData = bmSource.GetData(intSourceX + bldInsert.intSourceX, y, intSourceZ + bldInsert.intSourceZ);
                            bmDest.SetID(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest, intBlockID);
                            bmDest.SetData(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest, intBlockData);

                            foreach (EntityPainting entPainting in AllEntityPainting)
                            {
                                if (entPainting.TileX == intSourceX + bldInsert.intSourceX &&
                                    entPainting.TileY == y &&
                                    entPainting.TileZ == intSourceZ + bldInsert.intSourceZ)
                                {
                                    EntityPainting entNewPainting = (EntityPainting)entPainting.Copy();
                                    entNewPainting.TileX = intBlockStart + x + x1dest;
                                    entNewPainting.TileY = y;
                                    entNewPainting.TileZ = intBlockStart + z + z1dest;
                                    entNewPainting.Position.X = entNewPainting.TileX;
                                    entNewPainting.Position.Z = entNewPainting.TileZ;
                                    entNewPainting.Direction = BlockHelper.RotatePortrait(entPainting.Direction, intRotate);
                                    ChunkRef chunkBuilding = cmDest.GetChunkRef((intBlockStart + x + x1dest) / 16, (intBlockStart + z + z1dest) / 16);
                                    chunkBuilding.Entities.Add(entNewPainting);
                                    cmDest.Save();
                                }
                            }

                            #region Rotation
                            if (intRotate > 0)
                            {
                                switch (intBlockID)
                                {
                                    case (int)BlockType.SIGN_POST:
                                        bmDest.SetData(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateSignPost(intBlockData, intRotate));
                                        break;
                                    case (int)BlockType.STONE_BUTTON:
                                        bmDest.SetData(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateButton(intBlockData, intRotate));
                                        break;
                                    case (int)BlockType.PUMPKIN:
                                    case (int)BlockType.JACK_O_LANTERN:
                                        bmDest.SetData(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest,
                                                       BlockHelper.RotatePumpkin(intBlockData, intRotate));
                                        break;
                                    case (int)BlockType.IRON_DOOR:
                                    case (int)BlockType.WOOD_DOOR:
                                        bmDest.SetData(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateDoor(intBlockData, intRotate));
                                        break;
                                    case (int)BlockType.TRAPDOOR:
                                        bmDest.SetData(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateTrapdoor(intBlockData, intRotate));
                                        break;
                                    case (int)BlockType.BED:
                                        bmDest.SetData(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateBed(intBlockData, intRotate));
                                        break;
                                    case (int)BlockType.REDSTONE_TORCH_OFF:
                                    case (int)BlockType.REDSTONE_TORCH_ON:
                                    case (int)BlockType.TORCH:
                                    case (int)BlockType.LEVER:
                                        bmDest.SetData(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateTorchOrLever(intBlockData, intRotate));
                                        break;
                                    case (int)BlockType.WALL_SIGN:
                                    case (int)BlockType.LADDER:
                                    case (int)BlockType.DISPENSER:
                                    case (int)BlockType.FURNACE:
                                        bmDest.SetData(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest,
                                                        BlockHelper.RotateWallSignOrLadderOrFurnanceOrDispenser(intBlockData, intRotate));
                                        break;
                                    case (int)BlockType.COBBLESTONE_STAIRS:
                                    case (int)BlockType.WOOD_STAIRS:
                                        bmDest.SetData(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest,
                                                        BlockHelper.RotateStairs(intBlockData, intRotate));
                                        break;
                                }
                            }
                            #endregion
                            #region Handle entities
                            switch (intBlockID)
                            {
                                case (int)BlockType.CHEST:
                                    TileEntityChest tec = (TileEntityChest)bmSource.GetTileEntity(intSourceX + bldInsert.intSourceX, y, intSourceZ + bldInsert.intSourceZ);
                                    if (booIncludeItemsInChests)
                                    {
                                        if (tec.Items[0] != null)
                                        {
                                            if (tec.Items[0].ID == ItemInfo.Paper.ID &&
                                                tec.Items[0].Count == 3)
                                            {
                                                tec = MakeHouseChest();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        tec.Items.ClearAllItems();
                                    }
                                    bmDest.SetTileEntity(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest, tec);
                                    break;
                                case (int)BlockType.FURNACE:
                                case (int)BlockType.MONSTER_SPAWNER:
                                case (int)BlockType.NOTE_BLOCK:
                                case (int)BlockType.JUKEBOX:
                                case (int)BlockType.TRAPDOOR:
                                    bmDest.SetTileEntity(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest,
                                                         bmSource.GetTileEntity(intSourceX + bldInsert.intSourceX, y, intSourceZ + bldInsert.intSourceZ));
                                    break;
                                case (int)BlockType.SIGN_POST:
                                case (int)BlockType.WALL_SIGN:
                                    #region Determine sign text
                                    int intUniqueType = 0;
                                    TileEntitySign tes = (TileEntitySign)bmSource.GetTileEntity(intSourceX + bldInsert.intSourceX, y, intSourceZ + bldInsert.intSourceZ);
                                    string strSourceSign = tes.Text1 + " " + tes.Text2 + " " + tes.Text3 + " " + tes.Text4;
                                    switch (strSourceSign.Substring(0, 1))
                                    {
                                        case "2":
                                        case "1":
                                        case "0":
                                            intUniqueType = Convert.ToInt32(strSourceSign.Substring(0, 1));
                                            strSourceSign = strSourceSign.Remove(0, 1);
                                            break;
                                        default:
                                            intUniqueType = 0;
                                            break;
                                    }
                                    if (strSourceSign.Contains("["))
                                    {
                                        string strSignText;
                                        bool booDuplicate;
                                        int intFail = 0;
                                        do
                                        {                                            
                                            strSignText = SignText(strSourceSign);
                                            booDuplicate = false;
                                            switch (intUniqueType)
                                            {
                                                case 1: // unique by instance
                                                    booDuplicate = lstInstanceSigns.Contains(strSignText);
                                                    break;
                                                case 2: // unique by city
                                                    booDuplicate = lstCitySigns.Contains(strSignText);
                                                    break;
                                            }
                                            intFail++;
                                            if (intFail > 100)
                                            {
                                                Debug.WriteLine("Could not make a unique sign for " + strSourceSign);
                                                booDuplicate = false;
                                            }
                                        } while (booDuplicate);
                                        lstCitySigns.Add(strSignText);
                                        lstInstanceSigns.Add(strSignText);
                                        string[] strRandomSign = BlockHelper.TextToSign(ConvertToSignText(strSignText));
                                        tes.Text1 = strRandomSign[0];
                                        tes.Text2 = strRandomSign[1];
                                        tes.Text3 = strRandomSign[2];
                                        tes.Text4 = strRandomSign[3];
                                    }
                                    bmDest.SetTileEntity(intBlockStart + x + x1dest, y, intBlockStart + z + z1dest, tes);
                                    break;
                                    #endregion
                            }
                            #endregion
                        }
                    }
                }
            }
        }
        private static int CheckArea(int[,] intArea, int X, int Z)
        {
            if (X >= 0 && Z >= 0 && X < intArea.GetLength(0) && Z < intArea.GetLength(1))
            {
                return intArea[X, Z];
            }
            else
            {
                return 0;
            }
        }
        private static string SignText(string strOverwrite)
        {
            if (strOverwrite.ToLower().StartsWith("[nb]") ||
                strOverwrite.ToLower().StartsWith("[nb1]") ||
                strOverwrite.ToLower().StartsWith("[nb2]"))
            {
                return NoticeBoard.GenerateNoticeboardSign(strOverwrite);
            }
            if (strOverwrite.ToLower().Contains("[house]"))
            {
                intHouseNumber++;
            }
            Regex reSquareBrackets = new Regex(@"(\[).*?(\])");
            MatchCollection mcSquareBrackets = reSquareBrackets.Matches(strOverwrite);
            // go backwards, because that way the indexes don't get messed up
            for (int intIndex = mcSquareBrackets.Count - 1; intIndex >= 0; intIndex--)
            {
                string strCurrentWord = strOverwrite.Substring(mcSquareBrackets[intIndex].Index + 1, mcSquareBrackets[intIndex].Length - 2);
                if (strCurrentWord.ToLower() == "house")
                {
                    strCurrentWord = "House " + intHouseNumber;
                }
                else if (File.Exists(Path.Combine("Resources", strCurrentWord + ".txt")))
                {
                    strCurrentWord = RandomHelper.RandomFileLine(Path.Combine("Resources", strCurrentWord + ".txt"));
                }
                else
                {
                    strCurrentWord = "?" + strCurrentWord + "?";
                }
                strOverwrite = strOverwrite.Remove(mcSquareBrackets[intIndex].Index, mcSquareBrackets[intIndex].Length);
                strOverwrite = strOverwrite.Insert(mcSquareBrackets[intIndex].Index, strCurrentWord);
            }
            return strOverwrite;
        }
        public static string ConvertToSignText(string strText)
        {
            string[] strSignText = new string[4] { "", "", "", "" };
            strText = strText.Replace("~", "~ ");
            strText = strText.Replace("  ", " ").Trim();
            string[] strWords = strText.Split(' ');
            int intLine = 0;
            for (int a = 0; a < strWords.GetLength(0); a++)
            {
                if (strSignText[intLine].Length + strWords[a].Replace("~", "").Length + 1 > 14)
                {
                    intLine++;
                    if (intLine > 3)
                    {
                        Debug.WriteLine("Sign text is too long: " + strText);
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
        private static TileEntityChest MakeHouseChest()
        {
            TileEntityChest tec = new TileEntityChest();
            for (int intItems = RandomHelper.Next(4, 8); intItems >= 0; intItems--)
            {
                switch (RandomHelper.Next(0, 17))
                {
                    case 0:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Apple.ID, 1);
                        break;
                    case 1:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Book.ID, 1);
                        break;
                    case 2:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Bowl.ID, 1);
                        break;
                    case 3:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Bread.ID, 1);
                        break;
                    case 4:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Cake.ID, 1);
                        break;
                    case 5:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Clock.ID, 1);
                        break;
                    case 6:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Compass.ID, 1);
                        break;
                    case 7:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Cookie.ID, 1);
                        break;
                    case 8:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Diamond.ID, 1);
                        break;
                    case 9:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Egg.ID, 1);
                        break;
                    case 10:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Feather.ID, 1);
                        break;
                    case 11:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.FishingRod.ID, 1);
                        break;
                    case 12:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.GoldMusicDisc.ID, 1);
                        break;
                    case 13:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.GreenMusicDisc.ID, 1);
                        break;
                    case 14:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Paper.ID, 1);
                        break;
                    case 15:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.Saddle.ID, 1);
                        break;
                    case 16:
                        tec.Items[intItems] = BlockHelper.MakeItem(ItemInfo.String.ID, 1);
                        break;
                }
            }
            return tec;
        }
    }
}
