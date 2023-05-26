using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Rooms;

public struct RoomOptions
{
    public static Dictionary<int, Rectangle> blockOptions = new Dictionary<int, Rectangle>()
    {
        // FloorTile
        { 0, new Rectangle(463, 0, 16, 16) },
        // SpecialTile
        { 1, new Rectangle(480, 0, 16, 16) },
        // StatieRight
        { 2, new Rectangle(497, 0, 16, 16) },
        // StatueLeft
        { 3, new Rectangle(514, 0, 16, 16) },
        // BlackTile
        { 4, new Rectangle(463, 17, 16, 16) },
        // Sand
        { 5, new Rectangle(480, 17, 16, 16) },
        // Blue
        { 6, new Rectangle(497, 17, 16, 16) },
        // Stairs
        { 7, new Rectangle(514, 17, 16, 16) },
        // GreyBricks
        { 8, new Rectangle(463, 34, 16, 16) },
        // GreyStripes
        { 9, new Rectangle(480, 34, 16, 16) },
        // Water
        { 10, new Rectangle(497, 34, 16, 16) }
    };

    public static Dictionary<int, Rectangle> itemOptions = new Dictionary<int, Rectangle>()
    {
        //bomb
        { 0, new Rectangle(364, 226, 8, 14) },
        //key
        { 1, new Rectangle(364, 255, 8, 16) },
        //RedPotion
        { 2, new Rectangle(394, 285, 8, 16) },
        //BluePotion
        { 3,  new Rectangle(424, 285, 8, 16) },
        //TriforcePiece
        { 4,  new Rectangle(333, 288, 10, 10) },
        //Map
        { 5,  new Rectangle(274, 255, 8, 16) },
        //Compass
        { 6,  new Rectangle(392, 257, 11, 12)},
        //Bow
        { 7,  new Rectangle(424, 255, 8, 16)},
        //OrangeRupee
        { 8,  new Rectangle(244, 225, 8, 16)},
        //BlueRupee
        { 9,  new Rectangle(274, 225, 8, 16)},
        //HeartDrop
        { 10,  new Rectangle(244, 199, 7, 8)},
        //HeartContainer
        { 11,  new Rectangle(301, 196, 13, 13)},
        //Boomerang
        { 12, new Rectangle(334, 256, 8, 14) },
        //Blank Enemy
        {-1, new Rectangle(400,20,1,1)}
    };
}

