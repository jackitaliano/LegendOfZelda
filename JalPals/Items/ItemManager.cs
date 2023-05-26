using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace JalPals.Items
{
    public class ItemManager : IItemManager
    {
        public IList<IItem> ItemsInFrame { get; set; } // visible after room cleared
        public IList<IItem> ItemDrops { get; set; } // visible immediately
        private ContentLoader contentLoader;


        public ItemManager(ContentLoader contentLoader)
        {
            this.ItemsInFrame = new List<IItem>();
            this.ItemDrops = new List<IItem>();
            this.contentLoader = contentLoader;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawItems(spriteBatch);
            DrawDrops(spriteBatch);
        }

        public void DrawItems(SpriteBatch spriteBatch)
        {
            foreach (IItem Item in ItemsInFrame)
            {
                Item.Draw(spriteBatch);
            }
        }

        public void DrawDrops(SpriteBatch spriteBatch)
        {
            foreach (IItem Item in ItemDrops)
            {
                Item.Draw(spriteBatch);
            }
        }

        public void Update()
        {
            foreach (IItem Item in ItemsInFrame)
            {
                Item.Update();
            }

        }
        public void DeleteItem(IItem item)
        {
            if (ItemDrops.Contains(item)) ItemDrops.Remove(item);
            else ItemsInFrame.Remove(item);
        }

        public void AddItem(IItem item)
        {
            this.ItemsInFrame.Add(item);
        }

        public void AddDrop(IItem item)
        {
            this.ItemDrops.Add(item);
	    }

        public void AddHeartDrop(Vector2 position, float scale)
        {
            IItem drop = new HeartDropItem(contentLoader.ItemTexture, position, scale);
            AddDrop(drop);
	    }

        public void AddRupeeDrop(Vector2 position, float scale)
        {
            IItem drop = new OrangeRupeeItem(contentLoader.ItemTexture, position, scale);
            AddDrop(drop);
        }

        public void AddItemByID(int id, Vector2 position)
        {
            //Arbitrarily initialized to a BombItem
            IItem item = null;

            //Checking what kind of item the current Item really is.
            switch (id)
            {
                case 0:
                    item = new BombItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
                case 1:
                    item = new KeyItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
                case 2:
                    item = new RedPotionItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
                case 3:
                    item = new BluePotionItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
                case 4:
                    item = new TriforceItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.6));
                    break;
                case 5:
                    item = new MapItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
                case 6:
                    item = new CompassItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
                case 7:
                    item = new BowItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
                case 8:
                    item = new OrangeRupeeItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
                case 9:
                    item = new BlueRupeeItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
                case 10:
                    item = new HeartDropItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
                case 11:
                    item = new HeartContainerItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
                case 12:
                    item = new BoomerangItem(contentLoader.ItemTexture, position, (int)(contentLoader.scale * 1.5));
                    break;
            }

            //adding it to the list of items for this room.
            if (item != null)
                AddItem(item);
        }

        public void ClearItems()
        {
            this.ItemsInFrame = new List<IItem>();
	    }
    }
}
