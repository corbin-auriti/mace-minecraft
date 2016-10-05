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
using Substrate.Entities;
using Substrate.TileEntities;

namespace Mace
{
    static class BlockHelper
    {
        static BlockManager _bmDest;

        public static void SetupClass(BlockManager bmDest)
        {
            _bmDest = bmDest;
        }
        public static void MakeSign(int x, int y, int z, string strSignText, int intBlockAgainst, int intMirror)
        {
            _bmDest.SetID(x, y, z, BlockInfo.WallSign.ID);
            Substrate.TileEntities.TileEntitySign tes = new Substrate.TileEntities.TileEntitySign();
            string[] strSign = strSignText.Split('~');
            tes.Text1 = strSign[0];
            tes.Text2 = strSign[1];
            tes.Text3 = strSign[2];
            tes.Text4 = strSign[3];
            _bmDest.SetTileEntity(x, y, z, tes);
            _bmDest.SetData(x, y, z, BlockDirectionLadderSign(x, y, z, intBlockAgainst));
            if (intMirror > 0)
            {
                MakeSign(City.mapLength - x, y, z, strSignText, intBlockAgainst, 0);
                MakeSign(x, y, City.mapLength - z, strSignText, intBlockAgainst, 0);
                MakeSign(City.mapLength - x, y, City.mapLength - z, strSignText, intBlockAgainst, 0);
                if (intMirror == 2)
                {
                    MakeSign(z, y, x, strSignText, intBlockAgainst, 1);
                }
            }
        }
        public static void MakeSignPost(int x, int y, int z, string strSignText)
        {
            Substrate.TileEntities.TileEntitySign tes = new Substrate.TileEntities.TileEntitySign();
            string[] strRandomSign = strSignText.Split('~');
            tes.Text1 = strRandomSign[0];
            tes.Text2 = strRandomSign[1];
            tes.Text3 = strRandomSign[2];
            tes.Text4 = strRandomSign[3];
            _bmDest.SetTileEntity(x, y, z, tes);
        }
        public static void MakeBed(int x1, int x2, int y, int z1, int z2, int intMirror)
        {
            int intBedOrientation;
            if (x1 == x2 - 1)
            {
                intBedOrientation = (int)BedOrientation.NORTH;
            }
            else if (x1 == x2 + 1)
            {
                intBedOrientation = (int)BedOrientation.SOUTH;
            }
            else if (z1 == z2 - 1)
            {
                intBedOrientation = (int)BedOrientation.EAST;
            }
            else
            {
                intBedOrientation = (int)BedOrientation.WEST;
            }
            BlockShapes.MakeBlock(x1, y, z1, BlockInfo.Bed.ID, (int)BedState.HEAD + intBedOrientation);
            BlockShapes.MakeBlock(x2, y, z2, BlockInfo.Bed.ID, intBedOrientation);
            if (intMirror > 0)
            {
                MakeBed(City.mapLength - x1, City.mapLength - x2, y, z1, z2, 0);
                MakeBed(x1, x2, y, City.mapLength - z1, City.mapLength - z2, 0);
                MakeBed(City.mapLength - x1, City.mapLength - x2, y, City.mapLength - z1, City.mapLength - z2, 0);
                if (intMirror == 2)
                {
                    MakeBed(z1, z2, y, x1, x2, 1);
                }
            }
        }
        public static void MakeTorch(int x, int y, int z, int intBlockAgainst, int intMirror)
        {
            BlockShapes.MakeBlock(x, y, z, BlockInfo.Torch.ID, BlockDirection(x, y, z, intBlockAgainst));
            if (intMirror > 0)
            {
                MakeTorch(City.mapLength - x, y, z, intBlockAgainst, 0);
                MakeTorch(x, y, City.mapLength - z, intBlockAgainst, 0);
                MakeTorch(City.mapLength - x, y, City.mapLength - z, intBlockAgainst, 0);
                if (intMirror == 2)
                {
                    MakeTorch(z, y, x, intBlockAgainst, 1);
                }
            }
        }
        public static void MakeLever(int x, int y, int z, int intBlockAgainst, int intMirror)
        {
            BlockShapes.MakeBlock(x, y, z, BlockInfo.Lever.ID, BlockDirection(x, y, z, intBlockAgainst));
            if (intMirror > 0)
            {
                MakeLever(City.mapLength - x, y, z, intBlockAgainst, 0);
                MakeLever(x, y, City.mapLength - z, intBlockAgainst, 0);
                MakeLever(City.mapLength - x, y, City.mapLength - z, intBlockAgainst, 0);
                if (intMirror == 2)
                {
                    MakeLever(z, y, x, intBlockAgainst, 1);
                }
            }
        }
        public static void MakeLadder(int x, int y1, int y2, int z, int intMirror, int intBlockAgainst)
        {
            int intDirection = BlockDirectionLadderSign(x, y1, z, intBlockAgainst);
            for (int y = y1; y <= y2; y++)
            {
                BlockShapes.MakeBlock(x, y, z, BlockInfo.Ladder.ID, intDirection);
            }
            if (intMirror > 0)
            {
                MakeLadder(City.mapLength - x, y1, y2, z, 0, intBlockAgainst);
                MakeLadder(x, y1, y2, City.mapLength - z, 0, intBlockAgainst);
                MakeLadder(City.mapLength - x, y1, y2, City.mapLength - z, 0, intBlockAgainst);
                if (intMirror == 2)
                {
                    MakeLadder(z, y1, y2, x, 1, intBlockAgainst);
                }
            }
        }
        public static void MakeChest(int x, int y, int z, int intBlockAgainst, TileEntityChest tec, int intMirror)
        {
            int direction = BlockHelper.BlockDirection(x, y, z, intBlockAgainst);
            switch (direction)
            {
                case 1: direction = 5; break;
                case 2: direction = 4; break;
                case 3: direction = 3; break;
                case 4: direction = 2; break;
            }
            BlockShapes.MakeBlock(x, y, z, BlockInfo.Chest.ID, direction);
            _bmDest.SetTileEntity(x, y, z, tec);
            if (intMirror > 0)
            {
                MakeChest(City.mapLength - x, y, z, intBlockAgainst, tec, 0);
                MakeChest(x, y, City.mapLength - z, intBlockAgainst, tec, 0);
                MakeChest(City.mapLength - x, y, City.mapLength - z, intBlockAgainst, tec, 0);
                if (intMirror == 2)
                {
                    MakeChest(z, y, x, intBlockAgainst, tec, 1);
                }
            }
        }
        public static void MakeDoor(int x, int y, int z, int intBlockAgainst, bool booIronDoor, int intMirror)
        {
            int intDirection = BlockHelper.DoorDirection(x, y + 1, z, intBlockAgainst);
            int intBlockType = BlockInfo.WoodDoor.ID;
            if (booIronDoor)
                intBlockType = BlockInfo.IronDoor.ID;
            BlockShapes.MakeBlock(x, y + 1, z, intBlockType, (int)DoorState.TOPHALF);
            BlockShapes.MakeBlock(x, y, z, intBlockType, intDirection);
            if (intMirror > 0)
            {
                MakeDoor(City.mapLength - x, y, z, intBlockAgainst, booIronDoor, 0);
                MakeDoor(x, y, City.mapLength - z, intBlockAgainst, booIronDoor, 0);
                MakeDoor(City.mapLength - x, y, City.mapLength - z, intBlockAgainst, booIronDoor, 0);
                if (intMirror == 2)
                {
                    MakeDoor(z, y, x, intBlockAgainst, booIronDoor, 1);
                }
            }
        }
        public static void MakeRail(int x, int y, int z)
        {
            int intDataPlus = 0;
            bool booSuccess = true;
            if (_bmDest.GetID(x, y, z) == BlockInfo.PoweredRail.ID)
            {
                if (_bmDest.GetData(x, y, z) >= (int)PoweredRailState.POWERED)
                {
                    _bmDest.SetData(x, y, z, _bmDest.GetData(x, y, z) - (int)PoweredRailState.POWERED);
                    intDataPlus = (int)PoweredRailState.POWERED;
                }
            }
            if (IsRailPiece(x, y, z - 1) &&
                IsRailPiece(x - 1, y, z))
            {
                _bmDest.SetData(x, y, z, 8);
            }
            else if (IsRailPiece(x, y, z - 1) &&
                     IsRailPiece(x + 1, y, z))
            {
                _bmDest.SetData(x, y, z, 9);
            }
            else if (IsRailPiece(x, y, z + 1) &&
                     IsRailPiece(x - 1, y, z))
            {
                _bmDest.SetData(x, y, z, 7);
            }
            else if (IsRailPiece(x, y, z + 1) &&
                     IsRailPiece(x + 1, y, z))
            {
                _bmDest.SetData(x, y, z, 6);
            }
            else if (IsRailPiece(x, y, z + 1))
            {
                _bmDest.SetData(x, y, z, 0);
            }
            else if (IsRailPiece(x, y, z - 1))
            {
                _bmDest.SetData(x, y, z, 0);
            }
            else if (IsRailPiece(x + 1, y, z))
            {
                _bmDest.SetData(x, y, z, 1);
            }
            else if (IsRailPiece(x - 1, y, z))
            {
                _bmDest.SetData(x, y, z, 1);
            }
            else
            {
                Debug.WriteLine("Rail fail: " + x + ", " + y + ", " + z);
                booSuccess = false;
            }
            if (booSuccess && intDataPlus >= 0)
            {
                _bmDest.SetData(x, y, z, _bmDest.GetData(x, y, z) + intDataPlus);
            }
        }
        private static bool IsRailPiece(int x, int y, int z)
        {
            switch (_bmDest.GetID(x, y, z))
            {
                case BlockType.DETECTOR_RAIL:
                case BlockType.POWERED_RAIL:
                case BlockType.RAILS:
                    return true;
            }
            return false;
        }
        public static int DoorDirection(int x, int y, int z, int intBlockAgainst)
        {
            if (_bmDest.GetID(x - 1, y, z) == intBlockAgainst)
            {
                return (int)DoorHinge.SOUTHWEST;
            }
            else if (_bmDest.GetID(x + 1, y, z) == intBlockAgainst)
            {
                return (int)DoorHinge.NORTHEAST;
            }
            else if (_bmDest.GetID(x, y, z - 1) == intBlockAgainst)
            {
                return (int)DoorHinge.NORTHWEST;
            }
            else if (_bmDest.GetID(x, y, z + 1) == intBlockAgainst)
            {
                return (int)DoorHinge.SOUTHEAST;
            }
            else
            {
                Debug.Assert(false);
                return 0;
            }
        }
        public static int BlockDirection(int x, int y, int z, int intBlockAgainst)
        {
            if (_bmDest.GetID(x - 1, y, z) == intBlockAgainst)
            {
                return 1;
            }
            else if (_bmDest.GetID(x + 1, y, z) == intBlockAgainst)
            {
                return 2;
            }
            else if (_bmDest.GetID(x, y, z - 1) == intBlockAgainst)
            {
                return 3;
            }
            else if (_bmDest.GetID(x, y, z + 1) == intBlockAgainst)
            {
                return 4;
            }
            else
            {
                Debug.Fail("Could not find a suitable block direction");
                return 0;
            }
        }
        /// <summary>
        /// ladders and wall signs have different direction data,
        ///   so they use this method instead
        /// </summary>
        public static int BlockDirectionLadderSign(int x, int y, int z, int intBlockAgainst)
        {
            if (_bmDest.GetID(x, y, z + 1) == intBlockAgainst)
            {
                return 2;
            }
            else if (_bmDest.GetID(x, y, z - 1) == intBlockAgainst)
            {
                return 3;
            }
            else if (_bmDest.GetID(x + 1, y, z) == intBlockAgainst)
            {
                return 4;
            }
            else if (_bmDest.GetID(x - 1, y, z) == intBlockAgainst)
            {
                return 5;
            }
            else
            {
                Debug.WriteLine("Could not find a suitable block direction for a ladder or sign");
                return 0;
            }
        }
        public static Item MakeItem(int intID, int intCount)
        {
            Item i = new Item();
            i.ID = intID;
            i.Count = intCount;
            return i;
        }

