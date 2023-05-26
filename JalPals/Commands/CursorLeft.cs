namespace JalPals.Commands
{
    public class CursorLeft : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public CursorLeft(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.menu.MoveCursor(-1, 0);
        }
    }
}

