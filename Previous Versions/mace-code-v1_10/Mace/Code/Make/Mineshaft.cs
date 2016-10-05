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
    along with this program.  If not, see <http://www.gnu.org/licenses/>
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Text;
using Substrate;
using Substrate.TileEntities;

namespace Mace
{
    static class Mineshaft
    {
        private struct structPoint
        {
            public int x;
            public int z;
        }
        private struct structSection
        {
            public int x;
            public int z;
            public SourceWorld.Building bldMineshaftSection;
        }

        // todo low: delete unused9 and check everything else is working correctly
        enum MineshaftBlocks { NaturalTerrain, Air, Placeholder, EntranceSection, RailWithSupport,
                               Support, ChestAndOrTorch, Rail, CeilingSupport, Unused9, Structure };

        private const int MINECRAFT_ITEM_STACK_AMOUNT = 64;
        private const int SPLIT_CHANCE = 7;
        private const int MULTIPLIER = 5;
        static Dictionary<string, string> _dictResourceIDs =
          Utils.MakeDictionaryFromChildNodeAttributes(Path.Combine("Resources", "Mineshaft.xml"), "ids");
        static Dictionary<string, string> _dictResourceAmounts =
          Utils.MakeDictionaryFromChildNodeAttributes(Path.Combine("Resources", "Mineshaft.xml"), "amounts");
        static int _intBlockStartBuildings;

