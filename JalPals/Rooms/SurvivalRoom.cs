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
using JalPals.Sprites;

namespace JalPals.Rooms;

public class SurvivalRoom : IRoom
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

    private int waveNumber;
    private const int waveCooldownDuration = 300;
    private int waveCooldown;
    private int[][] enemyWaves = SurvivalWaves.enemies;
    private Random rnd = new Random(Guid.NewGuid().GetHashCode());
    private bool cleared;

    public SurvivalRoom(int id,
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
        waveNumber = 0;
        waveCooldown = waveCooldownDuration;
        cleared = false;

        RoomID = id;
        _thisRoomManager = rm;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawRoomBlocks(spriteBatch);
        DrawRoomEntities(spriteBatch);
        DrawText(spriteBatch, new Vector2(100, 200), "Wave: " + (waveNumber + 1));
        if (cleared)  
            DrawText(spriteBatch, new Vector2(275,375), "Our savior!");
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

    public void DrawText(SpriteBatch spriteBatch, Vector2 position, string text)
    {
        spriteBatch.DrawString(font, text, position, Color.Red, 0, new Vector2(0, 0), 1.25f, SpriteEffects.None, 0.5f);
    }

    public void Update()
    {
        EnemyManager.Update();
        ProjectileManager.Update();
        if (RoomCleared)
        { 
            ItemManager.Update();
            if (waveCooldown > 0)
            { 
                waveCooldown--;
                Console.WriteLine(waveCooldown);
	        }
            if (waveNumber < enemyWaves.Length && waveCooldown == 0)
                NextWave();
            if (waveNumber >= enemyWaves.Length)
            {
                cleared = true;
                Console.WriteLine("Enemies cleared");
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

    private void NextWave()
    {
        Console.WriteLine("Next wave");
        ItemManager.ClearItems();
        int[] enemyIDs = enemyWaves[waveNumber];
        foreach (int enemyID in enemyIDs)
        {
            int spawnLocation = rnd.Next(0, SurvivalWaves.spawnLocations.Length);
            Vector2 position = SurvivalWaves.spawnLocations[spawnLocation];
            EnemyManager.AddEnemyByID(enemyID,  position, ProjectileManager);
	    }
        waveCooldown = waveCooldownDuration;
        waveNumber++;
    }
}



