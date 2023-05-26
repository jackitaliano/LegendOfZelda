using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Items;

public interface IItem : IGameObject
{
    public Rectangle SourceRect { get; set; }
    public Rectangle DestRect { get; set; }
    public Rectangle InventoryRect { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Dimensions { get; set; }
    public Texture2D Texture { get; }
    public bool InInventory { get; set; }
    public bool ItemStatus { get; set; }
    public bool DisplayInventory { get; set; }
    public bool Equipable { get; set; }
    void Draw(SpriteBatch spriteBatch);
    void Update();
}
