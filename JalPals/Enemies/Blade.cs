using JalPals.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using JalPals.Player;
using System.Threading;

namespace JalPals.Enemies
{
    public class Blade : ISprite
    {
        // Properties
        public Texture2D spriteSheet { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle destRectangle { get; set; }
        public Rectangle collisionRectangle { get; set; }
        public Vector2 velocityVector { get; set; }
        public int enemyHealth { get; set; }
        public Rectangle origin { get; set; }

        // Private variables
        private int animationFrame;
        private int currDirection;
        private int speed = 3;
        private int scale = 3;
        private int health;
        private Rectangle srcRec, destRec;
        private Random rnd = new Random(Guid.NewGuid().GetHashCode());
        private ILink player;
        private GameTime gametime = new GameTime();
        private int count = 0;
        private bool move = true;

        public Blade(ILink link, Texture2D spriteSheet, Vector2 position)
        {
            this.Position = position;
            this.spriteSheet = spriteSheet;
            this.currDirection = rnd.Next(0, 8);
            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width, srcRec.Height);
            collisionRectangle = destRectangle;
            origin = destRectangle;
            health = EnemyHealth.BLADE_HEALTH;
            enemyHealth = health;

            player = link;

        }

        public void Update()
        {
            //Is the Blade matched with the players position?
            bool xValueMatch = this.Position.X > player.Position.X - 20.0 && this.Position.X < player.Position.X + 20.0;
            bool yValueMatch = this.Position.Y > player.Position.Y - 20.0 && this.Position.Y < player.Position.Y + 20.0;

            //Is the blade above/below/right/left of the players position>
            bool xValueTest = this.Position.X < player.Position.X;
            bool yValueTest = this.Position.Y < player.Position.Y;

            if(count > 200)
            {
                moveBackToOrigin();
                
                if(count > 400) 
                {
                    count = 0;
                }

            }

            //If the player is standing on the same X-Value and under the blade.
            if (xValueMatch)
            {
                if(yValueTest)
                {
                        float ypos = this.Position.Y;
                        this.Position = new Vector2(this.Position.X, ypos + speed);      
                }
            }

            //If the player is standing on the same X-Value and under the blade.
            if (xValueMatch)
            {
                if (!yValueTest)
                {
                        float ypos = this.Position.Y;
                        this.Position = new Vector2(this.Position.X, ypos - speed);
                }
            }

            //If the player is standing on the same Y-Value and right of the blade.
            if (yValueMatch)
            {
                if (xValueTest)
                {
                        float xpos = this.Position.X;
                        this.Position = new Vector2(xpos + speed, this.Position.Y);
                }
            }

            //If the player is standing on the same Y-Value and left of the blade.
            if (yValueMatch)
            {
                if (!xValueTest)
                {
                        float xpos = this.Position.X;
                        this.Position = new Vector2(xpos - speed, this.Position.Y);
                }
            }

            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width * scale, srcRec.Height * scale);
            collisionRectangle = destRectangle;

            count++;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int destX;
            int destY;
            srcRec = new Rectangle(164, 59, 16, 16);
            destX = (int)this.Position.X;
            destY = (int)this.Position.Y;
            destRectangle = new Rectangle(destX, destY, srcRec.Width * scale, srcRec.Height * scale);

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

        private void moveBackToOrigin()
        {
                if(this.Position.X < origin.X) {
                    float xpos = this.Position.X;
                    this.Position = new Vector2(xpos + speed, this.Position.Y);
                } else
                {
                    float xpos = this.Position.X;
                    this.Position = new Vector2(xpos - speed, this.Position.Y);
                }

                if (this.Position.Y < origin.Y)
                {
                    float ypos = this.Position.Y;
                    this.Position = new Vector2(this.Position.X, ypos + speed);
                }
                else
                {
                    float ypos = this.Position.Y;
                    this.Position = new Vector2(this.Position.X, ypos - speed);
                }
            
        }
    }
}

