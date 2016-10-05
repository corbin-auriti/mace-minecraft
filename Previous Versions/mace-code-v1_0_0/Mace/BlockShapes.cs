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

namespace Mace
{
    class BlockShapes
    {
        /*
         * lots of these methods have an intMirror parameter.
         * - when this is set to 1, the blocks will be mirrored to the other 4 quarters of the map.
         * - when this is set to 2, the blocks will be mirrored within the current quarter, and then
         *     around to all the other quarters (so eight blocks in total).
         */

        static int intMapSize;
        static BlockManager bm;
        static Random rand = new Random();
        public static void SetupClass(BlockManager bmOriginal, int intMapSizeOriginal)
        {
            bm = bmOriginal;
            intMapSize = intMapSizeOriginal;
        }
        public static void MakeSolidBox(int x1, int x2, int y1, int y2, int z1, int z2, int intBlock, int intMirror = 0)
        {
            for (int x = x1; x <= x2; x++)
                for (int y = y1; y <= y2; y++)
                    for (int z = z1; z <= z2; z++)
                    {
                        bm.SetID(x, y, z, intBlock);
                        if (intMirror >= 1)
                        {
                            bm.SetID(intMapSize - x, y, z, intBlock);
                            bm.SetID(x, y, intMapSize - z, intBlock);
                            bm.SetID(intMapSize - x, y, intMapSize - z, intBlock);
                            if (intMirror == 2)
                                MakeSolidBox(z1, z2, y1, y2, x1, x2, intBlock, 1);
                        }
                    }
        }
        public static void MakeHollowBox(int x1, int x2, int y1, int y2, int z1, int z2, int intBlock, int intMirror = 0)
        {
            for (int x = x1; x <= x2; x++)
                for (int y = y1; y <= y2; y++)
                    for (int z = z1; z <= z2; z++)
                        if (x == x1 || y == y1 || z == z1 || x == x2 || y == y2 || z == z2)
                        {
                            bm.SetID(x, y, z, intBlock);
                            if (intMirror >= 1)
                            {
                                bm.SetID(intMapSize - x, y, z, intBlock);
                                bm.SetID(x, y, intMapSize - z, intBlock);
                                bm.SetID(intMapSize - x, y, intMapSize - z, intBlock);
                                if (intMirror == 2)
                                    MakeHollowBox(z1, z2, y1, y2, x1, x2, intBlock, 1);
                            }
                        }
        }
        public static void MakeHollowLayers(int x1, int x2, int y1, int y2, int z1, int z2, int intBlock, int intMirror = 0)
        {
            for (int x = x1; x <= x2; x++)
                for (int z = z1; z <= z2; z++)
                    if (x == x1 || z == z1 || x == x2 || z == z2)
                        for (int y = y1; y <= y2; y++)
                        {
                            bm.SetID(x, y, z, intBlock);
                            if (intMirror >= 1)
                            {
                                bm.SetID(intMapSize - x, y, z, intBlock);
                                bm.SetID(x, y, intMapSize - z, intBlock);
                                bm.SetID(intMapSize - x, y, intMapSize - z, intBlock);
                                if (intMirror == 2)
                                    MakeHollowLayers(z1, z2, y, x1, x2, intBlock, 1);
                            }
                        }
        }
        public static void MakeBlock(int x, int y, int z, int intBlock, int intMirror = 0, int intChance = 100)
        {
            if (rand.Next(100) <= intChance)
                bm.SetID(x, y, z, intBlock);
            if (intMirror >= 1)
            {
                if (rand.Next(100) <= intChance)
                    bm.SetID(intMapSize - x, y, z, intBlock);
                if (rand.Next(100) <= intChance)
                    bm.SetID(x, y, intMapSize - z, intBlock);
                if (rand.Next(100) <= intChance)
                    bm.SetID(intMapSize - x, y, intMapSize - z, intBlock);
                if (intMirror == 2)
                    MakeBlock(z, y, x, intBlock, 1);
            }
        }
        public static void MakeLadder(int x, int y1, int y2, int z, int intDirection = 0, int intMirror = 0)
        {
            if (intDirection == 0)
                intDirection = BlockDirection(x, y1, z, (int)BlockType.STONE);
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
            }
        }
        public static int BlockDirection(int x, int y, int z, int intBlock)
        {
            if (bm.GetID(x + 1, y, z) == intBlock)
                return 4;
            else if (bm.GetID(x - 1, y, z) == intBlock)
                return 5;
            else if (bm.GetID(x, y, z + 1) == intBlock)
                return 2;
            else if (bm.GetID(x, y, z - 1) == intBlock)
                return 3;
            else
                return 0;
        }
    }
}
