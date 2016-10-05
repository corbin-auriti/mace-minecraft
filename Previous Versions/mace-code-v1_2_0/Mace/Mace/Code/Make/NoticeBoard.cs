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
    class NoticeBoard
    {
        static Random rand = new Random();
        public static void MakeNoticeBoard(BlockManager bm, int intFarmSize, int intMapSize)
        {
            BlockHelper.MakeSign((intMapSize / 2) - 8, 67, intMapSize - (intFarmSize + 11),
                                 "|Public|Notice Board|", (int)BlockType.STONE);
            for (int x = (intMapSize / 2) - 9; x <= (intMapSize / 2) - 7; x++)
                for (int y = 65; y <= 66; y++)
                    if (x == (intMapSize / 2) - 7 && y == 66)
                    {
                        Version ver = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
                        BlockHelper.MakeSign(x, y, intMapSize - (intFarmSize + 11), String.Format("Created by|Mace v{0}.{1}.{2}|by Robson.|Have fun :)",
                                                                                                  ver.Major, ver.Minor, ver.Build), (int)BlockType.STONE);
                    }
                    else
                    {
                        BlockHelper.MakeSign(x, y, intMapSize - (intFarmSize + 11), RandomSign(), (int)BlockType.STONE);
                    }
        }
        public static string RandomSign()
        {
            string strSignText = "*|*|*|*";
            switch (rand.Next(9))
            {
                case 0:
                case 1:
                case 2: strSignText = RandomHelper.RandomString("I will", "I can", "Will", "Can") + " " +
                                      RandomHelper.RandomString("trade", "swap", "sell") +
                                      "|" + RandomHelper.RandomString("gold", "iron", "dirt", "glass", "flowers", "cake") +
                                      "|for " + RandomHelper.RandomString("obsidian", "wood", "sand",
                                                                          "coal", "stone", "cookies") +
                                      "|- " + RandomHelper.RandomString("See", "Talk to") + " " +
                                      RandomHelper.RandomLetterUpper() + "." + RandomHelper.RandomLetterUpper() + ".";
                    break;
                case 3: strSignText = RandomHelper.RandomString("Church", "Order") + " of the" +
                                      "|Holy " + RandomHelper.RandomString("Pickaxe", "Notch", "Herobrine", "Creeper",
                                                                    "Chicken", "Squid", "Pigman", "Bucket", "Sword") +
                                      "|are meeting" +
                                      "|this " + RandomHelper.RandomDay();
                    break;
                case 4: strSignText = "Lost pet|creeper. Last|seen near the|mini crater";
                    break;
                case 5: strSignText = "Israphel||Wanted dead|or alive";
                    break;
                case 6: strSignText = "Lost|Jaffa Cakes.|Please return|to Honeydew";
                    break;
                case 7: strSignText = "|Read note " + rand.Next(500, 999) + "||";
                    break;
                case 8: strSignText = "|Squids spotted|in the sewers!|";
                    break;
            }
            return strSignText;
        }
    }
}
