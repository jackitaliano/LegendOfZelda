using JalPals.Commands;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace JalPals.Controllers
{
    public class KeyboardController : IController
    {
        private KeyboardState currentState;
        private KeyboardState previousState;

        private CommandManager commandManager;

        private Dictionary<Keys, ICommand> heldCommands;
        private Dictionary<Keys, ICommand> pressedCommands;

        private Dictionary<Keys, ICommand> playHeldCommands;
        private Dictionary<Keys, ICommand> playPressedCommands;

        private Dictionary<Keys, ICommand> pauseHeldCommands;
        private Dictionary<Keys, ICommand> pausePressedCommands;

        private Dictionary<Keys, ICommand> titleHeldCommands;
        private Dictionary<Keys, ICommand> titlePressedCommands;

        private ICommand defaultCommand;
        private Game1 game;

        public KeyboardController(Game1 game, CommandManager commandManager)
        {
            this.commandManager = commandManager;
            this.currentState = Keyboard.GetState();
            this.previousState = Keyboard.GetState();
            this.game = game;

            defaultCommand = new LinkIdle(game);

            playHeldCommands = new Dictionary<Keys, ICommand>()
            {
                { Keys.Right, new LinkMoveRight(game) },
                { Keys.D, new LinkMoveRight(game) },

                { Keys.Left, new LinkMoveLeft(game) },
                { Keys.A, new LinkMoveLeft(game) },

                { Keys.Up, new LinkMoveUp(game) },
                { Keys.W, new LinkMoveUp(game) },

                { Keys.Down, new LinkMoveDown(game) },
                { Keys.S, new LinkMoveDown(game) },
                { Keys.Q, new ExitCommand(game) },
            };

            playPressedCommands = new Dictionary<Keys, ICommand>()
            {
                { Keys.L, new LinkWand(game) },
                { Keys.D1, new LinkHit(game) },
                { Keys.D2, new LinkShootBoomerang(game) },
                { Keys.D3, new LinkShootArrow(game) },
                { Keys.D4, new LinkShootFireball(game) },
                { Keys.B, new LinkUseEquipped(game) },

                { Keys.Z, new LinkWand(game) },
                { Keys.Space, new LinkSword(game) },

                {Keys.R, new NewGame(game) },

                {Keys.Enter, new Menu(game) }
            };

            pauseHeldCommands = new Dictionary<Keys, ICommand>()
            {
                { Keys.Q, new ExitCommand(game) }
            };

            pausePressedCommands = new Dictionary<Keys, ICommand>()
            {
                { Keys.Right, new CursorRight(game) },
                { Keys.D, new CursorRight(game) },

                { Keys.Left, new CursorLeft(game) },
                { Keys.A, new CursorLeft(game) },

                { Keys.Up, new CursorUp(game) },
                { Keys.W, new CursorUp(game) },

                { Keys.Down, new CursorDown(game) },
                { Keys.S, new CursorDown(game) },

                {Keys.Enter, new Menu(game) },
                {Keys.R, new NewGame(game) },

                {Keys.E, new CursorEquip(game) }
            };

            titleHeldCommands = new Dictionary<Keys, ICommand>()
            {
                { Keys.Q, new ExitCommand(game) },

            };

            titlePressedCommands = new Dictionary<Keys, ICommand>()
            {
                {Keys.Enter, new Menu(game) },
                {Keys.R, new NewGame(game) }
            };
        }

        public void Update()
        {
            switch (game.gameState)
            {
                case (GameStates.TITLE):
                    heldCommands = titleHeldCommands;
                    pressedCommands = titlePressedCommands;
                    break;
                case (GameStates.DEAD):
                    heldCommands = titleHeldCommands;
                    pressedCommands = titlePressedCommands;
                    break;
                case (GameStates.WIN):
                    heldCommands = titleHeldCommands;
                    pressedCommands = titlePressedCommands;
                    break;
                case (GameStates.PLAY):
                    heldCommands = playHeldCommands;
                    pressedCommands = playPressedCommands;
                    break;
                case (GameStates.PAUSE):
                    heldCommands = pauseHeldCommands;
                    pressedCommands = pausePressedCommands;
                    break;
            }

            // Update current keyboard state
            currentState = Keyboard.GetState();

            // Check if valid current state
            //if (ValidCurrentState())
            HandleState();

            // Update previous keyboard state
            previousState = currentState;
        }

        private void HandleState()
        {
            Keys[] keysPressed = currentState.GetPressedKeys();


            if (keysPressed.Length == 0)
                return;

            foreach (var key in keysPressed)
            {
                ICommand command = GetCommand(key);
                commandManager.AddNewCommand(command);
            }
        }

        private ICommand GetCommand(Keys key)
        {
            if (pressedCommands.ContainsKey(key) && !RepeatState())
                return pressedCommands[key];
            if (heldCommands.ContainsKey(key))
                return heldCommands[key];
            return defaultCommand;
        }

        private bool RepeatState()
        {
            // Check state has changed
            return currentState == previousState;
        }
    }
}

