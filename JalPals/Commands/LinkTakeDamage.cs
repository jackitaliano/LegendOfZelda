namespace JalPals.Commands
{
    public class LinkTakeDamage : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;


        public LinkTakeDamage(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.TakeDamage();
        }
    }
}

