using System;
namespace JalPals.Commands
{
    public class ExitCommand : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public ExitCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            Console.WriteLine("Exiting...");
            game.Exit();
        }
    }
}

