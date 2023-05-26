using JalPals.Collision;
using JalPals.Doors;
using JalPals.Enemies;
using JalPals.Items;
using JalPals.Player;
using JalPals.Projectiles;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Collections;
using System.Drawing;

namespace JalPals.Rooms;

public class RoomManager : IRoomManager
{
    public SortedDictionary<int, IRoom> rooms { get; set; }
    public IRoom currentRoom { get; set; }
    public int currentID { get; set; }
    private CollisionManager collisionManager { get; }
    public ILink link { get; }
    public bool InTransition { get; set; }
    public TransitionSide transitionSide { get; set; }
    public IRoom transitionRoom { get; set; }

    private int roomCount;
    private float transitionSpeed = 15;
    private Vector2 transitionVel;
    private float transitionXDistance = 768;
    private float transitionYDistance = 432;
    private SpriteBatch spriteBatch;   

    public RoomManager(ILink link, string[] filePaths, ContentLoader contentLoader, int scale, CollisionManager collisionManager)
    {
        int frameYOffset = 54;
        //Texture2D spriteSheet = content.Load<Texture2D>("dungeon_spritesheet");
        //Texture2D itemSheet = content.Load<Texture2D>("link");
        //Texture2D enemySheet = content.Load<Texture2D>("dungeonEnemiesTransparent");
        //Texture2D bossSheet = content.Load<Texture2D>("bosses");
        //Texture2D fireSheet = content.Load<Texture2D>("fire");
        //SpriteFont font = content.Load<SpriteFont>("Zelda");
        this.collisionManager = collisionManager;

        this.link = link;
        this.InTransition = false;
        this.transitionVel = new Vector2(0, 0);

        RoomFactory roomFactory = new RoomFactory(this, contentLoader, RoomOptions.blockOptions, RoomOptions.itemOptions, scale, frameYOffset);

        rooms = new SortedDictionary<int, IRoom>();
        currentID = 0;

        foreach (String filePath in filePaths)
        {
            int id;
            using (var reader = new StreamReader(filePath))
            {
                id = Int32.Parse(reader.ReadLine().Split(',')[0]);
            }
            EnemyManager enemyManager = new EnemyManager(this, contentLoader);
            ProjectileManager projectileManager = new ProjectileManager(contentLoader);
            ItemManager itemManager = new ItemManager(contentLoader);
            IRoom room = roomFactory.CreateRoom(filePath, id, enemyManager, itemManager, projectileManager);
            rooms.Add(id, room);
        }

        currentRoom = rooms[currentID];
        roomCount = rooms.Count;
        UpdateGameObjects();
    }

	public void Update()
	{
        if (InTransition) {
            UpdateTransition();
            return;
	    }
		currentRoom.Update();
        UpdateGameObjects();
    }

	public void Draw(SpriteBatch spriteBatch)
	{
        this.spriteBatch = spriteBatch; // for transitions for now
		currentRoom.Draw(spriteBatch);
	}

    public void DrawTransition(SpriteBatch spriteBatch) {
        this.currentRoom.DrawRoomBlocks(spriteBatch);
        this.transitionRoom.DrawRoomBlocks(spriteBatch);
    }

    private void UpdateTransition()
    {
        this.currentRoom.RoomOffset += this.transitionVel;
        this.transitionRoom.RoomOffset += this.transitionVel;

        if (TransitionComplete())
            EndTransition();
    }

    public void SwitchRoom(int room, int side)
    {
        Console.WriteLine("side: " + side);
        link.Cooldown(100);

        Level1_Rooms lv = new Level1_Rooms();
        int element = lv.level1RoomMappings[room, side];
        if (element != -1 && element != currentID)
        {
            IRoom fromRoom = currentRoom;
            currentRoom = rooms[element];
            currentID = element;
            UpdatePlayerManagers();
            //UpdateGameObjects(); <- check back later for fuunctionality, don't think we need

            SetTransition(fromRoom, currentRoom, side);
        }
    }

