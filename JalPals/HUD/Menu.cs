using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using JalPals.Inventory;
using JalPals.Items;
using System;

namespace JalPals.HUD
{
    public class Menu : IMenu
    {
        /*
         * Public properties
         */
        public int currentID { get; set; }
        public Boolean transition { get; set; }
        public int cursorLoc { get; set; }

        /*
         * Textures...
         * Probably should condense into a single sprite sheet but I am lazy.
         */
        private Texture2D hudSheet;
        private Texture2D mapSheet;
        private Texture2D blackRectangle;
        private Texture2D hearts;
        private Texture2D pauseSheet;
        private Texture2D title;
        private Texture2D cursor;
        private Texture2D win;
        private SpriteFont gameFont;

        /*
         * Various private variables
         */
        public int rupeeCount;
        public int keyCount;
        private int bombCount;
        private IInventory inventory;


        /*
         * Animation variables
         */
        private int yOffset;
        private const int animationLength = 30;
        private int cursorFrame;
        private Boolean isPaused;
        private int transitionFrame;
        private int titleFrame;
        private int confetti;
        private Random rnd;
        

        /*
         * Map variables
         */
        private int mapX, mapY;
        private const int mapWidth = 94;
        private const int mapHeight = 46;

        /*
         * Heart variables
         */
        private Rectangle fullHeart = new Rectangle(0, 0, 7, 8);
        private Rectangle halfHeart = new Rectangle(7, 0, 7, 8);
        private Rectangle emptyHeart = new Rectangle(14, 0, 7, 8);
        private int heartCount;
        private int maxHealth;
        private Rectangle[] heartArr = new Rectangle[30];

        public Menu(ContentManager content, IInventory inventory)
        {
            // Load textures... wowza that's a lot
            this.hudSheet = content.Load<Texture2D>("HUD");
            this.mapSheet = content.Load<Texture2D>("maps");
            this.blackRectangle = content.Load<Texture2D>("black");
            this.hearts = content.Load<Texture2D>("hearts");
            this.pauseSheet = content.Load<Texture2D>("menu_layout");
            this.cursor = content.Load<Texture2D>("cursor");
            this.gameFont = content.Load<SpriteFont>("Zelda");
            this.title = content.Load<Texture2D>("title_animation");
            this.win = content.Load<Texture2D>("win");
            this.inventory = inventory;


            // Initial variable states
            rupeeCount = 0;
            cursorLoc = 0;
            confetti = 0;
            keyCount = 0;
            bombCount = 0;
            transitionFrame = 0;
            cursorFrame = 0;
            isPaused = false;

            // Full hearts.heartCount is the number of half hearts Link has.
            heartCount = 10;
            maxHealth = heartCount;
            heartArr[0] = fullHeart;
            heartArr[1] = fullHeart;
            heartArr[2] = fullHeart;
            heartArr[3] = fullHeart;
            heartArr[4] = fullHeart;
        }

