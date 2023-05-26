using JalPals.Blocks2;
using JalPals.Doors;
using JalPals.Enemies;
using JalPals.Items;
using JalPals.Projectiles;
using JalPals.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace JalPals.Rooms;

public class RoomFactory
{
    private CSVParser parser;
    private RoomManager roomManager;
    private Dictionary<int, Rectangle> blockOptions;
    private Dictionary<int, Rectangle> itemOptions;
    private ContentLoader contentLoader;
    private int scale;
    private int frameYOffset;
    private Vector2 roomStart;

    public RoomFactory(
        RoomManager roomManager,
        ContentLoader contentLoader,
        Dictionary<int, Rectangle> blockOptions,
        Dictionary<int, Rectangle> itemOptions,
        int scale,
        int frameYOffset
        )
    {
        this.roomManager = roomManager;
        this.contentLoader = contentLoader;
        this.blockOptions = blockOptions;
        this.itemOptions = itemOptions;
        this.frameYOffset = frameYOffset;
        this.roomStart = new Vector2(32, frameYOffset - 16);
        this.scale = scale;

        parser = new CSVParser();
    }

    public IRoom CreateRoom(string filePath, int roomID, EnemyManager enemyManager, ItemManager itemManager, ProjectileManager projectileManager)
    {
        List<string[]> parse = parser.Parse(filePath, ',');
        IRoom room = PopulateRoom(parse, roomID, enemyManager, itemManager, projectileManager);

        return room;
    }

    private IRoom PopulateRoom(List<string[]> parse, int id, EnemyManager enemyManager, ItemManager itemManager, ProjectileManager projectileManager)
    {
        (List<IBlock2> blocks, List<IBlock2> collideableBlocks) = PopulateBlocks(parse);
        PopulateEnemies(parse, enemyManager, projectileManager);
        PopulateItems(parse, itemManager);

        IRoom room;
        if (id == 17)
            room = new SurvivalRoom(id, roomManager, enemyManager, itemManager, projectileManager, blocks, collideableBlocks, contentLoader.gameFont);
        else
            room = new Room(id, roomManager, enemyManager, itemManager, projectileManager, blocks, collideableBlocks, contentLoader.gameFont);

        List<IDoor> doors = PopulateDoors(parse, room);
        List<IGameObject> wallRectangles = findCollidableWalls(doors);

        //Hard Coded stairs in the 15th room
        if (id == 15)
        {
            Rectangle currDest = new Rectangle(scale * ((int)roomStart.X + 16 * 6), scale * ((int)roomStart.Y + 16 * 6), scale * 16, scale * 16);
            doors.Add(new Stairs(currDest, true, room));
        }

        room.doors = doors;
        room.wallRectangles = wallRectangles;

        return room;
    }

    private List<IDoor> PopulateDoors(List<string[]> parse, IRoom room)
    {
        List<IDoor> doors = new List<IDoor>();

        // Add doors to door list
        for (int i = 0; i < 4; i++)
        {
            // Get door code from CSV
            int c = Int32.Parse(parse[2][i]);

            // Get door src
            Rectangle currSrc = new Rectangle(294 + (33 * c), (33 * i), 32, 32);

            // Get door dest
            Rectangle currDest = new Rectangle(0, 0, scale * 32, scale * 32);

            bool doorO = (c == 1 || c == 4);
            switch (i)
            {
                case (0):
                    // Top
                    currDest = new Rectangle(112 * scale, frameYOffset * scale, scale * 32, scale * 32);
                    doors.Add(new Door(contentLoader.DungeonTexture, c, currDest, doorO, DoorSide.TOP, room));
                    break;
                case (1):
                    // Left
                    currDest = new Rectangle(0, scale * (frameYOffset + 72), scale * 32, scale * 32);
                    doors.Add(new Door(contentLoader.DungeonTexture, c, currDest, doorO, DoorSide.LEFT, room));
                    break;
                case (2):
                    // Right
                    currDest = new Rectangle(224 * scale, scale * (frameYOffset + 72), scale * 32, scale * 32);
                    doors.Add(new Door(contentLoader.DungeonTexture, c, currDest, doorO, DoorSide.RIGHT, room));
                    break;
                case (3):
                    // Bottom
                    currDest = new Rectangle(112 * scale, scale * (frameYOffset + 144), scale * 32, scale * 32);
                    doors.Add(new Door(contentLoader.DungeonTexture, c, currDest, doorO, DoorSide.BOTTOM, room));
                    break;

            }
        }
        return doors;
    }

