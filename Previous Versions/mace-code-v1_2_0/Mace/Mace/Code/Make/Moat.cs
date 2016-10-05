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
    class Moat
    {
        static Random rand = new Random();
        public static void MakeMoat(int intFarmSize, int intMapSize, string strMoatLiquid)
        {
            int intMoatLiquid = (int)BlockType.STATIONARY_WATER;
            if (strMoatLiquid == "Lava" || (strMoatLiquid == "Random" && rand.NextDouble() > 0.75))
                    intMoatLiquid = (int)BlockType.STATIONARY_LAVA;
            for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                BlockShapes.MakeHollowLayers(a, intMapSize - a, 59, 62, a, intMapSize - a, intMoatLiquid);
            for (int a = intFarmSize - 1; a <= intFarmSize + 5; a++)
                BlockShapes.MakeHollowLayers(a, intMapSize - a, 63, 63, a, intMapSize - a, (int)BlockType.AIR);
        }
    }
}