        public void Update()
        {
            if (this.transition && !this.isPaused && transitionFrame < animationLength)
            {
                yOffset += 16;
                transitionFrame++;
            }
            else if (this.transition && this.isPaused && transitionFrame < animationLength)
            {
                yOffset -= 16;
                transitionFrame++;
            }
            else if (this.transition && !this.isPaused)
            {
                isPaused = true;
                transitionFrame = 0;
                this.transition = false;
            }
            else if (this.transition && this.isPaused)
            {
                isPaused = false;
                transitionFrame = 0;
                this.transition = false;
            }

            if (isPaused)
            {
                cursorFrame++;
                if (cursorFrame >= 30)
                {
                    cursorFrame = 0;
                }
            }
            UpdateKey();
            UpdateBomb();
            UpdateRupee();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle srcRec;
            Rectangle destRec;

            // Menu things
            if (this.transition || this.isPaused)
            {
                // Black box to cover blocks
                srcRec = new Rectangle(0, 0, 154, 64);
                destRec = new Rectangle(0, 160, 768, yOffset);
                spriteBatch.Draw(blackRectangle, destRec, srcRec, Color.White);

                // Menu layout template
                srcRec = new Rectangle(0, 0, 416, 288);
                destRec = new Rectangle(64, -432 + yOffset, 624, 432);
                spriteBatch.Draw(pauseSheet, destRec, srcRec, Color.White);

                // Inventory items
                inventory.Draw(spriteBatch, yOffset);

                // Cursor
                DrawCursor(spriteBatch);

            }

            // Draw hearts
            for (int i = 0; i < maxHealth; i++)
            {
                if (i < 6)
                { 
                    Rectangle heartDest = new Rectangle(600 + 24 * i, 76 + yOffset, 21, 24);
                    spriteBatch.Draw(hearts, heartDest, heartArr[i], Color.White);
                }
                else
                {
                    Rectangle heartDest = new Rectangle(600 + 24 * (i- 6), 100 + yOffset, 21, 24);
                    spriteBatch.Draw(hearts, heartDest, heartArr[i], Color.White);
                }

            }

            // Draw text
            Vector2 position = new Vector2(75, 50 + yOffset);
            spriteBatch.DrawString(gameFont, "LEVEL-1", position, Color.White, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);

            position = new Vector2(575, 50 + yOffset);
            spriteBatch.DrawString(gameFont, "-LIFE-", position, Color.Red, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);

            position = new Vector2(290, 52 + yOffset);
            spriteBatch.DrawString(gameFont, "X" + rupeeCount, position, Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0.5f);

            position = new Vector2(290, 100 + yOffset);
            spriteBatch.DrawString(gameFont, "X" + keyCount, position, Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0.5f);

            position = new Vector2(290, 126 + yOffset);
            spriteBatch.DrawString(gameFont, "X" + bombCount, position, Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0.5f);

            // Draw HUD icons
            srcRec = new Rectangle(0, 0, 154, 64);
            destRec = new Rectangle(264, 50 + yOffset, 231, 96);
            spriteBatch.Draw(hudSheet, destRec, srcRec, Color.White);

            // Draw equipped item
            inventory.DrawEquipped(spriteBatch, yOffset);

            // Draw map
            mapX = mapWidth * (currentID % 5);
            mapY = mapHeight * (currentID / 5);

            srcRec = new Rectangle(mapX, mapY, mapWidth, mapHeight);
            destRec = new Rectangle(65, 80 + yOffset, (int)(mapWidth * 1.5f), (int)(mapHeight * 1.5f));
            spriteBatch.Draw(mapSheet, destRec, srcRec, Color.White);


        }

        public void DrawDead(SpriteBatch spriteBatch)
        {
            Rectangle srcRec = new Rectangle(0, 0, 154, 64);
            Rectangle destRec = new Rectangle(0, 160, 768, 1000);
            spriteBatch.Draw(blackRectangle, destRec, srcRec, Color.White);

            Vector2 position = new Vector2(300, 300);
            spriteBatch.DrawString(gameFont, "GAME OVER", position, Color.White, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);

            position = new Vector2(230, 500);
            spriteBatch.DrawString(gameFont, "PRESS R TO RESET", position, Color.White, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);
            position = new Vector2(237, 550);
            spriteBatch.DrawString(gameFont, "PRESS Q TO QUIT", position, Color.White, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);

        }

        public void DrawWin(SpriteBatch spriteBatch)
        {
            if(confetti > 2000)
            {
                confetti = 0;
            }

            Rectangle srcRec = new Rectangle(0, 0, 154, 64);
            Rectangle destRec = new Rectangle(0, 160, 768, 1000);
            spriteBatch.Draw(blackRectangle, destRec, srcRec, Color.White);

            srcRec = new Rectangle(0, 0, 768, 1000);
            destRec = new Rectangle(0, -768 + confetti, 768, 1000);
            spriteBatch.Draw(win, destRec, srcRec, Color.White);

            Vector2 position = new Vector2(150, 300);
            spriteBatch.DrawString(gameFont, "CONGLASDURATIONS YOU WON!", position, Color.White, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);

            position = new Vector2(230, 500);
            spriteBatch.DrawString(gameFont, "PRESS R TO RESET", position, Color.White, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);
            position = new Vector2(237, 550);
            spriteBatch.DrawString(gameFont, "PRESS Q TO QUIT", position, Color.White, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);

            confetti += 7;
        }

