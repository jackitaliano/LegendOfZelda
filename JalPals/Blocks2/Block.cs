using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Blocks2
{
    public class Block : IBlock2
    {
        // Public properties
        public Rectangle destRectangle { get; set; }
        public Rectangle collisionRectangle { get; set; }
        public Rectangle srcRectangle { get; set; }
        public Vector2 velocityVector { get; set; }
        public bool collidable { get; set; }
        public bool Moveable { get { return false; } set { /*NO-OP*/ } }

        // Private variables
        Texture2D spriteSheet;

        // Debug code
        Texture2D whiteRectangle;

        public Block(Texture2D spriteSheet, Rectangle src, Rectangle dest, bool collidable)
        {
            this.spriteSheet = spriteSheet;
            this.destRectangle = dest;
            this.srcRectangle = src;
            this.collidable = collidable;
            Rectangle newCollide = new Rectangle(destRectangle.X, destRectangle.Y, destRectangle.Width, destRectangle.Height - 24);
            collisionRectangle = newCollide;
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
            // NO-OP
	    }


        public GameObjectType getType()
        {
            return GameObjectType.BLOCK;

        }

        public void ResolveCollision(IGameObject obj, int side)
        {
            // NO OP
        }
    }
}

