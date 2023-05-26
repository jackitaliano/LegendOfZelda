namespace JalPals.Commands
{
    public class LinkUseEquipped : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkUseEquipped (Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.UseEquipped();
        }
    }
}