using Microsoft.Xna.Framework;
namespace JalPals;

public enum GameObjectType
{
    LINK,
    ENEMY,
    BLOCK,
    LINKPROJECTILE,
    ENEMYPROJECTILE,
    ITEM,
    TEXT,
    WALL,
    DOOR

}

public interface IGameObject
{
    public Rectangle collisionRectangle { get; set; }
    public Vector2 velocityVector { get; set; }
    public void ResolveCollision(IGameObject obj, int side);
    public GameObjectType getType();
}

