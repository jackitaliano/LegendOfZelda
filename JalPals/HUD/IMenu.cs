using Microsoft.Xna.Framework.Graphics;
using System;

namespace JalPals.HUD
{
    public interface IMenu
    {
        public int currentID { get; set; }
        public Boolean transition { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void DrawDead(SpriteBatch spriteBatch);
        void DrawWin(SpriteBatch spriteBatch);
        void DrawTitle(Game1 game, SpriteBatch spriteBatch);
        public void MoveCursor(int x, int y);
        public int cursorLoc { get; set; }
        void Update();
        public void UpdateHeart(int delta);
        public void UpdateRupee();
        public void UpdateKey();
        public void UpdateBomb();
        public int HeartCount();
        public void AddHeartContainer();
    }
}

