using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Items;

public class BowItem : IItem
{

    public Rectangle SourceRect { get; set; }
    public Rectangle DestRect { get; set; }
    public Rectangle collisionRectangle { get; set; }
    public Vector2 velocityVector { get; set; }
    public bool Equipable { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Dimensions { get; set; }
    public Rectangle InventoryRect { get; set; }
    public Texture2D Texture { get; }
    public float scale { get; }
    public bool InInventory { get; set; }
    public bool ItemStatus { get; set; }
    public bool DisplayInventory { get; set; }

    public BowItem(Texture2D Texture, Vector2 Position, float Scale)
    {
        this.Equipable = true;
        this.SourceRect = ItemAnimations.BowItem;
        this.Texture = Texture;
        this.Position = Position;
        this.DisplayInventory = true;
        this.scale = Scale / 2;
        this.InInventory = false;
        int width = (int)(SourceRect.Width * scale);
        int height = (int)(SourceRect.Height * scale);
        DestRect = new Rectangle((int)Position.X, (int)Position.Y, width, height);
        collisionRectangle = DestRect;

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (InInventory)
        {
            spriteBatch.Draw(Texture, InventoryRect, SourceRect, Color.White);
        }
        else
        {
            spriteBatch.Draw(Texture, DestRect, SourceRect, Color.White);
        }
    }
    public void Update()
    {

    }

    public GameObjectType getType()
    {
        return GameObjectType.ITEM;
    }

    public void ResolveCollision(IGameObject obj1, int side)
    {

    }

}
