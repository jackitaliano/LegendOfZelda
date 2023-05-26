using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Sprites
{
    public class TextSprite : ISprite
    {
        public string ID { get; }
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public SpriteFont Font { get; set; }
        public Color Color { get; set; }
        public Rectangle destRectangle { get; set; }
        public Rectangle collisionRectangle { get; set; }
        public Vector2 velocityVector { get; set; }
        public int enemyHealth { get; set; }

        public TextSprite(string id, SpriteFont font, string text, Vector2 position, Color color)
        {
            this.ID = id;
            this.Position = position;
            this.Text = text;
            this.Font = font;
            this.Color = color;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color);
        }

        public void ChangeText(string text)
        {
            this.Text = text;
        }

        public GameObjectType getType()
        {
            return GameObjectType.TEXT;
        }

        public void ResolveCollision(IGameObject obj1, int side)
        {

        }
    }
}

