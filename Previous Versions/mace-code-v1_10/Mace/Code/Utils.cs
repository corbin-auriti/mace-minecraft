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
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using Substrate;

namespace Mace
{
    public static class Utils
    {       
        // array manipulation
        public static int[] StringArrayToIntArray(this string[] strOriginal)
        {
            int[] intNumbers = new int[strOriginal.Length];
            for (int a = 0; a < strOriginal.Length; a++)
            {
                int.TryParse(strOriginal[a], out intNumbers[a]);
            }
            return intNumbers;
        }
        public static int SumOfNeighbours2D(this int[,] intArray, int x, int z)
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
        public static int ZerosInArray2D(this int[,] intCheck)
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
        public static bool IsArraySectionAllZeros2D(this int[,] intArray, int x1, int y1, int x2, int y2)
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
        public static int[,] RotateArray(this int[,] intArray, int intRotate)
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
        public static int[, ,] EnlargeArray3D(this int[, ,] intArea, int intMultiplierX, int intMultiplierY, int intMultiplierZ)
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
        public static int[, ,] SmudgeArray3D(this int[, ,] intArea, double dblChance)
        {
            int[, ,] intNew = new int[intArea.GetLength(0), intArea.GetLength(1), intArea.GetLength(2)];
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int y = 0; y < intArea.GetLength(1); y++)
                {
                    for (int z = 0; z < intArea.GetLength(2); z++)
                    {
                        if (RNG.NextDouble() <= dblChance)
                        {
                            int intNewX, intNewY, intNewZ;
                            do
                            {
                                intNewX = RNG.Next(-1, 2);
                                intNewY = RNG.Next(-1, 2);
                                intNewZ = RNG.Next(-1, 2);
                            } while (!intArea.IsValidPositionInArray3D(x + intNewX, y + intNewY, z + intNewZ) ||
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
        public static bool IsValidPositionInArray3D(this int[, ,] intArray, int X, int Y, int Z)
        {
            return X >= 0 && Y >= 0 && Z >= 0 &&
                   X <= intArray.GetUpperBound(0) &&
                   Y <= intArray.GetUpperBound(1) &&
                   Z <= intArray.GetUpperBound(2);
        }
        public static bool IsZeros(this int[,] intArray, int x1, int z1, int x2, int z2)
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
        public static string ArrayToString2D(this int[,] intArray)
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

        // xml lovelies
        public static Dictionary<string, string> MakeDictionaryFromChildNodeAttributes(string strFilenameXML, string strRootNode)
        {
            var dictFriendlyNames = new Dictionary<string, string>();
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
        public static string RandomValueFromXMLElement(string strFilenameXML, string strParentNode, string strChildNode)
        {
            string strValues = ValueFromXMLElement(strFilenameXML, strParentNode, strChildNode);
            return RNG.RandomItem(strValues.Split(','));
        }
        public static string[] ArrayFromXMLElement(string strFilenameXML, string strParentNode, string strChildNode)
        {
            return ValueFromXMLElement(strFilenameXML, strParentNode, strChildNode).Split(',');
        }

        // generic functions related to mace, substrate or minecraft
        public static string ToMinecraftSaveDirectory(this string subPath)
        {
            // this code was contributed by Surrogard <surrogard@googlemail.com>
            // (apart from the mac code, which I did, so it's probably wrong)
            // thank you!
            string MinecraftSavesDirectory = String.Empty;
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32Windows:
                case PlatformID.Win32NT:
                    MinecraftSavesDirectory = Environment.GetEnvironmentVariable("APPDATA") + @"\.minecraft\saves\" + subPath;
                    break;
                case PlatformID.Unix:
                    MinecraftSavesDirectory = Environment.GetEnvironmentVariable("HOME") + "/.minecraft/saves/" + subPath;
                    break;
                case PlatformID.MacOSX:
                    MinecraftSavesDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) +
                           "/Library/Application support/minecraft/saves/" + subPath;
                    break;
                default:
                    MinecraftSavesDirectory = Environment.GetEnvironmentVariable("APPDATA") + @"\.minecraft\saves\" + subPath;
                    break;
            }
            return MinecraftSavesDirectory;
        }       
        public static string GenerateWorldName()
        {
            RNG.SetRandomSeed();
            string strFolder = String.Empty;
            string strWorldName = String.Empty;
            string strType, strStart, strEnd;
            do
            {
                strType = RNG.RandomFileLine(Path.Combine("Resources", "WorldTypes.txt"));
                strStart = RNG.RandomFileLine(Path.Combine("Resources", "Adjectives.txt"));
                strEnd = RNG.RandomFileLine(Path.Combine("Resources", "Nouns.txt"));
                strWorldName = strType + " of " + strStart + strEnd;
                strFolder = strWorldName.ToMinecraftSaveDirectory();
            } while (strStart.ToLower().Trim() == strEnd.ToLower().Trim() || Directory.Exists(strFolder) || (strStart + strEnd).Length > 14);
            return strWorldName;
        }
        public static void CropMaceWorld(frmMace frmLogForm)
        {
            // thank you to Surrogard <surrogard@googlemail.com> for providing a linux friendly version of this code:            
            Directory.CreateDirectory("macecopy".ToMinecraftSaveDirectory());
            BetaWorld bwCopy = BetaWorld.Create("macecopy".ToMinecraftSaveDirectory());
            BetaChunkManager cmCopy = bwCopy.GetChunkManager();
            BetaWorld bwCrop = BetaWorld.Open("macemaster".ToMinecraftSaveDirectory());
            BetaChunkManager cmCrop = bwCrop.GetChunkManager();

            foreach (ChunkRef chunk in cmCrop)
            {
                if (chunk.X >= -7 && chunk.X <= 11 && chunk.Z >= 0 && chunk.Z <= 11)
                {
                    Debug.WriteLine("Copying chunk " + chunk.X + "," + chunk.Z);
                    cmCopy.SetChunk(chunk.X, chunk.Z, chunk.GetChunkRef());
                }
                cmCopy.Save();
            }
            bwCopy.Level.GameType = GameType.CREATIVE;
            cmCopy.Save();
            bwCopy.Save();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Process.Start("explorer.exe", @"/select," + "macecopy".ToMinecraftSaveDirectory() + "\\level.dat");
            }
        }    
    
        // sign stuff
        /// <summary>
        /// Minecraft signs have four lines and each line allows for 14 characters maximum.
        /// this code takes a string and places ~ characters between each line.
        /// You can force newlines by putting ~ characters in the original sign text.
        /// </summary>
        /// <param name="SignText">example: Hello world! This is an example message!</param>
        /// <returns>example: Hello world!~This is an~example~message!</returns>
        public static string ConvertStringToSignText(this string SignText)
        {
            string[] SignTextByLine = new string[4] { String.Empty, String.Empty, String.Empty, String.Empty };
            SignText = SignText.Replace("~", "~ ");
            // remove accidental spaces
            SignText = SignText.Replace("  ", " ").Trim();
            string[] SignWords = SignText.Split(' ');
            int SignLine = 0;
            for (int a = 0; a < SignWords.GetLength(0); a++)
            {
                if (SignTextByLine[SignLine].Length + SignWords[a].Replace("~", String.Empty).Length + 1 > 14)
                {
                    if (++SignLine > SignTextByLine.GetUpperBound(0))
                    {
                        Debug.WriteLine("Sign text is too long: " + SignText);
                        break;
                    }
                }
                SignTextByLine[SignLine] += ' ' + SignWords[a];
                SignTextByLine[SignLine] = SignTextByLine[SignLine].Trim();
                // this is used to force a new line
                if (SignTextByLine[SignLine].EndsWith("~"))
                {
                    // get rid of the last letter
                    SignTextByLine[SignLine] = SignTextByLine[SignLine].Remove(SignTextByLine[SignLine].Length - 1, 1);
                    SignLine++;
                }
            }
            // move the text down if we've only used half the sign
            if (SignTextByLine[2].Length == 0)
            {
                SignTextByLine[2] = SignTextByLine[1];
                SignTextByLine[1] = SignTextByLine[0];
                SignTextByLine[0] = String.Empty;
            }
            return String.Join("~", SignTextByLine);
        }
        public static bool IsValidSign(this string strText)
        {
            // hack low: should properly handle errors before they happen
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

        // miscellaneous
        public static int ToJavaHashCode(this string strHash)
        {
            // this generates the same random hashes as java,
            //   which means we can replicate the minecraft random seed generator
            int intReturn = 0;
            foreach (char chrLetter in strHash)
            {
                intReturn = 31 * intReturn + chrLetter;
            }
            return intReturn;
        }
        public static bool IsAffirmative(this string strValue)
        {
            switch (strValue.Trim().ToLower())
            {
                case "yes":
                case "y":
                case "yeah":
                case "yea":
                case "1":
                case "-1":
                case "true":
                case "on":
                case "enable":
                case "enabled":
                case "oh god yes":
                case "oui":
                case "si":
                    return true;
                default:
                    return false;
            }
        }
        public static string ToSafeFilename(this string strFilename)
        {
            // windows doesn't like these characters
            foreach (char symbol in "\"\\/:?<>|*")
            {
                strFilename = strFilename.Replace(Char.ToString(symbol), String.Empty);
            }
            return strFilename;
        }
    }
}