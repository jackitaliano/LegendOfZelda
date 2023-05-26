namespace JalPals.Commands
{
    public class LinkShootFireball : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkShootFireball(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.UseFireball();
        }
    }
}