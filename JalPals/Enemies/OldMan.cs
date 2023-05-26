using JalPals.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Enemies
{
    public class OldMan : ISprite
    {
        // Properties
        public Texture2D spriteSheet { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle destRectangle { get; set; }
        public Vector2 velocityVector { get; set; }
        public Rectangle collisionRectangle { get; set; }

        public int enemyHealth { get; set; }

        // Private variables
        private int scale = 3;
        private Rectangle srcRec, destRec;
        private int health;

        public OldMan(Texture2D spriteSheet, Vector2 position)
        {
            this.Position = position;
            this.spriteSheet = spriteSheet;
            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width, srcRec.Height);
            collisionRectangle = destRec;

            health = EnemyHealth.OLDMAN_HEALTH;
            enemyHealth = health;

        }

        public void Update()
        {

            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width * scale, srcRec.Height * scale);
            collisionRectangle = destRectangle;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int destX;
            int destY;
            srcRec = new Rectangle(272, 59, 16, 16);
            destX = (int)this.Position.X;
            destY = (int)this.Position.Y;

            // (magic number because of luke's ocd)
            destRectangle = new Rectangle(destX+20, destY, srcRec.Width * scale, srcRec.Height * scale);

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
    }
}

