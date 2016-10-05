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

namespace Mace
{    
    class Buildings
    {

        static BlockManager bmDest;
        static Random rand = new Random();

        public static void MakeInsideCity(BlockManager bmDestOriginal, BetaWorld worldDest,
                                          int[,] intArea, int intFarmSize, int intMapSize, bool booIncludePaths)
        {
            bmDest = bmDestOriginal;
            int intBlockStart = intFarmSize + 14;
            MakeBuildings(intArea, intBlockStart, worldDest, intFarmSize);
            MakeFlowers(intFarmSize, intMapSize);
            if (!booIncludePaths)
            {
                RemovePaths(intArea, intBlockStart);
            }
        }

        private static void MakeBuildings(int[,] intArea, int intBlockStart, BetaWorld world, int intFarmSize)
        {
            int intBuildings = 0;
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int z = 0; z < intArea.GetLength(1); z++)
                {
                    if (intArea[x, z] >= 100 && intArea[x, z] <= 500)
                    {
                        SourceWorld.Building CurrentBuilding = SourceWorld.GetBuilding(intArea[x, z] - 100);
                        SourceWorld.InsertBuilding(bmDest, intBlockStart, x, z, CurrentBuilding);
                        intArea[x + CurrentBuilding.intSize - 2, z + CurrentBuilding.intSize - 2] = 0;
                        if (++intBuildings == 20)
                        {
                            world.Save();
                            intBuildings = 0;
                        }
                    }
                }
            }
        }
        private static void MakeFlowers(int intFarmSize, int intMapSize)
        {
            for (int x = intFarmSize + 13; x <= intMapSize - (intFarmSize + 13); x++)
            {
                for (int z = intFarmSize + 13; z <= intMapSize - (intFarmSize + 13); z++)
                {
                    bool booFree = true;
                    for (int xCheck = x - 1; xCheck <= x + 1 && booFree; xCheck++)
                    {
                        for (int zCheck = z - 1; zCheck <= z + 1 && booFree; zCheck++)
                        {
                            if (bmDest.GetID(xCheck, 63, zCheck) != BlockType.GRASS)
                            {
                                booFree = false;
                            }
                            else if (bmDest.GetID(xCheck, 64, zCheck) != BlockType.AIR)
                            {
                                booFree = false;
                            }
                        }
                    }
                    if (booFree && rand.NextDouble() > 0.75)
                    {
                        switch (rand.Next(4))
                        {
                            case 0:
                                bmDest.SetID(x, 64, z, BlockType.RED_ROSE);
                                break;
                            case 1:
                                bmDest.SetID(x, 64, z, BlockType.YELLOW_FLOWER);
                                break;
                            case 2:
                            case 3:
                                bmDest.SetID(x, 64, z, BlockType.TALL_GRASS);
                                bmDest.SetData(x, 64, z, rand.Next(1, 3));
                                break;
                        }
                    }
                }
            }
        }
        private static void RemovePaths(int[,] intArea, int intBlockStart)
        {
            for (int x = 0; x < intArea.GetLength(0); x++)
            {
                for (int z = 0; z < intArea.GetLength(1); z++)
                {
                    if (intArea[x, z] == 1)
                    {
                        bmDest.SetID(intBlockStart + x, 63, intBlockStart + z, (int)BlockType.GRASS);
                    }
                }
            }
        }
   
    }
}
