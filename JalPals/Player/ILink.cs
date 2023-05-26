using JalPals.Inventory;
using JalPals.Items;
using JalPals.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JalPals.Player;


public interface ILink : IGameObject
{
    public ILinkState LinkState { get; set; }
    public ILinkHealth LinkHealth { get; set; }
    public IInventory LinkInventory { get; }
    public IItemManager LinkItemManager { get; set; }
    public IProjectileManager LinkProjManager { get; set; }
    public Rectangle SourceRect { get; set; }
    public Rectangle destRectangle { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Dimensions { get; }
    public Texture2D Texture { get; }
    public float Scale { get; }
    public Game1 Game { get; }


    public int StepDistance { get; }

    void Update();
    void Draw(SpriteBatch spriteBatch);
    void Idle();
    void MoveUp();
    void MoveRight();
    void MoveDown();
    void MoveLeft();
    void Hit();
    void Sword();
    void Wand();
    void TakeDamage();
    void PickupItem(IItem item);
    void UseArrow();
    void UseBoomerang();
    void UseFireball();
    void UseEquipped();
    void Cooldown(int length);
}
