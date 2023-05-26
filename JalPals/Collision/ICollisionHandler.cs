namespace JalPals.Collision
{
    public interface ICollisionHandler
    {
        void ResolveCollision(IGameObject obj1, IGameObject obj2, int side);
    }
}

