namespace JalPals.Commands
{
    public class LinkIdle : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkIdle(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.Idle();
        }
    }
}

