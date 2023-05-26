namespace JalPals.Commands
{
    public class CursorDown : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public CursorDown(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.menu.MoveCursor(0, 1);
        }
    }
}

