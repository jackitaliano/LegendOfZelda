namespace JalPals.Commands
{
    public class LinkMoveUp : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkMoveUp(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.MoveUp();
        }
    }
}

