namespace JalPals.Commands
{
    public class Menu : ICommand
    {
        public ExecutionStatus Status { get; set; }
        private Game1 game;

        public Menu(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            if (game.gameState == GameStates.PLAY)
            {
                game.gameState = GameStates.PAUSE;
                game.menu.transition = true;

            }
            else if (game.gameState == GameStates.PAUSE)
            {
                game.gameState = GameStates.PLAY;
                game.menu.transition = true;
            }

            if (game.gameState == GameStates.TITLE)
            {
                game.changeState(GameStates.PLAY);
            }

        }
    }
}