    private (List<IBlock2>, List<IBlock2>) PopulateBlocks(List<string[]> parse)
    {
        List<IBlock2> blocks = new List<IBlock2>();
        List<IBlock2> collideables = new List<IBlock2>();

        // Add frame to list
        Rectangle frameSrc = new Rectangle(0, 0, 256, 176);
        Rectangle frameDest = new Rectangle(0, scale * frameYOffset, scale * 256, scale * 176);
        blocks.Add(new Block(contentLoader.DungeonTexture, frameSrc, frameDest, false));

        // Add blocks to list
        for (int i = 3; i < 10; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                int currCode = Int32.Parse(parse[i][j]);
                Rectangle currSrc = blockOptions[currCode];
                Rectangle currDest = new Rectangle(scale * ((int)roomStart.X + 16 * j), scale * ((int)roomStart.Y + 16 * i), scale * 16, scale * 16);

                if (currCode > 0 && currCode < 4 || currCode == 10)
                {
                    IBlock2 newBlock;
                    if (currCode == 1) {
                        blocks.Add(new Block(contentLoader.DungeonTexture, blockOptions[0], currDest, false));
                        newBlock = new MoveableBlock(contentLoader.DungeonTexture, currSrc, currDest, true);
		            } else
                        newBlock = new Block(contentLoader.DungeonTexture, currSrc, currDest, true);

                    collideables.Add(newBlock);
                }
                else
                {
                    Block newBlock = new Block(contentLoader.DungeonTexture, currSrc, currDest, true);
                    blocks.Add(newBlock);
                }
            }
        }

