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

namespace Mace
{
    class Maze
    {
        static Random rand = new Random();
        public bool[,] GenerateMaze(int intSizeX, int intSizeZ)
        {  
            bool[,] booMaze = new bool[intSizeX, intSizeZ];
            int intDirX = 2, intDirZ = 0;
            int intGoX, intGoZ;
            int intPaths = 0;
            do
            {
                if (intPaths == 0)
                {
                    intGoX = 3;
                    intGoZ = 3;
                    booMaze[intGoX, intGoZ] = true;
                }
                else
                {
                    intGoX = 1 + (2 * rand.Next((intSizeX - 1) / 2));
                    intGoZ = 1 + (2 * rand.Next((intSizeZ - 1) / 2));
                }   
                if (booMaze[intGoX, intGoZ])
                {
                    switch (rand.Next(4))
                    {
                        case 0: intDirX = 0;  intDirZ = -2; break;
                        case 1: intDirX = 0;  intDirZ = 2;  break;
                        case 2: intDirX = 2;  intDirZ = 0;  break;
                        case 3: intDirX = -2; intDirZ = 0;  break;
                    }
                    
                    if (intGoX + intDirX < intSizeX && intGoX + intDirX >= 1 &&
                        intGoZ + intDirZ < intSizeZ && intGoZ + intDirZ >= 1)
                    {
                        if (!booMaze[intGoX + intDirX, intGoZ + intDirZ])
                        {
                            booMaze[intGoX + intDirX, intGoZ + intDirZ] = true;
                            booMaze[intGoX + (intDirX / 2), intGoZ + (intDirZ / 2)] = true;
                            intPaths++;
                        }
                    }
                }
            } while (intPaths + 1 < (intSizeX - 1) * (intSizeZ - 1) / 4);
            return booMaze;
        }
        public bool[,] CreateLoops(bool[,] booMaze, int intLoops)
        {
            do
            {
                int x = rand.Next(1, booMaze.GetUpperBound(0));
                int z = rand.Next(1, booMaze.GetUpperBound(1));
                if ((x + z) % 2 == 1 && !booMaze[x, z])
                {
                    booMaze[x, z] = true;
                    intLoops--;
                }
            } while (intLoops > 0);
            return booMaze;
        }
        public bool[,] DeleteDeadEnds(bool[,] booMaze, bool[,] booKeep1, bool[,] booKeep2)
        {
            bool blnChanged;
            do
            {
                blnChanged = false;
                for (int x = 1; x < booMaze.GetUpperBound(0); x++)
                {
                    for (int z = 1; z < booMaze.GetUpperBound(1); z++)
                    {
                        if (booMaze[x, z])
                        {
                            if (x % 2 == 0 || z % 2 == 0 ||
                                !(booKeep1[(x - 1) / 2, (z - 1) / 2] || booKeep2[(x - 1) / 2, (z - 1) / 2]))
                            {
                                int intNeighbours = 0;
                                if (booMaze[x - 1, z]) intNeighbours++;
                                if (booMaze[x, z - 1]) intNeighbours++;
                                if (booMaze[x + 1, z]) intNeighbours++;
                                if (booMaze[x, z + 1]) intNeighbours++;
                                if (intNeighbours == 1)
                                {
                                    booMaze[x, z] = false;
                                    blnChanged = true;
                                }
                            }
                        }
                    }
                }
            } while (blnChanged);
            return booMaze;
        }
        // don't need this to be enabled, because it's just for testing
        /*
        public void DisplayMaze(bool[,] booMaze)
        {
            for (int x = 0; x < booMaze.GetLength(0); x++)
            {
                for (int z = 0; z < booMaze.GetLength(1); z++)
                {
                    if (!booMaze[x, z])
                        Console.Write("X");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine("");
            }
        }
        */
    }
}
