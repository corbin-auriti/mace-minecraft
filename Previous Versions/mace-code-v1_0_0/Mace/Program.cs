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
    along with this program.  If not, see <http://www.gnu.org/licenses/>
*/

/* Useful links
   ------------

 * Project site
    * http://code.google.com/p/mace-minecraft/
 * ToDo
    * http://code.google.com/p/mace-minecraft/wiki/ToDo
 * Block IDs
    * http://www.minecraftwiki.net/wiki/File:DataValuesBeta.png
 * Data values
    * http://www.minecraftwiki.net/wiki/Data_values
 * Substrate site
    * http://code.google.com/p/substrate-minecraft/
 * Substrate topic
    * http://www.minecraftforum.net/topic/245996-sdk-substrate-map-editing-library-for-cnet-051/
 
 */

using System;
using System.IO;
using Substrate;
using System.Reflection;
using Substrate.TileEntities;

namespace Mace
{
    class Program
    {      
        static void Main(string[] args)
        {
            Version ver = Assembly.GetEntryAssembly().GetName().Version;
            Console.WriteLine("Mace v{0}.{1}.{2} by Robson [http://iceyboard.no-ip.org]\n",
                              ver.Major, ver.Minor, ver.Revision);

            Random rand = new Random();

            TextGenerators tg = new TextGenerators();
            string strFolder, strCityName;

            do
            {
                strCityName = tg.CityName();
                strFolder = Environment.GetEnvironmentVariable("APPDATA") + @"\.minecraft\saves\" + strCityName + @"\";
            } while(Directory.Exists(strFolder));

            Directory.CreateDirectory(strFolder);
            BetaWorld world = BetaWorld.Create(@strFolder);

            int intFarmSize = 28;
            int intPlotSize = 12;
            int intMapSize = (rand.Next(12, 20) * intPlotSize) + (intFarmSize * 2);

            ChunkManager cm = world.GetChunkManager();
            Generation.MakeChunks(cm, -1, 2 + (intMapSize / 16));

            BlockManager bm = world.GetBlockManager();
            bm.AutoLight = false;

            BlockShapes.SetupClass(bm, intMapSize);
            Generation.SetupClass(bm, intMapSize, intPlotSize, intFarmSize);

            bool[,] booSewerEntrances = Generation.MakeSewers();
            Generation.MakePlots(booSewerEntrances);
            Generation.MakeWall();
            Generation.MakeMoat();
            Generation.MakeDrawbridges();
            Generation.MakeGuardTowers();
            //Generation.MakeNoticeBoard();
            Generation.MakeFarms();

            world.Level.LevelName = strCityName;
            world.Level.SpawnX = intMapSize / 2;
            world.Level.SpawnZ = intMapSize - 21;
            world.Level.SpawnY = 64;
            if (rand.NextDouble() < 0.1)
            {
                world.Level.IsRaining = true;
                if (rand.NextDouble() < 0.25)
                    world.Level.IsThundering = true;
            }

            // hmmmmm... none of these things are fixing the lighting bug :(
            // todo: retry when next version comes out

            //bm.AutoLight = true;

            //cm.RelightDirtyChunks();
            
            //foreach (ChunkRef chunk in cm)
            //{
            //    chunk.Blocks.RebuildHeightMap();
            //    chunk.Blocks.ResetBlockLight();
            //    chunk.Blocks.ResetSkyLight();
            //    cm.Save();
            //}

            //foreach (ChunkRef chunk in cm)
            //{
            //    chunk.Blocks.RebuildBlockLight();
            //    chunk.Blocks.RebuildSkyLight();
            //    cm.Save();
            //}

            world.Save();

            Console.WriteLine("\nCreated the {0}!", strCityName);
            Console.WriteLine("Look for it at the end of your MineCraft world list.");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}