namespace JalPals.Commands
{
    public class LinkSword : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkSword(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.Sword();
        }
    }
}

