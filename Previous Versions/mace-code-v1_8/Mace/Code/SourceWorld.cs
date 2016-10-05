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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Substrate;
using Substrate.Entities;
using Substrate.TileEntities;

namespace Mace
{
    static class SourceWorld
    {
        private struct Spawner
        {
            public int sx, sy, sz;
            public int gx, gy, gz;
            public bool booGrass;
            public string strEntityID;
        }

        public enum BuildingTypes { City, Farming, MineshaftEntrance, MineshaftSection };

        public struct Building
        {
            public string strName;
            public int intSourceX;
            public int intSourceStartY;
            public int intSourceZ;
            public string strFrequency;
            public string[] strFrequencyMineshaft;
            public int intID;
            public bool booUnique;
            public BuildingTypes btThis;
            // mineshaft section values
            public int intSizeX;
            public int intSizeZ;
            public string strPattern;
            public int intPosX;
            public int intPosZ;
        }
        static Building[] _AllBuildings = new Building[0];
        static EntityPainting[] _AllEntityPainting = new EntityPainting[0];

        static int _intHouseNumber;
        static BlockManager _bmSource;
        static BetaChunkManager _cmSource;
        static BetaChunkManager _cmDest;
        static List<string> _lstCitySigns = new List<string>();
        static List<string> _lstInstanceSigns = new List<string>();

