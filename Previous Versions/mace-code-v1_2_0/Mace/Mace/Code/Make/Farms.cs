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
    class Farms
    {
        static Random rand = new Random();
        enum FarmTypes { Cactus, Wheat, SugarCane, Mushroom };
        public static void MakeFarms(BlockManager bm, int intFarmSize, int intMapSize)
        {
            int intFarms = intMapSize / 6;
            while (intFarms > 0)
            {
                int xlen = rand.Next(6, 14);
                int x1 = rand.Next(intMapSize - xlen);
                int zlen = rand.Next(6, 14);
                int z1 = rand.Next(intMapSize - zlen);
                if (!(x1 >= intFarmSize && z1 >= intFarmSize && x1 <= intMapSize - intFarmSize && z1 <= intMapSize - intFarmSize))
                {
                    bool booValid = true;
                    for (int x = x1 - 2; x <= x1 + xlen + 2 && booValid; x++)
                        for (int z = z1 - 2; z <= z1 + zlen + 2 && booValid; z++)
                            // make sure it doesn't overlap with the spawn point or another farm
                            if ((x == intMapSize / 2 && z == intMapSize - 21) || bm.GetID(x, 63, z) != (int)BlockType.GRASS || bm.GetID(x, 64, z) != (int)BlockType.AIR)
                                booValid = false;
                    if (booValid)
                    {
                        FarmTypes curFarm;
                        int intFarmType = rand.Next(100);
                        if (intFarmType > 80)
                            curFarm = FarmTypes.Cactus;    // 20%
                        else if (intFarmType > 30)
                            curFarm = FarmTypes.Wheat;     // 50%
                        else
                            curFarm = FarmTypes.SugarCane; // 30%

                        BlockShapes.MakeHollowLayers(x1, x1 + xlen, 64, 64, z1, z1 + zlen, (int)BlockType.FENCE);

                        if (curFarm == FarmTypes.Cactus)
                        {
                            int intAttempts = 0;
                            do
                            {
                                int xCactus = rand.Next(x1 + 1, x1 + xlen), zCactus = rand.Next(z1 + 1, z1 + zlen);
                                bool booValidFarm = true;
                                for (int xCheck = xCactus - 1; xCheck <= xCactus + 1 && booValidFarm; xCheck++)
                                    for (int zCheck = zCactus - 1; zCheck <= zCactus + 1 && booValidFarm; zCheck++)
                                        if (bm.GetID(xCheck, 64, zCheck) != (int)BlockType.AIR)
                                            booValidFarm = false;
                                if (booValidFarm)
                                {
                                    bm.SetID(xCactus, 64, zCactus, (int)BlockType.CACTUS);
                                    if (rand.NextDouble() > 0.5)
                                        bm.SetID(xCactus, 65, zCactus, (int)BlockType.CACTUS);
                                }
                                intAttempts++;
                            }
                            while (intAttempts < 100);
                        }

                        for (int x = x1 + 1; x <= x1 + xlen - 1; x++)
                        {
                            for (int z = z1 + 1; z <= z1 + zlen - 1; z++)
                            {
                                switch ((int)curFarm)
                                {
                                    case (int)FarmTypes.Cactus:
                                        bm.SetID(x, 63, z, (int)BlockType.SAND);
                                        break;
                                    case (int)FarmTypes.Wheat:
                                        if (z == z1 + 1)
                                        {
                                            bm.SetID(x, 63, z, (int)BlockType.DOUBLE_SLAB);
                                        }
                                        else if (x % 2 == 0)
                                        {
                                            bm.SetID(x, 63, z, (int)BlockType.FARMLAND);
                                            bm.SetData(x, 63, z, 1);
                                            bm.SetID(x, 64, z, (int)BlockType.CROPS);
                                        }
                                        else
                                        {
                                            bm.SetID(x, 63, z, (int)BlockType.STATIONARY_WATER);
                                        }
                                        break;
                                    case (int)FarmTypes.SugarCane:
                                        if (z != z1 + 1)
                                        {
                                            if (x % 2 == 0)
                                            {
                                                bm.SetID(x, 64, z, (int)BlockType.SUGAR_CANE);
                                                if (rand.Next(100) > 50)
                                                    bm.SetID(x, 65, z, (int)BlockType.SUGAR_CANE);
                                            }
                                            else
                                            {
                                                bm.SetID(x, 63, z, (int)BlockType.STATIONARY_WATER);
                                            }
                                        }
                                        break;
                                }
                            }
                        }                        
                        int d = rand.Next(x1 + 1, x1 + xlen - 1);
                        if (curFarm == FarmTypes.Wheat)
                            bm.SetID(d, 63, z1, (int)BlockType.DOUBLE_SLAB);
                        bm.SetID(d, 64, z1, (int)BlockType.WOOD_DOOR);
                        bm.SetData(d, 64, z1, 4);
                        bm.SetID(d, 65, z1, (int)BlockType.WOOD_DOOR);
                        bm.SetData(d, 65, z1, 12);
                        intFarms--;
                    }
                }
            }
        }
    }
}
