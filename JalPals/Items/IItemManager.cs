using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Items
{
    public interface IItemManager
    {
        public IList<IItem> ItemsInFrame { get; set; }
        public IList<IItem> ItemDrops { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void DrawItems(SpriteBatch spriteBatch);
        void DrawDrops(SpriteBatch spriteBatch);
        void Update();
        void AddItem(IItem item);
        void DeleteItem(IItem item);
        void AddHeartDrop(Vector2 position, float scale);
        void AddRupeeDrop(Vector2 position, float scale);
        void AddItemByID(int id, Vector2 position);
        void ClearItems();
    }
}

