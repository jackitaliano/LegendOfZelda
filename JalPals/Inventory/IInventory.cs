using JalPals.Items;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace JalPals.Inventory;

public interface IInventory
{
    public IDictionary<IItem, int> InventoryDict { get; }
    public Queue<IItem> InventoryQueue { get; }
    public IItem[] slots { get; set; }

    void Draw(SpriteBatch spriteBatch, int offset);
    void DrawEquipped(SpriteBatch spriteBatch, int offset);
    public void Equip(int i);

    void AddItem(IItem item);
    void RemoveItem(IItem item);
    void EmptyInventory();
    public IItem EquippedItem { get; set; }
    IItem DropItem(IItem item);
    bool HasKey();
    void RemoveKey();
    void RemoveRupee();
    int KeyCount();
    int BombCount();
    int RupeeCount();
    public bool HasBow { get; set; }
    public bool HasBoomerang { get; set; }
    void RemoveBomb();
}


