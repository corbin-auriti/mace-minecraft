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
    static class NoticeBoard
    {
        private const int intAmountOfSignTypes = 14;

        static bool[] _booSignUsed;

        public static void SetupClass()
        {
            _booSignUsed = new bool[intAmountOfSignTypes];
        }
        public static string GenerateNoticeboardSign(string strOverwrite)
        {
            switch (strOverwrite.ToLower().Substring(0, 5))
            {
                case "[nb1]":
                    Version ver = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
                    return String.Format("Created by~Mace v{0}.{1}.{2}~by Robson. ~Have fun :)",
                                                    ver.Major, ver.Minor, ver.Build);
                default:
                    return RandomSign();
            }
        }        
        private static string RandomSign()
        {            
            string strSignText = "*~*~*~*";

            int intRand;            
            do
            {
                intRand = RandomHelper.Next(intAmountOfSignTypes);
            } while (intRand >= 5 && _booSignUsed[intRand]);
            _booSignUsed[intRand] = true;

            do
            {
                switch (intRand)
                {
                    case 0:
                    case 1: strSignText = RandomHelper.RandomString("I will", "I can", "Will", "Can") + " " +
                                          RandomHelper.RandomString("trade", "swap", "sell") +
                                          " " + RandomHelper.RandomString("gold", "iron", "dirt",
                                                                          "glass", "flowers", "cake") +
                                          " for " + RandomHelper.RandomString("obsidian", "wood", "sand",
                                                                              "coal", "stone", "cookies") +
                                          "~- " + RandomHelper.RandomString("See", "Talk to") + " " +
                                          RandomHelper.RandomLetterUpper() + "." + RandomHelper.RandomLetterUpper() + ".";
                        break;
                    case 2: strSignText = RandomHelper.RandomString("Church", "Order") + " of the Holy " +
                                          RandomHelper.RandomFileLine(Path.Combine("Resources", "ChurchNoun.txt")) +
                                          " are meeting this " + RandomHelper.RandomDay();
                        break;
                    case 3: strSignText = RandomHelper.RandomString("Mrs", "Miss") + " " +
                                          RandomHelper.RandomFileLine(Path.Combine("Resources", "Adjectives.txt")) +
                                            " has lost her " +
                                          RandomHelper.RandomString("cat", "dog", "glasses", "marbles") +
                                          RandomHelper.RandomString(". Reward offered", ". Please help");
                        break;
                    case 4: strSignText = RandomHelper.RandomString("Armour", "Property", "House", "Weapons", "Gold",
                                                                    "Bodyguard", "Pet wolf", "Books", "Tools") +
                                          " for sale~- " + RandomHelper.RandomString("See", "Talk to") + " " +
                                          RandomHelper.RandomLetterUpper() + "." + RandomHelper.RandomLetterUpper() + ".";
                        break;
                    case 5: strSignText = "Lost pet creeper. Last seen near the mini crater";
                        break;
                    case 6: strSignText = "Israphel~~Wanted dead~or alive";
                        break;
                    case 7: strSignText = "Lost Jaffa Cakes. Please return to Honeydew";
                        break;
                    case 8: strSignText = "Read note " + RandomHelper.Next(500, 999);
                        break;
                    case 9: strSignText = "Buy one get one free on gravestones!";
                        break;
                    case 10: strSignText = "Archery practice this " + RandomHelper.RandomDay() + " afternoon";
                        break;
                    case 11: strSignText = "Seen a crime? Tell the nearest city guard";
                        break;
                    case 12: strSignText = "New city law: No minors can be miners";
                        break;
                    case 13: strSignText = "Council meeting this " + RandomHelper.RandomDay();
                        break;
                    default:
                        Debug.Fail("Invalid switch result");
                        break;
                }
            } while (!Utils.IsValidSign(strSignText));
            return strSignText;
        }
    }
}
