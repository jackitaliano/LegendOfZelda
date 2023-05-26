using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Rooms;

public enum TransitionSide { 
    TOP,
    RIGHT,
    BOTTOM,
    LEFT
}

public interface IRoomManager
{
    public SortedDictionary<int, IRoom> rooms { get; set; }
    public IRoom currentRoom { get; set; }
    public int currentID { get; set; }
    public bool InTransition { get; set; }
    public TransitionSide transitionSide { get; set; }
    public IRoom transitionRoom { get; set; }
    void Update();
    void Draw(SpriteBatch spriteBatch);
    void DrawTransition(SpriteBatch spriteBatch);
    void SwitchRoom(int CurrentRoom, int doorSide);
    void SpawnItemDrops(Vector2 position, int dropType);
}

