using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Projectiles
{
    public class Boomerang : IProjectile
    {
        // Properties
        public Vector2 Position { get; set; }
        public Rectangle destRectangle { get; set; }
        public Rectangle collisionRectangle { get; set; }
        public Vector2 velocityVector { get; set; }

        public bool Friendly { get; set; }
        public bool Visible { get; set; }

        // Private Variables
        private Texture2D spriteSheet;
        private Vector2 velocity;
        private int animationFrame;
        private Rectangle[] rightSrc, leftSrc;
        private Rectangle srcRec, destRec;
        private int destX, destY;
        private int sizeMultiplier = 2;
        private int lifeTime;

        public Boomerang(Texture2D spriteSheet, Vector2 position, Vector2 velocity, bool Friendly = false)
        {
            this.velocity = velocity;
            this.Position = position;
            this.spriteSheet = spriteSheet;
            this.Visible = true;
            this.lifeTime = 0;
            this.Friendly = Friendly;
            animationFrame = 0;
            rightSrc = ProjectileAnimations.BoomerangRight;
            leftSrc = ProjectileAnimations.BoomerangLeft;


            destX = (int)this.Position.X;
            destY = (int)this.Position.Y;
            destRectangle = new Rectangle(destX, destY, srcRec.Width * sizeMultiplier, srcRec.Height * sizeMultiplier);
            collisionRectangle = destRectangle;
        }

        public void Update()
        {
            this.lifeTime++;
            if (this.lifeTime == 31)
            {
                velocity = new Vector2(-velocity.X, -velocity.Y);
            }
            else if (this.lifeTime > 60)
            {
                this.Visible = false;
            }

            this.Position += velocity;
            animationFrame++;
            if (animationFrame == rightSrc.Length)
            {
                animationFrame = 0;
            }
            destRectangle = new Rectangle((int)Position.X, (int)Position.Y, srcRec.Width * sizeMultiplier, srcRec.Height * sizeMultiplier);
            collisionRectangle = destRectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (velocity.X > 0)
            {
                srcRec = rightSrc[animationFrame];
            }
            else
            {
                srcRec = leftSrc[animationFrame];
            }
            destX = (int)this.Position.X;
            destY = (int)this.Position.Y;
            destRec = new Rectangle(destX, destY, srcRec.Width * sizeMultiplier, srcRec.Height * sizeMultiplier);
            spriteBatch.Draw(spriteSheet, destRectangle, srcRec, Color.White);
        }

        public GameObjectType getType()
        {
            if (Friendly) return GameObjectType.LINKPROJECTILE;
            else return GameObjectType.ENEMYPROJECTILE;
        }

        public void ResolveCollision(IGameObject obj1, int side)
        {
            GameObjectType type = obj1.getType();
            switch (type)
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
                    // shouldn't collide with blocks
                    //DeleteObject();
                    break;
            }
        }

        private void DeleteObject()
        {
            Visible = false;
        }
    }
}

