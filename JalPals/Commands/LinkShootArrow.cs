namespace JalPals.Commands
{
    public class LinkShootArrow : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkShootArrow(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.UseArrow();
        }
    }
}