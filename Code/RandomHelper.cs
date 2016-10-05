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
using System.IO;
using System.Linq;

namespace Mace
{
    static class RNG
    {
        private static Random _rand;

        public static void SetSeed(int intSeed)
        {
            _rand = new Random(intSeed);
        }
        public static void SetRandomSeed()
        {
            _rand = new Random();
        }
        public static double NextDouble()
        {
            return _rand.NextDouble();
        }
        public static int Next()
        {
            return _rand.Next();
        }
        public static int Next(int intMax)
        {
            return _rand.Next(intMax);
        }
        public static int Next(int intMin, int intMax)
        {
            return _rand.Next(intMin, intMax);
        }
        public static char RandomLetter()
        {
            return (char)((short)'a' + RNG.Next(26));
        }
        public static T RandomItem<T>(params T[] items)
        {
            return items[RNG.Next(items.Length)];
        }
        public static T RandomItemFromArray<T>(T[] items)
        {
            return items[RNG.Next(items.Length)];
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
            return RandomItem("Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday");
        }
        public static int RandomWeightedNumber(int[] intWeights)
        {
            int intMax = 0;
            int intChoice = intWeights.Sum();
            intChoice = RNG.Next(intChoice);
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
        public static T[] ShuffleArray<T>(T[] items)
        {
            return items.OrderBy(a => RNG.Next()).ToArray();
        }
    }
}