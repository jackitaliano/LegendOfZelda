using JalPals.Doors;
using JalPals.Inventory;
using JalPals.Items;
using JalPals.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
//using System.Drawing;

namespace JalPals.Player;

public class Link : ILink
{
    public ILinkState LinkState { get; set; }
    public ILinkHealth LinkHealth { get; set; }
    public IInventory LinkInventory { get; set; }
    public IItemManager LinkItemManager { get; set; }
    public IProjectileManager LinkProjManager { get; set; }
    public Game1 Game { get { return linkGame; } }

    public Rectangle SourceRect { get; set; }
    public Rectangle destRectangle { get; set; }
    public Rectangle collisionRectangle { get; set; }
    public Vector2 velocityVector { get; set; }
    public float Scale { get { return scale; } }

    public Vector2 Position { get; set; }
    public Vector2 Dimensions { get { return new Vector2(SourceRect.Width * scale, SourceRect.Height * scale); } }
    public Texture2D Texture { get; }

    public int StepDistance { get; } = 4;
    private float scale;
    private Color drawColor;
    public Game1 linkGame;
    public int currentLinkHealth;
    private int currentFrame;
    private int cooldownLen;
    private bool isCooldown;
    private bool isDamaged;
    private int cooldownMax;
    private int shootCooldown;
    private int shootMaxCooldown;
    private int meleeCooldown;
    private int meleeMaxCooldown;

    GameTime gameTime;

    private Rectangle ladderRect = new Rectangle(360, 165, 16, 16);

