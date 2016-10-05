using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Collections.Generic;

namespace Mace
{
    public static class Utils
    {
        // generic functions
        public static int[,] RotateArray(int[,] intArray, int intRotate)
        {
            if (intRotate == 0)
            {
                return intArray;
            }
            else
            {
                int[,] intNewArray;
                if (intRotate == 2)
                {
                    intNewArray = new int[intArray.GetLength(0), intArray.GetLength(1)];
                }
                else
                {
                    intNewArray = new int[intArray.GetLength(1), intArray.GetLength(0)];
                }
                for (int x = 0; x < intArray.GetLength(0); x++)
                {
                    for (int y = 0; y < intArray.GetLength(1); y++)
                    {
                        switch (intRotate)
                        {
                            case 1:
                                intNewArray[y, x] = intArray[x, y];
                                break;
                            case 2:
                                intNewArray[x, y] = intArray[intArray.GetUpperBound(0) - x,
                                                             intArray.GetUpperBound(1) - y];
                                break;
                            case 3:
                                intNewArray[intNewArray.GetUpperBound(0) - y,
                                            intNewArray.GetUpperBound(1) - x] = intArray[x, y];
                                break;
                            default:
                                Debug.Fail("Invalid switch result");
                                break;
                        }
                    }
                }
                return intNewArray;
            }
        }
        public static int[] ShuffleArray(int[] intArray)
        {
            int intArrayLength = intArray.Length;
            while (intArrayLength > 1)
            {
                int intRandomItem = RandomHelper.Next(intArrayLength);
                intArrayLength--;
                int intTempValue = intArray[intArrayLength];
                intArray[intArrayLength] = intArray[intRandomItem];
                intArray[intRandomItem] = intTempValue;
            }
            return intArray;
        }
        public static int[, ,] EnlargeThreeDimensionalArray(int[, ,] intArea,
                                                     int intMultiplierX, int intMultiplierY, int intMultiplierZ)
        {
            int[, ,] intReturn = new int[intArea.GetLength(0) * intMultiplierX,
                                        intArea.GetLength(1) * intMultiplierY,
                                        intArea.GetLength(2) * intMultiplierZ];
            for (int x = 0; x < intReturn.GetLength(0); x++)
            {
                for (int y = 0; y < intReturn.GetLength(1); y++)
                {
                    for (int z = 0; z < intReturn.GetLength(2); z++)
                    {
                        intReturn[x, y, z] = intArea[x / intMultiplierX, y / intMultiplierY, z / intMultiplierZ];
                    }
                }
            }
            return intReturn;
        }
        public static int[, ,] SmudgeArray3D(int[, ,] intArea, double dblChance)
        {
            int[, ,] intNew = new int[intArea.GetLength(0), intArea.GetLength(1), intArea.GetLength(2)];
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int y = 0; y < intArea.GetLength(1); y++)
                {
                    for (int z = 0; z < intArea.GetLength(2); z++)
                    {
                        if (RandomHelper.NextDouble() <= dblChance)
                        {
                            int intNewX, intNewY, intNewZ;
                            do
                            {
                                intNewX = RandomHelper.Next(-1, 2);
                                intNewY = RandomHelper.Next(-1, 2);
                                intNewZ = RandomHelper.Next(-1, 2);
                            } while (!IsValidPositionInArray3D(intArea, x + intNewX, y + intNewY, z + intNewZ) ||
                                     Math.Abs(intNewX) + Math.Abs(intNewY) + Math.Abs(intNewZ) > 1);
                            intNew[x, y, z] = intArea[x + intNewX, y + intNewY, z + intNewZ];
                        }
                        else
                        {
                            intNew[x, y, z] = intArea[x, y, z];
                        }
                    }
                }
            }
            return intNew;
        }
        public static bool IsValidPositionInArray3D(int[, ,] intArray, int X, int Y, int Z)
        {
            return X >= 0 && Y >= 0 && Z >= 0 &&
                   X <= intArray.GetUpperBound(0) &&
                   Y <= intArray.GetUpperBound(1) &&
                   Z <= intArray.GetUpperBound(2);
        }
        public static Dictionary<string, string> MakeDictionaryFromChildNodeAttributes(string strFilenameXML,
                                                                                       string strRootNode)
        {
            Dictionary<string, string> dictFriendlyNames = new Dictionary<string, string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFilenameXML);
            foreach (XmlNode xmlRootNode in xmlDoc.GetElementsByTagName(strRootNode))
            {
                foreach (XmlNode xmlChildNode in xmlRootNode)
                {
                    dictFriendlyNames.Add(xmlChildNode.Name, xmlChildNode.Attributes[0].InnerText);
                }
            }
            return dictFriendlyNames;
        }
        public static bool IsZeros(int[,] intArray, int x1, int z1, int x2, int z2)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int z = z1; z <= z2; z++)
                {
                    if (intArray[x, z] > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static string TwoDimensionalArrayToString(int[,] intArray)
        {
            //string[] strOutputChars = { "0", "\\", "*", "!", "=", "W", "C", "S", "P", "G" };
            StringBuilder sbOutput = new StringBuilder();
            for (int x = 0; x < intArray.GetLength(0); x++)
            {
                for (int z = 0; z < intArray.GetLength(1); z++)
                {
                    sbOutput.Append(Convert.ToString(intArray[x, z], 16));
                }
                sbOutput.AppendLine();
            }
            return sbOutput.ToString().Replace("0", ".");
        }
        public static string ValueFromXMLElement(string strFilenameXML, string strParentNode, string strChildNode)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFilenameXML);
            foreach (XmlNode xmlRootNode in xmlDoc.GetElementsByTagName(strParentNode))
            {
                foreach (XmlNode xmlChildNode in xmlRootNode)
                {
                    if (xmlChildNode.Name == strChildNode)
                    {
                        return xmlChildNode.InnerText;
                    }
                }
            }
            return String.Empty;
        }
        public static int[] StringArrayToIntArray(string[] strOriginal)
        {
            int[] intNumbers = new int[strOriginal.Length];
            for (int a = 0; a < strOriginal.Length; a++)
            {
                int.TryParse(strOriginal[a], out intNumbers[a]);
            }
            return intNumbers;
        }
        public static int SumOfNeighbours2D(int[,] intArray, int x, int z)
        {
            int intNeighbours = 0;
            for (int a = x - 1; a <= x + 1; a++)
            {
                for (int b = z - 1; b <= z + 1; b++)
                {
                    if (a != x || b != z)
                    {
                        intNeighbours += intArray[a, b];
                    }
                }
            }
            return intNeighbours;
        }
        public static int ZerosInArray2D(int[,] intCheck)
        {
            int intWasted = 0;
            foreach (int intValue in intCheck)
            {
                if (intValue == 0)
                {
                    intWasted++;
                }
            }
            return intWasted;
        }
        public static bool IsArraySectionAllZeros2D(int[,] intArray, int x1, int y1, int x2, int y2)
        {
            bool booValid = true;
            for (int a = x1; a <= x2 && booValid; a++)
            {
                for (int b = y1; b <= y2 && booValid; b++)
                {
                    if (intArray[a, b] > 0)
                    {
                        booValid = false;
                    }
                }
            }
            return booValid;
        }
        
        // generic functions related to substrate or minecraft
        public static string GetMinecraftSavesDirectory(string subPath)
        {
            // this code was contributed by Surrogard <surrogard@googlemail.com>
            // (apart from the mac code, which I did, so it's probably wrong)
            // thank you!
            string MinecraftSavesDirectory = String.Empty;
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    MinecraftSavesDirectory = Environment.GetEnvironmentVariable("APPDATA") + @"\.minecraft\saves\" + subPath;
                    break;
                case PlatformID.Unix:
                    MinecraftSavesDirectory = Environment.GetEnvironmentVariable("HOME") + "/.minecraft/saves/" + subPath;
                    break;
                case PlatformID.MacOSX:
                    MinecraftSavesDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) +
                           "/Library/Application Support/.minecraft/saves/" + subPath;
                    break;
                default:
                    MinecraftSavesDirectory = Environment.GetEnvironmentVariable("APPDATA") + @"\.minecraft\saves\" + subPath;
                    break;
            }
            return MinecraftSavesDirectory;
        }
        public static string ConvertStringToSignText(string strText)
        {
            string[] strSignText = new string[4] { String.Empty, String.Empty, String.Empty, String.Empty };
            strText = strText.Replace("~", "~ ");
            strText = strText.Replace("  ", " ").Trim();
            string[] strWords = strText.Split(' ');
            int intLine = 0;
            for (int a = 0; a < strWords.GetLength(0); a++)
            {
                if (strSignText[intLine].Length + strWords[a].Replace("~", String.Empty).Length + 1 > 14)
                {
                    if (++intLine > 3)
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
            if (strSignText[2].Length == 0)
            {
                strSignText[2] = strSignText[1];
                strSignText[1] = strSignText[0];
                strSignText[0] = String.Empty;
            }
            return String.Join("~", strSignText);
        }
        public static bool IsValidSign(string strText)
        {
            // hack: should properly handle errors before they happen
            try
            {
                string[] strSignText = new string[4] { String.Empty, String.Empty, String.Empty, String.Empty };
                strText = strText.Replace("~", "~ ");
                strText = strText.Replace("  ", " ").Trim();
                string[] strWords = strText.Split(' ');
                int intLine = 0;
                for (int a = 0; a < strWords.GetLength(0); a++)
                {
                    if (strSignText[intLine].Length + strWords[a].Replace("~", String.Empty).Length + 1 > 14)
                    {
                        if (++intLine > 3)
                        {
                            return false;
                        }
                    }
                    strSignText[intLine] += ' ' + strWords[a];
                    strSignText[intLine] = strSignText[intLine].Trim();
                    if (strSignText[intLine].EndsWith("~"))
                    {
                        strSignText[intLine] = strSignText[intLine].Substring(0, strSignText[intLine].Length - 1);
                        intLine++;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string[] TextToSign(string strSignText)
        {
            string[] strSign = strSignText.Split('~');
            for (int a = 0; a <= 3; a++)
            {
                while (strSign[a].Length < 14)
                {
                    strSign[a] += ' ';
                    if (strSign[a].Length < 14)
                    {
                        strSign[a] = ' ' + strSign[a];
                    }
                }
            }
            return strSign;
        }
    }
}

