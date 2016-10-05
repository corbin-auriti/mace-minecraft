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
            string[] strSign = Utils.TextToSign(strSignText);
            tes.Text1 = strSign[0];
            tes.Text2 = strSign[1];
            tes.Text3 = strSign[2];
            tes.Text4 = strSign[3];
            _bmDest.SetTileEntity(x, y, z, tes);
            _bmDest.SetData(x, y, z, BlockDirectionLadderSign(x, y, z, intBlockAgainst));
            if (intMirror > 0)
            {
                MakeSign(City.MapLength - x, y, z, strSignText, intBlockAgainst, 0);
                MakeSign(x, y, City.MapLength - z, strSignText, intBlockAgainst, 0);
                MakeSign(City.MapLength - x, y, City.MapLength - z, strSignText, intBlockAgainst, 0);
                if (intMirror == 2)
                {
                    MakeSign(z, y, x, strSignText, intBlockAgainst, 1);
                }
            }
        }
        public static void MakeSignPost(int x, int y, int z, string strSignText)
        {
            Substrate.TileEntities.TileEntitySign tes = new Substrate.TileEntities.TileEntitySign();
            string[] strRandomSign = Utils.TextToSign(strSignText);
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
                MakeBed(City.MapLength - x1, City.MapLength - x2, y, z1, z2, 0);
                MakeBed(x1, x2, y, City.MapLength - z1, City.MapLength - z2, 0);
                MakeBed(City.MapLength - x1, City.MapLength - x2, y, City.MapLength - z1, City.MapLength - z2, 0);
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
                MakeTorch(City.MapLength - x, y, z, intBlockAgainst, 0);
                MakeTorch(x, y, City.MapLength - z, intBlockAgainst, 0);
                MakeTorch(City.MapLength - x, y, City.MapLength - z, intBlockAgainst, 0);
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
                MakeLever(City.MapLength - x, y, z, intBlockAgainst, 0);
                MakeLever(x, y, City.MapLength - z, intBlockAgainst, 0);
                MakeLever(City.MapLength - x, y, City.MapLength - z, intBlockAgainst, 0);
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
                MakeLadder(City.MapLength - x, y1, y2, z, 0, intBlockAgainst);
                MakeLadder(x, y1, y2, City.MapLength - z, 0, intBlockAgainst);
                MakeLadder(City.MapLength - x, y1, y2, City.MapLength - z, 0, intBlockAgainst);
                if (intMirror == 2)
                {
                    MakeLadder(z, y1, y2, x, 1, intBlockAgainst);
                }
            }
        }
        public static void MakeChest(int x, int y, int z, int intBlockAgainst, TileEntityChest tec, int intMirror)
        {
            BlockShapes.MakeBlock(x, y, z, BlockInfo.Chest.ID, BlockHelper.BlockDirection(x, y, z, intBlockAgainst));
            _bmDest.SetTileEntity(x, y, z, tec);
            if (intMirror > 0)
            {
                MakeChest(City.MapLength - x, y, z, intBlockAgainst, tec, 0);
                MakeChest(x, y, City.MapLength - z, intBlockAgainst, tec, 0);
                MakeChest(City.MapLength - x, y, City.MapLength - z, intBlockAgainst, tec, 0);
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
            BlockShapes.MakeBlock(x, y + 1, z, intBlockType, (int)DoorState.TOPHALF + intDirection);
            BlockShapes.MakeBlock(x, y, z, intBlockType, intDirection);
            if (intMirror > 0)
            {
                MakeDoor(City.MapLength - x, y, z, intBlockAgainst, booIronDoor, 0);
                MakeDoor(x, y, City.MapLength - z, intBlockAgainst, booIronDoor, 0);
                MakeDoor(City.MapLength - x, y, City.MapLength - z, intBlockAgainst, booIronDoor, 0);
                if (intMirror == 2)
                {
                    MakeDoor(z, y, x, intBlockAgainst, booIronDoor, 1);
                }
            }
        }
        public static void MakeRail(int x, int y, int z)
        {
            if (_bmDest.GetID(x, y, z - 1) == BlockInfo.Rails.ID &&
                _bmDest.GetID(x - 1, y, z) == BlockInfo.Rails.ID)
            {
                _bmDest.SetData(x, y, z, 8);
            }
            else if (_bmDest.GetID(x, y, z - 1) == BlockInfo.Rails.ID &&
                     _bmDest.GetID(x + 1, y, z) == BlockInfo.Rails.ID)
            {
                _bmDest.SetData(x, y, z, 9);
            }
            else if (_bmDest.GetID(x, y, z + 1) == BlockInfo.Rails.ID &&
                     _bmDest.GetID(x - 1, y, z) == BlockInfo.Rails.ID)
            {
                _bmDest.SetData(x, y, z, 7);
            }
            else if (_bmDest.GetID(x, y, z + 1) == BlockInfo.Rails.ID &&
                     _bmDest.GetID(x + 1, y, z) == BlockInfo.Rails.ID)
            {
                _bmDest.SetData(x, y, z, 6);
            }
            else if (_bmDest.GetID(x, y, z + 1) == BlockInfo.Rails.ID)
            {
                _bmDest.SetData(x, y, z, 0);
            }
            else if (_bmDest.GetID(x, y, z - 1) == BlockInfo.Rails.ID)
            {
                _bmDest.SetData(x, y, z, 0);
            }
            else if (_bmDest.GetID(x + 1, y, z) == BlockInfo.Rails.ID)
            {
                _bmDest.SetData(x, y, z, 1);
            }
            else if (_bmDest.GetID(x - 1, y, z) == BlockInfo.Rails.ID)
            {
                _bmDest.SetData(x, y, z, 1);
            }
            else
            {
                Debug.WriteLine("Rail fail: " + x + ", " + y + ", " + z);
                //Debug.Fail("rail");
            }
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
        
        public static int RotateTorchOrLever(int intData, int intRotation)
        {
            switch (intRotation)
            {
                case 1:
                    switch (intData)
                    {
                        case (int)TorchOrientation.FLOOR: return (int)TorchOrientation.FLOOR;
                        case (int)TorchOrientation.NORTH: return (int)TorchOrientation.WEST;
                        case (int)TorchOrientation.EAST: return (int)TorchOrientation.NORTH;
                        case (int)TorchOrientation.SOUTH: return (int)TorchOrientation.EAST;
                        case (int)TorchOrientation.WEST: return (int)TorchOrientation.SOUTH;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                case 2:
                    switch (intData)
                    {
                        case (int)TorchOrientation.FLOOR: return (int)TorchOrientation.FLOOR;
                        case (int)TorchOrientation.NORTH: return (int)TorchOrientation.EAST;
                        case (int)TorchOrientation.EAST: return (int)TorchOrientation.SOUTH;
                        case (int)TorchOrientation.SOUTH: return (int)TorchOrientation.WEST;
                        case (int)TorchOrientation.WEST: return (int)TorchOrientation.NORTH;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                case 3:
                    switch (intData)
                    {
                        case (int)TorchOrientation.FLOOR: return (int)TorchOrientation.FLOOR;
                        case (int)TorchOrientation.NORTH: return (int)TorchOrientation.SOUTH;
                        case (int)TorchOrientation.EAST: return (int)TorchOrientation.WEST;
                        case (int)TorchOrientation.SOUTH: return (int)TorchOrientation.NORTH;
                        case (int)TorchOrientation.WEST: return (int)TorchOrientation.EAST;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                default:
                    return intData;
            }
        }
        public static int RotateWallSignOrLadderOrFurnanceOrDispenser(int intData, int intRotation)
        {
            switch (intRotation)
            {
                case 1:
                    switch (intData)
                    {
                        case (int)WallSignOrientation.NORTH: return (int)WallSignOrientation.WEST;
                        case (int)WallSignOrientation.EAST: return (int)WallSignOrientation.NORTH;
                        case (int)WallSignOrientation.SOUTH: return (int)WallSignOrientation.EAST;
                        case (int)WallSignOrientation.WEST: return (int)WallSignOrientation.SOUTH;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                case 2:
                    switch (intData)
                    {
                        case (int)WallSignOrientation.NORTH: return (int)WallSignOrientation.EAST;
                        case (int)WallSignOrientation.EAST: return (int)WallSignOrientation.SOUTH;
                        case (int)WallSignOrientation.SOUTH: return (int)WallSignOrientation.WEST;
                        case (int)WallSignOrientation.WEST: return (int)WallSignOrientation.NORTH;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                case 3:
                    switch (intData)
                    {
                        case (int)WallSignOrientation.NORTH: return (int)WallSignOrientation.SOUTH;
                        case (int)WallSignOrientation.EAST: return (int)WallSignOrientation.WEST;
                        case (int)WallSignOrientation.SOUTH: return (int)WallSignOrientation.NORTH;
                        case (int)WallSignOrientation.WEST: return (int)WallSignOrientation.EAST;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                default:
                    return intData;
            }
        }
        public static int RotatePortal(int intData, int intRotation)
        {
            switch (intRotation)
            {
                case 1: case 3:
                    return 2;
                default:
                    return intData;
            }
        }
        public static int RotateStairs(int intData, int intRotation)
        {
            switch (intRotation)
            {
                case 1:
                    switch (intData)
                    {
                        case (int)StairOrientation.ASCEND_NORTH: return (int)StairOrientation.ASCEND_WEST;
                        case (int)StairOrientation.ASCEND_EAST: return (int)StairOrientation.ASCEND_NORTH;
                        case (int)StairOrientation.ASCEND_SOUTH: return (int)StairOrientation.ASCEND_EAST;
                        case (int)StairOrientation.ASCEND_WEST: return (int)StairOrientation.ASCEND_SOUTH;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                case 2:
                    switch (intData)
                    {
                        case (int)StairOrientation.ASCEND_NORTH: return (int)StairOrientation.ASCEND_EAST;
                        case (int)StairOrientation.ASCEND_EAST: return (int)StairOrientation.ASCEND_SOUTH;
                        case (int)StairOrientation.ASCEND_SOUTH: return (int)StairOrientation.ASCEND_WEST;
                        case (int)StairOrientation.ASCEND_WEST: return (int)StairOrientation.ASCEND_NORTH;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                case 3:
                    switch (intData)
                    {
                        case (int)StairOrientation.ASCEND_NORTH: return (int)StairOrientation.ASCEND_SOUTH;
                        case (int)StairOrientation.ASCEND_EAST: return (int)StairOrientation.ASCEND_WEST;
                        case (int)StairOrientation.ASCEND_SOUTH: return (int)StairOrientation.ASCEND_NORTH;
                        case (int)StairOrientation.ASCEND_WEST: return (int)StairOrientation.ASCEND_EAST;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                default:
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
                            case (int)BedOrientation.EAST: intDirection = (int)BedOrientation.WEST;   break;
                            case (int)BedOrientation.SOUTH: intDirection = (int)BedOrientation.NORTH; break;
                            case (int)BedOrientation.WEST: intDirection = (int)BedOrientation.EAST;   break;
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
                Debug.WriteLine(intRotation);
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
            switch (intRotation)
            {
                case 1:
                    switch (intData)
                    {
                        case (int)TrapdoorOrientation.NORTH: return (int)TrapdoorOrientation.WEST;
                        case (int)TrapdoorOrientation.EAST: return (int)TrapdoorOrientation.NORTH;
                        case (int)TrapdoorOrientation.SOUTH: return (int)TrapdoorOrientation.EAST;
                        case (int)TrapdoorOrientation.WEST: return (int)TrapdoorOrientation.SOUTH;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                case 2:
                    switch (intData)
                    {
                        case (int)TrapdoorOrientation.NORTH: return (int)TrapdoorOrientation.EAST;
                        case (int)TrapdoorOrientation.EAST: return (int)TrapdoorOrientation.SOUTH;
                        case (int)TrapdoorOrientation.SOUTH: return (int)TrapdoorOrientation.WEST;
                        case (int)TrapdoorOrientation.WEST: return (int)TrapdoorOrientation.NORTH;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                case 3:
                    switch (intData)
                    {
                        case (int)TrapdoorOrientation.NORTH: return (int)TrapdoorOrientation.SOUTH;
                        case (int)TrapdoorOrientation.EAST: return (int)TrapdoorOrientation.WEST;
                        case (int)TrapdoorOrientation.SOUTH: return (int)TrapdoorOrientation.NORTH;
                        case (int)TrapdoorOrientation.WEST: return (int)TrapdoorOrientation.EAST;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                default:
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
        public static int RotatePumpkin(int intData, int intRotation)
        {
            switch (intRotation)
            {
                case 1:
                    switch (intData)
                    {
                        case (int)PumpkinOrientation.NORTH: return (int)PumpkinOrientation.WEST;
                        case (int)PumpkinOrientation.EAST: return (int)PumpkinOrientation.NORTH;
                        case (int)PumpkinOrientation.SOUTH: return (int)PumpkinOrientation.EAST;
                        case (int)PumpkinOrientation.WEST: return (int)PumpkinOrientation.SOUTH;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                case 2:
                    switch (intData)
                    {
                        case (int)PumpkinOrientation.NORTH: return (int)PumpkinOrientation.EAST;
                        case (int)PumpkinOrientation.EAST: return (int)PumpkinOrientation.SOUTH;
                        case (int)PumpkinOrientation.SOUTH: return (int)PumpkinOrientation.WEST;
                        case (int)PumpkinOrientation.WEST: return (int)PumpkinOrientation.NORTH;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                case 3:
                    switch (intData)
                    {
                        case (int)PumpkinOrientation.NORTH: return (int)PumpkinOrientation.SOUTH;
                        case (int)PumpkinOrientation.EAST: return (int)PumpkinOrientation.WEST;
                        case (int)PumpkinOrientation.SOUTH: return (int)PumpkinOrientation.NORTH;
                        case (int)PumpkinOrientation.WEST: return (int)PumpkinOrientation.EAST;
                        default: Debug.Fail("Invalid switch result"); break;
                    }
                    return intData;
                default:
                    return intData;
            }
        }
        public static int RotateDoor(int intData, int intRotation)
        {
            if (intRotation > 0)
            {
                bool booSwung = false;
                bool booTopHalf = false;
                if (intData >= (int)DoorState.TOPHALF)
                {
                    intData -= (int)DoorState.TOPHALF;
                    booTopHalf = true;
                }
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
                if (booTopHalf)
                {
                    intDirection += (int)DoorState.TOPHALF;
                }
                if (booSwung)
                {
                    intDirection += (int)DoorState.SWUNG;
                }
                return intDirection;
            }
            else
            {
                return intData;
            }
        }
    }
}
