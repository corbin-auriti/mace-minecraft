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
using System.Diagnostics;
using Substrate;

namespace Mace
{
    static class Moat
    {
        public static void MakeMoat(frmMace frmLogForm, BlockManager bm)
        {
            frmLogForm.UpdateLog("Moat type: " + City.moatType, true, true);
            switch (City.moatType)
            {
                case "Drop to Bedrock":
                    for (int a = City.edgeLength - 1; a <= City.edgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 2, 63, a,
                                                     City.mapLength - a, BlockInfo.Air.ID, 0, -1);
                    }
                    BlockShapes.MakeHollowLayers(City.edgeLength - 2, City.mapLength - (City.edgeLength - 2),
                                                 64, 64,
                                                 City.edgeLength - 2, City.mapLength - (City.edgeLength - 2), BlockInfo.Fence.ID, 0, -1);
                    break;
                case "Cactus":
                    for (int a = City.edgeLength - 1; a <= City.edgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 63, 63, a,
                                                     City.mapLength - a, BlockInfo.Sand.ID, 0, -1);
                    }
                    for (int a = City.edgeLength + 1; a <= City.mapLength / 2; a += 2)
                    {
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 64, City.edgeLength + 1, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.edgeLength + 1, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 64, City.edgeLength + 3, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.edgeLength + 3, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                    }
                    for (int a = City.edgeLength; a <= City.mapLength / 2; a += 2)
                    {
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 64, City.edgeLength, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.edgeLength, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 64, City.edgeLength + 2, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.edgeLength + 2, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 64, City.edgeLength + 4, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.edgeLength + 4, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                    }
                    if (City.hasGuardTowers)
                    {
                        for (int a = City.edgeLength + 3; a <= City.edgeLength + 13; a += 2)
                        {
                            BlockShapes.MakeBlock(a, 64, City.edgeLength + 3, BlockInfo.Air.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.edgeLength + 3, BlockInfo.Air.ID, 2, 100, -1);
                        }
                    }
                    break;
                case "Cactus Low":
                    for (int a = City.edgeLength - 1; a <= City.edgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 59, 63,
                                                     a, City.mapLength - a, BlockInfo.Air.ID, 0, -1);
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 58, 58,
                                                     a, City.mapLength - a, BlockInfo.Sand.ID, 0, -1);
                    }
                    for (int a = City.edgeLength + 1; a <= City.mapLength / 2; a += 2)
                    {
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 59, City.edgeLength + 1, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.edgeLength + 1, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 59, City.edgeLength + 3, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.edgeLength + 3, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                    }
                    for (int a = City.edgeLength; a <= City.mapLength / 2; a += 2)
                    {
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 59, City.edgeLength, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.edgeLength, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 59, City.edgeLength + 2, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.edgeLength + 2, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 59, City.edgeLength + 4, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.edgeLength + 4, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                    }
                    if (City.hasGuardTowers)
                    {
                        for (int a = City.edgeLength + 3; a <= City.edgeLength + 13; a += 2)
                        {
                            BlockShapes.MakeBlock(a, 59, City.edgeLength + 3, BlockInfo.Air.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.edgeLength + 3, BlockInfo.Air.ID, 2, 100, -1);
                        }
                    }
                    break;
                case "Lava":
                    for (int a = City.edgeLength - 1; a <= City.edgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 55, 56,
                                                     a, City.mapLength - a, BlockInfo.Lava.ID, 0, -1);
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 57, 63,
                                                     a, City.mapLength - a, BlockInfo.Air.ID, 0, -1);
                    }
                    break;
                case "Fire":
                    for (int a = City.edgeLength - 1; a <= City.edgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 56, 56,
                                                     a, City.mapLength - a, BlockInfo.Netherrack.ID, 0, -1);
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 57, 57,
                                                     a, City.mapLength - a, BlockInfo.Fire.ID, 0, -1);
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 58, 63,
                                                     a, City.mapLength - a, BlockInfo.Air.ID, 0, -1);
                    }
                    break;
                case "Water":
                    for (int a = City.edgeLength - 1; a <= City.edgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 59, 63,
                                                     a, City.mapLength - a, BlockInfo.Water.ID, 0, -1);
                    }
                    break;
                case "Cobweb":
                    for (int a = City.edgeLength - 1; a <= City.edgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 59, 59,
                                                     a, City.mapLength - a, BlockInfo.Cobweb.ID, 0, -1);
                        BlockShapes.MakeHollowLayers(a, City.mapLength - a, 60, 63,
                                                     a, City.mapLength - a, BlockInfo.Air.ID, 0, -1);
                    }
                    break;
                default:
                    Debug.Fail("Invalid switch result");
                    break;
            }
            // drawbridge
            int intBridgeEnd = City.hasMoat ? -2 : 5;
            if (City.moatType == "Lava" || City.moatType == "Fire")
            {
                BlockShapes.MakeSolidBox((City.mapLength / 2) - 2, City.mapLength / 2, 63, 63,
                                         City.edgeLength + intBridgeEnd, City.edgeLength + 6, BlockInfo.StoneBrick.ID, 2);
            }
            else
            {
                BlockShapes.MakeSolidBox((City.mapLength / 2) - 2, City.mapLength / 2, 63, 63,
                                         City.edgeLength + intBridgeEnd, City.edgeLength + 6, BlockInfo.WoodPlank.ID, 2);
            }
            BlockShapes.MakeSolidBox((City.mapLength / 2) - 2, City.mapLength / 2, 64, 65,
                         City.edgeLength + intBridgeEnd, City.edgeLength + 5, BlockInfo.Air.ID, 2);
        }
    }
}
