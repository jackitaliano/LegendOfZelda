using JalPals.Items;
using Microsoft.Xna.Framework.Graphics;
using System;
using JalPals.HUD;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace JalPals.Inventory;


public class LinkInventory : IInventory
{
    public IDictionary<IItem, int> InventoryDict { get; }
    public Queue<IItem> InventoryQueue { get; }
    public IItem CurrentItem { get; set; }
    private Game1 game;
    private int index;
    public IItem[] slots { get; set; }
    public IItem EquippedItem { get; set; }

    public bool HasBow { get; set; }
    public bool HasBoomerang { get; set; }
    public int NumKeys = 0;
    public int NumHearts = 0;
    private int NumBlueRupees, NumOrgRupees = 0;
    private int NumBombs = 0;

    public LinkInventory(Texture2D texture, Game1 game)
    {
        this.InventoryDict = new Dictionary<IItem, int>();
        this.InventoryQueue = new Queue<IItem>();
        this.CurrentItem = null;
        this.index = 0;
        this.game = game;
        this.slots = new IItem[10];
        NumKeys = 0;
        HasBow = false;
        HasBoomerang = false;
    }

    public void Draw(SpriteBatch spriteBatch, int offset)
    {
        int currSlot = 0;
        foreach(IItem curr in InventoryQueue)
        {
            if(currSlot < 10 && curr.DisplayInventory)
            {
                int row = (currSlot / 5);
                int col = currSlot % 5;
                curr.InventoryRect = new Rectangle(415 + 54*col, -350 + offset + 50 * row, (int) 2.5 * curr.SourceRect.Width, (int) 2.5 * curr.SourceRect.Height);
                curr.Draw(spriteBatch);
                slots[currSlot] = curr;
                currSlot++;
            }

        }
    }

    public void DrawEquipped(SpriteBatch spriteBatch, int offset)
    {
        if(EquippedItem != null)
        {
            //Console.WriteLine("Draw equipped");
            EquippedItem.InventoryRect = new Rectangle(386, 76 + offset, 3 * EquippedItem.SourceRect.Width, 3 * EquippedItem.SourceRect.Height);
            EquippedItem.Draw(spriteBatch);
        }
    }

    public void Equip(int i)
    {

        if (slots[i] != null && slots[i].Equipable)
        {
            Console.WriteLine("Equip " + i);
            this.EquippedItem = slots[i];
            if (this.EquippedItem.SourceRect == ItemAnimations.BowItem)
            { 
                HasBow = true;
                Console.WriteLine("Bow equipped");
	        }
            else HasBow = false;
            if (this.EquippedItem.SourceRect == ItemAnimations.BoomerangItem)
            { 
                HasBoomerang = true;
                Console.WriteLine("Boomerang equipped");
	        }
            else HasBoomerang = false;
        }
    }

    public void AddItem(IItem item)
    {
        if (item.SourceRect == ItemAnimations.HeartDropItem)
        {
            // Add heart
            game.menu.UpdateHeart(2);
        }
        else if (item.SourceRect == ItemAnimations.HeartContainerItem)
        {
            // Add full hearts
            game.menu.UpdateHeart(30);
        }
        else if (item.SourceRect == ItemAnimations.OrangeRupeeItem)
        {
            // Add rupee
            NumOrgRupees += 5;
        }
        else if (item.SourceRect == ItemAnimations.BlueRupeeItem)
        {
            // Add five rupee
            NumBlueRupees += 5;
        }
        else if (item.SourceRect == ItemAnimations.Key)
        {
            // Add key
            NumKeys++;
            
        }
        else if (item.SourceRect == ItemAnimations.Bomb)
        {
            // Add bomb
            NumBombs++;
        }
        
        // Add to inventory
        if (InventoryDict.ContainsKey(item))
        {
            this.InventoryDict[item]++;
        }
        else
        {
            item.InInventory = true;
            this.InventoryQueue.Enqueue(item);
            this.InventoryDict.Add(item, 1);
        }
        Console.WriteLine(InventoryQueue.Count);

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
        InventoryQueue.Clear();
    }

    public IItem DropItem(IItem item)
    {
        if (InventoryDict.ContainsKey(item) && (InventoryDict[item] > 0))
        {
            this.InventoryDict[item]--;
            item.InInventory = false;
        }
        return item;
    }

    public bool HasKey()
    {
        return NumKeys > 0;
    }

    public void RemoveKey()
    {
        NumKeys--;
    }
    public void RemoveBomb()
    {
        NumBombs--;
    }
    public void RemoveRupee()
    {
        NumOrgRupees--;
    }

    public int KeyCount()
    {
        return NumKeys;
    }

    public int BombCount()
    {
        return NumBombs;
    }

    public int RupeeCount()
    {
        return NumBlueRupees + NumOrgRupees;
    }

    public int BlueRupeeCount()
    {
        return NumBlueRupees;
    }
    public int OrgRupeeCount()
    {
        return NumOrgRupees;
    }
}