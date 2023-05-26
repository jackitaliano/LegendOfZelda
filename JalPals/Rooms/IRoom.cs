using JalPals.Blocks2;
using JalPals.Collision;
using JalPals.Doors;
using JalPals.Enemies;
using JalPals.Items;
using JalPals.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace JalPals.Rooms
{
    public interface IRoom
    {
        void Draw(SpriteBatch spriteBatch);
        void DrawRoomBlocks(SpriteBatch spriteBatch);
        void DrawRoomEntities(SpriteBatch spriteBatch);

        void Update();
        List<IBlock2> blocks { get; set; }
        List<IBlock2> collideable { get; set; }
        List<IGameObject> wallRectangles { get; set; }
        List<IDoor> doors { get; set; }
        bool RoomCleared { get; }
        CollisionManager CollisionManager { get; }
        EnemyManager EnemyManager { get; }
        IItemManager ItemManager { get; }
        IProjectileManager ProjectileManager { get; }
        Vector2 RoomOffset { get; set; }
        int RoomID { get; set; }
        RoomManager _thisRoomManager { get; set; }
    }
}

