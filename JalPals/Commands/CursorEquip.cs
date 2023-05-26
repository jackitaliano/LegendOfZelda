namespace JalPals.Commands
{
    public class CursorEquip : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public CursorEquip(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Link.LinkInventory.Equip(game.menu.cursorLoc);
        }
    }
}

