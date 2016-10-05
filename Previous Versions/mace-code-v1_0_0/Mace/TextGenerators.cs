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

namespace Mace
{
    class TextGenerators
    {
        static Random rand = new Random();
        public string CityName()
        {
            string[] strStartOfCityName = new string[] { "Never", "Frozen", "Dark", "Broken", "Raven", "North", "Forgotten", "Owl", "Sacred", "Iron", "Red", "Wolf", 
                                                         "Dragon", "Gold", "Black", "White", "Holy", "Scum", "Steel", "Shadow", "Soul", "Cold" };

            string[] strEndOfCityName = new string[] { "claw", "winter", "hold", "water", "forge", "angel", "henge", "vale", "pass", "willow", "shield",
                                                       "stone", "bone", "hammer", "bridge", "vine", "skull", "shire", "ford", "moon" };

            return "City of " + strStartOfCityName[rand.Next(strStartOfCityName.Length)] +
                                strEndOfCityName[rand.Next(strEndOfCityName.Length)];
        }
        public string RandomSign()
        {
            string strSignText = "*|*|*|*";
            switch(rand.Next(0, 4))
            {
                case 0: strSignText = "Lost pet|creeper. Last|seen near the|mini crater"; break;
                case 1: strSignText = "Israphel||Wanted dead|or alive"; break;
                case 2: strSignText = "Will trade one|diamond block|for " + RandomNumber() + "|sheep. Talk to " + RandomLetter() + RandomLetter(); break;
                case 3: strSignText = "|Read|note " + rand.Next(500, 999) + "|"; break;
            }
            return strSignText;
        }
        private string RandomNumber()
        {
            string[] strNumbers = new string[] { "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            return strNumbers[rand.Next(strNumbers.Length)];
        }
        private char RandomLetter()
        {
            return (char)((short)'A' + rand.Next(26));
        }
    }
}
