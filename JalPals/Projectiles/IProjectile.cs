using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Projectiles
{
    public interface IProjectile : IGameObject
    {
        Vector2 Position { get; set; }
        public bool Friendly { get; set; }
        public bool Visible { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void Update();
    }
}