        public static bool SetupClass(BetaWorld worldDest)
        {
            _AllEntityPainting = new EntityPainting[0];
            _AllBuildings = new Building[0];
            _intHouseNumber = 0;
            BetaWorld worldSource = BetaWorld.Open(Path.Combine("Resources", "Mace"));
            _bmSource = worldSource.GetBlockManager();
            _cmSource = worldSource.GetChunkManager();
            _cmDest = worldDest.GetChunkManager();
            _lstCitySigns.Clear();
            _lstInstanceSigns.Clear();
            return ReadBuildings();
        }
        private static bool ReadBuildings()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path.Combine("Resources", "Themes", City.ThemeName + ".xml"));
            XmlNodeList buildingList = xmlDoc.GetElementsByTagName("building");
            Array.Resize(ref _AllBuildings, buildingList.Count);
            int intBuildings = 0;
            bool booFrequencyAboveRare = false;
            foreach (XmlNode xmlNode in buildingList)
            {
                XmlElement buildingElement = (XmlElement)xmlNode;
                _AllBuildings[intBuildings].strFrequencyMineshaft = new string[8];
                _AllBuildings[intBuildings].strName =
                  buildingElement.GetElementsByTagName("name")[0].InnerText;
                if (buildingElement.GetElementsByTagName("size")[0] != null)
                {
                    _AllBuildings[intBuildings].intSizeX =
                      Convert.ToInt32(buildingElement.GetElementsByTagName("size")[0].InnerText);
                    _AllBuildings[intBuildings].intSizeZ =
                      Convert.ToInt32(buildingElement.GetElementsByTagName("size")[0].InnerText);
                }
                else
                {
                    _AllBuildings[intBuildings].intSourceStartY = 63;
                }
                _AllBuildings[intBuildings].intSourceX =
                  Convert.ToInt32(buildingElement.GetElementsByTagName("source_x")[0].InnerText);
                _AllBuildings[intBuildings].intSourceZ =
                  Convert.ToInt32(buildingElement.GetElementsByTagName("source_z")[0].InnerText);
                if (buildingElement.GetElementsByTagName("source_start_y")[0] != null)
                {
                    _AllBuildings[intBuildings].intSourceStartY =
                      Convert.ToInt32(buildingElement.GetElementsByTagName("source_start_y")[0].InnerText);
                }
                else
                {
                    _AllBuildings[intBuildings].intSourceStartY = 63;
                }
                if (buildingElement.GetElementsByTagName("farm")[0] != null)
                {
                    if (Utils.IsAffirmative(buildingElement.GetElementsByTagName("farm")[0].InnerText))
                    {
                        _AllBuildings[intBuildings].btThis = BuildingTypes.Farming;
                    }
                }
                if (buildingElement.GetElementsByTagName("mineshaft_entrance")[0] != null)
                {
                    if (Utils.IsAffirmative(buildingElement.GetElementsByTagName("mineshaft_entrance")[0].InnerText))
                    {
                        _AllBuildings[intBuildings].btThis = BuildingTypes.MineshaftEntrance;
                    }
                }
                if (buildingElement.GetElementsByTagName("mineshaft_section")[0] != null)
                {
                    if (Utils.IsAffirmative(buildingElement.GetElementsByTagName("mineshaft_section")[0].InnerText))
                    {
                        _AllBuildings[intBuildings].btThis = BuildingTypes.MineshaftSection;
                        _AllBuildings[intBuildings].intSizeX =
                          Convert.ToInt32(buildingElement.GetElementsByTagName("size_x")[0].InnerText);
                        _AllBuildings[intBuildings].intSizeZ =
                          Convert.ToInt32(buildingElement.GetElementsByTagName("size_z")[0].InnerText);
                        _AllBuildings[intBuildings].intPosX =
                          Convert.ToInt32(buildingElement.GetElementsByTagName("pattern_position_x")[0].InnerText);
                        _AllBuildings[intBuildings].intPosZ =
                          Convert.ToInt32(buildingElement.GetElementsByTagName("pattern_position_z")[0].InnerText);
                        _AllBuildings[intBuildings].strPattern = buildingElement.GetElementsByTagName("pattern")[0].InnerText;
                        for (int intDepth = 1; intDepth <= 7; intDepth++)
                        {
                            _AllBuildings[intBuildings].strFrequencyMineshaft[intDepth] =
                                buildingElement.GetElementsByTagName("frequency" + intDepth)[0].InnerText;
                        }
                    }
                }
                if (buildingElement.GetElementsByTagName("frequency")[0] != null)
                {
                    _AllBuildings[intBuildings].strFrequency =
                      buildingElement.GetElementsByTagName("frequency")[0].InnerText;
                }
                else
                {
                    _AllBuildings[intBuildings].strFrequency = "exclude";
                }
                switch (_AllBuildings[intBuildings].strFrequency.ToLower())
                {
                    case "very common":
                    case "common":
                        booFrequencyAboveRare = true;
                        break;
                    // no need for a default - we only care about the values above
                }
                if (buildingElement.GetElementsByTagName("unique")[0] != null)
                {
                    _AllBuildings[intBuildings].booUnique = Utils.IsAffirmative(buildingElement.GetElementsByTagName("unique")[0].InnerText);
                }
                _AllBuildings[intBuildings].intID = intBuildings;
                intBuildings++;
            }
            ReadPaintings();
            if (!booFrequencyAboveRare)
            {
                MessageBox.Show("No buildings have a frequency of \"very common\" or \"common\". Stopping.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private static void ReadPaintings()
        {
            foreach (ChunkRef chunk in _cmSource)
            {
                foreach (EntityPainting entPainting in chunk.Entities.FindAll("Painting"))
                {
                    Array.Resize(ref _AllEntityPainting, _AllEntityPainting.Length + 1);
                    _AllEntityPainting[_AllEntityPainting.GetLength(0) - 1] = entPainting;
                }
            }
        }
        public static Building SelectRandomBuilding(BuildingTypes btNeeded, int intDepth)
        {
            int intFail = 0;
            string[] strFrequencyList = { "very common", "common", "average", "rare", "very rare" };
            int intFrequency = RandomHelper.RandomWeightedNumber(new int[] { 9, 8, 7, 6, 5 });
            int intBuilding = 0;
            bool booValid;
            do
            {
                if (++intFail >= 100)
                {
                    intFrequency = RandomHelper.RandomWeightedNumber(new int[] { 9, 8, 7, 6, 5 });
                    intFail = 0;
                }
                intBuilding = RandomHelper.Next(_AllBuildings.GetLength(0));
                booValid = _AllBuildings[intBuilding].btThis == btNeeded;
                if (booValid)
                {
                    if (btNeeded == BuildingTypes.MineshaftSection)
                    {
                        booValid = _AllBuildings[intBuilding].strFrequencyMineshaft[intDepth] == strFrequencyList[intFrequency];
                    }
                    else
                    {
                        booValid = _AllBuildings[intBuilding].strFrequency == strFrequencyList[intFrequency];
                    }
                }
            } while (!booValid);
            return _AllBuildings[intBuilding];
        }
        public static Building GetBuilding(int intID)
        {
            return _AllBuildings[intID];
        }
        public static Building GetBuilding(string strName)
        {
            for (int a = 0; a <= _AllBuildings.GetUpperBound(0); a++)
            {
                if (_AllBuildings[a].strName.ToLower() == strName.ToLower())
                {
                    return _AllBuildings[a];
                }
            }
            Debug.Assert(false);
            MessageBox.Show("Error: Could not find a building called " + strName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return _AllBuildings[0];
        }
        public static void InsertBuilding(BlockManager bmDest, int[,] intArea, int intBlockStart,
                                          int x1dest, int z1dest, Building bldInsert, int y1dest)
        {
            List<Spawner> lstSpawners = new List<Spawner>();
            _lstInstanceSigns.Clear();
            int intRandomWoolColour = RandomHelper.Next(16);
            int intSourceX = 0, intSourceZ = 0;
            int intRotate = -1;
            if (bldInsert.btThis == BuildingTypes.MineshaftSection)
            {
                intRotate = 0;
            }
            else
            {
                for (int intDistance = 0; intRotate == -1 && intDistance < 10; intDistance++)
                {
                    for (int intCheck = 0; intRotate == -1 && intCheck <= bldInsert.intSizeX; intCheck++)
                    {
                        if (CheckArea(intArea, x1dest + intCheck, z1dest - intDistance) == 1)
                        {
                            intRotate = 0;
                        }
                        else if (CheckArea(intArea, x1dest - intDistance, z1dest + intCheck) == 1)
                        {
                            intRotate = 1;
                        }
                        else if (CheckArea(intArea, x1dest + bldInsert.intSizeX + intDistance, z1dest + intCheck) == 1)
                        {
                            intRotate = 2;
                        }
                        else if (CheckArea(intArea, x1dest + intCheck, z1dest + bldInsert.intSizeZ + intDistance) == 1)
                        {
                            intRotate = 3;
                        }
                    }
                }
            }
            if (intRotate == -1)
            {
                intRotate = RandomHelper.Next(4);
            }

            for (int x = 0; x < bldInsert.intSizeX; x++)
            {
                for (int z = 0; z < bldInsert.intSizeZ; z++)
                {
                    switch (intRotate)
                    {
                        case 0:
                            intSourceX = x;
                            intSourceZ = z;
                            break;
                        case 1:
                            intSourceX = (bldInsert.intSizeX - 1) - z;
                            intSourceZ = x;
                            break;
                        case 2:
                            intSourceX = z;
                            intSourceZ = (bldInsert.intSizeZ - 1) - x;
                            break;
                        case 3:
                            intSourceX = (bldInsert.intSizeX - 1) - x;
                            intSourceZ = (bldInsert.intSizeZ - 1) - z;
                            break;
                        default:
                            Debug.Fail("Invalid switch result");
                            break;
                    }

                    int intSourceEndY;
                    if (bldInsert.btThis == BuildingTypes.MineshaftSection)
                    {
                        intSourceEndY = bldInsert.intSourceStartY + 4;
                    }
                    else
                    {
                        for (intSourceEndY = 128; intSourceEndY > 64; intSourceEndY--)
                        {
                            if (_bmSource.GetID(intSourceX + bldInsert.intSourceX, intSourceEndY,
                                                intSourceZ + bldInsert.intSourceZ) != BlockInfo.Air.ID)
                            {
                                break;
                            }
                        }
                    }
                    for (int ySource = bldInsert.intSourceStartY; ySource <= intSourceEndY; ySource++)
                    {
                        int yDest = ySource;
                        if (bldInsert.btThis == BuildingTypes.MineshaftSection)
                        {
                            yDest = y1dest + (ySource - bldInsert.intSourceStartY);
                        }
                        
                        if (bldInsert.btThis == BuildingTypes.MineshaftSection ||
                            ((yDest != 64 || bmDest.GetID(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest) == BlockInfo.Air.ID) &&
                            bmDest.GetID(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest) != BlockInfo.WoodPlank.ID))
                        {
                            int intBlockID = _bmSource.GetID(intSourceX + bldInsert.intSourceX, ySource,
                                                             intSourceZ + bldInsert.intSourceZ);
                            int intBlockData = _bmSource.GetData(intSourceX + bldInsert.intSourceX, ySource,
                                                                 intSourceZ + bldInsert.intSourceZ);
                            bmDest.SetID(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, intBlockID);
                            bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, intBlockData);

                            #region import paintings
                            // todo low: test for mineshaft features
                            foreach (EntityPainting entPainting in _AllEntityPainting)
                            {
                                if (entPainting.TileX == intSourceX + bldInsert.intSourceX &&
                                    entPainting.TileY == ySource &&
                                    entPainting.TileZ == intSourceZ + bldInsert.intSourceZ)
                                {
                                    EntityPainting entNewPainting = (EntityPainting)entPainting.Copy();
                                    entNewPainting.TileX = intBlockStart + x + x1dest;
                                    entNewPainting.TileY = yDest;
                                    entNewPainting.TileZ = intBlockStart + z + z1dest;
                                    entNewPainting.Position.X = entNewPainting.TileX;
                                    entNewPainting.Position.Z = entNewPainting.TileZ;
                                    entNewPainting.Direction = BlockHelper.RotatePortrait(entPainting.Direction,
                                                                                          intRotate);
                                    ChunkRef chunkBuilding = _cmDest.GetChunkRef((intBlockStart + x + x1dest) / 16,
                                                                                 (intBlockStart + z + z1dest) / 16);
                                    chunkBuilding.Entities.Add(entNewPainting);
                                    _cmDest.Save();
                                }
                            }
                            #endregion
                            #region Rotation
                            if (intRotate > 0)
                            {
#pragma warning disable
                                switch (intBlockID)
                                {
                                    case BlockType.PORTAL:
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                       BlockHelper.RotatePortal(intBlockData, intRotate));
                                        break;
                                    case BlockType.SIGN_POST:
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateSignPost(intBlockData, intRotate));
                                        break;
                                    case BlockType.STONE_BUTTON:
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateButton(intBlockData, intRotate));
                                        break;
                                    case BlockType.PUMPKIN:
                                    case BlockType.JACK_O_LANTERN:
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                       BlockHelper.RotatePumpkin(intBlockData, intRotate));
                                        break;
                                    case BlockType.IRON_DOOR:
                                    case BlockType.WOOD_DOOR:
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateDoor(intBlockData, intRotate));
                                        break;
                                    case BlockType.TRAPDOOR:
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateTrapdoor(intBlockData, intRotate));
                                        break;
                                    case BlockType.BED:
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateBed(intBlockData, intRotate));
                                        break;
                                    case BlockType.REDSTONE_TORCH_OFF:
                                    case BlockType.REDSTONE_TORCH_ON:
                                    case BlockType.TORCH:
                                    case BlockType.LEVER:
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                       BlockHelper.RotateTorchOrLever(intBlockData, intRotate));
                                        break;
                                    case BlockType.WALL_SIGN:
                                    case BlockType.LADDER:
                                    case BlockType.DISPENSER:
                                    case BlockType.FURNACE:
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                        BlockHelper.RotateWallSignOrLadderOrFurnanceOrDispenser(
                                                          intBlockData, intRotate));
                                        break;
                                    case BlockType.COBBLESTONE_STAIRS:
                                    case BlockType.WOOD_STAIRS:
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                        BlockHelper.RotateStairs(intBlockData, intRotate));
                                        break;
                                    // no need for a default - all other blocks are okay
                                }
                            }
