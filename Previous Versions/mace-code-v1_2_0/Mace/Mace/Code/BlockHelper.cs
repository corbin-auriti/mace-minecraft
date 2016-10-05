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
            string[] strRandomSign = strSignText.Split('|');
            for (int a = 0; a <= 3; a++)
            {
                while (strRandomSign[a].Length < 14)
                {
                    strRandomSign[a] += ' ';
                    if (strRandomSign[a].Length < 14)
                        strRandomSign[a] = ' ' + strRandomSign[a];
                }
            }
            tes.Text1 = strRandomSign[0];
            tes.Text2 = strRandomSign[1];
            tes.Text3 = strRandomSign[2];
            tes.Text4 = strRandomSign[3];
            bm.SetTileEntity(x, y, z, tes);
            bm.SetData(x, y, z, BlockDirectionLadderSign(x, y, z, intBlockAgainst));
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
                    MakeTorch(z, y, x, intBlockAgainst, 1);
            }
        }
        public static void MakeLadder(int x, int y1, int y2, int z, int intMirror = 0)
        {
            int intDirection = BlockDirectionLadderSign(x, y1, z, (int)BlockType.STONE);
            for (int y = y1; y <= y2; y++)
            {
                bm.SetID(x, y, z, (int)BlockType.LADDER);
                bm.SetData(x, y, z, intDirection);
            }
            if (intMirror > 0)
            {
                MakeLadder(intMapSize - x, y1, y2, z);
                MakeLadder(x, y1, y2, intMapSize - z);
                MakeLadder(intMapSize - x, y1, y2, intMapSize - z);
                if (intMirror == 2)
                    MakeLadder(z, y1, y2, x, 1);
            }
        }
        public static void MakeChest(int x, int y, int z, int intBlockAgainst, TileEntityChest tec, int intMirror = 0)
        {
            BlockShapes.MakeBlock(x, y, z, (int)BlockType.CHEST, 2);
            bm.SetData(x, y, z, BlockHelper.BlockDirection(x, y, z, intBlockAgainst));
            bm.SetTileEntity(x, y, z, tec);
            if (intMirror > 0)
            {
                MakeChest(intMapSize - x, y, z, intBlockAgainst, tec);
                MakeChest(x, y, intMapSize - z, intBlockAgainst, tec);
                MakeChest(intMapSize - x, y, intMapSize - z, intBlockAgainst, tec);
                if (intMirror == 2)
                    MakeChest(z, y, x, intBlockAgainst, tec, 1);
            }
        }
        
        public static int BlockDirection(int x, int y, int z, int intBlockAgainst)
        {
            if (bm.GetID(x - 1, y, z) == intBlockAgainst)
                return 1;
            else if (bm.GetID(x + 1, y, z) == intBlockAgainst)
                return 2;
            else if (bm.GetID(x, y, z - 1) == intBlockAgainst)
                return 3;
            else if (bm.GetID(x, y, z + 1) == intBlockAgainst)
                return 4;
            else
            {
                Debug.Assert(false);
                return 0;
            }
        }
        // ladders and wall signs have different direction data,
        //   so they use this method instead
        public static int BlockDirectionLadderSign(int x, int y, int z, int intBlockAgainst)
        {
            if (bm.GetID(x, y, z + 1) == intBlockAgainst)
                return 2;
            else if (bm.GetID(x, y, z - 1) == intBlockAgainst)
                return 3;
            else if (bm.GetID(x + 1, y, z) == intBlockAgainst)
                return 4;
            else if (bm.GetID(x - 1, y, z) == intBlockAgainst)
                return 5;
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
