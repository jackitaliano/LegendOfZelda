namespace JalPals.Commands
{
    public class LinkMoveDown : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkMoveDown(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.MoveDown();
        }
    }
}

