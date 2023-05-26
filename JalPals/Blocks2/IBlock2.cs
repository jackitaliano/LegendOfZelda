using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JalPals.Blocks2
{
    public interface IBlock2 : IGameObject
    {
        void Draw(SpriteBatch spriteBatch, Vector2 offset);
        void Update();
        bool Moveable { get; set; }
    }
}

