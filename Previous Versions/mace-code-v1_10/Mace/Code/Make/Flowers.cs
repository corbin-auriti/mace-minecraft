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
using Substrate;

namespace Mace
{
    class Flowers
    {
        private const int NumberOfFlowersBetweenSaves = 100;
        private const int MinimumFreeNeighboursNeededForFlowers = 3;

        public static void MakeFlowers(BetaWorld worldDest, BlockManager bm)
        {
            string[] strFlowers = Utils.ArrayFromXMLElement(Path.Combine("Resources", "Themes", City.ThemeName + ".xml"), "options", "flowers");
            int FlowerCount = 0;
            for (int x = 0; x <= City.MapLength; x++)
            {
                for (int z = -City.FarmLength; z <= City.MapLength; z++)
                {
                    if (bm.GetID(x, 63, z) == City.GroundBlockID &&
                        bm.GetID(x, 64, z) == BlockInfo.Air.ID)
                    {
                        int FreeNeighbours = 0;
                        for (int xCheck = x - 1; xCheck <= x + 1; xCheck++)
                        {
                            for (int zCheck = z - 1; zCheck <= z + 1; zCheck++)
                            {
                                if (bm.GetID(xCheck, 63, zCheck) == City.GroundBlockID &&
                                    bm.GetID(xCheck, 64, zCheck) == BlockInfo.Air.ID)
                                {
                                    FreeNeighbours++;
                                }
                            }
                        }
                        if (FreeNeighbours >= MinimumFreeNeighboursNeededForFlowers && RNG.NextDouble() * 100 <= City.FlowerSpawnPercent)
                        {
                            AddFlowersToBlock(bm, x, z, FreeNeighbours, RNG.RandomItemFromArray(strFlowers));
                            if (++FlowerCount >= NumberOfFlowersBetweenSaves)
                            {
                                worldDest.Save();
                                FlowerCount = 0;
                            }
                        }
                    }
                }
            }
        }
        private static void AddFlowersToBlock(BlockManager bm, int x, int z, int intFreeNeighbours, string strFlower)
        {
            int intBlock = Convert.ToInt32(strFlower.Split('_')[0]);
            int intID = 0;
            if (strFlower.Contains("_"))
            {
                intID = City.GroundBlockData = Convert.ToInt32(strFlower.Split('_')[1]);
            }
            if (intBlock != BlockInfo.Cactus.ID || intFreeNeighbours == 9)
            {
                BlockShapes.MakeBlock(x, 64, z, intBlock, intID);
            }
        }
    }
}
