using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace JalPals.Projectiles
{
    public class ProjectileManager : IProjectileManager
    {
        // Properties
        public List<IProjectile> projectiles { get; set; }

        // Private variables
        private Texture2D enemyTexture;
        private Texture2D bossTexture;
        private Texture2D linkTexture;
        private ContentLoader contentLoader;
        private int arrowVelocity;


        public ProjectileManager(ContentLoader contentLoader)
        {
            this.contentLoader = contentLoader;
            enemyTexture = contentLoader.EnemyTexture;
            bossTexture = contentLoader.BossTexture;
            linkTexture = contentLoader.LinkMovementTexture;
            projectiles = new List<IProjectile>();
            arrowVelocity = 5;
        }

        public void AddFireball(Vector2 position, Vector2 velocity, bool isFriendly)
        {
            projectiles.Add(new Fireball(bossTexture, position, velocity, isFriendly));
        }

        public void AddBoomerang(Vector2 position, Vector2 velocity, bool isFriendly)
        {
            projectiles.Add(new Boomerang(enemyTexture, position, velocity, isFriendly));
        }

        public void AddArrowUp(Vector2 position, float scale, bool isFriendly)
        {
            projectiles.Add(new GreenArrowUp(linkTexture, position, arrowVelocity, scale, isFriendly));
        }

        public void AddArrowRight(Vector2 position, float scale, bool isFriendly)
        {
            projectiles.Add(new GreenArrowRight(linkTexture, position, arrowVelocity, scale, isFriendly));
        }

        public void AddArrowDown(Vector2 position, float scale, bool isFriendly)
        {
            projectiles.Add(new GreenArrowDown(linkTexture, position, arrowVelocity, scale, isFriendly));
        }

        public void AddArrowLeft(Vector2 position, float scale, bool isFriendly)
        {
            projectiles.Add(new GreenArrowLeft(linkTexture, position, arrowVelocity, scale, isFriendly));
        }

        public void AddHitBox(Vector2 position, Vector2 dimensions, bool isFriendly)
        {
            projectiles.Add(new HitBox(linkTexture, position, dimensions, isFriendly));
        }

        public void RemoveItem(IProjectile projectile)
        {
            projectiles.Remove(projectile);
        }

        public void Update()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i].Visible)
                {
                    projectiles[i].Update();

                }
                else
                {
                    projectiles.RemoveAt(i);
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IProjectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
    }
}

