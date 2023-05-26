namespace JalPals.Commands
{
    public class CursorRight : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public CursorRight(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.menu.MoveCursor(1, 0);
        }
    }
}

