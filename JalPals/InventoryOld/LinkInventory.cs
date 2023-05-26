using System;
using System.Collections.Generic;
using System.Reflection;
using JalPals.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Inventory;

public class LinkInventory : IInventory
{
    public IDictionary<IItem, int> InventoryDict { get; }
    public IList<IItem> InventoryList { get; }
    public IItem CurrentItem { get; set; }
    private int index;


    public LinkInventory(Texture2D texture)
    {
        this.InventoryDict = new Dictionary<IItem, int>();
        this.InventoryList = new List<IItem>();
        this.CurrentItem = null;
        this.index = 0;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        /*
        foreach (KeyValuePair<IItem, int> Item in InventoryDict)
        {
            Item.Key.Draw(spriteBatch);
        }
        */
        if (index < InventoryList.Count && InventoryList.Count != 0)
        {
            InventoryList[index].Draw(spriteBatch);
        }
        
    }

    public void AddItem(IItem item)
    {
        if (InventoryList.Contains(item))
        {
            this.InventoryDict[item]++;
        }
        else
        {
            this.InventoryDict.Add(item, 1);
            this.InventoryList.Add(item);
        }
    }

    public void RemoveItem(IItem item)
    {
        if (InventoryDict.ContainsKey(item) && (InventoryDict[item] > 0))
        {
            this.InventoryDict[item]--;
        } 
    }

    public void EmptyInventory()
    {
        InventoryDict.Clear();
        InventoryList.Clear();
    }

    public IItem DropItem(IItem item)
    {
        if (InventoryDict.ContainsKey(item) && (InventoryDict[item] > 0))
        {
            this.InventoryDict[item]--;
        }
        return item;
    }


    public void NextItem()
    {
        index++;
        if (InventoryList.Count != 0) index %= InventoryList.Count; 

        setItem();
    }

    public void PreviousItem()
    {
        index--; // decrement index
        if (index < 0)
        {
            index = InventoryList.Count - 1; // clip index (sadly, % cannot be used here, because it is NOT a modulus operator)
        }
        // or above code as a one-liner:
        /* index = (items.Count+index-1)%items.Count; */ // (credits to Matthew Watson)

        setItem();
    }

    private void setItem()
    {
        if (InventoryList.Count != 0) this.CurrentItem = InventoryList[index];
    }
}