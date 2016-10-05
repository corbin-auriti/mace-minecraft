/*
    Mace
    Copyright (C) 2011-2012 Robson
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
        private const int intAmountOfSignTypes = 15;

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
                intRand = RNG.Next(intAmountOfSignTypes);
            } while (intRand >= 5 && _booSignUsed[intRand]);
            _booSignUsed[intRand] = true;

            do
            {
                switch (intRand)
                {
                    case 0:
                    case 1: strSignText = String.Format("{0} {1} {2} for {3}~-{4} {5}.{6}.",
                                            RNG.RandomItem("I will", "I can", "Will", "Can", "Can you"),
                                            RNG.RandomItem("trade", "swap", "sell"),
                                            RNG.RandomItem("gold", "iron", "dirt", "tools", "glass", "flowers", "cake", "mushrooms"),
                                            RNG.RandomItem("obsidian", "wood", "sand", "bricks", "coal", "stone", "cookies", "diamonds"),
                                            RNG.RandomItem("See", "Talk to"),
                                            RNG.RandomLetter().ToString().ToUpper(),
                                            RNG.RandomLetter().ToString().ToUpper());
                        break;
                    case 2: strSignText = String.Format("{0} of the holy {1} are meeting this {2}",
                                            RNG.RandomItem("Church", "Order"),
                                            RNG.RandomFileLine(Path.Combine("Resources", "ChurchNoun.txt")),
                                            RNG.RandomDay());
                        break;
                    case 3: strSignText = String.Format("{0} {1} has lost her {2}. {3}",
                                            RNG.RandomItem("Mrs", "Miss"),
                                            RNG.RandomFileLine(Path.Combine("Resources", "Adjectives.txt")),
                                            RNG.RandomItem("cat", "dog", "glasses", "marbles", "knitting"),
                                            RNG.RandomItem("Reward offered", "Please help"));
                        break;
                    case 4: strSignText = String.Format("{0} for sale~-{1} {2}.{3}.",
                                            RNG.RandomItem("Armour", "Property", "House", "Weapons", "Gold", "Bodyguard", "Pet wolf", "Books", "Tools"),
                                            RNG.RandomItem("See", "Talk to"),
                                            RNG.RandomLetter().ToString().ToUpper(),
                                            RNG.RandomLetter().ToString().ToUpper());
                        break;
                    case 5: strSignText = "Lost pet creeper. Last seen near the mini crater";
                        break;
                    case 6: strSignText = "Israphel~~Wanted dead~or alive";
                        break;
                    case 7: strSignText = "Lost Jaffa Cakes. Please return to Honeydew";
                        break;
                    case 8: strSignText = "Read note " + RNG.Next(500, 999);
                        break;
                    case 9: strSignText = "Buy one get one free on gravestones!";
                        break;
                    case 10: strSignText = "Archery practice this " + RNG.RandomDay() + " afternoon";
                        break;
                    case 11: strSignText = "Seen a crime? Tell the nearest city guard";
                        break;
                    case 12: strSignText = "New city law: No minors can be miners";
                        break;
                    case 13: strSignText = "Council meeting this " + RNG.RandomDay();
                        break;
                    case 14: strSignText = "Numbers for lovers:~" + RNG.RandomItem("220 284", "1184 1210", "2620 2924", "5020 5564", "6232 6368");
                        break;
                    default:
                        Debug.Fail("Invalid switch result");
                        break;
                }
            } while (!strSignText.IsValidSign());
            return strSignText;
        }
    }
}
