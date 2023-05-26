using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Inventory;

public interface IItem
{
    public Rectangle SourceRect { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Dimensions { get; set; }
    public Texture2D Texture { get; }
    public bool InInventory { get; set; }

    void Draw(SpriteBatch spriteBatch);
    void Update();
    void Pickup();

}
