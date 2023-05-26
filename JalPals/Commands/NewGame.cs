using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace JalPals.Commands
{
    public class NewGame : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public NewGame(Game1 game)
        {
           
            this.game = game;

        }

        public void Execute()
        {
            game.restartGame();
        }
    }
}

