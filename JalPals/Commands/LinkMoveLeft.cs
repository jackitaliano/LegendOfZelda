namespace JalPals.Commands
{
    public class LinkMoveLeft : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkMoveLeft(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.MoveLeft();
        }
    }
}

