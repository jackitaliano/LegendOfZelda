using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JalPals.Blocks2
{
    public class MoveableBlock : IBlock2
    {
        // Public properties
        public Rectangle destRectangle { get; set; }
        public Rectangle collisionRectangle { get; set; }
        public Rectangle srcRectangle { get; set; }
        public Vector2 velocityVector { get; set; }
        public bool collidable { get; set; }
        public bool Moveable { get { return moveable; } set { moveable = value; } }

        // Private variables
        Texture2D spriteSheet;
        private Vector2 vel;
        private Vector2 position;
        private Vector2 maxMoveDistance;
        private Vector2 moveDistance;
        private int collisionHeight = 24;
        private bool moveable;

        // Debug code
        Texture2D whiteRectangle;

        public MoveableBlock(Texture2D spriteSheet, Rectangle src, Rectangle dest, bool collidable)
        {
            this.spriteSheet = spriteSheet;
            this.destRectangle = dest;
            this.srcRectangle = src;
            this.collidable = collidable;
            this.position = new Vector2(dest.X, dest.Y);
            Rectangle newCollide = new Rectangle(destRectangle.X, destRectangle.Y, destRectangle.Width, destRectangle.Height - collisionHeight);
            collisionRectangle = newCollide;
            moveable = true;
            vel = new Vector2(0, 0);
            moveDistance = new Vector2(0, 0);
            maxMoveDistance = new Vector2(destRectangle.Width, destRectangle.Height);
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
            if (!moveable)
                return;

            position += vel;
            moveDistance.X += Math.Abs(vel.X);
            moveDistance.Y += Math.Abs(vel.Y);  

            destRectangle = new Rectangle((int)position.X, (int)position.Y, destRectangle.Width, destRectangle.Height);
            collisionRectangle = new Rectangle(destRectangle.X, destRectangle.Y, destRectangle.Width, destRectangle.Height - collisionHeight);

            if (moveDistance.X >= maxMoveDistance.X || moveDistance.Y >= maxMoveDistance.Y) {
                moveable = false;
                vel = new Vector2(0, 0);
		    }
        }


        public GameObjectType getType()
        {
            return GameObjectType.BLOCK;

        }

        public void ResolveCollision(IGameObject obj, int side)
        {
            if (obj.getType() == GameObjectType.BLOCK)
            {
                IBlock2 block = (IBlock2)obj;
                if (block.Moveable)
                {
                    moveable = false;
                    block.Moveable = false;
                }

	        }
            if (obj.getType() == GameObjectType.LINK)
            {
                Move(side);
	        }
        }

        private void Move(int side)
        {
            if (!moveable) return;

            if (side == 1)
            {
                MoveDown();
            } else if (side == 2 )
            {
                MoveLeft();
	        } else if (side == 3)
            {
                MoveUp();
	        } else
            {
                MoveRight();
            }
        }

        private void MoveUp()
        {
            Console.WriteLine("Move block up");
            vel = new Vector2(0, -1);
	    }

        private void MoveRight()
        {
            Console.WriteLine("Move block right");
            vel = new Vector2(1, 0);
	    }

        private void MoveDown()
        {
            Console.WriteLine("Move block down");
            vel = new Vector2(0, 1);
	    }

        private void MoveLeft()
        {
            Console.WriteLine("Move block left");
            vel = new Vector2(-1, 0);
	    }
    }
}

