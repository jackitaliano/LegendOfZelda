using JalPals.Collision;
using JalPals.Commands;
using JalPals.Controllers;
using JalPals.HUD;
using JalPals.Player;
using JalPals.Rooms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace JalPals;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private IController keyboardController;
    private IController mouseController;
    private CommandManager commandManager;
    public CollisionManager collisionManager;

    public ILink Link { get; set; }
    public IRoomManager roomManager { get; set; }
    public IMenu menu { get; set; }
    public GameStates gameState { get; set; }
    public ContentLoader content;

    public Vector2 StartPosition = new Vector2(364, 552);

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        // Set screen size to 3x native resolution
        _graphics.PreferredBackBufferWidth = 256 * 3;
        _graphics.PreferredBackBufferHeight = 240 * 3;
        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Initialize managers
        commandManager = new CommandManager(this);
        collisionManager = new CollisionManager();

        // Initialize controllers
        keyboardController = new KeyboardController(this, commandManager);
        mouseController = new MouseController(this, commandManager);

        

        base.Initialize();
    }

    protected override void LoadContent()
    {
        //_spriteBatch manages the sprites for the game.
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        //conent loads in all the pictures, sounds, and csv files. Basicallly anything that goes through the content folder
        //is handled and packaged in here.
        content = new ContentLoader(this);

        //This is the player object.
        Link = new Link(this, content, StartPosition, content.scale, null, null);

        //roomManager handles a lot of things, but it basically builds all of the rooms and their attibutes. Items, blocks, enemies, etc.
        roomManager = new RoomManager(Link, content.roomFiles, content, (int)content.scale, collisionManager);

        //menu is the heads up display and contains links hearts, items, the map, and more.
        menu = new HUD.Menu(Content, Link.LinkInventory);

        //Link.LinkItemManager = roomManager.currentRoom.ItemManager;
        Link.LinkProjManager = roomManager.currentRoom.ProjectileManager;
        Link.LinkState = new LinkWalkingUpState(Link);

        // Set initial gameState
        changeState(GameStates.TITLE);
    }

    protected override void Update(GameTime gameTime)
    {
        switch (gameState)
        {
            /* TITLE STATE */
            case GameStates.TITLE:
                keyboardController.Update();
                commandManager.Update();
                break;

            /* PLAY STATE */
            case GameStates.PLAY:

                //This will create the beeping sound when link is below one heart.
                if (Link.LinkHealth.LinkHealthVal < 2 && gameTime.TotalGameTime.Milliseconds % 20000 == 0)
                {
                    this.content.soundManager.Play("LowHealth");
                }

                keyboardController.Update();
                mouseController.Update();
                commandManager.Update();
                roomManager.Update();
                Link.Update();
                collisionManager.HandleCollisions();

                menu.Update();
                menu.currentID = roomManager.currentID;

                base.Update(gameTime);
                break;

            /* PAUSE STATE */
            case GameStates.PAUSE:
                keyboardController.Update();
                menu.Update();
                mouseController.Update();
                commandManager.Update();
                break;

            /* DEAD STATE */
            case GameStates.DEAD:
                keyboardController.Update();
                commandManager.Update();
                break;

            /* WIN STATE */
            case GameStates.WIN:
                keyboardController.Update();
                commandManager.Update();
                break;
        }

        /* Stuff to do regardless of state */
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

    }

    protected override void Draw(GameTime gameTime)
    {

        // Limit the frame rate to 30 frames per second
        float targetFrameTime = 1.0f / 30.0f;

        Thread.Sleep((int)(targetFrameTime * 1000));

        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        if (this.roomManager.InTransition) {
            roomManager.DrawTransition(_spriteBatch);
	    } else { 
            roomManager.Draw(_spriteBatch);
            Link.Draw(_spriteBatch);
	    }

        menu.Draw(_spriteBatch);

        /* State specific code */
        switch (gameState)
        {
            /* TITLE STATE */
            case GameStates.TITLE:
                menu.DrawTitle(this, _spriteBatch);
                break;

            /* PLAY STATE */
            case GameStates.PLAY:
                // NO-OP
                break;

            /* PAUSE STATE */
            case GameStates.PAUSE:
                // NO-OP
                break;

            /* DEAD STATE */
            case GameStates.DEAD:
                menu.DrawDead(_spriteBatch);
                break;

            /* WIN STATE */
            case GameStates.WIN:
                menu.DrawWin(_spriteBatch);
                break;
        }


        base.Draw(gameTime);
        _spriteBatch.End();
    }

    public void restartGame()
    { 
        Link = new Link(this, content, StartPosition, content.scale, null , null);
        roomManager = new RoomManager(Link, content.roomFiles, content, (int)content.scale, this.collisionManager);
        menu = new HUD.Menu(Content, Link.LinkInventory);
        changeState(GameStates.TITLE);
    }

    public void changeState(GameStates newGameState)
    {
        /*
         * This is code we want to run JUST ONCE when changing game state
         */
        switch (newGameState)
        {
            /* TITLE STATE */
            case GameStates.TITLE:

                MediaPlayer.Volume = 0.3F;
                MediaPlayer.IsRepeating = true;

                gameState = GameStates.TITLE;

                MediaPlayer.Play(content.titleTheme);
                break;

            /* PLAY STATE */
            case GameStates.PLAY:
                gameState = GameStates.PLAY;

                MediaPlayer.Stop();
                MediaPlayer.Volume = 0.1F;
                MediaPlayer.Play(content.dungeonTheme);
                break;

            /* PAUSE STATE */
            case GameStates.PAUSE:
                gameState = GameStates.PAUSE;
                // NO-OP
                break;

            /* DEAD STATE */
            case GameStates.DEAD:
                gameState = GameStates.DEAD;
                MediaPlayer.Stop();
                MediaPlayer.Volume = 0.10F;
                MediaPlayer.Play(content.gameOverTheme);
                break;

            /* WIN STATE */
            case GameStates.WIN:
                gameState = GameStates.WIN;
                MediaPlayer.Stop();
                MediaPlayer.Volume = 0.2F;
                MediaPlayer.Play(content.celebrationTheme);
                break;
        }

    }
}


