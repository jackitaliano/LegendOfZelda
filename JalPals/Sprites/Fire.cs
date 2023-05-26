using System;
using JalPals.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Sprites
{
	public class Fire : ISprite
	{
        public Texture2D spriteSheet { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle destRectangle { get; set; }
        public Rectangle collisionRectangle { get; set; }
        public Vector2 velocityVector { get; set; }

        public int enemyHealth { get; set; }

        // Private variables
        private int animationFrame;
        private int currDirection;
        private int speed = 2;
        private int scale = 3;
        private int health;
        private Rectangle srcRec, destRec;
        private Random rnd = new Random(Guid.NewGuid().GetHashCode());

        public Fire(Texture2D spriteSheet, Vector2 position)
        {
            this.Position = position;
            this.spriteSheet = spriteSheet;
            animationFrame = 0;
            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width, srcRec.Height);
            collisionRectangle = destRectangle;
            enemyHealth = 5;

        }

        public void Update()
        {
            animationFrame++;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(animationFrame % 2 == 0)
            {
                srcRec = new Rectangle(0, 0, 16, 16);
            }
            else
            {
                srcRec = new Rectangle(16, 0, 16, 16);
            }
            destRectangle = new Rectangle((int)Position.X + 20, (int)Position.Y, 16 * scale, 16 * scale);

            spriteBatch.Draw(this.spriteSheet, destRectangle, srcRec, Color.White);
        }

        public GameObjectType getType()
        {
            return GameObjectType.ENEMY;
        }

        private void TakeDamage()
        {
            // NO-OP, fire cannot take damage
        }

        public void ResolveCollision(IGameObject obj1, int side)
        {
            // NO-OP, fire cannot collide
        }

        private void CollisionRebound(int side)
        {
            // NO-OP
        }
    }
}