    public Link(Game1 game, ContentLoader contentLoader, Vector2 position, float scale, IProjectileManager projManager, IItemManager itemManager)
    {
        this.Texture = contentLoader.LinkMovementTexture;
        this.Position = position;
        this.scale = scale;
        LinkState = new LinkWalkingUpState(this);
        LinkHealth = new LinkHealth(this);
        currentLinkHealth = LinkHealth.LinkHealthVal;


        float width = SourceRect.Width * scale;
        float height = SourceRect.Height * scale;
        destRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)width, (int)height);
        collisionRectangle = destRectangle;

        cooldownLen = 0;
        cooldownMax = 40;
        isCooldown = false;
        drawColor = Color.White;
        shootCooldown = 0;
        shootMaxCooldown = 40;
        meleeCooldown = 0;
        meleeMaxCooldown = 20;

        LinkItemManager = new ItemManager(contentLoader);
        LinkProjManager = new ProjectileManager(contentLoader);
        linkGame = game;
        LinkInventory = new LinkInventory(Texture, linkGame);
    }

    public void Update()
    {
        int collideWidth = (int)collisionRectangle.Width;
        int collideHeight = (int)collisionRectangle.Height;

        collisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, collideWidth, collideHeight);

        LinkState.Update();
        HandleCooldowns();

        currentLinkHealth = linkGame.menu.HeartCount();
        if(currentLinkHealth <= 0)
        {
            linkGame.changeState(GameStates.DEAD);
        }

        destRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)(Dimensions.X), (int)(Dimensions.Y));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        destRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y);

        spriteBatch.Draw(Texture, destRectangle, SourceRect, drawColor);
        //uncomment below for link hit box
        //spriteBatch.Draw(Texture, collisionRectangle, ladderRect, Color.White);
        //LinkInventory.Draw(spriteBatch, 0);
    }

    private void HandleCooldowns()
    {
        // Room change cooldown
        if (isCooldown && !isDamaged && cooldownLen <= cooldownMax)
        {
            cooldownLen++;
        }
        else if (isCooldown && !isDamaged)
        {
            isCooldown = false;
            Console.WriteLine("Cooldown over.");
            cooldownLen = 0;
        }

        // Damage cooldown
        if (isCooldown && isDamaged && cooldownLen <= cooldownMax)
        {
            if ((cooldownLen / 10) % 2 == 0)
            {
                drawColor = Color.Red;
            }
            else
            {
                drawColor = Color.White;
            }
            cooldownLen++;
        }
        else if (isCooldown && isDamaged)
        {
            isCooldown = false;
            isDamaged = false;
            drawColor = Color.White;
            cooldownLen = 0;
        }

        // shooting cooldown
        if (shootCooldown > 0)
            shootCooldown--;

        if (meleeCooldown > 0)
            meleeCooldown--;
    }

    public void Idle()
    {
        LinkState.Idle();
    }
    public void MoveLeft()
    {
        LinkState.MoveLeft();
    }

    public void MoveRight()
    {
        LinkState.MoveRight();
    }

    public void MoveUp()
    {
        LinkState.MoveUp();
    }

    public void MoveDown()
    {
        LinkState.MoveDown();
    }

    public void Hit()
    {
        if (meleeCooldown > 0) return;
        meleeCooldown = meleeMaxCooldown;
        LinkState.Hit();
    }

    public void Sword()
    {
        if (meleeCooldown > 0) return;

        meleeCooldown = meleeMaxCooldown;
        //Play sound effect of using the Sword Slash
        linkGame.content.soundManager.Play("Sword_Slash");
        LinkState.Sword();
    }

    public void Wand()
    {
        if (meleeCooldown > 0) return;

        meleeCooldown = meleeMaxCooldown;
        //Play sound effect of using the Magical Rod
        linkGame.content.soundManager.Play("MagicalRod");

        LinkState.Wand();
    }

    public void TakeDamage()
    {
        if (!isCooldown)
        {
            CooldownHurt(40);
            Game.menu.UpdateHeart(-1);
            linkGame.content.soundManager.Play("Link_Hurt");
            LinkState.TakeDamage();
            LinkHealth.RemoveHealth();
        }

    }

    public void Cooldown(int length)
    {
        cooldownMax = length;
        isCooldown = true;
    }

    public void CooldownHurt(int length)
    {
        cooldownMax = length;
        isCooldown = true;
        isDamaged = true;
    }

    public void PickupItem(IItem item)
    {
        //Play sound effect of getting a Ruppe
        if (item.SourceRect == ItemAnimations.OrangeRupeeItem || item.SourceRect == ItemAnimations.BlueRupeeItem)
        {
            linkGame.content.soundManager.Play("Get_Rupee");
        }
        //Play sound effect of getting a Heart
        else if (item.SourceRect == ItemAnimations.HeartDropItem)
        {
            linkGame.content.soundManager.Play("Get_Heart");
        }
        else if (item.SourceRect == ItemAnimations.Bomb || item.SourceRect == ItemAnimations.RedPotion || item.SourceRect == ItemAnimations.BluePotion)
        {
            linkGame.content.soundManager.Play("Get_Item");
        }
        //Play sound effect of getting an Item
        else
        {
            linkGame.content.soundManager.Play("Fanfare");
        }

        if(item.SourceRect == ItemAnimations.TriforceItem)
        {
            linkGame.changeState(GameStates.WIN);
        }

        if(item.SourceRect == ItemAnimations.HeartContainerItem)
        {
            this.linkGame.menu.AddHeartContainer();
        }

        LinkState.PickupItem();
        linkGame.roomManager.currentRoom.ItemManager.DeleteItem(item);
        LinkInventory.AddItem(item);


    }

    public void UseEquipped()
    {
        if (LinkInventory.HasBow) UseArrow();
        else if (LinkInventory.HasBoomerang) UseBoomerang();
    }

    public void UseArrow()
    {
        if (shootCooldown > 0) return;
        //Play sound effect of getting the Arrow/Boomerang
        if (!LinkInventory.HasBow || LinkInventory.RupeeCount() <= 0)
            return;
        shootCooldown = shootMaxCooldown;
        LinkInventory.RemoveRupee();
        LinkState.UseArrow();
        linkGame.content.soundManager.Play("Arrow_Boomerang");
        //LinkProjManager.AddArrowRight(Position, scale, true);
    }

    public void UseBoomerang()
    {
        if (shootCooldown > 0) return;

        shootCooldown = shootMaxCooldown;
        LinkState.UseBoomerang();
        //Play sound effect of getting the Arrow/Boomerang
        linkGame.content.soundManager.Play("Arrow_Boomerang");
    }

    public void UseFireball()
    {
        LinkState.UseFireball();
    }

    public GameObjectType getType()
    {
        return GameObjectType.LINK;
    }

    public void ResolveCollision(IGameObject obj1, int side)
    {
        GameObjectType type = obj1.getType();
        switch (type)
        {
            case GameObjectType.ENEMY:
                CollideEnemy(obj1, side);
                break;
            case GameObjectType.ENEMYPROJECTILE:
                CollideEnemyProjectile(obj1, side);
                break;
            case GameObjectType.LINKPROJECTILE:
                CollideFriendlyProjectile(obj1, side);
                break;
            case GameObjectType.ITEM:
                CollideItem(obj1, side);
                break;
            case GameObjectType.BLOCK:
                CollideBlock(obj1, side);
                break;
            case GameObjectType.WALL:
                CollideWall(obj1, side);
                break;
            case GameObjectType.DOOR:
                CollideDoor(obj1, side);
                break;
        }
    }


    private void CollideEnemy(IGameObject enemy, int side)
    {
        CollisionRebound(side);
        TakeDamage();
    }

    private void CollideEnemyProjectile(IGameObject projectile, int side)
    {
        Console.WriteLine("enemy proj");
        TakeDamage();
    }

    private void CollideFriendlyProjectile(IGameObject obj1, int side)
    {

    }

    private void CollideItem(IGameObject obj1, int side)
    {
        PickupItem((IItem)obj1);
    }

    private void CollideBlock(IGameObject obj1, int side)
    {
        CollisionRebound(side);
    }

    private void CollideWall(IGameObject obj1, int side)
    {
        CollisionRebound(side);
    }

    private void CollideDoor(IGameObject obj1, int side)
    {
        float posX = Position.X;
        float posY = Position.Y;
        int sc = (int)Scale;
        Point origin = new Point(32, 288);
        int roomNavWidth = 12 * 16 * sc;
        int roomNavHeidth = 7 * 16 * sc;
        Rectangle rmNav = new Rectangle(origin.X + 2 * 16 * sc, origin.Y + 2 * 16 * sc, roomNavWidth, roomNavHeidth);

        bool IsOpen = ((IDoor)obj1).isOpen;
        if (!IsOpen) CollisionRebound(side);


    }

    private void CollisionRebound(int side)
    {
        switch (side)
        {
            case 1: // Top side
                Position = new Vector2(Position.X, Position.Y + StepDistance);
                break;
            case 2: // Right side
                Position = new Vector2(Position.X - StepDistance, Position.Y);
                break;
            case 3: // Bottom side
                Position = new Vector2(Position.X, Position.Y - StepDistance);
                break;
            case 4: // Left side
                Position = new Vector2(Position.X + StepDistance, Position.Y);
                break;
        }
    }
}

