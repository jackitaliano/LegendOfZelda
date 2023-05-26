using JalPals.Projectiles;
using JalPals.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JalPals.Enemies
{
    public class Goriya : ISprite
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
        private int currDirection;
        private int speed = 1;
        private int scale = 3;
        private int health;
        private Microsoft.Xna.Framework.Rectangle[] sourceRecs;
        private Rectangle srcRec, destRec;     
        private Random rnd = new Random();
        private bool isThrowing;
        private IProjectileManager projManager;
        private Vector2 boomSrc;

        public Goriya(Texture2D spriteSheet, Vector2 position, IProjectileManager projectileManager)
        {
            this.Position = position;
            this.spriteSheet = spriteSheet;
            this.projManager = projectileManager;
            this.currDirection = rnd.Next(0, 4);
            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width, srcRec.Height);
            collisionRectangle = destRectangle;

            health = EnemyHealth.GORIYA_HEALTH;
            enemyHealth = health;

            sourceRecs = new Rectangle[]
            {
                new Rectangle(257, 11, 13, 16),
                new Rectangle(275, 12, 14, 15),

                new Rectangle(257, 29, 13, 16),
                new Rectangle(275, 30, 14, 15),

                new Rectangle(241, 11, 13, 16),
                new Rectangle(241, 29, 13, 16),

                new Rectangle(224, 11, 13, 16),
                new Rectangle(224, 29, 13, 16),
            };

            switch (currDirection)
            {
                case 0:
                    if (!isThrowing)
                    {
                        velocityVector = new Vector2(speed, 0);
                    }
                    break;
                case 1:
                    if (!isThrowing)
                    {
                        velocityVector = new Vector2(-speed, 0);
                    }
                    break;
                case 2:
                    velocityVector = new Vector2(0, -speed);
                    break;
                case 3:
                    velocityVector = new Vector2(0, speed);
                    break;
            }

        }

        public void Update()
        {
            animationFrame++;

            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width * scale, srcRec.Height * scale);
            collisionRectangle = destRectangle;

            if (animationFrame >= 240)
            {
                animationFrame = 0;
            }

            if (animationFrame % 60 == 0)
            {
                currDirection = rnd.Next(0, 4);
                if (currDirection < 2 && rnd.Next(0, 2) == 1)
                {
                    isThrowing = true;

                    if (currDirection == 0)
                    {
                        boomSrc = new Vector2(this.Position.X + srcRec.Width, this.Position.Y + 15);
                        projManager.AddBoomerang(boomSrc, new Vector2(5, 0), false);

                    }
                    else
                    {
                        boomSrc = new Vector2(this.Position.X, this.Position.Y + 15);
                        projManager.AddBoomerang(boomSrc, new Vector2(-5, 0), false);
                    }

                }
                else
                {
                    isThrowing = false;
                }

                switch (currDirection)
                {
                    case 0:
                        if (!isThrowing)
                        {
                            velocityVector = new Vector2(speed, 0);
                        }
                        break;
                    case 1:
                        if (!isThrowing)
                        {
                            velocityVector = new Vector2(-speed, 0);
                        }
                        break;
                    case 2:
                        velocityVector = new Vector2(0, -speed);
                        break;
                    case 3:
                        velocityVector = new Vector2(0, speed);
                        break;
                }


            }

            Position += velocityVector;



        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int destX;
            int destY;

            if (animationFrame % 2 == 0)
            {
                srcRec = sourceRecs[2 * currDirection];
                destX = (int)this.Position.X;
                destY = (int)this.Position.Y;
                destRec = new Rectangle(destX, destY, srcRec.Width * scale, srcRec.Height * scale);
            }
            else
            {
                srcRec = sourceRecs[2 * currDirection + 1];
                destX = (int)this.Position.X;
                destY = (int)this.Position.Y;
                destRec = new Rectangle(destX, destY, srcRec.Width * scale, srcRec.Height * scale);
            }

            spriteBatch.Draw(this.spriteSheet, destRec, srcRec, Color.White);
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
    }
}

