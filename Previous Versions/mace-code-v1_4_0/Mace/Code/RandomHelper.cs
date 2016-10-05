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
using System.IO;

namespace Mace
{
    class RandomHelper
    {
        static Random rand = new Random();

        public static string RandomString(params string[] strStrings)
        {
            return strStrings[rand.Next(strStrings.Length)];
        }
        public static int RandomNumber(params int[] intNumbers)
        {
            return intNumbers[rand.Next(intNumbers.Length)];
        }
        public static char RandomLetterUpper()
        {
            return (char)((short)'A' + rand.Next(26));
        }
        public static char RandomLetterLower()
        {
            return (char)((short)'a' + rand.Next(26));
        }
        public static string RandomItemFromArray(string[] strStrings)
        {
            return strStrings[rand.Next(strStrings.Length)];
        }
        public static int RandomItemFromArray(int[] intNumbers)
        {
            return intNumbers[rand.Next(intNumbers.Length)];
        }
        public static string RandomFileLine(string strFile)
        {
            string[] strFileArray;
            strFileArray = File.ReadAllLines(strFile);
            string strItem;
            do
            {
                strItem = RandomItemFromArray(strFileArray);
            } while (strItem.StartsWith("//"));
            return strItem;
        }
        public static string RandomDay()
        {
            return RandomString("Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday");
        }
        public static int RandomWeightedNumber(int[] intWeights)
        {
            int intMax = 0;
            int intChoice = 0;
            // array sum isn't in the v2 framework, so we do it like this instead
            for (int a = 0; a <= intWeights.GetUpperBound(0); a++)
            {
                intChoice += intWeights[a];
            }
            intChoice = rand.Next(intChoice);
            for (int intItem = 0; intItem <= intWeights.GetUpperBound(0); intItem++)
            {
                intMax += intWeights[intItem];
                if (intMax > intChoice)
                {
                    return intItem;
                }
            }
            return 0;
        }
    }
}