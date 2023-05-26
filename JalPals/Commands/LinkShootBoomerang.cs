namespace JalPals.Commands
{
    public class LinkShootBoomerang : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkShootBoomerang(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.UseBoomerang();
        }
    }
}