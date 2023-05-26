using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace JalPals.Projectiles
{
    public interface IProjectileManager
    {
        List<IProjectile> projectiles { get; set; }
        void AddFireball(Vector2 position, Vector2 velocity, bool isFriendly);
        void AddBoomerang(Vector2 position, Vector2 velocity, bool isFriendly);
        void AddArrowUp(Vector2 position, float scale, bool isFriendly);
        void AddArrowRight(Vector2 position, float scale, bool isFriendly);
        void AddArrowDown(Vector2 position, float scale, bool isFriendly);
        void AddArrowLeft(Vector2 position, float scale, bool isFriendly);
        void AddHitBox(Vector2 position, Vector2 dimensions, bool isFriendly);
        void RemoveItem(IProjectile projectile);
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }

}

