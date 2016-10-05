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
using Substrate;
using Substrate.Entities;

namespace Mace
{
    static class BlockShapes
    {
        // lots of these methods have an intMirror parameter.
        // - when this is set to 1, the blocks will be mirrored to the other 4 quarters of the map.
        // - when this is set to 2, the blocks will be mirrored within the current quarter, and then
        //     around to all the other quarters (so eight blocks in total).

        static BlockManager _bmDest;
        static RegionChunkManager _cmDest;

        public static void SetupClass(BlockManager bmDest, RegionChunkManager cmDest)
        {
            _bmDest = bmDest;
            _cmDest = cmDest;
        }
        public static void MakeSolidBox(int x1, int x2, int y1, int y2, int z1, int z2, int intBlock, int intMirror)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    for (int z = z1; z <= z2; z++)
                    {
                        _bmDest.SetID(x, y, z, intBlock);
                        if (intMirror >= 1)
                        {
                            _bmDest.SetID(City.mapLength - x, y, z, intBlock);
                            _bmDest.SetID(x, y, City.mapLength - z, intBlock);
                            _bmDest.SetID(City.mapLength - x, y, City.mapLength - z, intBlock);
                            if (intMirror == 2)
                            {
                                MakeSolidBox(z1, z2, y1, y2, x1, x2, intBlock, 1);
                            }
                        }
                    }
                }
            }
        }
        public static void MakeSolidBoxWithData(int x1, int x2, int y1, int y2, int z1, int z2,
                                                int intBlock, int intMirror, int intData)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    for (int z = z1; z <= z2; z++)
                    {
                        _bmDest.SetID(x, y, z, intBlock);
                        _bmDest.SetData(x, y, z, intData);
                        if (intMirror >= 1)
                        {
                            _bmDest.SetID(City.mapLength - x, y, z, intBlock);
                            _bmDest.SetData(City.mapLength - x, y, z, intData);
                            _bmDest.SetID(x, y, City.mapLength - z, intBlock);
                            _bmDest.SetData(x, y, City.mapLength - z, intData);
                            _bmDest.SetID(City.mapLength - x, y, City.mapLength - z, intBlock);
                            _bmDest.SetData(City.mapLength - x, y, City.mapLength - z, intData);
                            if (intMirror == 2)
                            {
                                MakeSolidBoxWithData(z1, z2, y1, y2, x1, x2, intBlock, 1, intData);
                            }
                        }
                    }
                }
            }
        }
        public static void MakeHollowBox(int x1, int x2, int y1, int y2, int z1, int z2,
                                         int intBlock, int intMirror, int intData)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    for (int z = z1; z <= z2; z++)
                    {
                        if (x == x1 || y == y1 || z == z1 || x == x2 || y == y2 || z == z2)
                        {
                            _bmDest.SetID(x, y, z, intBlock);
                            if (intData >= 0)
                            {
                                _bmDest.SetData(x, y, z, intData);
                            }
                            if (intMirror >= 1)
                            {
                                _bmDest.SetID(City.mapLength - x, y, z, intBlock);
                                _bmDest.SetID(x, y, City.mapLength - z, intBlock);
                                _bmDest.SetID(City.mapLength - x, y, City.mapLength - z, intBlock);
                                if (intMirror == 2)
                                {
                                    MakeHollowBox(z1, z2, y1, y2, x1, x2, intBlock, 1, intData);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// imagine a cardboard box with no top or bottom - that's what this code does!
        /// </summary>
        public static void MakeHollowLayers(int x1, int x2, int y1, int y2, int z1, int z2,
                                            int intBlock, int intMirror, int intData)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int z = z1; z <= z2; z++)
                {
                    if (x == x1 || z == z1 || x == x2 || z == z2)
                    {
                        for (int y = y1; y <= y2; y++)
                        {
                            _bmDest.SetID(x, y, z, intBlock);
                            if (intData >= 0)
                                _bmDest.SetData(x, y, z, intData);
                            if (intMirror >= 1)
                            {
                                _bmDest.SetID(City.mapLength - x, y, z, intBlock);
                                _bmDest.SetID(x, y, City.mapLength - z, intBlock);
                                _bmDest.SetID(City.mapLength - x, y, City.mapLength - z, intBlock);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// this sets the block and the data with one call, so it avoids repetition elsewhere
        /// </summary>
        public static void MakeBlock(int x, int y, int z, int intBlock, int intData)
        {
            _bmDest.SetID(x, y, z, intBlock);
            _bmDest.SetData(x, y, z, intData);
        }
        /// <summary>
        /// this just makes one block, so there's no point calling this unless the
        /// intMirror or intChance parameters are used
        /// </summary>
        public static void MakeBlock(int x, int y, int z, int intBlock, int intMirror, int intChance, int intData)
        {
            if (RNG.Next(100) <= intChance)
            {
                _bmDest.SetID(x, y, z, intBlock);
                if (intData >= 0)
                {
                    _bmDest.SetData(x, y, z, intData);
                }
            }
            if (intMirror >= 1)
            {
                MakeBlock(City.mapLength - x, y, z, intBlock, 0, intChance, intData);
                MakeBlock(x, y, City.mapLength - z, intBlock, 0, intChance, intData);
                MakeBlock(City.mapLength - x, y, City.mapLength - z, intBlock, 0, intChance, intData);
                if (intMirror == 2)
                {
                    MakeBlock(z, y, x, intBlock, 1, intChance, intData);
                }
            }
        }

        public static void MakeEntity(int x, int y, int z, TypedEntity entity, int intMirror)
        {
            entity.Position.X = x;
            entity.Position.Y = y;
            entity.Position.Z = z;

            ChunkRef chunkBuilding = _cmDest.GetChunkRef(x / 16, z / 16);
            chunkBuilding.Entities.Add(entity);  
            _cmDest.Save();

            if (intMirror >= 1)
            {
                MakeEntity(City.mapLength - x, y, z, entity, 0);
                MakeEntity(x, y, City.mapLength - z, entity, 0);
                MakeEntity(City.mapLength - x, y, City.mapLength - z, entity, 0);
                if (intMirror == 2)
                {
                    MakeEntity(z, y, x, entity, 1);
                }
            }            
        }

    }
}