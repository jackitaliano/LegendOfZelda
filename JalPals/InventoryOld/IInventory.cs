using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Inventory;

public interface IInventory
{
    public IDictionary<IItem, int> InventoryDict { get; }
    

    void Draw(SpriteBatch spriteBatch);
    void AddItem(IItem item);
    void RemoveItem(IItem item);
    void EmptyInventory();
    void NextItem();
    void PreviousItem();
    IItem DropItem(IItem item);
}