        // i hate rotation so much :(
        public static HugeMushroomType RotateMushroom(int intData, int intRotation)
        {
            HugeMushroomType hmtCurrent = (HugeMushroomType)intData;
            switch (hmtCurrent)
            {
                case HugeMushroomType.STEM:
                case HugeMushroomType.FLESHY:
                case HugeMushroomType.CAP_TOP:
                    return hmtCurrent;
            }
            switch (intRotation)
            {
                case 1:
                    switch (hmtCurrent)
                    {
                        case HugeMushroomType.CAP_CORNER_NORTHWEST: return HugeMushroomType.CAP_CORNER_SOUTHWEST;
                        case HugeMushroomType.CAP_CORNER_NORTHEAST: return HugeMushroomType.CAP_CORNER_NORTHWEST;
                        case HugeMushroomType.CAP_CORNER_SOUTHEAST: return HugeMushroomType.CAP_CORNER_NORTHEAST;
                        case HugeMushroomType.CAP_CORNER_SOUTHWEST: return HugeMushroomType.CAP_CORNER_SOUTHEAST;
                        case HugeMushroomType.CAP_SIDE_NORTH: return HugeMushroomType.CAP_SIDE_WEST;
                        case HugeMushroomType.CAP_SIDE_EAST: return HugeMushroomType.CAP_SIDE_NORTH;
                        case HugeMushroomType.CAP_SIDE_SOUTH: return HugeMushroomType.CAP_SIDE_EAST;
                        case HugeMushroomType.CAP_SIDE_WEST: return HugeMushroomType.CAP_SIDE_SOUTH;
                        default: Debug.Fail("Invalid switch result"); return hmtCurrent;
                    }
                case 2:
                    switch (hmtCurrent)
                    {
                        case HugeMushroomType.CAP_CORNER_NORTHWEST: return HugeMushroomType.CAP_CORNER_NORTHEAST;
                        case HugeMushroomType.CAP_CORNER_NORTHEAST: return HugeMushroomType.CAP_CORNER_SOUTHEAST;
                        case HugeMushroomType.CAP_CORNER_SOUTHEAST: return HugeMushroomType.CAP_CORNER_SOUTHWEST;
                        case HugeMushroomType.CAP_CORNER_SOUTHWEST: return HugeMushroomType.CAP_CORNER_NORTHWEST;
                        case HugeMushroomType.CAP_SIDE_NORTH: return HugeMushroomType.CAP_SIDE_EAST;
                        case HugeMushroomType.CAP_SIDE_EAST: return HugeMushroomType.CAP_SIDE_SOUTH;
                        case HugeMushroomType.CAP_SIDE_SOUTH: return HugeMushroomType.CAP_SIDE_WEST;
                        case HugeMushroomType.CAP_SIDE_WEST: return HugeMushroomType.CAP_SIDE_NORTH;
                        default: Debug.Fail("Invalid switch result"); return hmtCurrent;
                    }
                case 3:
                    switch (hmtCurrent)
                    {
                        case HugeMushroomType.CAP_CORNER_NORTHWEST: return HugeMushroomType.CAP_CORNER_SOUTHEAST;
                        case HugeMushroomType.CAP_CORNER_NORTHEAST: return HugeMushroomType.CAP_CORNER_SOUTHWEST;
                        case HugeMushroomType.CAP_CORNER_SOUTHEAST: return HugeMushroomType.CAP_CORNER_NORTHWEST;
                        case HugeMushroomType.CAP_CORNER_SOUTHWEST: return HugeMushroomType.CAP_CORNER_NORTHEAST;
                        case HugeMushroomType.CAP_SIDE_NORTH: return HugeMushroomType.CAP_SIDE_SOUTH;
                        case HugeMushroomType.CAP_SIDE_EAST: return HugeMushroomType.CAP_SIDE_WEST;
                        case HugeMushroomType.CAP_SIDE_SOUTH: return HugeMushroomType.CAP_SIDE_NORTH;
                        case HugeMushroomType.CAP_SIDE_WEST: return HugeMushroomType.CAP_SIDE_EAST;
                        default: Debug.Fail("Invalid switch result"); return hmtCurrent;
                    }
                default:
                    return hmtCurrent;
            }
        }
        public static TorchOrientation RotateTorch(int intData, int intRotation)
        {
            TorchOrientation toCurrent = (TorchOrientation)intData;
            switch (intRotation)
            {
                case 1:
                    switch (toCurrent)
                    {
                        case TorchOrientation.FLOOR: return TorchOrientation.FLOOR;
                        case TorchOrientation.NORTH: return TorchOrientation.WEST;
                        case TorchOrientation.EAST: return TorchOrientation.NORTH;
                        case TorchOrientation.SOUTH: return TorchOrientation.EAST;
                        case TorchOrientation.WEST: return TorchOrientation.SOUTH;
                        default: Debug.Fail("Invalid switch result"); return toCurrent;
                    }
                case 2:
                    switch (toCurrent)
                    {
                        case TorchOrientation.FLOOR: return TorchOrientation.FLOOR;
                        case TorchOrientation.NORTH: return TorchOrientation.EAST;
                        case TorchOrientation.EAST: return TorchOrientation.SOUTH;
                        case TorchOrientation.SOUTH: return TorchOrientation.WEST;
                        case TorchOrientation.WEST: return TorchOrientation.NORTH;
                        default: Debug.Fail("Invalid switch result"); return toCurrent;
                    }
                case 3:
                    switch (toCurrent)
                    {
                        case TorchOrientation.FLOOR: return TorchOrientation.FLOOR;
                        case TorchOrientation.NORTH: return TorchOrientation.SOUTH;
                        case TorchOrientation.EAST: return TorchOrientation.WEST;
                        case TorchOrientation.SOUTH: return TorchOrientation.NORTH;
                        case TorchOrientation.WEST: return TorchOrientation.EAST;
                        default: Debug.Fail("Invalid switch result"); return toCurrent;
                    }
                default:
                    return toCurrent;
            }
        }
        public static int RotateLever(int intData, int intRotation)
        {
            if (intRotation > 0)
            {
                bool booPowered = false;
                if (intData >= (int)LeverState.POWERED)
                {
                    intData -= (int)LeverState.POWERED;
                    booPowered = true;
                }
                int intDirection = 0;
                switch (intRotation)
                {
                    case 1:
                        switch (intData)
                        {
                            case (int)LeverOrientation.NORTH: intDirection = (int)LeverOrientation.WEST; break;
                            case (int)LeverOrientation.EAST: intDirection = (int)LeverOrientation.NORTH; break;
                            case (int)LeverOrientation.SOUTH: intDirection = (int)LeverOrientation.EAST; break;
                            case (int)LeverOrientation.WEST: intDirection = (int)LeverOrientation.SOUTH; break;
                            case (int)LeverOrientation.GROUND_EASTWEST: intDirection = (int)LeverOrientation.GROUND_NORTHSOUTH; break;
                            case (int)LeverOrientation.GROUND_NORTHSOUTH: intDirection = (int)LeverOrientation.GROUND_EASTWEST; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 2:
                        switch (intData)
                        {
                            case (int)LeverOrientation.NORTH: intDirection = (int)LeverOrientation.EAST; break;
                            case (int)LeverOrientation.EAST: intDirection = (int)LeverOrientation.SOUTH; break;
                            case (int)LeverOrientation.SOUTH: intDirection = (int)LeverOrientation.WEST; break;
                            case (int)LeverOrientation.WEST: intDirection = (int)LeverOrientation.NORTH; break;
                            case (int)LeverOrientation.GROUND_EASTWEST: intDirection = (int)LeverOrientation.GROUND_NORTHSOUTH; break;
                            case (int)LeverOrientation.GROUND_NORTHSOUTH: intDirection = (int)LeverOrientation.GROUND_EASTWEST; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 3:
                        switch (intData)
                        {
                            case (int)LeverOrientation.NORTH: intDirection = (int)LeverOrientation.SOUTH; break;
                            case (int)LeverOrientation.EAST: intDirection = (int)LeverOrientation.WEST; break;
                            case (int)LeverOrientation.SOUTH: intDirection = (int)LeverOrientation.NORTH; break;
                            case (int)LeverOrientation.WEST: intDirection = (int)LeverOrientation.EAST; break;
                            case (int)LeverOrientation.GROUND_EASTWEST: intDirection = (int)LeverOrientation.GROUND_EASTWEST; break;
                            case (int)LeverOrientation.GROUND_NORTHSOUTH: intDirection = (int)LeverOrientation.GROUND_NORTHSOUTH; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    default:
                        Debug.WriteLine("Oh noes!");
                        break;
                }
                if (booPowered)
                {
                    intDirection += (int)LeverState.POWERED;
                }
                return intDirection;
            }
            else
            {
                return intData;
            }
        }
        public static WallSignOrientation RotateWallSignOrLadderOrFurnanceOrDispenser(int intData, int intRotation)
        {
            WallSignOrientation wsoCurrent = (WallSignOrientation)intData;
            switch (intRotation)
            {
                case 1:
                    switch (wsoCurrent)
                    {
                        case WallSignOrientation.NORTH: return WallSignOrientation.WEST;
                        case WallSignOrientation.EAST: return WallSignOrientation.NORTH;
                        case WallSignOrientation.SOUTH: return WallSignOrientation.EAST;
                        case WallSignOrientation.WEST: return WallSignOrientation.SOUTH;
                        default: Debug.Fail("Invalid switch result"); return wsoCurrent;
                    }
                case 2:
                    switch (wsoCurrent)
                    {
                        case WallSignOrientation.NORTH: return WallSignOrientation.EAST;
                        case WallSignOrientation.EAST: return WallSignOrientation.SOUTH;
                        case WallSignOrientation.SOUTH: return WallSignOrientation.WEST;
                        case WallSignOrientation.WEST: return WallSignOrientation.NORTH;
                        default: Debug.Fail("Invalid switch result"); return wsoCurrent;
                    }
                case 3:
                    switch (wsoCurrent)
                    {
                        case WallSignOrientation.NORTH: return WallSignOrientation.SOUTH;
                        case WallSignOrientation.EAST: return WallSignOrientation.WEST;
                        case WallSignOrientation.SOUTH: return WallSignOrientation.NORTH;
                        case WallSignOrientation.WEST: return WallSignOrientation.EAST;
                        default: Debug.Fail("Invalid switch result"); return wsoCurrent;
                    }
                default:
                    return wsoCurrent;
            }
        }
        public static int RotateStairs(int intData, int intRotation)
        {
            if (intRotation > 0)
            {
                bool booInverted = false;
                if (intData >= (int)StairInversion.Inverted)
                {
                    intData -= (int)StairInversion.Inverted;
                    booInverted = true;
                }
                int intDirection = 0;
                switch (intRotation)
                {
                    case 1:
                        switch (intData)
                        {
                            case (int)StairOrientation.ASCEND_NORTH: intDirection = (int)StairOrientation.ASCEND_WEST; break;
                            case (int)StairOrientation.ASCEND_EAST: intDirection = (int)StairOrientation.ASCEND_NORTH; break;
                            case (int)StairOrientation.ASCEND_SOUTH: intDirection = (int)StairOrientation.ASCEND_EAST; break;
                            case (int)StairOrientation.ASCEND_WEST: intDirection = (int)StairOrientation.ASCEND_SOUTH; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 2:
                        switch (intData)
                        {
                            case (int)StairOrientation.ASCEND_NORTH: intDirection = (int)StairOrientation.ASCEND_EAST; break;
                            case (int)StairOrientation.ASCEND_EAST: intDirection = (int)StairOrientation.ASCEND_SOUTH; break;
                            case (int)StairOrientation.ASCEND_SOUTH: intDirection = (int)StairOrientation.ASCEND_WEST; break;
                            case (int)StairOrientation.ASCEND_WEST: intDirection = (int)StairOrientation.ASCEND_NORTH; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 3:
                        switch (intData)
                        {
                            case (int)StairOrientation.ASCEND_NORTH: intDirection = (int)StairOrientation.ASCEND_SOUTH; break;
                            case (int)StairOrientation.ASCEND_EAST: intDirection = (int)StairOrientation.ASCEND_WEST; break;
                            case (int)StairOrientation.ASCEND_SOUTH: intDirection = (int)StairOrientation.ASCEND_NORTH; break;
                            case (int)StairOrientation.ASCEND_WEST: intDirection = (int)StairOrientation.ASCEND_EAST; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    default:
                        Debug.WriteLine("Oh noes!");
                        break;
                }
                if (booInverted)
                {
                    intDirection += (int)StairInversion.Inverted;
                }
                return intDirection;
            }
            else
            {
                return intData;
            }
        }
        public static int RotateRedStoneRepeater(int intData, int intRotation)
        {
            RepeaterDelay rdCurrent = RepeaterDelay.DELAY_1;
            if ((intData & (int)RepeaterDelay.DELAY_4) == (int)RepeaterDelay.DELAY_4)
            {
                rdCurrent = RepeaterDelay.DELAY_4;
            }
            else if ((intData & (int)RepeaterDelay.DELAY_3) == (int)RepeaterDelay.DELAY_3)
            {
                rdCurrent = RepeaterDelay.DELAY_3;
            }
            else if ((intData & (int)RepeaterDelay.DELAY_2) == (int)RepeaterDelay.DELAY_2)
            {
                rdCurrent = RepeaterDelay.DELAY_2;
            }
            RepeaterOrientation roCurrent = (RepeaterOrientation)((intData -= (int)rdCurrent) % 4);
            switch (intRotation)
            {
                case 1:
                    switch (roCurrent)
                    {
                        case RepeaterOrientation.NORTH: roCurrent = RepeaterOrientation.WEST; break;
                        case RepeaterOrientation.EAST: roCurrent = RepeaterOrientation.NORTH; break;
                        case RepeaterOrientation.SOUTH: roCurrent = RepeaterOrientation.EAST; break;
                        case RepeaterOrientation.WEST: roCurrent = RepeaterOrientation.SOUTH; break;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    break;
                case 2:
                    switch (roCurrent)
                    {
                        case RepeaterOrientation.NORTH: roCurrent = RepeaterOrientation.EAST; break;
                        case RepeaterOrientation.EAST: roCurrent = RepeaterOrientation.SOUTH; break;
                        case RepeaterOrientation.SOUTH: roCurrent = RepeaterOrientation.WEST; break;
                        case RepeaterOrientation.WEST: roCurrent = RepeaterOrientation.NORTH; break;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    break;
                case 3:
                    switch (roCurrent)
                    {
                        case RepeaterOrientation.NORTH: roCurrent = RepeaterOrientation.SOUTH; break;
                        case RepeaterOrientation.EAST: roCurrent = RepeaterOrientation.WEST; break;
                        case RepeaterOrientation.SOUTH: roCurrent = RepeaterOrientation.NORTH; break;
                        case RepeaterOrientation.WEST: roCurrent = RepeaterOrientation.EAST; break;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    break;
                default:
                    Debug.WriteLine("Oh noes!");
                    break;
            }
            return (int)roCurrent + (int)rdCurrent;
        }
        public static int RotateFenceGate(int intData, int intRotation)
        {
            if (intRotation > 0)
            {
                bool booOpen = false;
                if (intData >= (int)FenceGateState.OPEN)
                {
                    intData -= (int)FenceGateState.OPEN;
                    booOpen = true;
                }
                int intDirection = 0;
                switch (intRotation)
                {
                    case 1:
                        switch (intData)
                        {
                            case (int)FenceGateOrientation.NORTH: intDirection = (int)FenceGateOrientation.WEST; break;
                            case (int)FenceGateOrientation.EAST: intDirection = (int)FenceGateOrientation.NORTH; break;
                            case (int)FenceGateOrientation.SOUTH: intDirection = (int)FenceGateOrientation.EAST; break;
                            case (int)FenceGateOrientation.WEST: intDirection = (int)FenceGateOrientation.SOUTH; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 2:
                        switch (intData)
                        {
                            case (int)FenceGateOrientation.NORTH: intDirection = (int)FenceGateOrientation.EAST; break;
                            case (int)FenceGateOrientation.EAST: intDirection = (int)FenceGateOrientation.SOUTH; break;
                            case (int)FenceGateOrientation.SOUTH: intDirection = (int)FenceGateOrientation.WEST; break;
                            case (int)FenceGateOrientation.WEST: intDirection = (int)FenceGateOrientation.NORTH; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 3:
                        switch (intData)
                        {
                            case (int)FenceGateOrientation.NORTH: intDirection = (int)FenceGateOrientation.SOUTH; break;
                            case (int)FenceGateOrientation.EAST: intDirection = (int)FenceGateOrientation.WEST; break;
                            case (int)FenceGateOrientation.SOUTH: intDirection = (int)FenceGateOrientation.NORTH; break;
                            case (int)FenceGateOrientation.WEST: intDirection = (int)FenceGateOrientation.EAST; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    default:
                        Debug.WriteLine("Oh noes!");
                        break;
                }
                if (booOpen)
                {
                    intDirection += (int)FenceGateState.OPEN;
                }
                return intDirection;
            }
            else
            {
                return intData;
            }
        }
        public static int RotatePistonBody(int intData, int intRotation)
        {
            if (intRotation > 0)
            {
                bool booExtended = false;
                if (intData >= (int)PistonBodyState.EXTENDED)
                {
                    intData -= (int)PistonBodyState.EXTENDED;
                    booExtended = true;
                }
                int intDirection = 0;
                switch (intRotation)
                {
                    case 1:
                        switch (intData)
                        {
                            case (int)PistonOrientation.NORTH: intDirection = (int)PistonOrientation.WEST; break;
                            case (int)PistonOrientation.EAST: intDirection = (int)PistonOrientation.NORTH; break;
                            case (int)PistonOrientation.SOUTH: intDirection = (int)PistonOrientation.EAST; break;
                            case (int)PistonOrientation.WEST: intDirection = (int)PistonOrientation.SOUTH; break;
                            case (int)PistonOrientation.UP: intDirection = (int)PistonOrientation.UP; break;
                            case (int)PistonOrientation.DOWN: intDirection = (int)PistonOrientation.DOWN; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 2:
                        switch (intData)
                        {
                            case (int)PistonOrientation.NORTH: intDirection = (int)PistonOrientation.EAST; break;
                            case (int)PistonOrientation.EAST: intDirection = (int)PistonOrientation.SOUTH; break;
                            case (int)PistonOrientation.SOUTH: intDirection = (int)PistonOrientation.WEST; break;
                            case (int)PistonOrientation.WEST: intDirection = (int)PistonOrientation.NORTH; break;
                            case (int)PistonOrientation.UP: intDirection = (int)PistonOrientation.UP; break;
                            case (int)PistonOrientation.DOWN: intDirection = (int)PistonOrientation.DOWN; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 3:
                        switch (intData)
                        {
                            case (int)PistonOrientation.NORTH: intDirection = (int)PistonOrientation.SOUTH; break;
                            case (int)PistonOrientation.EAST: intDirection = (int)PistonOrientation.WEST; break;
                            case (int)PistonOrientation.SOUTH: intDirection = (int)PistonOrientation.NORTH; break;
                            case (int)PistonOrientation.WEST: intDirection = (int)PistonOrientation.EAST; break;
                            case (int)PistonOrientation.UP: intDirection = (int)PistonOrientation.UP; break;
                            case (int)PistonOrientation.DOWN: intDirection = (int)PistonOrientation.DOWN; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    default:
                        Debug.WriteLine("Oh noes!");
                        break;
                }
                if (booExtended)
                {
                    intDirection += (int)PistonBodyState.EXTENDED;
                }
                return intDirection;
            }
            else
            {
                return intData;
            }
        }
        public static int RotatePistonExtension(int intData, int intRotation)
        {
            if (intRotation > 0)
            {
                bool booSticky = false;
                if (intData >= (int)PistonHeadState.STICKY)
                {
                    intData -= (int)PistonHeadState.STICKY;
                    booSticky = true;
                }
                int intDirection = 0;
                switch (intRotation)
                {
                    case 1:
                        switch (intData)
                        {
                            case (int)PistonOrientation.NORTH: intDirection = (int)PistonOrientation.WEST; break;
                            case (int)PistonOrientation.EAST: intDirection = (int)PistonOrientation.NORTH; break;
                            case (int)PistonOrientation.SOUTH: intDirection = (int)PistonOrientation.EAST; break;
                            case (int)PistonOrientation.WEST: intDirection = (int)PistonOrientation.SOUTH; break;
                            case (int)PistonOrientation.UP: intDirection = (int)PistonOrientation.UP; break;
                            case (int)PistonOrientation.DOWN: intDirection = (int)PistonOrientation.DOWN; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 2:
                        switch (intData)
                        {
                            case (int)PistonOrientation.NORTH: intDirection = (int)PistonOrientation.EAST; break;
                            case (int)PistonOrientation.EAST: intDirection = (int)PistonOrientation.SOUTH; break;
                            case (int)PistonOrientation.SOUTH: intDirection = (int)PistonOrientation.WEST; break;
                            case (int)PistonOrientation.WEST: intDirection = (int)PistonOrientation.NORTH; break;
                            case (int)PistonOrientation.UP: intDirection = (int)PistonOrientation.UP; break;
                            case (int)PistonOrientation.DOWN: intDirection = (int)PistonOrientation.DOWN; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 3:
                        switch (intData)
                        {
                            case (int)PistonOrientation.NORTH: intDirection = (int)PistonOrientation.SOUTH; break;
                            case (int)PistonOrientation.EAST: intDirection = (int)PistonOrientation.WEST; break;
                            case (int)PistonOrientation.SOUTH: intDirection = (int)PistonOrientation.NORTH; break;
                            case (int)PistonOrientation.WEST: intDirection = (int)PistonOrientation.EAST; break;
                            case (int)PistonOrientation.UP: intDirection = (int)PistonOrientation.UP; break;
                            case (int)PistonOrientation.DOWN: intDirection = (int)PistonOrientation.DOWN; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    default:
                        Debug.WriteLine("Oh noes!");
                        break;
                }
                if (booSticky)
                {
                    intDirection += (int)PistonHeadState.STICKY;
                }
                return intDirection;
            }
            else
            {
                return intData;
            }
        }
        public static int RotateBed(int intData, int intRotation)
        {
            if (intRotation > 0)
            {
                bool booHead = false;
                if (intData >= (int)BedState.HEAD)
                {
                    intData -= (int)BedState.HEAD;
                    booHead = true;
                }
                int intDirection = 0;
                switch (intRotation)
                {
                    case 1:
                        switch (intData)
                        {
                            case (int)BedOrientation.NORTH: intDirection = (int)BedOrientation.WEST; break;
                            case (int)BedOrientation.EAST: intDirection = (int)BedOrientation.NORTH; break;
                            case (int)BedOrientation.SOUTH: intDirection = (int)BedOrientation.EAST; break;
                            case (int)BedOrientation.WEST: intDirection = (int)BedOrientation.SOUTH; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 2:
                        switch (intData)
                        {
                            case (int)BedOrientation.NORTH: intDirection = (int)BedOrientation.EAST; break;
                            case (int)BedOrientation.EAST: intDirection = (int)BedOrientation.SOUTH; break;
                            case (int)BedOrientation.SOUTH: intDirection = (int)BedOrientation.WEST; break;
                            case (int)BedOrientation.WEST: intDirection = (int)BedOrientation.NORTH; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 3:
                        switch (intData)
                        {
                            case (int)BedOrientation.NORTH: intDirection = (int)BedOrientation.SOUTH; break;
                            case (int)BedOrientation.EAST: intDirection = (int)BedOrientation.WEST; break;
                            case (int)BedOrientation.SOUTH: intDirection = (int)BedOrientation.NORTH; break;
                            case (int)BedOrientation.WEST: intDirection = (int)BedOrientation.EAST; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    default:
                        Debug.WriteLine("Oh noes!");
                        break;
                }
                if (booHead)
                {
                    intDirection += (int)BedState.HEAD;
                }
                return intDirection;
            }
            else
            {
                return intData;
            }
        }
        public static int RotateSignPost(int intData, int intRotation)
        {
            switch (intRotation)
            {
                case 1:
                    intData += 12;
                    break;
                case 2:
                    intData += 4;
                    break;
                case 3:
                    intData += 8;
                    break;
                default:
                    Debug.WriteLine("Oh noes!");
                    break;
            }
            return intData % 16;
        }
        public static int RotateButton(int intData, int intRotation)
        {
            if (intRotation > 0)
            {
                bool booPushed = false;
                if (intData >= (int)ButtonState.PRESSED)
                {
                    intData -= (int)ButtonState.PRESSED;
                    booPushed = true;
                }
                int intDirection = 0;
                switch (intRotation)
                {
                    case 1:
                        switch (intData)
                        {
                            case (int)ButtonOrientation.NORTH: intDirection = (int)ButtonOrientation.EAST; break;
                            case (int)ButtonOrientation.EAST: intDirection = (int)ButtonOrientation.SOUTH; break;
                            case (int)ButtonOrientation.SOUTH: intDirection = (int)ButtonOrientation.WEST; break;
                            case (int)ButtonOrientation.WEST: intDirection = (int)ButtonOrientation.NORTH; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 2:
                        switch (intData)
                        {
                            case (int)ButtonOrientation.NORTH: intDirection = (int)ButtonOrientation.WEST; break;
                            case (int)ButtonOrientation.EAST: intDirection = (int)ButtonOrientation.NORTH; break;
                            case (int)ButtonOrientation.SOUTH: intDirection = (int)ButtonOrientation.EAST; break;
                            case (int)ButtonOrientation.WEST: intDirection = (int)ButtonOrientation.SOUTH; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 3:
                        switch (intData)
                        {
                            case (int)ButtonOrientation.NORTH: intDirection = (int)ButtonOrientation.SOUTH; break;
                            case (int)ButtonOrientation.EAST: intDirection = (int)ButtonOrientation.WEST; break;
                            case (int)ButtonOrientation.SOUTH: intDirection = (int)ButtonOrientation.NORTH; break;
                            case (int)ButtonOrientation.WEST: intDirection = (int)ButtonOrientation.EAST; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    default:
                        Debug.WriteLine("Oh noes!");
                        break;
                }
                if (booPushed)
                {
                    intDirection += (int)ButtonState.PRESSED;
                }
                return intDirection;
            }
            else
            {
                return intData;
            }
        }
        public static int RotateTrapdoor(int intData, int intRotation)
        {
            if (intRotation > 0)
            {
                bool booOpen = false;
                if (intData >= (int)TrapdoorState.SWUNG)
                {
                    intData -= (int)TrapdoorState.SWUNG;
                    booOpen = true;
                }
                int intDirection = 0;
                switch (intRotation)
                {
                    case 1:
                        switch (intData)
                        {
                            case (int)TrapdoorOrientation.NORTH: intDirection = (int)TrapdoorOrientation.WEST; break;
                            case (int)TrapdoorOrientation.EAST: intDirection = (int)TrapdoorOrientation.NORTH; break;
                            case (int)TrapdoorOrientation.SOUTH: intDirection = (int)TrapdoorOrientation.EAST; break;
                            case (int)TrapdoorOrientation.WEST: intDirection = (int)TrapdoorOrientation.SOUTH; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 2:
                        switch (intData)
                        {
                            case (int)TrapdoorOrientation.NORTH: intDirection = (int)TrapdoorOrientation.EAST; break;
                            case (int)TrapdoorOrientation.EAST: intDirection = (int)TrapdoorOrientation.SOUTH; break;
                            case (int)TrapdoorOrientation.SOUTH: intDirection = (int)TrapdoorOrientation.WEST; break;
                            case (int)TrapdoorOrientation.WEST: intDirection = (int)TrapdoorOrientation.NORTH; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    case 3:
                        switch (intData)
                        {
                            case (int)TrapdoorOrientation.NORTH: intDirection = (int)TrapdoorOrientation.SOUTH; break;
                            case (int)TrapdoorOrientation.EAST: intDirection = (int)TrapdoorOrientation.WEST; break;
                            case (int)TrapdoorOrientation.SOUTH: intDirection = (int)TrapdoorOrientation.NORTH; break;
                            case (int)TrapdoorOrientation.WEST: intDirection = (int)TrapdoorOrientation.EAST; break;
                            default: Debug.Fail("Invalid switch result"); break;
                        }
                        break;
                    default:
                        Debug.WriteLine("Oh noes!");
                        break;
                }
                if (booOpen)
                {
                    intDirection += (int)TrapdoorState.SWUNG;
                }
                return intDirection;
            }
            else
            {
                return intData;
            }
        }
        public static EntityPainting.DirectionType RotatePortrait(EntityPainting.DirectionType dtData, int intRotation)
        {
            switch (intRotation)
            {
                case 1:
                    switch (dtData)
                    {
                        case EntityPainting.DirectionType.NORTH: return EntityPainting.DirectionType.WEST;
                        case EntityPainting.DirectionType.EAST: return EntityPainting.DirectionType.NORTH;
                        case EntityPainting.DirectionType.SOUTH: return EntityPainting.DirectionType.EAST;
                        case EntityPainting.DirectionType.WEST: return EntityPainting.DirectionType.SOUTH;
                        default: Debug.Fail("Painting rotation"); break;
                    }
                    return dtData;
                case 2:
                    switch (dtData)
                    {
                        case EntityPainting.DirectionType.NORTH: return EntityPainting.DirectionType.EAST;
                        case EntityPainting.DirectionType.EAST: return EntityPainting.DirectionType.SOUTH;
                        case EntityPainting.DirectionType.SOUTH: return EntityPainting.DirectionType.WEST;
                        case EntityPainting.DirectionType.WEST: return EntityPainting.DirectionType.NORTH;
                        default: Debug.Fail("Painting rotation"); break;
                    }
                    return dtData;
                case 3:
                    switch (dtData)
                    {
                        case EntityPainting.DirectionType.NORTH: return EntityPainting.DirectionType.SOUTH;
                        case EntityPainting.DirectionType.EAST: return EntityPainting.DirectionType.WEST;
                        case EntityPainting.DirectionType.SOUTH: return EntityPainting.DirectionType.NORTH;
                        case EntityPainting.DirectionType.WEST: return EntityPainting.DirectionType.EAST;
                        default: Debug.Fail("Painting rotation"); break;
                    }
                    return dtData;
                default:
                    //Debug.Fail("Painting rotated to default");
                    return dtData;
            }
        }
        public static PumpkinOrientation RotatePumpkin(int intData, int intRotation)
        {
            PumpkinOrientation poCurrent = (PumpkinOrientation)intData;
            switch (intRotation)
            {
                case 1:
                    switch (poCurrent)
                    {
                        case PumpkinOrientation.NORTH: return PumpkinOrientation.WEST;
                        case PumpkinOrientation.EAST: return PumpkinOrientation.NORTH;
                        case PumpkinOrientation.SOUTH: return PumpkinOrientation.EAST;
                        case PumpkinOrientation.WEST: return PumpkinOrientation.SOUTH;
                        default: Debug.Fail("Invalid switch result"); return poCurrent;
                    }
                case 2:
                    switch (poCurrent)
                    {
                        case PumpkinOrientation.NORTH: return PumpkinOrientation.EAST;
                        case PumpkinOrientation.EAST: return PumpkinOrientation.SOUTH;
                        case PumpkinOrientation.SOUTH: return PumpkinOrientation.WEST;
                        case PumpkinOrientation.WEST: return PumpkinOrientation.NORTH;
                        default: Debug.Fail("Invalid switch result"); return poCurrent;
                    }
                case 3:
                    switch (poCurrent)
                    {
                        case PumpkinOrientation.NORTH: return PumpkinOrientation.SOUTH;
                        case PumpkinOrientation.EAST: return PumpkinOrientation.WEST;
                        case PumpkinOrientation.SOUTH: return PumpkinOrientation.NORTH;
                        case PumpkinOrientation.WEST: return PumpkinOrientation.EAST;
                        default: Debug.Fail("Invalid switch result"); return poCurrent;
                    }
                default:
                    return poCurrent;
            }
        }
        public static VineCoverageState RotateVines(int intData, int intRotation)
        {
            VineCoverageState vcsCurrent = (VineCoverageState)intData;
            switch (intRotation)
            {
                case 1:
                    switch (vcsCurrent)
                    {
                        case VineCoverageState.NORTH: return VineCoverageState.WEST;
                        case VineCoverageState.EAST: return VineCoverageState.NORTH;
                        case VineCoverageState.SOUTH: return VineCoverageState.EAST;
                        case VineCoverageState.WEST: return VineCoverageState.SOUTH;
                        case VineCoverageState.TOP: return VineCoverageState.TOP;
                        default: Debug.Fail("Invalid switch result"); return vcsCurrent;
                    }
                case 2:
                    switch (vcsCurrent)
                    {
                        case VineCoverageState.NORTH: return VineCoverageState.EAST;
                        case VineCoverageState.EAST: return VineCoverageState.SOUTH;
                        case VineCoverageState.SOUTH: return VineCoverageState.WEST;
                        case VineCoverageState.WEST: return VineCoverageState.NORTH;
                        case VineCoverageState.TOP: return VineCoverageState.TOP;
                        default: Debug.Fail("Invalid switch result"); return vcsCurrent;
                    }
                case 3:
                    switch (vcsCurrent)
                    {
                        case VineCoverageState.NORTH: return VineCoverageState.SOUTH;
                        case VineCoverageState.EAST: return VineCoverageState.WEST;
                        case VineCoverageState.SOUTH: return VineCoverageState.NORTH;
                        case VineCoverageState.WEST: return VineCoverageState.EAST;
                        case VineCoverageState.TOP: return VineCoverageState.TOP;
                        default: Debug.Fail("Invalid switch result"); return vcsCurrent;
                    }
                default:
                    return vcsCurrent;
            }
        }
        public static int RotateDoor(int intData, int intRotation)
        {
            if (intRotation > 0)
            {
                bool booSwung = false;
                if (intData >= (int)DoorState.TOPHALF)
                {
                    return intData;
                }
                else
                {
                    if (intData >= (int)DoorState.SWUNG)
                    {
                        intData -= (int)DoorState.SWUNG;
                        booSwung = true;
                    }
                    int intDirection = 0;
                    switch (intRotation)
                    {
                        case 1:
                            switch (intData)
                            {
                                case (int)DoorHinge.NORTHWEST: intDirection = (int)DoorHinge.SOUTHWEST; break;
                                case (int)DoorHinge.NORTHEAST: intDirection = (int)DoorHinge.NORTHWEST; break;
                                case (int)DoorHinge.SOUTHEAST: intDirection = (int)DoorHinge.NORTHEAST; break;
                                case (int)DoorHinge.SOUTHWEST: intDirection = (int)DoorHinge.SOUTHEAST; break;
                                default: Debug.Fail("Invalid switch result"); break;
                            }
                            break;
                        case 2:
                            switch (intData)
                            {
                                case (int)DoorHinge.NORTHWEST: intDirection = (int)DoorHinge.NORTHEAST; break;
                                case (int)DoorHinge.NORTHEAST: intDirection = (int)DoorHinge.SOUTHEAST; break;
                                case (int)DoorHinge.SOUTHEAST: intDirection = (int)DoorHinge.SOUTHWEST; break;
                                case (int)DoorHinge.SOUTHWEST: intDirection = (int)DoorHinge.NORTHWEST; break;
                                default: Debug.Fail("Invalid switch result"); break;
                            }
                            break;
                        case 3:
                            switch (intData)
                            {
                                case (int)DoorHinge.NORTHWEST: intDirection = (int)DoorHinge.SOUTHEAST; break;
                                case (int)DoorHinge.NORTHEAST: intDirection = (int)DoorHinge.SOUTHWEST; break;
                                case (int)DoorHinge.SOUTHEAST: intDirection = (int)DoorHinge.NORTHWEST; break;
                                case (int)DoorHinge.SOUTHWEST: intDirection = (int)DoorHinge.NORTHEAST; break;
                                default: Debug.Fail("Invalid switch result"); break;
                            }
                            break;
                        default:
                            Debug.WriteLine("Oh noes!");
                            break;
                    }
                    if (booSwung)
                    {
                        intDirection += (int)DoorState.SWUNG;
                    }
                    return intDirection;
                }
            }
            else
            {
                return intData;
            }
        }
    }
}
