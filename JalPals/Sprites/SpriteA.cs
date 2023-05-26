using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Sprites
{
    abstract class SpriteA : ISprite
    {
        public abstract Vector2 Position { get; set; }

        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);

        public Rectangle collisionRectangle { get; set; }
        public Vector2 velocityVector { get; set; }
        public int enemyHealth { get; set; }

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
                    break;
                case GameObjectType.ITEM:
                    break;
                case GameObjectType.BLOCK:
                    CollisionRebound(side);
                    break;
            }
        }

        private void CollisionRebound(int side)
        {
            switch (side)
            {
                case 1: // Top side
                    Position = new Vector2(Position.X, Position.Y + 5);
                    break;
                case 2: // Right side
                    Position = new Vector2(Position.X - 5, Position.Y);
                    break;
                case 3: // Bottom side
                    Position = new Vector2(Position.X, Position.Y - 5);
                    break;
                case 4: // Left side
                    Position = new Vector2(Position.X + 5, Position.Y);
                    break;
            }
        }




        public abstract GameObjectType getType();


        private SpriteA()
        {
        }




    }
}

