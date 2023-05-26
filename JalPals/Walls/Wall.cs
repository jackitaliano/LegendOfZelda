using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JalPals.Walls
{
    public class Wall : IWall
    {
        // Public properties
        public Rectangle collisionRectangle { get; set; }
        public Vector2 velocityVector { get; set; }

        // Private variables
        Texture2D spriteSheet;

        public Wall(Rectangle dest)
        {
            collisionRectangle = dest;
        }


        public GameObjectType getType()
        {
            Console.WriteLine("Get wall type.");
            return GameObjectType.WALL;

        }

        public void ResolveCollision(IGameObject obj, int side)
        {
            // N/A
        }
    }
}

