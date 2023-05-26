namespace JalPals.Commands
{
    public class LinkMoveRight : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkMoveRight(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.MoveRight();
        }
    }
}

