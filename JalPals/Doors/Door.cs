using JalPals.Inventory;
using JalPals.Player;
using JalPals.Rooms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JalPals.Doors
{
    public class Door : IDoor
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
        private int doorType;

        public Door(Texture2D spriteSheet, int c, Rectangle dest, bool isOpen, DoorSide side, IRoom room)
        {

            this.spriteSheet = spriteSheet;
            this.destRectangle = dest;
            this.srcRectangle = new Rectangle(294 + (33 * c), (33 * doorTranslation(side)), 32, 32);
            this.isOpen = isOpen;
            this.doorSide = side;

            if (isOpen) this.collisionRectangle = new Rectangle(dest.Left + 30, dest.Top + 30, dest.Width - 60 , dest.Height - 60);
            else this.collisionRectangle = new Rectangle(dest.Left + 15, dest.Top + 15, dest.Width - 30, dest.Height - 30);

            this.room = room;
            doorType = c;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            int xWithOffset = (int)(destRectangle.X + offset.X);
            int yWithOffset = (int)(destRectangle.Y + offset.Y);
            Rectangle offsetRectangle = new Rectangle(xWithOffset, yWithOffset, destRectangle.Width, destRectangle.Height);
            spriteBatch.Draw(this.spriteSheet, offsetRectangle, srcRectangle, Color.White);
        }

        public void Update()
        {

            if (isOpen) this.collisionRectangle = new Rectangle(destRectangle.Left + 30, destRectangle.Top + 30, destRectangle.Width - 60, destRectangle.Height - 60);
            this.srcRectangle = new Rectangle(294 + (33 * doorType), (33 * doorTranslation(doorSide)), 32, 32);
        }


        public GameObjectType getType()
        {
            return GameObjectType.DOOR;

        }

        public void ResolveCollision(IGameObject obj, int side)
        {
            if (obj.getType() == GameObjectType.LINK)
            {
                LinkDoorCollision((ILink)obj);
            }
            
        }

        private int doorTranslation(DoorSide doorSide)
        {
            if (doorSide == DoorSide.TOP) return 0;
            if (doorSide == DoorSide.BOTTOM) return 3;
            if (doorSide == DoorSide.LEFT) return 1;
            else return 2;
        }

        private void LinkDoorCollision(ILink link)
        {
            if (isOpen)
            {

                float posX = link.Position.X;
                float posY = link.Position.Y;
                if (doorSide == DoorSide.BOTTOM)
                {
                    posY = 275;
                }
                else if (doorSide == DoorSide.LEFT)
                {
                    posX = 625;
                }
                else if (doorSide == DoorSide.TOP)
                {
                    posY = 550;
                }
                else if (doorSide == DoorSide.RIGHT)
                {
                    posX = 100;
                }

                link.Position = new Vector2(posX, posY);

                room._thisRoomManager.SwitchRoom(room.RoomID, (int)doorSide);
            }
            else if (link.LinkInventory.HasKey() && (doorType == 2))
            {

                link.LinkInventory.RemoveKey();
                isOpen = true;
                doorType = 1;
            } else if ((room.RoomID == 5 || room.RoomID == 6) && link.LinkInventory.BombCount() > 0 && doorType == 0 && doorSide == DoorSide.TOP) 
            {
                link.LinkInventory.RemoveBomb();
                isOpen = true;
                doorType = 4;
            }


        }

        public void SetDoorOpen()
        {
            if (doorType == 3)
            {
                doorType = 1;
                isOpen = true;
            }
        }
    }
}