#pragma warning restore

                            #endregion
                            #region Handle entities
#pragma warning disable
                            switch (intBlockID)
                            {
                                case BlockType.LAPIS_ORE:
                                    bmDest.SetID(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, BlockInfo.Wool.ID);
                                    bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                   intRandomWoolColour);
                                    break;
                                case BlockType.GLOWING_REDSTONE_ORE:
                                case BlockType.REDSTONE_ORE:
                                    if (yDest == 63)
                                    {
                                        bmDest.SetID(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, City.GroundBlockID);
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, City.GroundBlockData);
                                    }
                                    break;
                                case BlockType.GOLD_ORE:
                                    if (yDest == 63)
                                    {
                                        bmDest.SetID(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, City.PathBlockID);
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, City.PathBlockData);
                                    }
                                    break;
                                case BlockType.CHEST:
                                    TileEntityChest tec = (TileEntityChest)_bmSource.GetTileEntity(
                                        intSourceX + bldInsert.intSourceX, ySource, intSourceZ + bldInsert.intSourceZ);
                                    if (City.HasItemsInChests)
                                    {
                                        if (tec.Items[0] != null)
                                        {
                                            if (tec.Items[0].ID == ItemInfo.Paper.ID &&
                                                tec.Items[0].Count == 3)
                                            {
                                                tec = MakeHouseChest();
                                            }
                                            if (tec.Items[0].ID == ItemInfo.Paper.ID &&
                                                tec.Items[0].Count == 4)
                                            {
                                                tec = MakeTreasureChest();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        tec.Items.ClearAllItems();
                                    }
                                    bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, 0);
                                    bmDest.SetTileEntity(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, tec);
                                    break;
                                case BlockType.FURNACE:
                                case BlockType.MONSTER_SPAWNER:
                                case BlockType.NOTE_BLOCK:
                                    bmDest.SetTileEntity(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                         _bmSource.GetTileEntity(intSourceX + bldInsert.intSourceX, ySource,
                                                                                 intSourceZ + bldInsert.intSourceZ));
                                    break;
                                case BlockType.SIGN_POST:
                                case BlockType.WALL_SIGN:
                                    #region Determine sign text
                                    int intUniqueType = 0;
                                    TileEntitySign tes = (TileEntitySign)_bmSource.
                                        GetTileEntity(intSourceX + bldInsert.intSourceX, ySource,
                                                      intSourceZ + bldInsert.intSourceZ);
                                    string strSourceSign = tes.Text1 + " " + tes.Text2 + " " + tes.Text3 + " " + tes.Text4;
                                    switch (strSourceSign.Substring(0, 1))
                                    {
                                        case "2":
                                        case "1":
                                        case "0":
                                            intUniqueType = Convert.ToInt32(strSourceSign.Substring(0, 1));
                                            strSourceSign = strSourceSign.Remove(0, 1);
                                            break;
                                        default:
                                            intUniqueType = 0;
                                            break;
                                    }
                                    if (tes.Text1.ToLower() == "commentsign" || tes.Text1.ToLower() == "comment sign")
                                    {
                                        // these exist to give advice to people in the resource world.
                                        // so we just remove them from the real world
                                        bmDest.SetID(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest,
                                                     BlockInfo.Air.ID);
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, 0);
                                    }
                                    if (tes.Text1.ToLower() == "spawner" || tes.Text1.ToLower() == "gspawner")
                                    {
                                        if (!tes.Text2.ToLower().StartsWith("g") || tes.Text2.ToLower() == "[randomenemy]" || City.HasGhostdancerSpawners)
                                        {
                                            Spawner spawnerCurrent = new Spawner();
                                            if (tes.Text2.ToLower() == "[randomenemy]")
                                            {
                                                if (City.HasGhostdancerSpawners)
                                                {
                                                    spawnerCurrent.strEntityID = RandomHelper.RandomString("Creeper", "Skeleton", "Spider", "Zombie", "GGeist", "GGrim");                                                    
                                                }
                                                else
                                                {
                                                    spawnerCurrent.strEntityID = RandomHelper.RandomString("Creeper", "Skeleton", "Spider", "Zombie");
                                                }
                                            }
                                            else
                                            {
                                                spawnerCurrent.strEntityID = tes.Text2;
                                            }

                                            if (tes.Text3.Split(' ').GetUpperBound(0) == 2)
                                            {
                                                spawnerCurrent.sy = yDest + Convert.ToInt32(tes.Text3.Split(' ')[1]);
                                                spawnerCurrent.sx = 0;
                                                spawnerCurrent.sz = 0;
                                                switch (intRotate)
                                                {
                                                    case 0:
                                                        spawnerCurrent.sx = intBlockStart + x + x1dest +
                                                                            (Convert.ToInt32(tes.Text3.Split(' ')[0]));
                                                        spawnerCurrent.sz = intBlockStart + z + z1dest +
                                                                            (Convert.ToInt32(tes.Text3.Split(' ')[2]));
                                                        break;
                                                    case 1:
                                                        spawnerCurrent.sx = intBlockStart + x + x1dest +
                                                                            (Convert.ToInt32(tes.Text3.Split(' ')[2]));
                                                        spawnerCurrent.sz = intBlockStart + z + z1dest +
                                                                            (-1 * Convert.ToInt32(tes.Text3.Split(' ')[0]));
                                                        break;
                                                    case 2:
                                                        spawnerCurrent.sx = intBlockStart + x + x1dest +
                                                                            (-1 * Convert.ToInt32(tes.Text3.Split(' ')[2]));
                                                        spawnerCurrent.sz = intBlockStart + z + z1dest +
                                                                            (Convert.ToInt32(tes.Text3.Split(' ')[0]));
                                                        break;
                                                    case 3:
                                                        spawnerCurrent.sx = intBlockStart + x + x1dest +
                                                                            (-1 * Convert.ToInt32(tes.Text3.Split(' ')[0]));
                                                        spawnerCurrent.sz = intBlockStart + z + z1dest +
                                                                            (-1 * Convert.ToInt32(tes.Text3.Split(' ')[2]));
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                spawnerCurrent.sx = intBlockStart + x + x1dest;
                                                spawnerCurrent.sy = yDest - 2;
                                                spawnerCurrent.sz = intBlockStart + z + z1dest;
                                            }
                                            if (tes.Text4.ToLower() == "no")
                                            {
                                                spawnerCurrent.booGrass = false;
                                            }
                                            else if (tes.Text4.Split(' ').GetUpperBound(0) == 2)
                                            {
                                                spawnerCurrent.gy = yDest + Convert.ToInt32(tes.Text4.Split(' ')[1]);
                                                switch (intRotate)
                                                {
                                                    case 0:
                                                        spawnerCurrent.gx = intBlockStart + x + x1dest +
                                                                            (Convert.ToInt32(tes.Text4.Split(' ')[0]));
                                                        spawnerCurrent.gz = intBlockStart + z + z1dest +
                                                                            (Convert.ToInt32(tes.Text4.Split(' ')[2]));
                                                        break;
                                                    case 1:
                                                        spawnerCurrent.gx = intBlockStart + x + x1dest +
                                                                            (-1 * Convert.ToInt32(tes.Text4.Split(' ')[2]));
                                                        spawnerCurrent.gz = intBlockStart + z + z1dest +
                                                                            (Convert.ToInt32(tes.Text4.Split(' ')[0]));
                                                        break;
                                                    case 2:
                                                        spawnerCurrent.gx = intBlockStart + x + x1dest +
                                                                            (Convert.ToInt32(tes.Text4.Split(' ')[2]));
                                                        spawnerCurrent.gz = intBlockStart + z + z1dest +
                                                                            (-1 * Convert.ToInt32(tes.Text4.Split(' ')[0]));
                                                        break;
                                                    case 3:
                                                        spawnerCurrent.gx = intBlockStart + x + x1dest +
                                                                            (-1 * Convert.ToInt32(tes.Text4.Split(' ')[0]));
                                                        spawnerCurrent.gz = intBlockStart + z + z1dest +
                                                                            (-1 * Convert.ToInt32(tes.Text4.Split(' ')[2]));
                                                        break;
                                                }
                                                spawnerCurrent.booGrass = true;
                                            }
                                            else
                                            {
                                                spawnerCurrent.gx = intBlockStart + x + x1dest;
                                                spawnerCurrent.gy = yDest - 1;
                                                spawnerCurrent.gz = intBlockStart + z + z1dest;
                                                spawnerCurrent.booGrass = true;
                                            }
                                            lstSpawners.Add(spawnerCurrent);
                                        }
                                        bmDest.SetID(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, BlockInfo.Air.ID);
                                        bmDest.SetData(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, 0);
                                    }
                                    else if (strSourceSign.Contains("["))
                                    {
                                        string strSignText;
                                        bool booDuplicate;
                                        int intFail = 0;
                                        do
                                        {
                                            strSignText = SignText(strSourceSign);
                                            booDuplicate = false;
                                            switch (intUniqueType)
                                            {
                                                case 1: // unique by instance
                                                    booDuplicate = _lstInstanceSigns.Contains(strSignText);
                                                    break;
                                                case 2: // unique by city
                                                    booDuplicate = _lstCitySigns.Contains(strSignText);
                                                    break;
                                            }
                                            if (++intFail > 100)
                                            {
                                                Debug.WriteLine("Could not make a unique sign for " + strSourceSign);
                                                booDuplicate = false;
                                            }
                                        } while (booDuplicate);
                                        _lstCitySigns.Add(strSignText);
                                        _lstInstanceSigns.Add(strSignText);
                                        string[] strRandomSign = Utils.TextToSign(Utils.ConvertStringToSignText(strSignText));
                                        tes.Text1 = strRandomSign[0];
                                        tes.Text2 = strRandomSign[1];
                                        tes.Text3 = strRandomSign[2];
                                        tes.Text4 = strRandomSign[3];
                                        bmDest.SetTileEntity(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, tes);
                                    }
                                    else
                                    {
                                        bmDest.SetTileEntity(intBlockStart + x + x1dest, yDest, intBlockStart + z + z1dest, tes);
                                    }
                                    break;
                                    #endregion
                                // no need for a default - all the other blocks are okay
                            }
