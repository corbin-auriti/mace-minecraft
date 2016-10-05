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
using Substrate.TileEntities;

namespace Mace
{
    static class Entrances
    {
        public static void MakeEntrances(BlockManager bm)
        {
            if (City.HasWalls)
            {
                SourceWorld.Building CurrentBuilding;
                CurrentBuilding = SourceWorld.SelectRandomBuilding(SourceWorld.BuildingTypes.CityEntrance, 0);
                SourceWorld.InsertBuilding(bm, new int[0, 0], 0, (City.MapLength / 2) - (CurrentBuilding.intSizeX / 2),
                                                                 City.EdgeLength + 5, CurrentBuilding, 0, 0);
                SourceWorld.InsertBuilding(bm, new int[0, 0], 0, City.EdgeLength + 5,
                                                                 (City.MapLength / 2) - (CurrentBuilding.intSizeX / 2), CurrentBuilding, 0, 1);
                SourceWorld.InsertBuilding(bm, new int[0, 0], 0, City.MapLength - (City.EdgeLength + 4 + CurrentBuilding.intSizeX),
                                                                 (City.MapLength / 2) - (CurrentBuilding.intSizeX / 2), CurrentBuilding, 0, 2);
                SourceWorld.InsertBuilding(bm, new int[0, 0], 0, (City.MapLength / 2) - (CurrentBuilding.intSizeX / 2),
                                                                 City.MapLength - (City.EdgeLength + 4 + CurrentBuilding.intSizeX), CurrentBuilding, 0, 3);
            }
        }
    }
}
