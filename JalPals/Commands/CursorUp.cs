namespace JalPals.Commands
{
    public class CursorUp : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public CursorUp(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.menu.MoveCursor(0, -1);
        }
    }
}

