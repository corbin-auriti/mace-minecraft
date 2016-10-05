using System;
using System.IO;
using System.Text;

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
            return RandomItemFromArray(strFileArray);
        }
        public static string RandomDay()
        {
            return RandomString("Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday");
        }
    }
}