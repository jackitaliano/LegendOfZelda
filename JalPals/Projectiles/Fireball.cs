using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Projectiles
{
    public class Fireball : IProjectile
    {
        // Properties
        public Vector2 Position { get; set; }
        public Rectangle destRectangle { get; set; }
        public Rectangle collisionRectangle { get; set; }
        public Vector2 velocityVector { get; set; }

        public bool Friendly { get; set; }
        public bool Visible { get; set; }

        // Private Variables
        private Vector2 velocity;
        private int animationFrame;
        private Rectangle[] sourceRecs;
        private Rectangle srcRec, destRec;
        private Texture2D spriteSheet;
        private int destX, destY;
        private int lifeTime;
        private int sizeMultiplier = 2;

        public Fireball(Texture2D spriteSheet, Vector2 position, Vector2 velocity, bool Friendly)
        {

            this.velocity = velocity;
            this.Position = position;
            this.spriteSheet = spriteSheet;
            this.Visible = true;
            this.Friendly = Friendly;
            lifeTime = 0;
            animationFrame = 0;
            sourceRecs = ProjectileAnimations.FireBall;


            destX = (int)this.Position.X;
            destY = (int)this.Position.Y;
            destRectangle = new Rectangle(destX, destY, srcRec.Width * sizeMultiplier, srcRec.Height * sizeMultiplier);
            collisionRectangle = destRectangle;
        }

        public void Update()
        {
            this.Position += velocity;
            lifeTime++;
            if (lifeTime > 240)
            {
                this.Visible = false;
            }

            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width * sizeMultiplier, srcRec.Height * sizeMultiplier);
            collisionRectangle = destRectangle;
            UpdateAnimation();
        }

        public void UpdateAnimation()
        {
            animationFrame++;
            if (animationFrame >= sourceRecs.Length)
            {
                animationFrame = 0;
            }
        }

        public void UpdatePosition()
        {
            Position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            srcRec = sourceRecs[animationFrame];
            destX = (int)this.Position.X;
            destY = (int)this.Position.Y;
            destRec = new Rectangle(destX, destY, srcRec.Width * sizeMultiplier, srcRec.Height * sizeMultiplier);
            if(Visible) spriteBatch.Draw(spriteSheet, destRectangle, srcRec, Color.White);
        }

        public GameObjectType getType()
        {
            if (Friendly) return GameObjectType.LINKPROJECTILE;
            else return GameObjectType.ENEMYPROJECTILE;
        }

        public void ResolveCollision(IGameObject obj1, int side)
        {
            
            switch (obj1.getType())
            {
                case GameObjectType.LINK:
                    if (!Friendly)
                        DeleteObject();
                    break;
                case GameObjectType.ENEMY:
                    if (Friendly)
                        DeleteObject();
                    break;
                case GameObjectType.ENEMYPROJECTILE:
                    break;
                case GameObjectType.LINKPROJECTILE:
                    break;
                case GameObjectType.ITEM:
                    break;
                case GameObjectType.BLOCK:
                    DeleteObject();
                    break;
                case GameObjectType.WALL:
                    DeleteObject();
                    break;
                case GameObjectType.DOOR:
                    DeleteObject();
                    break;
            }
        }

        private void DeleteObject()
        {
            Visible = false;
           
        }
    }
}

