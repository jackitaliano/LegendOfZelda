using System;
using JalPals.Player;
using JalPals.Rooms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Doors
{
    public class Stairs : IDoor
    {
        // Public properties
        public DoorSide doorSide { get; }
        public Rectangle destRectangle { get; set; }
        public Rectangle collisionRectangle { get; set; }
        public Rectangle srcRectangle { get; set; }
        public Vector2 velocityVector { get; set; }
        public bool collidable { get; set; }
        public bool isOpen { get; set; }

        // Private variables
        Texture2D spriteSheet;
        private IRoom room;

        public Stairs(Rectangle dest, bool isOpen, IRoom room)
        {
            
            this.destRectangle = dest;
            this.isOpen = isOpen;
            this.collisionRectangle = new Rectangle(dest.X + 15, dest.Y + 15, dest.Width - 30, dest.Height - 30);
            this.room = room;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            
        }

        public void Update()
        {
        }


        public GameObjectType getType()
        {
            return GameObjectType.DOOR;

        }

        public void ResolveCollision(IGameObject obj, int side)
        {
            if (obj.getType() == GameObjectType.LINK && isOpen)
            {
                Console.WriteLine("Collide room " + doorSide);
                ILink linkTemp = (ILink)obj;
                float posX = linkTemp.Position.X;
                float posY = linkTemp.Position.Y;
                linkTemp.Position = new Vector2(500, 500);

                room._thisRoomManager.EnterZombiesRoom();
            }
        }

        public void SetDoorOpen()
        {
            //NO-OP
        }
    }
}

