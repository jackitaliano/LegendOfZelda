using JalPals.Sounds;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;

namespace JalPals
{
    /* ConentLoader is used to lighten the amount of code of the Game1 class. It is meant to handle all of the initialization of the
     * files in the projects "Content" folder. This includes things like images, sounds, songs, and csv files. It loads them in so the Game1
     * doesn't have to have a lot of clutter.
     */
    public class ContentLoader
    {
        public float scale = 3.0f;
        public ContentManager content;
        public SpriteFont gameFont;
        public Texture2D LinkMovementTexture;
        public Texture2D EnemyTexture;
        public Texture2D BossTexture;
        public Texture2D DungeonTexture;
        public Texture2D ItemTexture;
        public Texture2D FireTexture;
        public Dictionary<string, Sound> soundDictionary;
        public Song dungeonTheme;
        public Song celebrationTheme;
        public Song titleTheme;
        public Song gameOverTheme;
        public SoundManager soundManager;
        public string[] roomFiles;
        public Texture2D Title;

        public ContentLoader(Game1 game)
        {
            //This allows us to acess the content folder for the project. Very important.
            content = game.Content;

            //This is the game font that we uploaded. It will be referenced by any text sprites.
            gameFont = content.Load<SpriteFont>("Zelda");

            //This is the file that holds all of links movement sprites.
            LinkMovementTexture = content.Load<Texture2D>("link");
            ItemTexture = LinkMovementTexture;

            //This is the file that hols all of the enemy sprites.
            EnemyTexture = content.Load<Texture2D>("dungeonEnemiesTransparent");
            BossTexture = content.Load<Texture2D>("bosses");
            FireTexture = content.Load<Texture2D>("fire");

            // This is the file that holds all blocks/doors/walls/etc.
            DungeonTexture = content.Load<Texture2D>("dungeon_spritesheet");

            //soundDictionary, dungeonTheme, and soundManager handle all of the sounds and music for the game.
            Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();
            dungeonTheme = content.Load<Song>("DungeonTheme");
            celebrationTheme = content.Load<Song>("celebration");
            titleTheme = content.Load<Song>("TitleTheme");
            gameOverTheme = content.Load<Song>("GameOverTheme");

            soundManager = new SoundManager(soundDictionary, content);

            //These allow the user to access the csv files.
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            roomFiles = Directory.GetFiles(projectDirectory + "/Content/csv");

            Title = content.Load<Texture2D>("Title");

        }
    }
}
