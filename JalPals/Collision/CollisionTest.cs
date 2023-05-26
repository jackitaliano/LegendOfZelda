using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Collision
{
    public class CollisionTest
    {
        private Vector2 rect1_pos, rect2_pos;
        public Rectangle rect1, rect2;
        private ICollisionDetection collisionTest;
        private SpriteBatch spriteBatch1;
        private Texture2D _texture;
        private GraphicsDevice gd1;

        public Vector2 Velocity { get; set; }

        public CollisionTest(SpriteBatch spriteBatch, GraphicsDevice gd)
        {
            rect1_pos = new Vector2(100, 100);
            rect2_pos = new Vector2(300, 100);

            gd1 = gd;

            collisionTest = new CollisionDetection();
            spriteBatch1 = spriteBatch;
            rect1 = new Rectangle((int)rect1_pos.X, (int)rect1_pos.Y, 25, 25);
            rect2 = new Rectangle((int)rect2_pos.X, (int)rect2_pos.Y, 25, 25);

            Velocity = new Vector2(1, -1);
        }

        public void Draw()
        {

            // spriteBatch1.Draw(new Texture2D(gd1, 20, 20), rect1, Color.Red);
            // spriteBatch1.Draw(new Texture2D(gd1, 20, 20), rect2, Color.Red);

            this.collisionTest.DetectCollision(rect1, rect2);
        }

        public void Update()
        {
            rect1.X += (int)Velocity.X;
            rect2.X += (int)Velocity.Y;
        }
    }
}