        public static void MakeMineshaft(BetaWorld world, BlockManager bm, Buildings.structPoint spMineshaftEntrance, frmMace frmLogForm)
        {
            _intBlockStartBuildings = City.EdgeLength + 13;
            int intMineshaftSize = (1 + City.MapLength) - (_intBlockStartBuildings * 2);
            if (intMineshaftSize % 5 > 0)
            {
                intMineshaftSize += 5 - (intMineshaftSize % 5);
            }
            _intBlockStartBuildings -= 2;
            for (int intLevel = 1; intLevel <= 7; intLevel++)
            {
                if (Utils.ValueFromXMLElement(Path.Combine("Resources", "Mineshaft.xml"), "active", "level" + intLevel.ToString()).IsAffirmative())
                {
                    MakeLevel(world, bm, intLevel, intMineshaftSize, spMineshaftEntrance, frmLogForm);
                }
            }
            int intBlockToReplace = bm.GetID(spMineshaftEntrance.x, 63, spMineshaftEntrance.z + 1);
            BlockShapes.MakeSolidBox(spMineshaftEntrance.x, spMineshaftEntrance.x,
                                     3, 63, spMineshaftEntrance.z + 1, spMineshaftEntrance.z + 1, BlockInfo.Wood.ID, 0);
            BlockHelper.MakeLadder(spMineshaftEntrance.x, 4, 63, spMineshaftEntrance.z, 0, BlockInfo.Wood.ID);
            BlockShapes.MakeSolidBox(spMineshaftEntrance.x, spMineshaftEntrance.x, 3, 63, spMineshaftEntrance.z + 1,
                                     spMineshaftEntrance.z + 1, BlockInfo.Stone.ID, 0);
            bm.SetID(spMineshaftEntrance.x, 63, spMineshaftEntrance.z + 1, intBlockToReplace);
        }
        // todo low: this is way too big
        private static void MakeLevel(BetaWorld world, BlockManager bm, int intDepth, int intMineshaftSize,
                                      Buildings.structPoint spMineshaftEntrance, frmMace frmLogForm)
        {
            frmLogForm.UpdateLog("Creating mineshaft level " + intDepth, true, true);

            string[] strResourceNames = Utils.ValueFromXMLElement(Path.Combine("Resources", "Mineshaft.xml"),
                                                          "level" + intDepth, "names").Split(',');
            int[] intResourceChances = Utils.ValueFromXMLElement(
                Path.Combine("Resources", "Mineshaft.xml"), "level" + intDepth, "chances").Split(',').StringArrayToIntArray();
            int intTorchChance = Convert.ToInt32(Utils.ValueFromXMLElement(Path.Combine("Resources", "Mineshaft.xml"),
                                                                                        "level" + intDepth, "torch_chance"));
            MineshaftBlocks[,] mbAreaFull = new MineshaftBlocks[intMineshaftSize + (MULTIPLIER * 2), intMineshaftSize + (MULTIPLIER * 2)];

            int intXPosOriginal = spMineshaftEntrance.x - _intBlockStartBuildings;
            int intZPosOriginal = spMineshaftEntrance.z - _intBlockStartBuildings;

            _intBlockStartBuildings -= 2;
            int[,] intAreaOverview = new int[(mbAreaFull.GetLength(0) / MULTIPLIER), (mbAreaFull.GetLength(1) / MULTIPLIER)];
            int intXPos = intXPosOriginal / MULTIPLIER;
            int intZPos = intZPosOriginal / MULTIPLIER;
            intAreaOverview[intXPos, intZPos] = (int)MineshaftBlocks.Air;
            CreateRouteXPlus(intAreaOverview, intXPos + 1, intZPos, 0);
            CreateRouteZPlus(intAreaOverview, intXPos, intZPos + 1, 1);
            CreateRouteXMinus(intAreaOverview, intXPos - 1, intZPos, 2);
            CreateRouteZMinus(intAreaOverview, intXPos, intZPos - 1, 3);
            int intOffsetX = (intXPosOriginal - (intXPos * MULTIPLIER)) - 2;
            int intOffsetZ = (intZPosOriginal - (intZPos * MULTIPLIER)) - 2;

            List<structSection> lstSections = new List<structSection>();

            intAreaOverview[intXPos, intZPos] = (int)MineshaftBlocks.Placeholder;
            intAreaOverview = AddMineshaftSections(intAreaOverview, intDepth);
            intAreaOverview[intXPos, intZPos] = (int)MineshaftBlocks.Air;

            for (int x = 0; x < intAreaOverview.GetLength(0); x++)
            {
                for (int z = 0; z < intAreaOverview.GetLength(1); z++)
                {
                    if (intAreaOverview[x, z] >= 100)
                    {
                        structSection structCurrentSection = new structSection();
                        structCurrentSection.bldMineshaftSection = SourceWorld.GetBuilding(intAreaOverview[x, z] - 100);
                        structCurrentSection.x = ((x * MULTIPLIER) + intOffsetX) - 1;
                        structCurrentSection.z = ((z * MULTIPLIER) + intOffsetZ) - 1;
                        for (int x2 = x; x2 <= x + (structCurrentSection.bldMineshaftSection.intSizeX - 1) / 7; x2++)
                        {
                            for (int z2 = z; z2 <= z + (structCurrentSection.bldMineshaftSection.intSizeZ - 1) / 7; z2++)
                            {
                                if (intAreaOverview[x2, z2] == structCurrentSection.bldMineshaftSection.intID + 100)
                                {
                                    intAreaOverview[x2, z2] = (int)MineshaftBlocks.Structure;
                                }
                            }
                        }
                        lstSections.Add(structCurrentSection);
                    }
                }
            }
            for (int x = 4; x < mbAreaFull.GetLength(0) - 4; x++)
            {
                for (int z = 4; z < mbAreaFull.GetLength(1) - 4; z++)
                {
                    if (intAreaOverview.GetLength(0) > x / MULTIPLIER && intAreaOverview.GetLength(1) > z / MULTIPLIER)
                    {
                        switch (mbAreaFull[x + intOffsetX, z + intOffsetZ])
                        {
                            case MineshaftBlocks.NaturalTerrain:
                            case MineshaftBlocks.Air:
                            case MineshaftBlocks.Placeholder:
                            case MineshaftBlocks.EntranceSection:
                                mbAreaFull[x + intOffsetX, z + intOffsetZ] = (MineshaftBlocks)intAreaOverview[x / MULTIPLIER, z / MULTIPLIER];
                                break;
                        }
                    }
                    if ((x + 3) % 5 == 0 && (z + 3) % 5 == 0 && intAreaOverview[x / MULTIPLIER, z / MULTIPLIER] == (int)MineshaftBlocks.Air)
                    {
                        if (intAreaOverview[(x / MULTIPLIER) + 1, z / MULTIPLIER] >= 100)
                        {
                            for (int x2 = 0; x2 < 5; x2++)
                            {
                                mbAreaFull[x + intOffsetX + 3, z + intOffsetZ + x2 - 2] = MineshaftBlocks.Structure;
                            }
                        }
                        if (intAreaOverview[(x / MULTIPLIER) + 1, z / MULTIPLIER] == (int)MineshaftBlocks.Air)
                        {
                            for (int x2 = 0; x2 < 5; x2++)
                            {
                                if (x2 == 1 || x2 == 3)
                                {
                                    mbAreaFull[x + intOffsetX + 3, z + intOffsetZ + x2 - 2] = MineshaftBlocks.CeilingSupport;
                                    mbAreaFull[x + intOffsetX + 2, z + intOffsetZ + x2 - 2] = MineshaftBlocks.CeilingSupport;
                                }
                                else
                                {
                                    mbAreaFull[x + intOffsetX + 3, z + intOffsetZ + x2 - 2] = MineshaftBlocks.Support;
                                    mbAreaFull[x + intOffsetX + 2, z + intOffsetZ + x2 - 2] = MineshaftBlocks.Support;
                                }
                            }
                            for (int x2 = 0; x2 <= 5; x2++)
                            {
                                if (mbAreaFull[x + intOffsetX + x2, z + intOffsetZ] == MineshaftBlocks.Support)
                                {
                                    mbAreaFull[x + intOffsetX + x2, z + intOffsetZ] = MineshaftBlocks.RailWithSupport;
                                }
                                else
                                {
                                    mbAreaFull[x + intOffsetX + x2, z + intOffsetZ] = MineshaftBlocks.Rail;
                                }
                            }
                        }
                        if (intAreaOverview[x / MULTIPLIER, (z / MULTIPLIER) + 1] == (int)MineshaftBlocks.Air)
                        {
                            for (int z2 = 0; z2 < 5; z2++)
                            {
                                if (z2 == 1 || z2 == 3)
                                {
                                    mbAreaFull[x + intOffsetX + z2 - 2, z + intOffsetZ + 3] = MineshaftBlocks.CeilingSupport;
                                    mbAreaFull[x + intOffsetX + z2 - 2, z + intOffsetZ + 2] = MineshaftBlocks.CeilingSupport;
                                }
                                else
                                {
                                    mbAreaFull[x + intOffsetX + z2 - 2, z + intOffsetZ + 3] = MineshaftBlocks.Support;
                                    mbAreaFull[x + intOffsetX + z2 - 2, z + intOffsetZ + 2] = MineshaftBlocks.Support;
                                }
                            }
                            for (int z2 = 0; z2 <= 5; z2++)
                            {
                                if (mbAreaFull[x + intOffsetX, z + intOffsetZ + z2] == MineshaftBlocks.Support)
                                {
                                    mbAreaFull[x + intOffsetX, z + intOffsetZ + z2] = MineshaftBlocks.RailWithSupport;
                                }
                                else
                                {
                                    mbAreaFull[x + intOffsetX, z + intOffsetZ + z2] = MineshaftBlocks.Rail;
                                }
                            }
                        }
                        if (intAreaOverview[x / MULTIPLIER, z / MULTIPLIER] == (int)MineshaftBlocks.Air)
                        {
                            MakeChestAndOrTorch(intAreaOverview, mbAreaFull, (x - 3) / MULTIPLIER, z / MULTIPLIER,
                                                x + intOffsetX - 2, z + intOffsetZ);
                            MakeChestAndOrTorch(intAreaOverview, mbAreaFull, (x + 3) / MULTIPLIER, z / MULTIPLIER,
                                                x + intOffsetX + 2, z + intOffsetZ);
                            MakeChestAndOrTorch(intAreaOverview, mbAreaFull, x / MULTIPLIER, (z - 3) / MULTIPLIER,
                                                x + intOffsetX, z + intOffsetZ - 2);
                            MakeChestAndOrTorch(intAreaOverview, mbAreaFull, x / MULTIPLIER, (z + 3) / MULTIPLIER,
                                                x + intOffsetX, z + intOffsetZ + 2);
                        }
                    }
                }
            }
            mbAreaFull[intXPosOriginal, intZPosOriginal] = MineshaftBlocks.EntranceSection;
            int intSupportMaterial = RNG.RandomItem(BlockInfo.Wood.ID, BlockInfo.WoodPlank.ID, BlockInfo.Fence.ID);
            for (int x = 0; x < mbAreaFull.GetLength(0); x++)
            {
                for (int z = 0; z < mbAreaFull.GetLength(1); z++)
                {
                    if (intDepth <= 4)
                    {
                        if (bm.GetID(x + _intBlockStartBuildings, 42 - (5 * intDepth), z + _intBlockStartBuildings) == BlockInfo.Gravel.ID)
                        {
                            bm.SetID(x + _intBlockStartBuildings, 42 - (5 * intDepth), z + _intBlockStartBuildings, BlockInfo.Stone.ID);
                        }
                    }
                    if (intDepth <= 2)
                    {
                        if (bm.GetID(x + _intBlockStartBuildings, 42 - (5 * intDepth), z + _intBlockStartBuildings) == BlockInfo.Sand.ID ||
                            bm.GetID(x + _intBlockStartBuildings, 42 - (5 * intDepth), z + _intBlockStartBuildings) == BlockInfo.Sandstone.ID)
                        {
                            bm.SetID(x + _intBlockStartBuildings, 42 - (5 * intDepth), z + _intBlockStartBuildings, BlockInfo.Dirt.ID);
                        }
                    }
                    switch(mbAreaFull[x, z])
                    {
                        case MineshaftBlocks.NaturalTerrain:
                            break;
                        case MineshaftBlocks.Air:
                            for (int y = 39 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings, BlockInfo.Air.ID);
                            }
                            break;
                        case MineshaftBlocks.EntranceSection:
                        case MineshaftBlocks.Rail:
                            for (int y = 38 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                if (y == 38 - (5 * intDepth))
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings,
                                             BlockInfo.Gravel.ID);
                                }
                                else if (y == 39 - (5 * intDepth))
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings,
                                             BlockInfo.Rails.ID);
                                }
                                else
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings,
                                             BlockInfo.Air.ID);
                                }
                            }
                            break;
                        case MineshaftBlocks.RailWithSupport:
                            for (int y = 38 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                if (y == 38 - (5 * intDepth))
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings,
                                             BlockInfo.Gravel.ID);
                                }
                                else if (y == 39 - (5 * intDepth))
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings,
                                             BlockInfo.Rails.ID);
                                }
                                else if (y == 40 - (5 * intDepth))
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings,
                                             BlockInfo.Air.ID);
                                }
                                else
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings,
                                             intSupportMaterial);
                                }
                            }
                            break;
                        case MineshaftBlocks.Support:
                            for (int y = 39 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings,
                                         intSupportMaterial);
                            }
                            break;
                        case MineshaftBlocks.ChestAndOrTorch:
                            for (int y = 39 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                if (y == 39 - (5 * intDepth) &&
                                    RNG.NextDouble() > 0.9)
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings, BlockInfo.Chest.ID);
                                    MakeChestItems(bm, x + _intBlockStartBuildings, y, z + _intBlockStartBuildings, intResourceChances, strResourceNames);
                                }
                                else if (y == 41 - (5 * intDepth) &&
                                         RNG.NextDouble() < (double)intTorchChance / 100)
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings,
                                             BlockInfo.Torch.ID);
                                    if (mbAreaFull[x - 1, z] == 0)
                                    {
                                        bm.SetData(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings, 1);
                                    }
                                    else if (mbAreaFull[x + 1, z] == 0)
                                    {
                                        bm.SetData(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings, 2);
                                    }
                                    else if (mbAreaFull[x, z - 1] == 0)
                                    {
                                        bm.SetData(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings, 3);
                                    }
                                    else
                                    {
                                        bm.SetData(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings, 4);
                                    }
                                }
                                else
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings, BlockInfo.Air.ID);
                                }
                            }
                            break;
                        case MineshaftBlocks.CeilingSupport:
                            for (int y = 39 - (5 * intDepth); y <= 41 - (5 * intDepth); y++)
                            {
                                if (y == 41 - (5 * intDepth))
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings,
                                             intSupportMaterial);
                                }
                                else
                                {
                                    bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings, BlockInfo.Air.ID);
                                }
                            }
                            break;
                        case MineshaftBlocks.Unused9:
                            for (int y = 39 - (5 * intDepth); y <= 41 - (5 * (intDepth - 1)); y++)
                            {
                                bm.SetID(x + _intBlockStartBuildings, y, z + _intBlockStartBuildings, BlockInfo.Air.ID);
                            }
                            break;
                        case MineshaftBlocks.Structure:
                            // this will be overwritten later
                            break;
                        default:
                            Debug.Fail("Invalid switch result");
                            break;
                    }
                }
            }
            foreach (structSection MineshaftSection in lstSections)
            {
                SourceWorld.InsertBuilding(bm, new int[0, 0], _intBlockStartBuildings, MineshaftSection.x, MineshaftSection.z,
                                           MineshaftSection.bldMineshaftSection, 38 - (5 * intDepth), -1);
            }
            world.Save();
            _intBlockStartBuildings += 2;
            //#if DEBUG
            //    File.WriteAllText("output_area_" + intDepth + ".txt", Utils.TwoDimensionalArrayToString(intAreaOverview));
            //    File.WriteAllText("output_map_" + intDepth + ".txt", Utils.TwoDimensionalArrayToString(intAreaFull));
            //#endif
        }
        private static int[,] AddMineshaftSections(int[,] intAreaOverview, int intDepth)
        {
            int intFail = 0;
            int intPlaced = 0;
            int intSections = 0;
            List<int> lstBuildings = new List<int>();
            foreach (int intSection in intAreaOverview)
            {
                intSections += intSection;
            }
            do
            {
                SourceWorld.Building bldMineshaftFeature = SourceWorld.SelectRandomBuilding(SourceWorld.BuildingTypes.MineshaftSection, intDepth);
                if (!bldMineshaftFeature.booUnique || !lstBuildings.Contains(bldMineshaftFeature.intID))
                {
                    structPoint spPlace = MatchPatternInArray(intAreaOverview, bldMineshaftFeature.strPattern.Split(' '));
                    if (spPlace.x == -1)
                    {
                        intFail++;
                    }
                    else
                    {
                        int intPosX = bldMineshaftFeature.intPosX;
                        int intPosZ = bldMineshaftFeature.intPosZ;
                        int intSizeX = (bldMineshaftFeature.intSizeX - 1) / 6;
                        int intSizeZ = (bldMineshaftFeature.intSizeZ - 1) / 6;
                        if (spPlace.x >= 0)
                        {
                            for (int x = 0; x < intSizeX; x++)
                            {
                                for (int z = 0; z < intSizeZ; z++)
                                {
                                    intAreaOverview[spPlace.x + intPosX + x, spPlace.z + intPosZ + z] = 100 + bldMineshaftFeature.intID;
                                }
                            }
                            intPlaced++;
                            if (bldMineshaftFeature.booUnique)
                            {
                                lstBuildings.Add(bldMineshaftFeature.intID);
                            }
                        }
                    }
                }
            } while (intFail < 10 && intPlaced < intSections / 15);
            return intAreaOverview;
        }
        private static structPoint MatchPatternInArray(int[,] intArray, string[] strPattern)
        {
            List<structPoint> lstPoints = new List<structPoint>();
            for (int x = 0; x <= intArray.GetUpperBound(0) - strPattern[0].Length; x++)
            {
                for (int z = 0; z <= intArray.GetUpperBound(1) - strPattern[0].Length; z++)
                {
                    bool booMatches = true;
                    for (int xCheck = 0; xCheck < strPattern[0].Length && booMatches; xCheck++)
                    {
                        for (int zCheck = 0; zCheck < strPattern[0].Length && booMatches; zCheck++)
                        {
                            booMatches = intArray[x + xCheck, z + zCheck] <= 1 &&
                                         (strPattern[zCheck][(strPattern[0].Length - 1) - xCheck].ToString() == "?" ||
                                          intArray[x + xCheck, z + zCheck].ToString() == "" + strPattern[zCheck][(strPattern[0].Length - 1) - xCheck]);
                        }
                    }
                    if (booMatches)
                    {
                        structPoint pntFoundPattern = new structPoint();
                        pntFoundPattern.x = x;
                        pntFoundPattern.z = z;
                        lstPoints.Add(pntFoundPattern);
                    }
                }
            }
            if (lstPoints.Count == 0)
            {
                structPoint pntNoPattern = new structPoint();
                pntNoPattern.x = -1;
                pntNoPattern.z = -1;
                return pntNoPattern;
            }
            else
            {
                return lstPoints[RNG.Next(lstPoints.Count)];
            }
        }
        private static void MakeChestAndOrTorch(int[,] intArea, MineshaftBlocks[,] mbMap, int intAreaX, int intAreaZ, int intMapX, int intMapZ)
        {
            if (intArea[intAreaX, intAreaZ] == 0)
            {
                mbMap[intMapX, intMapZ] = MineshaftBlocks.ChestAndOrTorch;
            }
        }
        private static void MakeChestItems(BlockManager bm, int x, int y, int z, int[] intResourceChances, string[] strResourceNames)
        {
            string strResource = strResourceNames[RNG.RandomWeightedNumber(intResourceChances)];
            string strAmount;
            _dictResourceAmounts.TryGetValue(strResource.ToLower(), out strAmount);
            strAmount = strAmount ?? "1,1";
            int intAmount = RNG.Next(Convert.ToInt32(strAmount.Split(',')[0]), Convert.ToInt32(strAmount.Split(',')[1]) + 1);
            string strBlockID;
            _dictResourceIDs.TryGetValue(strResource.ToLower(), out strBlockID);
            TileEntityChest tec = new TileEntityChest();
            int intSlot = 0;
            do
            {
                if (intAmount > MINECRAFT_ITEM_STACK_AMOUNT)
                {
                    tec.Items[intSlot++] = BlockHelper.MakeItem(Convert.ToInt32(strBlockID), MINECRAFT_ITEM_STACK_AMOUNT);
                }
                else
                {
                    tec.Items[intSlot++] = BlockHelper.MakeItem(Convert.ToInt32(strBlockID), intAmount);
                }
                intAmount -= MINECRAFT_ITEM_STACK_AMOUNT;
            } while (intAmount > 0);
            
            bm.SetTileEntity(x, y, z, tec);
        }

        private static void CreateRouteXMinus(int[,] intArea, int intXPos, int intZPos, int intInvalidDirection)
        {
            if (intXPos > 2)
            {
                int intLength = RNG.Next(intXPos / 2, intXPos + 1);
                for (int X = intXPos - 1; X > intXPos - intLength; X--)
                {
                    if (intArea.IsZeros(X - 1, intZPos - 1, X, intZPos + 1))
                    {
                        intArea[intXPos, intZPos] = 1;
                        intArea[X, intZPos] = 1;
                        if (RNG.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 3)
                        {
                            CreateRouteZPlus(intArea, X, intZPos, intInvalidDirection);
                        }
                        if (RNG.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 1)
                        {
                            CreateRouteZMinus(intArea, X, intZPos, intInvalidDirection);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        private static void CreateRouteXPlus(int[,] intArea, int intXPos, int intZPos, int intInvalidDirection)
        {
            if (intArea.GetLength(0) - intXPos > 2)
            {
                int intLength = RNG.Next((intArea.GetLength(0) - intXPos) / 2, intArea.GetLength(0) - intXPos);
                for (int X = intXPos + 1; X < intXPos + intLength; X++)
                {
                    if (intArea.IsZeros(X, intZPos - 1, X + 1, intZPos + 1))
                    {
                        intArea[intXPos, intZPos] = 1;
                        intArea[X, intZPos] = 1;
                        if (RNG.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 3)
                        {
                            CreateRouteZPlus(intArea, X, intZPos, intInvalidDirection);
                        }
                        if (RNG.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 1)
                        {
                            CreateRouteZMinus(intArea, X, intZPos, intInvalidDirection);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        private static void CreateRouteZMinus(int[,] intArea, int intXPos, int intZPos, int intInvalidDirection)
        {
            if (intZPos > 2)
            {
                int intLength = RNG.Next(intZPos / 2, intZPos + 1);
                for (int Z = intZPos - 1; Z > intZPos - intLength; Z--)
                {
                    if (intArea.IsZeros(intXPos - 1, Z - 1, intXPos + 1, Z))
                    {
                        intArea[intXPos, intZPos] = 1;
                        intArea[intXPos, Z] = 1;
                        if (RNG.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 2)
                        {
                            CreateRouteXPlus(intArea, intXPos, Z, intInvalidDirection);
                        }
                        if (RNG.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 0)
                        {
                            CreateRouteXMinus(intArea, intXPos, Z, intInvalidDirection);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        private static void CreateRouteZPlus(int[,] intArea, int intXPos, int intZPos, int intInvalidDirection)
        {
            if (intArea.GetLength(1) - intZPos > 2)
            {
                int intLength = RNG.Next((intArea.GetLength(1) - intZPos) / 2, intArea.GetLength(1) - intZPos);
                for (int Z = intZPos + 1; Z < intZPos + intLength; Z++)
                {
                    if (intArea.IsZeros(intXPos - 1, Z, intXPos + 1, Z + 1))
                    {
                        intArea[intXPos, intZPos] = 1;
                        intArea[intXPos, Z] = 1;
                        if (RNG.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 2)
                        {
                            CreateRouteXPlus(intArea, intXPos, Z, intInvalidDirection);
                        }
                        if (RNG.Next(SPLIT_CHANCE) == 0 && intInvalidDirection != 0)
                        {
                            CreateRouteXMinus(intArea, intXPos, Z, intInvalidDirection);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}