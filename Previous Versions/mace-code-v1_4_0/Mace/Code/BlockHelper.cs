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
using Substrate;
using Substrate.TileEntities;
using System.Diagnostics;

namespace Mace
{
    class BlockHelper
    {
        public const int SaplingSpruce = 9;
        public const int SaplingBirch = 10;
        public const int SaplingOak = 11;

        static int intMapSize;
        static BlockManager bm;
        static Random rand = new Random();

        public static void SetupClass(BlockManager bmOriginal, int intMapSizeOriginal)
        {
            bm = bmOriginal;
            intMapSize = intMapSizeOriginal;
        }
        public static void MakeSign(int x, int y, int z, string strSignText, int intBlockAgainst)
        {
            bm.SetID(x, y, z, (int)BlockType.WALL_SIGN);
            Substrate.TileEntities.TileEntitySign tes = new Substrate.TileEntities.TileEntitySign();
            string[] strRandomSign = TextToSign(strSignText);
            tes.Text1 = strRandomSign[0];
            tes.Text2 = strRandomSign[1];
            tes.Text3 = strRandomSign[2];
            tes.Text4 = strRandomSign[3];
            bm.SetTileEntity(x, y, z, tes);
            bm.SetData(x, y, z, BlockDirectionLadderSign(x, y, z, intBlockAgainst));
        }
        public static string[] TextToSign(string strSignText)
        {
            string[] strSign = strSignText.Split('|');
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
        public static void MakeBed(int x1, int x2, int y, int z1, int z2, int intMirror = 0)
        {
            int intBedOrientation;
            if (x1 == x2 - 1)
            {
                intBedOrientation = (int)BedOrientation.NORTH;
            }
            else if (x1 == x2 + 1)
            {
                intBedOrientation = (int)BedOrientation.SOUTH;
            }
            else if (z1 == z2 - 1)
            {
                intBedOrientation = (int)BedOrientation.EAST;
            }
            else
            {
                intBedOrientation = (int)BedOrientation.WEST;
            }
            bm.SetID(x1, y, z1, (int)BlockType.BED);
            bm.SetData(x1, y, z1, (int)BedState.HEAD + intBedOrientation);
            bm.SetID(x2, y, z2, (int)BlockType.BED);
            bm.SetData(x2, y, z2, intBedOrientation);
            if (intMirror > 0)
            {
                MakeBed(intMapSize - x1, intMapSize - x2, y, z1, z2);
                MakeBed(x1, x2, y, intMapSize - z1, intMapSize - z2);
                MakeBed(intMapSize - x1, intMapSize - x2, y, intMapSize - z1, intMapSize - z2);
                if (intMirror == 2)
                {
                    MakeBed(z1, z2, y, x1, x2, 1);
                }
            }
        }
        public static void MakeTorch(int x, int y, int z, int intBlockAgainst, int intMirror = 0)
        {
            bm.SetID(x, y, z, (int)BlockType.TORCH);
            bm.SetData(x, y, z, BlockDirection(x, y, z, intBlockAgainst));
            if (intMirror > 0)
            {
                MakeTorch(intMapSize - x, y, z, intBlockAgainst);
                MakeTorch(x, y, intMapSize - z, intBlockAgainst);
                MakeTorch(intMapSize - x, y, intMapSize - z, intBlockAgainst);
                if (intMirror == 2)
                {
                    MakeTorch(z, y, x, intBlockAgainst, 1);
                }
            }
        }
        public static void MakeLever(int x, int y, int z, int intBlockAgainst, int intMirror = 0)
        {
            bm.SetID(x, y, z, (int)BlockType.LEVER);
            bm.SetData(x, y, z, BlockDirection(x, y, z, intBlockAgainst));
            if (intMirror > 0)
            {
                MakeLever(intMapSize - x, y, z, intBlockAgainst);
                MakeLever(x, y, intMapSize - z, intBlockAgainst);
                MakeLever(intMapSize - x, y, intMapSize - z, intBlockAgainst);
                if (intMirror == 2)
                {
                    MakeLever(z, y, x, intBlockAgainst, 1);
                }
            }
        }
        public static void MakeLadder(int x, int y1, int y2, int z, int intMirror = 0, int intBlockAgainst = (int)BlockType.STONE)
        {
            int intDirection = BlockDirectionLadderSign(x, y1, z, intBlockAgainst);
            for (int y = y1; y <= y2; y++)
            {
                bm.SetID(x, y, z, (int)BlockType.LADDER);
                bm.SetData(x, y, z, intDirection);
            }
            if (intMirror > 0)
            {
                MakeLadder(intMapSize - x, y1, y2, z, 0, intBlockAgainst);
                MakeLadder(x, y1, y2, intMapSize - z, 0, intBlockAgainst);
                MakeLadder(intMapSize - x, y1, y2, intMapSize - z, 0, intBlockAgainst);
                if (intMirror == 2)
                {
                    MakeLadder(z, y1, y2, x, 1, intBlockAgainst);
                }
            }
        }
        public static void MakeChest(int x, int y, int z, int intBlockAgainst, TileEntityChest tec, int intMirror = 0)
        {
            BlockShapes.MakeBlock(x, y, z, (int)BlockType.CHEST);
            bm.SetData(x, y, z, BlockHelper.BlockDirection(x, y, z, intBlockAgainst));
            bm.SetTileEntity(x, y, z, tec);
            if (intMirror > 0)
            {
                MakeChest(intMapSize - x, y, z, intBlockAgainst, tec);
                MakeChest(x, y, intMapSize - z, intBlockAgainst, tec);
                MakeChest(intMapSize - x, y, intMapSize - z, intBlockAgainst, tec);
                if (intMirror == 2)
                {
                    MakeChest(z, y, x, intBlockAgainst, tec, 1);
                }
            }
        }
        public static void MakeDoor(int x, int y, int z, int intBlockAgainst, bool booIronDoor, int intMirror = 0)
        {
            int intDirection = BlockHelper.DoorDirection(x, y + 1, z, intBlockAgainst);
            int intBlockType = (int)BlockType.WOOD_DOOR;
            if (booIronDoor)
                intBlockType = (int)BlockType.IRON_DOOR;
            BlockShapes.MakeBlock(x, y + 1, z, intBlockType, intData: (int)DoorState.TOPHALF + intDirection);
            BlockShapes.MakeBlock(x, y, z, intBlockType, intData: intDirection);
            if (intMirror > 0)
            {
                MakeDoor(intMapSize - x, y, z, intBlockAgainst, booIronDoor);
                MakeDoor(x, y, intMapSize - z, intBlockAgainst, booIronDoor);
                MakeDoor(intMapSize - x, y, intMapSize - z, intBlockAgainst, booIronDoor);
                if (intMirror == 2)
                {
                    MakeDoor(z, y, x, intBlockAgainst, booIronDoor, 1);
                }
            }
        }
        public static int DoorDirection(int x, int y, int z, int intBlockAgainst)
        {
            if (bm.GetID(x - 1, y, z) == intBlockAgainst)
            {
                return (int)DoorHinge.SOUTHWEST;
            }
            else if (bm.GetID(x + 1, y, z) == intBlockAgainst)
            {
                return (int)DoorHinge.NORTHEAST;
            }
            else if (bm.GetID(x, y, z - 1) == intBlockAgainst)
            {
                return (int)DoorHinge.NORTHWEST;
            }
            else if (bm.GetID(x, y, z + 1) == intBlockAgainst)
            {
                return (int)DoorHinge.SOUTHEAST;
            }
            else
            {
                Debug.Assert(false);
                return 0;
            }
        }        
        public static int BlockDirection(int x, int y, int z, int intBlockAgainst)
        {
            if (bm.GetID(x - 1, y, z) == intBlockAgainst)
            {
                return 1;
            }
            else if (bm.GetID(x + 1, y, z) == intBlockAgainst)
            {
                return 2;
            }
            else if (bm.GetID(x, y, z - 1) == intBlockAgainst)
            {
                return 3;
            }
            else if (bm.GetID(x, y, z + 1) == intBlockAgainst)
            {
                return 4;
            }
            else
            {
                Debug.Assert(false);
                return 0;
            }
        }
        /// <summary>
        /// ladders and wall signs have different direction data,
        ///   so they use this method instead
        /// </summary>
        public static int BlockDirectionLadderSign(int x, int y, int z, int intBlockAgainst)
        {
            if (bm.GetID(x, y, z + 1) == intBlockAgainst)
            {
                return 2;
            }
            else if (bm.GetID(x, y, z - 1) == intBlockAgainst)
            {
                return 3;
            }
            else if (bm.GetID(x + 1, y, z) == intBlockAgainst)
            {
                return 4;
            }
            else if (bm.GetID(x - 1, y, z) == intBlockAgainst)
            {
                return 5;
            }
            else
            {
                Debug.Assert(false);
                return 0;
            }
        }
        public static Item MakeItem(int intID, int intCount = 1)
        {
            Item i = new Item();
            i.ID = intID;
            i.Count = intCount;
            return i;
        }
    }
}
