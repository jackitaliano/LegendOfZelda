using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Projectiles;

public class GreenArrowDown : IProjectile
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
    public float scale { get; }
    private Rectangle _bowSrcRect;
    private int velocity;

    public GreenArrowDown(Texture2D Texture, Vector2 Position, int velocity, float Scale, bool Friendly = false)
    {
        this.Texture = Texture;
        this.Position = Position;
        this.scale = Scale;
        this.SourceRect = ProjectileAnimations.GreenArrowDown;
        this.Dimensions = new Vector2(SourceRect.Width * scale, SourceRect.Height * scale);
        this.Visible = true;
        this.Friendly = Friendly;
        this.velocity = velocity;

        _bowSrcRect = ProjectileAnimations.BowRight;
        destRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);
        collisionRectangle = destRectangle;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, destRectangle, SourceRect, Color.White);
    }
    public void Update()
    {
        Vector2 position = this.Position;
        position.Y += velocity;
        this.Position = position;
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
            case GameObjectType.WALL:
                DeleteObject();
                break;
        }
    }

    private void DeleteObject()
    {
        Visible = false;
    }
}
