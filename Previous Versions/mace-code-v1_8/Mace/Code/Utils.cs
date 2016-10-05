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
        public static int[, ,] EnlargeThreeDimensionalArray(int[, ,] intArea, int intMultiplierX, int intMultiplierY, int intMultiplierZ)
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

        // xml lovelies
        public static Dictionary<string, string> MakeDictionaryFromChildNodeAttributes(string strFilenameXML, string strRootNode)
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
            return RandomHelper.RandomString(strValues.Split(','));
        }

        // generic functions related to mace, substrate or minecraft
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
        public static string GenerateWorldName()
        {
            RandomHelper.SetRandomSeed();
            string strFolder = String.Empty;
            string strWorldName = String.Empty;
            string strType, strStart, strEnd;
            do
            {
                strType = RandomHelper.RandomFileLine(Path.Combine("Resources", "WorldTypes.txt"));
                strStart = RandomHelper.RandomFileLine(Path.Combine("Resources", "Adjectives.txt"));
                strEnd = RandomHelper.RandomFileLine(Path.Combine("Resources", "Nouns.txt"));
                strWorldName = strType + " of " + strStart + strEnd;
                strFolder = Utils.GetMinecraftSavesDirectory(strWorldName);
            } while (strStart.ToLower().Trim() == strEnd.ToLower().Trim() || Directory.Exists(strFolder) || (strStart + strEnd).Length > 14);
            return strWorldName;
        }
        public static void CropMaceWorld(frmMace frmLogForm)
        {
            // thank you to Surrogard <surrogard@googlemail.com> for providing a linux friendly version of this code:            
            Directory.CreateDirectory(Utils.GetMinecraftSavesDirectory("macecopy"));
            BetaWorld bwCopy = BetaWorld.Create(Utils.GetMinecraftSavesDirectory("macecopy"));
            BetaChunkManager cmCopy = bwCopy.GetChunkManager();
            BetaWorld bwCrop = BetaWorld.Open(Utils.GetMinecraftSavesDirectory("mace"));
            BetaChunkManager cmCrop = bwCrop.GetChunkManager();

            foreach (ChunkRef chunk in cmCrop)
            {
                if (chunk.X >= -7 && chunk.X <= 7 && chunk.Z >= 0 && chunk.Z <= 10)
                {
                    Debug.WriteLine("Copying chunk " + chunk.X + "," + chunk.Z);
                    cmCopy.SetChunk(chunk.X, chunk.Z, chunk.GetChunkRef());
                }
            }
            cmCopy.Save();
            bwCopy.Save();
        }    
    
        // sign stuff
        /// <summary>
        /// Minecraft signs have four lines and each line allows for 14 characters maximum.
        /// this code takes a string and places ~ characters between each line.
        /// You can force newlines by putting ~ characters in the original sign text.
        /// </summary>
        /// <param name="SignText">example: Hello world! This is an example message!</param>
        /// <returns>example: Hello world!~This is an~example~message!</returns>
        public static string ConvertStringToSignText(string SignText)
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
        public static string[] TextToSign(string strSignText)
        {
            // v2 - now with 50% less loops!
            string[] strSign = strSignText.Split('~');
            for (int a = 0; a <= strSign.GetUpperBound(0); a++)
            {
                strSign[a] = new String(' ', (14 - strSign[a].Length) / 2) +
                             strSign[a] +
                             new String(' ', (15 - strSign[a].Length) / 2);
            }
            return strSign;
        }
        public static bool IsValidSign(string strText)
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
        public static int JavaStringHashCode(string strHash)
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
        public static bool IsAffirmative(string strValue)
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
        public static string SafeFilename(string strFilename)
        {
            // windows doesn't like these characters
            string strUnsafe = @"*/\:?<>|" + '"';
            for (int a = 0; a < strUnsafe.Length; a++)
            {
                strFilename = strFilename.Replace(strUnsafe.Substring(a, 1), String.Empty);
            }
            return strFilename;
        }
    }
}