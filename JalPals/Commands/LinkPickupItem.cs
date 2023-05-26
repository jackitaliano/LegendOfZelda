namespace JalPals.Commands
{
    public class LinkPickupItem : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public LinkPickupItem(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {

        }
    }
}