        public void DrawTitle(Game1 game, SpriteBatch spriteBatch)
        {
            Rectangle destRec;
            Rectangle srcRec;
            int frame = (titleFrame / 15) % 4;
            int splash = (titleFrame / 4) % 2;
            int waterfall = (titleFrame / 4) % 4;

            switch (frame)
            {
                case 0:
                    srcRec = new Rectangle(1, 0, 256, 224);
                    break;
                case 1:
                    srcRec = new Rectangle(257, 0, 256, 224);
                    break;
                case 2:
                    srcRec = new Rectangle(513, 0, 256, 224);
                    break;
                default:
                    srcRec = new Rectangle(769, 0, 256, 224);
                    break;
            }

            titleFrame++;
            destRec = new Rectangle(0, 0, 768, 720);
            spriteBatch.Draw(title, destRec, srcRec, Color.White);

            switch (splash)
            {
                case 0:
                    srcRec = new Rectangle(1026, 0, 32, 16);
                    break;
                case 1:
                    srcRec = new Rectangle(1059, 0, 32, 16);
                    break;
            }
            destRec = new Rectangle(240, 518, 96, 48);
            spriteBatch.Draw(title, destRec, srcRec, Color.White);

            switch(waterfall)
            {
                case 0:
                    srcRec = new Rectangle(1026, 39, 32, 48);
                    break;
                case 1:
                    srcRec = new Rectangle(1059, 39, 32, 48);
                    break;
                case 2:
                    srcRec = new Rectangle(1092, 39, 32, 48);
                    break;
                case 3:
                    srcRec = new Rectangle(1125, 39, 32, 48);
                    break;
            }
            destRec = new Rectangle(240, 572, 96, 144);
            spriteBatch.Draw(title, destRec, srcRec, Color.White);

            Vector2 position = new Vector2(345, 416);
            spriteBatch.DrawString(gameFont, "2023 JALPALS", position, Color.White, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);

            position = new Vector2(250, 475);
            spriteBatch.DrawString(gameFont, "PRESS ENTER KEY", position, Color.White, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);
        }

        public void DrawCursor(SpriteBatch spriteBatch)
        {
            Rectangle srcRec, destRec;
            if (cursorFrame < 15)
            {
                srcRec = new Rectangle(0, 0, 32, 32);
            }
            else
            {
                srcRec = new Rectangle(32, 0, 32, 32);
            }
            destRec = new Rectangle(400+(54*(cursorLoc%5)), -360 + yOffset + (50 * (cursorLoc / 5)), 48, 48);
            spriteBatch.Draw(cursor, destRec, srcRec, Color.White);
        }

        public void MoveCursor(int x, int y)
        {
            if (x == 1 && cursorLoc < 9)
            {
                cursorLoc++;
            }
            else if (x == -1 && cursorLoc > 0)
            {
                cursorLoc--;
            }
            else if (y == 1 && cursorLoc < 5)
            {
                cursorLoc += 5;
            }
            else if(y == -1 && cursorLoc > 4)
            {
                cursorLoc -= 5;
            }
        }

        public void UpdateHeart(int delta)
        {
            //If we are adding more hearts than Link has room for, set his heartCount to the number of hearts times 2.
            if (heartCount + delta > maxHealth)
            {
                heartCount = maxHealth;
            }
            else if (heartCount + delta < 0)
            {
                heartCount = 0;
            }
            else
            {
                heartCount += delta;
            }

            //Calculate which hearts that need to be drawn and where.
            Boolean halfHeartIndicator = heartCount % 2 != 0;
            for(int i = 0; i < maxHealth / 2; i++)
            {
                if(heartCount >= (i + 1) * 2)
                {
                    heartArr[i] = fullHeart;
                }else if(halfHeartIndicator)
                {
                    heartArr[i] = halfHeart;
                    halfHeartIndicator = false;
                }
                else
                {
                    heartArr[i] = emptyHeart;
                }
            }
        }

        public void UpdateRupee()
        {
            rupeeCount = inventory.RupeeCount();
        }

        public void UpdateKey()
        {
            keyCount = inventory.KeyCount();
        }

        public void UpdateBomb()
        {
            bombCount = inventory.BombCount();
        }

        public int HeartCount()
        {
            return heartCount;
        }

        public void AddHeartContainer()
        {
            if (maxHealth < 24)
            {
                maxHealth = maxHealth + 2;
                heartArr[(maxHealth / 2) - 1] = fullHeart;
                heartCount = maxHealth;
            }
            this.UpdateHeart(2);
        }
    }
}

