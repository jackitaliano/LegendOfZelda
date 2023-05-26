namespace JalPals.Collision;

public class CollisionHandler : ICollisionHandler
{
    public CollisionHandler() { }

    public void ResolveCollision(IGameObject obj1, IGameObject obj2, int side)
    {
        obj1.ResolveCollision(obj2, side);

    }
}

