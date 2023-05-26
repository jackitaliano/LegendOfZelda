namespace JalPals.Commands
{
    public class LinkWand : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkWand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.Wand();
        }
    }
}

