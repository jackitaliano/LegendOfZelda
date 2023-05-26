using JalPals.Commands;
using Microsoft.Xna.Framework.Input;

namespace JalPals.Controllers
{
    public class MouseController : IController
    {
        private MouseState currentState;
        private MouseState previousState;
        private Game1 game;

        private CommandManager commandManager;
        private int screenWidth;
        private int screenHeight;

        public MouseController(Game1 game, CommandManager commandManager)
        {
            this.game = game;
            this.commandManager = commandManager;

            this.currentState = Mouse.GetState();
            this.previousState = currentState;

            this.screenWidth = game.GraphicsDevice.Viewport.Width;
            this.screenHeight = game.GraphicsDevice.Viewport.Height;
        }

        public void Update()
        {
            // Update current mouse state
            this.currentState = Mouse.GetState();

            // Check if valid current state
            if (!ValidCurrentState())
                return;

            HandleState();

            // Update previous mouse state
            previousState = currentState;
        }

        private void HandleState()
        {
            // Right click
            if (currentState.RightButton == ButtonState.Pressed)
                RightClick();
            // Left click
            else if (currentState.LeftButton == ButtonState.Pressed)
                LeftClick();
        }

        private bool ValidCurrentState()
        {
            // Check state has changed
            return currentState != previousState;
        }

        private void RightClick()
        {
            commandManager.AddNewCommand(new ExitCommand(game));
        }

        private void LeftClick()
        {
            QuadClick();
        }

        private void QuadClick()
        {
            int x = currentState.X;
            int y = currentState.Y;

            // 1st quadrant state
            if (x <= screenWidth / 2)
            {
            }
            else
            {
            }
        }
    }
}

