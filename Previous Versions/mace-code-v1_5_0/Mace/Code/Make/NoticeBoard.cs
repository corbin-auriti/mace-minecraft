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
        static bool[] booSignUsed = new bool[9];

        public static void MakeNoticeBoard(BlockManager bm, int intFarmSize, int intMapSize, int intCitySeed, int intWorldSeed)
        {
            booSignUsed = new bool[9];
            BlockHelper.MakeSign((intMapSize / 2) - 8, 68, intMapSize - (intFarmSize + 11),
                                 "|Public|Notice Board|", (int)BlockType.STONE);
            for (int x = (intMapSize / 2) - 9; x <= (intMapSize / 2) - 7; x++)
            {
                for (int y = 65; y <= 67; y++)
                {
                    string strSign;
                    if (x == (intMapSize / 2) - 8 && y == 66)
                    {
                        Version ver = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
                        strSign = String.Format("Created by|Mace v{0}.{1}.{2}|by Robson.|Have fun :)",
                                                       ver.Major, ver.Minor, ver.Build);
                    }
                    else if (x == (intMapSize / 2) - 8 && y == 65)
                    {
                        strSign = String.Format("City seed:|{0}|World seed:|{1}", intCitySeed, intWorldSeed);
                    }
                    else
                    {
                        strSign = RandomSign();
                    }
                    BlockHelper.MakeSign(x, y, intMapSize - (intFarmSize + 11), strSign, (int)BlockType.STONE);
                }
            }
        }
        private static string RandomSign()
        {            
            string strSignText = "*|*|*|*";

            int intRand = RandomHelper.Next(9);
            while (intRand >= 4 && booSignUsed[intRand])
            {
                intRand = RandomHelper.Next(9);
            };
            booSignUsed[intRand] = true;

            switch (intRand)
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
                                      "|Holy " + RandomHelper.RandomFileLine("Resources\\ChurchNoun.txt") +
                                      "|are meeting" +
                                      "|this " + RandomHelper.RandomDay();
                    break;
                case 4: strSignText = "Lost pet|creeper. Last|seen near the|mini crater";
                    break;
                case 5: strSignText = "Israphel||Wanted dead|or alive";
                    break;
                case 6: strSignText = "Lost|Jaffa Cakes.|Please return|to Honeydew";
                    break;
                case 7: strSignText = "|Read note " + RandomHelper.Next(500, 999) + "||";
                    break;
                case 8: strSignText = "Buy one|get one|free on|gravestones!";
                    break;
            }
            return strSignText;
        }
    }
}
