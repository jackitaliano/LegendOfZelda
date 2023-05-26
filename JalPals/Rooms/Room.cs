using JalPals.Blocks2;
using JalPals.Collision;
using JalPals.Doors;
using JalPals.Enemies;
using JalPals.Items;
using JalPals.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Reflection.Metadata;

namespace JalPals.Rooms;

public class Room : IRoom
{
    // Public properties
    public List<IBlock2> blocks { get; set; }
    public List<IDoor> doors { get; set; }
    public List<IBlock2> collideable { get; set; }
    public List<IGameObject> wallRectangles { get; set; }
    public RoomManager _thisRoomManager { get; set; }
    public int RoomID { get; set; }
    public bool RoomCleared { get { return EnemyManager.roomEnemies.Count == 0; } }

    private SpriteFont font;

    public CollisionManager CollisionManager { get; }
    public EnemyManager EnemyManager { get; }
    public IItemManager ItemManager { get; }
    public IProjectileManager ProjectileManager { get; }
    public Vector2 RoomOffset { get; set; }

    public Room(int id,
    RoomManager rm,
    EnemyManager enemyManager,
    ItemManager itemManager,
    ProjectileManager projectileManager,
    List<IBlock2> blocks,
    List<IBlock2> collidableBlocks,
    SpriteFont font
    )
    {
        this.blocks = blocks;
        this.collideable = collidableBlocks;
        this.EnemyManager = enemyManager;
        this.ItemManager = itemManager;
        this.ProjectileManager = projectileManager;
        this.RoomOffset = new Vector2(0, 0);
        this.font = font;

        RoomID = id;
        _thisRoomManager = rm;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawRoomBlocks(spriteBatch);
        DrawRoomEntities(spriteBatch);
        if(RoomID == 7)
        {
            DrawText(spriteBatch);
        }
    }

    public void DrawRoomBlocks(SpriteBatch spriteBatch)
    {
       // Console.WriteLine("offset: " + this.RoomOffset);
       // test
        foreach (IBlock2 block in blocks)
        {
            block.Draw(spriteBatch, this.RoomOffset);
        }

        foreach (IBlock2 block in collideable)
        {
            block.Draw(spriteBatch, this.RoomOffset);
	    }

        foreach (IDoor door in doors)
        {
            door.Draw(spriteBatch, this.RoomOffset);
        }
    }

    public void DrawRoomEntities(SpriteBatch spriteBatch) 
    {
        EnemyManager.Draw(spriteBatch);
        ProjectileManager.Draw(spriteBatch);
        ItemManager.DrawDrops(spriteBatch);
        if (RoomCleared)
            ItemManager.DrawItems(spriteBatch);
    }

    public void DrawText(SpriteBatch spriteBatch)
    {
        Vector2 position = new Vector2(200, 324);
        spriteBatch.DrawString(font, "EASTMOST PENINSULA", position, Color.White, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);
        position = new Vector2(240, 348);
        spriteBatch.DrawString(font, "IS THE SECRET", position, Color.White, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);
    }

    public void Update()
    {
        EnemyManager.Update();
        ProjectileManager.Update();
        if (RoomCleared)
        {
            ItemManager.Update();
            foreach (IDoor door in doors)
            {
                door.SetDoorOpen();
            }
        }
        foreach (IBlock2 block in collideable)
        {
            block.Update();
	    }
        foreach (IDoor door in doors) {
            door.Update();
        }
    }
}



