using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Sprites
{
    public interface ISprite : IGameObject
    {
        public Vector2 Position { get; set; }
        public int enemyHealth { get; set; }

        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}

