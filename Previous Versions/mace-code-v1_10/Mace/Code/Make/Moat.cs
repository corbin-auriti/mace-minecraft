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
            frmLogForm.UpdateLog("Moat type: " + City.MoatType, true, true);
            switch (City.MoatType)
            {
                case "Drop to Bedrock":
                    for (int a = City.EdgeLength - 1; a <= City.EdgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.MapLength - a, 2, 63, a,
                                                     City.MapLength - a, BlockInfo.Air.ID, 0, -1);
                    }
                    BlockShapes.MakeHollowLayers(City.EdgeLength - 2, City.MapLength - (City.EdgeLength - 2),
                                                 64, 64,
                                                 City.EdgeLength - 2, City.MapLength - (City.EdgeLength - 2), BlockInfo.Fence.ID, 0, -1);
                    break;
                case "Cactus":
                    for (int a = City.EdgeLength - 1; a <= City.EdgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.MapLength - a, 63, 63, a,
                                                     City.MapLength - a, BlockInfo.Sand.ID, 0, -1);
                    }
                    for (int a = City.EdgeLength + 1; a <= City.MapLength / 2; a += 2)
                    {
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 64, City.EdgeLength + 1, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.EdgeLength + 1, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 64, City.EdgeLength + 3, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.EdgeLength + 3, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                    }
                    for (int a = City.EdgeLength; a <= City.MapLength / 2; a += 2)
                    {
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 64, City.EdgeLength, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.EdgeLength, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 64, City.EdgeLength + 2, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.EdgeLength + 2, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 64, City.EdgeLength + 4, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.EdgeLength + 4, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                    }
                    if (City.HasGuardTowers)
                    {
                        for (int a = City.EdgeLength + 3; a <= City.EdgeLength + 13; a += 2)
                        {
                            BlockShapes.MakeBlock(a, 64, City.EdgeLength + 3, BlockInfo.Air.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 65, City.EdgeLength + 3, BlockInfo.Air.ID, 2, 100, -1);
                        }
                    }
                    break;
                case "Cactus Low":
                    for (int a = City.EdgeLength - 1; a <= City.EdgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.MapLength - a, 59, 63,
                                                     a, City.MapLength - a, BlockInfo.Air.ID, 0, -1);
                        BlockShapes.MakeHollowLayers(a, City.MapLength - a, 58, 58,
                                                     a, City.MapLength - a, BlockInfo.Sand.ID, 0, -1);
                    }
                    for (int a = City.EdgeLength + 1; a <= City.MapLength / 2; a += 2)
                    {
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 59, City.EdgeLength + 1, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.EdgeLength + 1, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 59, City.EdgeLength + 3, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.EdgeLength + 3, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                    }
                    for (int a = City.EdgeLength; a <= City.MapLength / 2; a += 2)
                    {
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 59, City.EdgeLength, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.EdgeLength, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 59, City.EdgeLength + 2, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.EdgeLength + 2, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                        if (RNG.NextDouble() > 0.5)
                        {
                            BlockShapes.MakeBlock(a, 59, City.EdgeLength + 4, BlockInfo.Cactus.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.EdgeLength + 4, BlockInfo.Cactus.ID, 2, 50, -1);
                        }
                    }
                    if (City.HasGuardTowers)
                    {
                        for (int a = City.EdgeLength + 3; a <= City.EdgeLength + 13; a += 2)
                        {
                            BlockShapes.MakeBlock(a, 59, City.EdgeLength + 3, BlockInfo.Air.ID, 2, 100, -1);
                            BlockShapes.MakeBlock(a, 60, City.EdgeLength + 3, BlockInfo.Air.ID, 2, 100, -1);
                        }
                    }
                    break;
                case "Lava":
                    for (int a = City.EdgeLength - 1; a <= City.EdgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.MapLength - a, 55, 56,
                                                     a, City.MapLength - a, BlockInfo.Lava.ID, 0, -1);
                        BlockShapes.MakeHollowLayers(a, City.MapLength - a, 57, 63,
                                                     a, City.MapLength - a, BlockInfo.Air.ID, 0, -1);
                    }
                    break;
                case "Fire":
                    for (int a = City.EdgeLength - 1; a <= City.EdgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.MapLength - a, 56, 56,
                                                     a, City.MapLength - a, BlockInfo.Netherrack.ID, 0, -1);
                        BlockShapes.MakeHollowLayers(a, City.MapLength - a, 57, 57,
                                                     a, City.MapLength - a, BlockInfo.Fire.ID, 0, -1);
                        BlockShapes.MakeHollowLayers(a, City.MapLength - a, 58, 63,
                                                     a, City.MapLength - a, BlockInfo.Air.ID, 0, -1);
                    }
                    break;
                case "Water":
                    for (int a = City.EdgeLength - 1; a <= City.EdgeLength + 5; a++)
                    {
                        BlockShapes.MakeHollowLayers(a, City.MapLength - a, 59, 63,
                                                     a, City.MapLength - a, BlockInfo.Water.ID, 0, -1);
                    }
                    break;
                default:
                    Debug.Fail("Invalid switch result");
                    break;
            }
            // drawbridge
            int intBridgeEnd = City.HasMoat ? -2 : 5;
            if (City.MoatType == "Lava" || City.MoatType == "Fire")
            {
                BlockShapes.MakeSolidBox((City.MapLength / 2) - 2, City.MapLength / 2, 63, 63,
                                         City.EdgeLength + intBridgeEnd, City.EdgeLength + 13, BlockInfo.StoneBrick.ID, 2);
            }
            else
            {
                BlockShapes.MakeSolidBox((City.MapLength / 2) - 2, City.MapLength / 2, 63, 63,
                                         City.EdgeLength + intBridgeEnd, City.EdgeLength + 13, BlockInfo.WoodPlank.ID, 2);
            }
            BlockShapes.MakeSolidBox((City.MapLength / 2) - 2, City.MapLength / 2, 64, 65,
                         City.EdgeLength + intBridgeEnd, City.EdgeLength + 13, BlockInfo.Air.ID, 2);
        }
    }
}
