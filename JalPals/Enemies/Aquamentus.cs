using JalPals.Projectiles;
using JalPals.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JalPals.Enemies
{
    public class Aquamentus : ISprite
    {
        // Properties
        public Texture2D spriteSheet { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle destRectangle { get; set; }
        public Rectangle collisionRectangle { get; set; }
        public Vector2 velocityVector { get; set; }

        public int enemyHealth { get; set; }

        // Private variables
        private int animationFrame;
        private int speed = 2;
        private int scale = 3;
        private int currDirection;
        private int health;

        private Rectangle srcRec, destRec;
        private Random rnd = new Random(Guid.NewGuid().GetHashCode());
        private IProjectileManager projManager;

        public Aquamentus(Texture2D spriteSheet, Vector2 position, IProjectileManager projectileManager)
        {
            this.Position = position;
            this.spriteSheet = spriteSheet;
            this.projManager = projectileManager;
            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width, srcRec.Height);
            collisionRectangle = destRec;

            health = EnemyHealth.AQUAMENTUS_HEALTH;
            enemyHealth = health;

        }

        public void Update()
        {
            animationFrame++;

            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width * scale, srcRec.Height * scale);
            collisionRectangle = destRectangle;

            //Get a direction for the Aqamentus
            if (animationFrame == 20)
            {
                currDirection = rnd.Next(0, 4);
                getNewDirection();
            }

            //Get a new drection 
            if (animationFrame == 70)
            {
                currDirection = rnd.Next(0, 4);
                getNewDirection();
            }

            

            //Every 120 frames, stop and fire the fireballs. Reset animation frames to 0.
            if (animationFrame == 120)
            {
                Vector2 fireSrc = new Vector2(this.Position.X, this.Position.Y + 15);
                projManager.AddFireball(fireSrc, new Vector2(-5, 0), false);
                projManager.AddFireball(fireSrc, new Vector2(-5, -2), false);
                projManager.AddFireball(fireSrc, new Vector2(-5, 2), false);
                animationFrame = 0;
            }

            this.Position += velocityVector;
            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width * scale, srcRec.Height * scale);
            collisionRectangle = new Rectangle(destRectangle.X, destRectangle.Y, destRectangle.Width, destRectangle.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int destX;
            int destY;

            if (animationFrame % 2 == 0)
            {
                srcRec = new Rectangle(51, 11, 24, 32);
                destX = (int)this.Position.X;
                destY = (int)this.Position.Y;
                destRectangle = new Rectangle(destX, destY, srcRec.Width * scale, srcRec.Height * scale);
            }
            else
            {
                srcRec = new Rectangle(76, 11, 24, 32);
                destX = (int)this.Position.X;
                destY = (int)this.Position.Y;
                destRectangle = new Rectangle(destX, destY, srcRec.Width * scale, srcRec.Height * scale);
            }
            spriteBatch.Draw(this.spriteSheet, destRectangle, srcRec, Color.White);
        }

        public GameObjectType getType()
        {
            return GameObjectType.ENEMY;
        }

        private void TakeDamage()
        {
            enemyHealth--;
        }

        public void ResolveCollision(IGameObject obj1, int side)
        {
            GameObjectType type = obj1.getType();
            switch (type)
            {
                case GameObjectType.LINK:
                    CollisionRebound(side);
                    break;
                case GameObjectType.ENEMY:
                    CollisionRebound(side);
                    break;
                case GameObjectType.ENEMYPROJECTILE:
                    break;
                case GameObjectType.LINKPROJECTILE:
                    TakeDamage();
                    break;
                case GameObjectType.ITEM:
                    break;
                case GameObjectType.BLOCK:
                    CollisionRebound(side);
                    break;
                case GameObjectType.DOOR:
                    CollisionRebound(side);
                    break;
                case GameObjectType.WALL:
                    CollisionRebound(side);
                    break;
            }
        }

        private void CollisionRebound(int side)
        {
            float veloY = velocityVector.Y;
            float veloX = velocityVector.X;

            switch (side)
            {
                case 1: // Top side
                    velocityVector = new Vector2(veloX, -1 * veloY);
                    Position = new Vector2(Position.X, Position.Y + 5);
                    break;
                case 2: // Right side
                    velocityVector = new Vector2(-1 * veloX, veloY);
                    Position = new Vector2(Position.X - 5, Position.Y);
                    break;
                case 3: // Bottom side
                    velocityVector = new Vector2(veloX, -1 * veloY);
                    Position = new Vector2(Position.X, Position.Y - 5);
                    break;
                case 4: // Left side
                    velocityVector = new Vector2(-1 * veloX, veloY);
                    Position = new Vector2(Position.X + 5, Position.Y);
                    break;
            }
        }
        private void getNewDirection()
        {

            switch (this.currDirection)
            {
                case 0:
                case 2:
                    velocityVector = new Vector2(speed, 0);
                    break;
                case 1:
                case 3:
                    velocityVector = new Vector2(-speed, 0);
                    break;
                //case 2:
                //    velocityVector = new Vector2(0, speed);
                //    break;
                //case 3:
                //    velocityVector = new Vector2(0, -speed);
                //    break;
            }


        }
    }

}

