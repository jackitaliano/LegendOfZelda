Game Controls:

 heldCommands = new Dictionary<Keys, ICommand>()
            {
                { Keys.Right, new LinkMoveRight(game) },
                { Keys.D, new LinkMoveRight(game) },

                { Keys.Left, new LinkMoveLeft(game) },
                { Keys.A, new LinkMoveLeft(game) },

                { Keys.Up, new LinkMoveUp(game) },
                { Keys.W, new LinkMoveUp(game) },

                { Keys.Down, new LinkMoveDown(game) },
                { Keys.S, new LinkMoveDown(game) },
                { Keys.Q, new ExitCommand(game) },
            };

 pressedCommands = new Dictionary<Keys, ICommand>()
            {
                { Keys.E, new LinkTakeDamage(game) },

                { Keys.L, new LinkWand(game) },
                { Keys.D1, new LinkHit(game) },
                { Keys.D2, new LinkShootBoomerang(game) },
                { Keys.D3, new LinkShootArrow(game) },
                { Keys.D4, new LinkShootFireball(game) },

                { Keys.Z, new LinkWand(game) },
                { Keys.N, new LinkSword(game) },

                {Keys.U, new SwapItemLeft(game) },
                {Keys.I, new SwapItemRight(game) },
                {Keys.T, new BlockUpdate(game) },
                {Keys.Y, new BlockUpdate(game) },

                {Keys.P, new NextEnemy(game) },
                {Keys.O, new PreviousEnemy(game) }
            };

 Next Room: Click on right half of screen.
 Previous Room: Click on left half of screen.


Known Bugs/Issues:
Link's projectiles arent perfectly matched with his sprite, so they look funky. Same with projectile motion like the boombeang
and the arrows/fireballs not disappearing. The U key must be pressed in order to load items into links "inventory" and from there
be swapped L/R. Additionally the U key will show link "picking up" and item. 