using Microsoft.Xna.Framework;

namespace JalPals.Rooms
{
    public class WallRectangle : IGameObject
    {
        public Rectangle collisionRectangle { get; set; }
        public Vector2 velocityVector { get; set; }



        public WallRectangle(Rectangle rect)
        {
            this.collisionRectangle = rect;

        }

        public void ResolveCollision(IGameObject obj, int side)
        {
            //throw new NotImplementedException();
        }
        public GameObjectType getType()
        {
            return GameObjectType.WALL;
        }
    }
}

