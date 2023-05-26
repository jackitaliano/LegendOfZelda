namespace JalPals.Commands
{
    public class LinkHit : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkHit(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.Hit();
        }
    }
}

