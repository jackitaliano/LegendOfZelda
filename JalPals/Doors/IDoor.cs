using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace JalPals.Doors
{
    public enum DoorSide
    {
        BOTTOM,
        LEFT,
        TOP,
        RIGHT
    }
    public interface IDoor : IGameObject
    {

        public DoorSide doorSide { get; }
        public Boolean isOpen { get; set; }
        void Draw(SpriteBatch spriteBatch, Vector2 offset);
        void Update();
        void SetDoorOpen();
    }
}