    public void SpawnItemDrops(Vector2 position, int dropType)
    {
        Console.WriteLine("droptype: " + dropType);
        if (dropType == 0)
            currentRoom.ItemManager.AddRupeeDrop(position, link.Scale);
        else
            currentRoom.ItemManager.AddHeartDrop(position, link.Scale);

        Console.WriteLine("Enemy spawned drop");
    }


    public void EnterZombiesRoom()
    {
        link.Cooldown(150);

        IRoom fromRoom = currentRoom;
        currentRoom = rooms[17];
        currentID = 17;
        UpdatePlayerManagers();
            
        SetTransition(fromRoom, currentRoom, 0);
    }

    private void SetTransition(IRoom fromRoom, IRoom toRoom, int side)
    {
        this.transitionRoom = fromRoom;
        this.InTransition = true;
        this.transitionRoom.RoomOffset = new Vector2(0, 0);

        if (side == 0) // bottom
        {
            this.currentRoom.RoomOffset = new Vector2(0, transitionYDistance);
            this.transitionVel = new Vector2(0, -1 * transitionSpeed);
            this.transitionSide = TransitionSide.BOTTOM;
        }
        else if (side == 1) // left
        {
            this.currentRoom.RoomOffset = new Vector2(-1 * transitionXDistance, 0);
            this.transitionVel = new Vector2(transitionSpeed, 0);
            this.transitionSide = TransitionSide.LEFT;
        }
        else if (side == 2) // top
        {
            this.currentRoom.RoomOffset = new Vector2(0, -1 * transitionYDistance);
            this.transitionVel = new Vector2(0, transitionSpeed);
            this.transitionSide = TransitionSide.TOP;
        }
        else if (side == 3) // right
        {
            this.currentRoom.RoomOffset = new Vector2(transitionXDistance, 0);
            this.transitionVel = new Vector2(-1 * transitionSpeed, 0);
            this.transitionSide = TransitionSide.RIGHT;
        }
    }

    private void EndTransition()
    {
        this.InTransition = false;
        this.transitionRoom.RoomOffset = new Vector2(0, 0);
        this.currentRoom.RoomOffset = new Vector2(0, 0);
        this.transitionVel = new Vector2(0, 0);
    }

    private bool TransitionComplete() { 
        if (transitionSide == TransitionSide.TOP && this.currentRoom.RoomOffset.Y > 0) { return true; }
        else if (transitionSide == TransitionSide.RIGHT && this.currentRoom.RoomOffset.X < 0) { return true; }
        else if (transitionSide == TransitionSide.BOTTOM && this.currentRoom.RoomOffset.Y < 0) { return true; }
        else if (transitionSide == TransitionSide.LEFT && this.currentRoom.RoomOffset.X > 0) { return true; }
        return false;
    }

    private void UpdatePlayerManagers()
    {
        link.LinkItemManager = currentRoom.ItemManager;
        link.LinkProjManager = currentRoom.ProjectileManager;
    }

    private void UpdateGameObjects()
    {
        List<List<IGameObject>> gameObjects = GetUpdatedGameObjectLists();
        collisionManager.UpdateGameObjects(gameObjects, this.link);
    }

    private List<List<IGameObject>> GetUpdatedGameObjectLists()
    {
        List<IGameObject> enemyColList = new List<IGameObject>(currentRoom.EnemyManager.roomEnemies);
        List<IGameObject> projList = new List<IGameObject>(currentRoom.ProjectileManager.projectiles);
        List<IGameObject> itemList = new List<IGameObject>(currentRoom.ItemManager.ItemsInFrame);
        List<IGameObject> itemDropList = new List<IGameObject>(currentRoom.ItemManager.ItemDrops);
        List<IGameObject> blockList = new List<IGameObject>(currentRoom.collideable);
        List<IGameObject> wallColList = new List<IGameObject>(currentRoom.wallRectangles);
        List<IGameObject> doorsList = new List<IGameObject>(currentRoom.doors);

        List<List<IGameObject>> objectLists = new List<List<IGameObject>>
        {
            enemyColList,
            projList,
            blockList,
            wallColList,
            doorsList,
            itemDropList,
        };
        if (currentRoom.RoomCleared) {
            objectLists.Add(itemList);
	    }

        return objectLists;
    }
}

