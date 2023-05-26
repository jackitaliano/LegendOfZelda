using System;
using JalPals.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace JalPals.Enemies
{
    public abstract class Enemy : ISprite
    {

        public abstract Rectangle collisionRectangle { get; set; }
        public abstract Vector2 velocityVector { get; set; }
        public abstract Vector2 Position { get; set; }
        public abstract int enemyHealth { get; set; }



        public Enemy(Texture2D spriteSheet, Vector2 position)
        {
            
        }


        public abstract void TakeDamage();

        //public abstract bool IsDead();

        public abstract void ResolveCollision(IGameObject obj, int side);

        public GameObjectType getType()
        {
            return GameObjectType.ENEMY;
        }


        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);

        public void EnemyDeathAction()
        {
        }
    }
}

