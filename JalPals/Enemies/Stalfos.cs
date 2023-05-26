using JalPals.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JalPals.Enemies
{
    public class Stalfos : ISprite
    {
        // Stalfos is skeleton character!

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
        private int speed = 2;
        private int scale = 3;
        private int health;
        Rectangle srcRec, destRec;

        // Used for randomized movement
        Random rnd = new Random(Guid.NewGuid().GetHashCode());

        public Stalfos(Texture2D spriteSheet, Vector2 position)
        {
            this.Position = position;
            this.spriteSheet = spriteSheet;
            currDirection = rnd.Next(0, 4);
            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width, srcRec.Height);
            collisionRectangle = destRectangle;

            health = EnemyHealth.STALFOS_HEALTH;
            enemyHealth = health;

        }

        public void Update()
        {
            animationFrame++;

            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width * scale, srcRec.Height * scale);
            collisionRectangle = destRectangle;
            if (animationFrame >= 20)
            {
                currDirection = rnd.Next(0, 4);
                animationFrame = 0;
                switch (currDirection)
                {
                    case 0:
                        velocityVector = new Vector2(speed, 0);

                        break;
                    case 1:
                        velocityVector = new Vector2(-speed, 0);

                        break;
                    case 2:
                        velocityVector = new Vector2(0, speed);

                        break;
                    case 3:
                        velocityVector = new Vector2(0, -speed);

                        break;
                }
            }

            this.Position += velocityVector;

        }

        private void createFireball()
        {

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (animationFrame % 2 == 0)
            {
                srcRec = new Rectangle(1, 59, 16, 16);
            }
            else
            {
                srcRec = new Rectangle(89, 107, 16, 16);
            }

            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width * scale, srcRec.Height * scale);

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
                    CollisionRebound(side);
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