        return (blocks, collideables);
    }

    private void PopulateItems(List<string[]> parse, ItemManager itemManager)
    {
        List<IItem> items = new List<IItem>();
        //add items to room
        //This line checks to see if the room as items in it.
        //If the parse.Count doesn't have a 10th line, then the room doesn't have items to spawn in. 
        if (parse.Count > 10)
        {
            //This will be the number of items for the room. It is gotten from the length of the string of the 10th
            //line in the csv code.
            int numOfItems = parse[10].Length;
            for (int i = 0; i < numOfItems; i++)
            {
                //These offsets are to allign the item properly in the square tiles on the floor.
                float offsetX = 15;
                float offsetY = -15;

                //get the current item ID.
                int currCode = Int32.Parse(parse[10][i]);
                int currXPos = Int32.Parse(parse[11][i]);
                int currYPos = Int32.Parse(parse[12][i]);
                //Get he source rectangle for this item
                Rectangle currSrc = itemOptions[currCode];

                //Calculate the position of the item. The calculations are complicated, but currXPos and currYPos
                //are the tile coordinates for the inner room. X = 0, Y = 0 is the top left corner. 
                Vector2 currPosition = new Vector2((scale * 16 * (currXPos + 2)) + offsetX, (scale * 16 * (currYPos + 6)) + offsetY);

                itemManager.AddItemByID(currCode, currPosition);
            }
        }
    }

    private void PopulateEnemies(List<string[]> parse, EnemyManager enemyManager, ProjectileManager projectileManager)
    {
        // add enemies to room
        //This line checks to see if the room as items in it.
        //If the parse.Count doesn't have a 12th line, then the room doesn't have enemies to spawn in. 
        if (parse.Count > 13)
        {
            //This will be the number of items for the room. It is gotten from the length of the string of the 10th
            //line in the csv code.
            int numOfEnemies = parse[13].Length;
            for (int i = 0; i < numOfEnemies; i++)
            {
                //These offsets are to allign the enemy properly in the square tiles on the floor.
                float offsetX = 0;
                float offsetY = -15;

                //get the current item ID.
                int currCode = Int32.Parse(parse[13][i]);
                int currXPos = Int32.Parse(parse[14][i]);
                int currYPos = Int32.Parse(parse[15][i]);
                //Get he source rectangle for this item
                Rectangle currSrc = itemOptions[currCode];

                //Calculate the position of the enemy. The calculations are complicated, but currXPos and currYPos
                //are the tile coordinates for the inner room. X = 0, Y = 0 is the top left corner. 
                Vector2 currPosition = new Vector2((scale * 16 * (currXPos + 2)) + offsetX, (scale * 16 * (currYPos + 6)) + offsetY);

                //Arbitrarily initialized to a Keese
                enemyManager.AddEnemyByID(currCode, currPosition, projectileManager);
            }
        }
    }

    private List<IGameObject> findCollidableWalls(List<IDoor> doors)
    {
        List<IGameObject> RectGameObjects = new List<IGameObject>();

        int roomWidth = 16 * 16 * scale;
        int roomHeidth = 11 * 16 * scale;

        int roomNavWidth = 12 * 16 * scale;
        int roomNavHeidth = 7 * 16 * scale;


        Point origin = new Point((int)roomStart.X, (int)roomStart.Y);
        Rectangle roomRect = new Rectangle(origin.X, origin.Y, roomWidth, roomHeidth);
        Rectangle roomNavRect = new Rectangle(origin.X + 2 * 16 * scale, origin.Y + 2 * 16 * scale, roomNavWidth, roomNavHeidth);

        bool[] sideRectFill = new bool[4] { false, false, false, false };

        List<Rectangle> walls = new List<Rectangle>();

        foreach (Door door in doors)
        {
            int PIXBUFF = 5;
            Rectangle doorRect = door.destRectangle;
            Rectangle wall = new Rectangle();
            Rectangle wall2 = new Rectangle();

            // Determine the direction of the wall based on the door's position
            if (door.doorSide == DoorSide.LEFT)
            {
                wall.X = doorRect.Left;
                wall.Y = origin.Y;
                wall.Width = doorRect.Width - PIXBUFF;
                wall.Height = doorRect.Top - origin.Y;

                wall2.X = doorRect.Left;
                wall2.Y = doorRect.Bottom;
                wall2.Width = doorRect.Width - PIXBUFF;
                wall2.Height = roomRect.Bottom - doorRect.Bottom;

                sideRectFill[0] = true;
            }
            else if (door.doorSide == DoorSide.RIGHT)
            {

                wall.X = doorRect.Left + PIXBUFF;
                wall.Y = origin.Y;
                wall.Width = doorRect.Width;
                wall.Height = doorRect.Y - origin.Y;

                wall2.X = doorRect.Left + PIXBUFF;
                wall2.Y = doorRect.Bottom;
                wall2.Width = doorRect.Width;
                wall2.Height = roomRect.Bottom - doorRect.Bottom;

                sideRectFill[2] = true;
            }
            else if (door.doorSide == DoorSide.TOP)
            {
                wall.X = origin.X;
                wall.Y = doorRect.Top;
                wall.Width = doorRect.Left - origin.X;
                wall.Height = doorRect.Height - 15;

                wall2.X = doorRect.Right;
                wall2.Y = doorRect.Top;
                wall2.Width = roomRect.Width - doorRect.Right;
                wall2.Height = doorRect.Height - 15;


                sideRectFill[1] = true;

            }
            else if (door.doorSide == DoorSide.BOTTOM)
            {
                wall.X = origin.X;
                wall.Y = doorRect.Top + PIXBUFF;
                wall.Width = doorRect.Left - origin.X;
                wall.Height = doorRect.Height;

                wall2.X = doorRect.Right;
                wall2.Y = doorRect.Top + PIXBUFF;
                wall2.Width = roomRect.Width - doorRect.Right;
                wall2.Height = doorRect.Height;

                sideRectFill[3] = true;
            }
            walls.Add(wall);
            walls.Add(wall2);
        }

        if (!sideRectFill[0])
        {
            walls.Add(new Rectangle(origin.X, origin.Y, roomNavRect.X - origin.X, roomRect.Height));
        }
        if (!sideRectFill[1])
        {
            walls.Add(new Rectangle(origin.X, origin.Y, roomNavRect.X, roomNavRect.Top - roomRect.Top));
        }
        if (!sideRectFill[2])
        {
            walls.Add(new Rectangle(roomNavRect.Right, origin.Y, 1, roomRect.Height));
        }
        if (!sideRectFill[3])
        {
            walls.Add(new Rectangle(origin.X, roomNavRect.Top, roomRect.Width, 1));
        }

        /*
        // Check for intersections with other walls and adjust size if necessary
        foreach (var otherWall in walls)
        {
            if (wall.Intersects(otherWall))
            {
                if (wall.Left == otherWall.Right)
                {
                    // Adjust the width of the wall to not overlap with the other wall
                    wall.X += otherWall.Width;
                    wall.Width -= otherWall.Width;
                }
                else if (wall.Right == otherWall.Left)
                {
                    // Adjust the width of the wall to not overlap with the other wall
                    wall.Width -= otherWall.Width;
                }
                else if (wall.Top == otherWall.Bottom)
                {
                    // Adjust the height of the wall to not overlap with the other wall
                    wall.Y += otherWall.Height;
                    wall.Height -= otherWall.Height;
                }
                else if (wall.Bottom == otherWall.Top)
                {
                    // Adjust the height of the wall to not overlap with the other wall
                    wall.Height -= otherWall.Height;
                }
            }
        }

            // Check for intersection with the room and adjust size if necessary
            if (wall.Left < room.Left)
            {
                wall.Width -= (room.Left - wall.Left);
                wall.X = room.Left;
            }
            if (wall.Right > room.Right)
            {
                wall.Width -= (wall.Right - room.Right);
            }
            if (wall.Top < room.Top
    */

        foreach (Rectangle rect in walls)
        {
            RectGameObjects.Add(new WallRectangle(rect));
        }

        return RectGameObjects;
    }
}

