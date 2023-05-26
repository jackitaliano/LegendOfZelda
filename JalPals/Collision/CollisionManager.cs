using System.Collections.Generic;


namespace JalPals.Collision;

public class CollisionManager
{
    private List<IGameObject> gameObjects;

    private ICollisionDetection collisionDetection;
    private ICollisionHandler collisionHandler;

    public CollisionManager()
    {
        this.gameObjects = new List<IGameObject>();
        collisionDetection = new CollisionDetection();
        collisionHandler = new CollisionHandler();
    }

    public void UpdateGameObjects(List<List<IGameObject>> gameObjectLists, params IGameObject[] gameObjects)
    {
        this.gameObjects = Flatten2DList(gameObjectLists);
        foreach (IGameObject gameObject in gameObjects)
        {
            this.gameObjects.Add(gameObject);
        }
    }

    public void AddObjectToList(IGameObject obj)
    {
        this.gameObjects.Add(obj);
    }

    public void HandleCollisions()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            for (int j = 0; j < gameObjects.Count; j++)
            {
                IGameObject obj1 = gameObjects[i];
                IGameObject obj2 = gameObjects[j];

                if (obj1 == obj2)
                    continue;

                int side = collisionDetection.DetectCollision(obj1.collisionRectangle, obj2.collisionRectangle);

                if (side == 0)
                    continue;

                collisionHandler.ResolveCollision(obj1, obj2, side);
            }
        }
    }

    private List<IGameObject> Flatten2DList(List<List<IGameObject>> objectLists)
    {
        List<IGameObject> objectList = new List<IGameObject>(objectLists.Count);

        foreach (List<IGameObject> list in objectLists)
        {
            foreach (IGameObject obj in list)
            {
                objectList.Add(obj);
            }
        }

        return objectList;
    }

}