#pragma warning restore
                            #endregion
                        }
                    }
                }
            }
            foreach (Spawner spnAdd in lstSpawners)
            {
                bmDest.SetID(spnAdd.sx, spnAdd.sy, spnAdd.sz, BlockInfo.MonsterSpawner.ID);
                TileEntityMobSpawner tems = new TileEntityMobSpawner();
                tems.EntityID = spnAdd.strEntityID;
                bmDest.SetTileEntity(spnAdd.sx, spnAdd.sy, spnAdd.sz, tems);
                if (spnAdd.booGrass)
                {
                    BlockShapes.MakeBlock(spnAdd.gx, spnAdd.gy, spnAdd.gz, City.GroundBlockID, City.GroundBlockData);
                }
            }
        }
        private static int CheckArea(int[,] intArea, int X, int Z)
        {
            if (X >= 0 && Z >= 0 && X < intArea.GetLength(0) && Z < intArea.GetLength(1))
            {
                return intArea[X, Z];
            }
            else
            {
                return 0;
            }
        }
        private static string SignText(string strOverwrite)
        {
            if (strOverwrite.ToLower().StartsWith("[nb]") ||
                strOverwrite.ToLower().StartsWith("[nb1]") ||
                strOverwrite.ToLower().StartsWith("[nb2]"))
            {
                return NoticeBoard.GenerateNoticeboardSign(strOverwrite);
            }
            if (strOverwrite.ToLower().Contains("[house]"))
            {
                _intHouseNumber++;
                do
                {
                    string strName = RandomHelper.RandomFileLine(Path.Combine("Resources", "Nouns.txt")).Trim();
                    strName = strName.Substring(0, 1).ToUpper() + strName.Substring(1, strName.Length - 1);
                    strOverwrite = RandomHelper.RandomFileLine(Path.Combine("Resources", "HouseTypes.txt")) + " of~" +
                                   RandomHelper.RandomFileLine(Path.Combine("Resources", "Titles.txt")) +
                                   " " + strName + "~- " + _intHouseNumber + " -";
                } while (!Utils.IsValidSign(strOverwrite));
            }
            else
            {
                Regex reSquareBrackets = new Regex(@"(\[).*?(\])");
                MatchCollection mcSquareBrackets = reSquareBrackets.Matches(strOverwrite);
                for (int intIndex = mcSquareBrackets.Count - 1; intIndex >= 0; intIndex--)
                {
                    string strCurrentWord = strOverwrite.Substring(mcSquareBrackets[intIndex].Index + 1,
                                                                   mcSquareBrackets[intIndex].Length - 2);
                    if (File.Exists(Path.Combine("Resources", strCurrentWord + ".txt")))
                    {
                        strCurrentWord = RandomHelper.RandomFileLine(Path.Combine("Resources", strCurrentWord + ".txt"));
                    }
                    else
                    {
                        strCurrentWord = "?" + strCurrentWord + "?";
                        Debug.Fail("Could not find a file for " + strCurrentWord);
                    }
                    strOverwrite = strOverwrite.Remove(mcSquareBrackets[intIndex].Index, mcSquareBrackets[intIndex].Length);
                    strOverwrite = strOverwrite.Insert(mcSquareBrackets[intIndex].Index, strCurrentWord);
                }
            }
            return strOverwrite;
        }
        private static TileEntityChest MakeHouseChest()
        {
            TileEntityChest tec = new TileEntityChest();
            for (int intItems = RandomHelper.Next(4, 8); intItems >= 0; intItems--)
            {
                tec.Items[intItems] = BlockHelper.MakeItem(RandomHelper.RandomNumber(
                    ItemInfo.Apple.ID,
                    ItemInfo.Book.ID,
                    ItemInfo.Bowl.ID,
                    ItemInfo.Bread.ID,
                    ItemInfo.Cake.ID,
                    ItemInfo.Clock.ID,
                    ItemInfo.Compass.ID,
                    ItemInfo.Cookie.ID,
                    ItemInfo.Diamond.ID,
                    ItemInfo.Egg.ID,
                    ItemInfo.Feather.ID,
                    ItemInfo.FishingRod.ID,
                    ItemInfo.GoldMusicDisc.ID,
                    ItemInfo.GreenMusicDisc.ID,
                    ItemInfo.Paper.ID,
                    ItemInfo.Saddle.ID,
                    ItemInfo.String.ID,
                    ItemInfo.GoldIngot.ID,
                    ItemInfo.IronIngot.ID,
                    BlockInfo.Torch.ID,
                    ItemInfo.FlintAndSteel.ID,
                    ItemInfo.Bow.ID,
                    ItemInfo.IronSword.ID,
                    ItemInfo.Shears.ID), 1);
            }
            return tec;
        }
        private static TileEntityChest MakeTreasureChest()
        {
            TileEntityChest tec = new TileEntityChest();
            for (int intItems = RandomHelper.Next(5, 7); intItems >= 2; intItems--)
            {
                tec.Items[intItems] = BlockHelper.MakeItem(RandomHelper.RandomNumber(
                    ItemInfo.GoldenApple.ID,
                    ItemInfo.GoldIngot.ID,
                    ItemInfo.Diamond.ID,
                    ItemInfo.GoldMusicDisc.ID,
                    ItemInfo.GreenMusicDisc.ID,
                    ItemInfo.IronIngot.ID,
                    ItemInfo.Saddle.ID,
                    ItemInfo.Compass.ID,
                    ItemInfo.Clock.ID,
                    ItemInfo.Cake.ID,
                    ItemInfo.Cookie.ID
                    ), 1);
            }
            tec.Items[0] = BlockHelper.MakeItem(ItemInfo.GetRandomItem().ID, 1);
            tec.Items[1] = BlockHelper.MakeItem(ItemInfo.GetRandomItem().ID, 1);
            return tec;
        }
    }
}
