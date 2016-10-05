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
using System.Diagnostics;

namespace Mace
{
    class Chunks
    {
        public static void MakeChunks(ChunkManager cm, int intStart, int intEnd, frmMace frmLogForm)
        {
            for (int xi = intStart; xi < intEnd; xi++)
            {
                for (int zi = intStart; zi < intEnd; zi++)
                {
                    ChunkRef chunkActive = cm.CreateChunk(xi, zi);
                    chunkActive.IsTerrainPopulated = true;
                    chunkActive.Blocks.AutoLight = false;
                    FlatChunk(chunkActive);
                    cm.Save();
                }
                frmLogForm.UpdateProgress((1 + xi) * 34 / (intEnd - intStart));
            }
            cm.Save();
        }
        private static void FlatChunk(ChunkRef chunk)
        {
            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    for (int y = 0; y < 2; y++)
                    {
                        chunk.Blocks.SetID(x, y, z, BlockType.BEDROCK);
                    }
                    for (int y = 2; y < 59; y++)
                    {
                        chunk.Blocks.SetID(x, y, z, BlockType.STONE);
                    }
                    for (int y = 59; y < 63; y++)
                    {
                        chunk.Blocks.SetID(x, y, z, BlockType.DIRT);
                    }
                    for (int y = 63; y < 64; y++)
                    {
                        chunk.Blocks.SetID(x, y, z, BlockType.GRASS);
                    }
                }
            }
        }
        public static void ResetLighting(BetaWorld world, ChunkManager cm, frmMace frmLogForm, int intTotalChunks)
        {
            int intChunksProcessed = 0;
             //this code is based on a substrate example
             //http://code.google.com/p/substrate-minecraft/source/browse/trunk/Substrate/SubstrateCS/Examples/Relight/Program.cs
             //see the <License Substrate.txt> file for copyright information
            foreach (ChunkRef chunk in cm)
            {
                try
                {
                    chunk.Blocks.RebuildHeightMap();
                    chunk.Blocks.ResetBlockLight();
                    chunk.Blocks.ResetSkyLight();
                    chunk.Blocks.RebuildBlockLight();
                    chunk.Blocks.RebuildSkyLight();
                    cm.Save();
                    intChunksProcessed++;
                    if (intChunksProcessed % 10 == 0)
                    {
                        frmLogForm.UpdateProgress(58 + ((intChunksProcessed * (100 - 58)) / intTotalChunks));
                    }
                }
                catch (Exception)
                {
                    Debug.WriteLine("Chunk light fail");
                }
            }
        }
        //public static void MoveChunks(BetaWorld world, ChunkManager cm)
        //{
        //    cm.Save();
        //    for (int x = 1; x < 4; x++)
        //    {
        //        for (int z = 1; z < 4; z++)
        //        {
        //            ChunkRef chunkSource = cm.GetChunkRef(x + 4, z + 4);
        //            ChunkRef chunkDestination = cm.CreateChunk(x + 20, z + 20);
        //            cm.CopyChunk(x + 4, z + 4, x + 20, z + 20);
        //            cm.Save();
        //            //chunkDestination.SetChunkRef(chunkSource.GetChunkCopy());
        //            Debug.WriteLine("Moved " + x + " : " + z);
        //        }
        //    }
        //    world.Save();
        //}
    }
}
