using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Inventory;

public class BluePotionItem : IItem
{
    public Rectangle SourceRect { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Dimensions { get; set; }
    public Texture2D Texture { get; }
    public float scale { get; }
    public bool InInventory { get; set; }


    public BluePotionItem(Texture2D Texture, Vector2 Position, float Scale)
    {
        this.SourceRect = ItemAnimations.BluePotion;
        this.Texture = Texture;
        this.Position = Position;
        this.scale = 2;
        this.InInventory = false;

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (InInventory)
        {
            spriteBatch.Draw(Texture, ItemSlots.Slot2, SourceRect, Color.White);
        }
        else
        {
            spriteBatch.Draw(Texture, getPickupDest(), SourceRect, Color.White);
            this.InInventory = true;
        }
    }
    public void Update()
    {

    }
    public void Pickup()
    {

    }

    private Rectangle getPickupDest()
    {
        float x_pos = this.Position.X * 2 + 15;
        float y_pos = this.Position.Y * 2 + 5;
        float width = 16;
        float height = 28;
        return new Rectangle((int)x_pos, (int)y_pos, (int)width, (int)height);
    }

}
