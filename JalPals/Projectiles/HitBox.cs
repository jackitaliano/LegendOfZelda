using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Projectiles;

public class HitBox : IProjectile
{
    // Properties
    public Vector2 Position { get; set; }
    public Rectangle destRectangle { get; set; }
    public Rectangle collisionRectangle { get; set; }
    public Vector2 velocityVector { get; set; }

    public bool Friendly { get; set; }
    public bool Visible { get; set; }

    public Rectangle SourceRect { get; set; }

    public Vector2 Dimensions { get; set; }
    public Texture2D Texture { get; }

    private int lifeTime;
    private float scale;

    public HitBox(Texture2D texture, Vector2 Position, Vector2 dimensions, bool Friendly = false)
    {
        this.scale = 3;
        this.lifeTime = 5;

        this.SourceRect = new Rectangle(360, 165, 16, 16);
        Texture = texture;
        dimensions.X *= scale;
        dimensions.Y *= scale;
        this.Dimensions = dimensions;

        this.Visible = true;
        this.Friendly = Friendly;
        this.Position = Position;

        destRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // NO-OP
        //uncomment below for hitbox
        //spriteBatch.Draw(Texture, destRectangle, SourceRect, Color.Red);
    }

    public void Update()
    {
        this.lifeTime--;
        if (this.lifeTime <= 0)
        {
            DeleteObject();
        }

        destRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
        collisionRectangle = destRectangle;
    }

    public GameObjectType getType()
    {
        return GameObjectType.LINKPROJECTILE;
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
                DeleteObject();
                break;
        }
    }

    private void DeleteObject()
    {
        Visible = false;
    }
}
