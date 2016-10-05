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
    class Sewers
    {
        static Random rand = new Random();
        public static bool[,] MakeSewers(int intFarmSize, int intMapSize, int intPlotSize)
        {
            int intPlots = 1 + ((intMapSize - ((intFarmSize + 16) * 2)) / intPlotSize);

            bool[,] booNewSewerEntrances = MakeSewerEntrances(intPlots, false);

            Maze maze = new Maze();
            // 51, 43, 35, 27, 19, 11, 3
            for (int y = 3; y <= 51; y += 8)
            {
                bool[,] booOldSewerEntrances = booNewSewerEntrances;
                booNewSewerEntrances = MakeSewerEntrances(intPlots, y == 51);
                bool[,] booMaze = maze.GenerateMaze((intPlots * 2) + 1, (intPlots * 2) + 1);
                booMaze = maze.CreateLoops(booMaze, booMaze.GetLength(0) / 4);
                if (y == 3)
                {
                    booMaze = maze.DeleteDeadEnds(booMaze, booNewSewerEntrances);
                }
                else
                {
                    booMaze = maze.DeleteDeadEnds(booMaze,
                                                  MergeBooleanArrays(booNewSewerEntrances, booOldSewerEntrances));
                }
                for (int x = 0; x < booMaze.GetUpperBound(0); x++)
                {
                    for (int z = 0; z < booMaze.GetUpperBound(1); z++)
                    {
                        if (booMaze[x, z])
                        {
                            MakeSewerSection(booMaze, x, z, intFarmSize + 18 + (x * intPlotSize / 2),
                                             intFarmSize + 18 + (z * intPlotSize / 2), y, intPlotSize);
                        }
                    }
                }
                for (int x = 0; x < booMaze.GetUpperBound(0); x++)
                {
                    for (int z = 0; z < booMaze.GetUpperBound(1); z++)
                    {
                        if (booMaze[x, z])
                        {
                            if (y > 3 &&
                                x % 2 == 1 && z % 2 == 1 &&
                                booOldSewerEntrances[(x - 1) / 2, (z - 1) / 2])
                            {
                                BlockShapes.MakeSolidBox(intFarmSize + 18 + (x * intPlotSize / 2) - 1,
                                                         intFarmSize + 18 + (x * intPlotSize / 2) + 1,
                                                         y - 7, y + 1,
                                                         intFarmSize + 18 + (z * intPlotSize / 2) - 1,
                                                         intFarmSize + 18 + (z * intPlotSize / 2) + 1,
                                                         (int)BlockType.AIR);
                                BlockShapes.MakeHollowLayers(intFarmSize + 18 + (x * intPlotSize / 2) - 1,
                                                             intFarmSize + 18 + (x * intPlotSize / 2) + 1,
                                                             y, y,
                                                             intFarmSize + 18 + (z * intPlotSize / 2) - 1,
                                                             intFarmSize + 18 + (z * intPlotSize / 2) + 1,
                                                             (int)BlockType.STONE);
                                BlockShapes.MakeSolidBox(intFarmSize + 18 + (x * intPlotSize / 2),
                                                         intFarmSize + 18 + (x * intPlotSize / 2),
                                                         y - 7, y + 1,
                                                         intFarmSize + 18 + (z * intPlotSize / 2) + 1,
                                                         intFarmSize + 18 + (z * intPlotSize / 2) + 1,
                                                         (int)BlockType.STONE);
                                BlockHelper.MakeLadder(intFarmSize + 18 + (x * intPlotSize / 2), y - 7, y + 1,
                                                       intFarmSize + 18 + (z * intPlotSize / 2));
                            }
                        }
                    }
                }
            }
            return booNewSewerEntrances;
        }
        static bool[,] MakeSewerEntrances(int intPlots, bool booTopLayer)
        {
            bool[,] booSewerEntrances = new bool[intPlots + 1, intPlots + 1];
            if (booTopLayer)
            {
                booSewerEntrances[rand.Next(1, intPlots - 1), rand.Next(1, intPlots - 1)] = true;
                booSewerEntrances[rand.Next(1, intPlots - 1), rand.Next(1, intPlots - 1)] = true;
            }
            else if (intPlots <= 6)
            {
                booSewerEntrances[0, 0] = true;
                booSewerEntrances[0, intPlots - 1] = true;
                booSewerEntrances[intPlots - 1, 0] = true;
                booSewerEntrances[intPlots - 1, intPlots - 1] = true;
            }
            else
            {
                booSewerEntrances[rand.Next((intPlots / 2) - 1), rand.Next((intPlots / 2) - 1)] = true;
                booSewerEntrances[intPlots - rand.Next(1, (intPlots / 2) - 1), rand.Next((intPlots / 2) - 1)] = true;
                booSewerEntrances[rand.Next((intPlots / 2) - 1), intPlots - rand.Next(1, (intPlots / 2) - 1)] = true;
                booSewerEntrances[intPlots - rand.Next(1, (intPlots / 2) - 1),
                                  intPlots - rand.Next(1, (intPlots / 2) - 1)] = true;
            }
            return booSewerEntrances;
        }
        static void MakeSewerSection(bool[,] booMaze, int intMazeX, int intMazeZ, int intMapX, int intMapZ,
                                     int intStartY, int intPlotSize)
        {
            BlockShapes.MakeSolidBox(intMapX - 1, intMapX + 1, intStartY, intStartY,
                                     intMapZ - 1, intMapZ + 1, (int)BlockType.STATIONARY_WATER);
            BlockShapes.MakeSolidBox(intMapX - 2, intMapX + 2, intStartY + 1, intStartY + 3,
                                     intMapZ - 2, intMapZ + 2, (int)BlockType.AIR);
            BlockShapes.MakeSolidBox(intMapX - 1, intMapX + 1, intStartY + 4, intStartY + 4,
                                     intMapZ - 1, intMapZ + 1, (int)BlockType.AIR);
            if (booMaze[intMazeX - 1, intMazeZ])
            {
                BlockShapes.MakeSolidBox(intMapX - (intPlotSize / 2), intMapX - 1, intStartY, intStartY,
                                         intMapZ - 1, intMapZ + 1, (int)BlockType.STATIONARY_WATER);
                BlockShapes.MakeSolidBox(intMapX - (intPlotSize / 2), intMapX - 2, intStartY + 1, intStartY + 3,
                                         intMapZ - 2, intMapZ + 2, (int)BlockType.AIR);
                BlockShapes.MakeSolidBox(intMapX - (intPlotSize / 2), intMapX - 1, intStartY + 4, intStartY + 4,
                                         intMapZ - 1, intMapZ + 1, (int)BlockType.AIR);
            }
            if (booMaze[intMazeX + 1, intMazeZ])
            {
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX + (intPlotSize / 2), intStartY, intStartY,
                                         intMapZ - 1, intMapZ + 1, (int)BlockType.STATIONARY_WATER);
                BlockShapes.MakeSolidBox(intMapX - 2, intMapX + (intPlotSize / 2), intStartY + 1, intStartY + 3,
                                         intMapZ - 2, intMapZ + 2, (int)BlockType.AIR);
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX + (intPlotSize / 2), intStartY + 4, intStartY + 4,
                                         intMapZ - 1, intMapZ + 1, (int)BlockType.AIR);
            }
            if (booMaze[intMazeX, intMazeZ - 1])
            {
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX - 1, intStartY, intStartY,
                                         intMapZ - (intPlotSize / 2), intMapZ + 1, (int)BlockType.STATIONARY_WATER);
                BlockShapes.MakeSolidBox(intMapX - 2, intMapX - 2, intStartY + 1, intStartY + 3,
                                         intMapZ - (intPlotSize / 2), intMapZ + 2, (int)BlockType.AIR);
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX - 1, intStartY + 4, intStartY + 4,
                                         intMapZ - (intPlotSize / 2), intMapZ + 1, (int)BlockType.AIR);
            }
            if (booMaze[intMazeX, intMazeZ + 1])
            {
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX + 1, intStartY, intStartY,
                                         intMapZ - 1, intMapZ + (intPlotSize / 2), (int)BlockType.STATIONARY_WATER);
                BlockShapes.MakeSolidBox(intMapX - 2, intMapX + 2, intStartY + 1, intStartY + 3,
                                         intMapZ - 2, intMapZ + (intPlotSize / 2), (int)BlockType.AIR);
                BlockShapes.MakeSolidBox(intMapX - 1, intMapX + 1, intStartY + 4, intStartY + 4,
                                         intMapZ - 1, intMapZ + (intPlotSize / 2), (int)BlockType.AIR);
            }
        }
        /// <summary>
        /// returns an array the same size as booArray1, with each element set to true, if it
        ///   is true in booArray1 or booArray2
        /// </summary>
        static bool[,] MergeBooleanArrays(bool[,] booArray1, bool[,] booArray2)
        {
            bool[,] booCombined = new bool[booArray1.GetLength(0), booArray2.GetLength(1)];
            for (int x = 0; x < booArray1.GetLength(0); x++)
            {
                for (int z = 0; z < booArray1.GetLength(1); z++)
                {
                    booCombined[x, z] = (booArray1[x, z] || booArray2[x, z]);
                }
            }
            return booCombined;
        }
    }
}
