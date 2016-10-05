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

/* Useful links
   ------------

 * Project site
    * http://code.google.com/p/mace-minecraft/
 * To Do
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
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mace
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMace());
        }
    }
}
