using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkWandLeftState : ILinkState
{
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private ILink link;

    private int stepDistance;
    private Vector2 dimensions;

    private List<Rectangle> animation = LinkAnimations.WandLeft;

    public LinkWandLeftState(ILink link)
    {
        this.link = link;
        animationFrame = 0;
        MaxAnimationFrames = animation.Count;
        stepDistance = link.StepDistance;

        float width = animation[animationFrame].Width * link.Scale - link.Dimensions.X;
        float height = animation[animationFrame].Height * link.Scale;

        this.dimensions = new Vector2(width, height);

        link.Position = new Vector2(link.Position.X - dimensions.X, link.Position.Y);
        AddHitBox();
    }

    public void Update()
    {
        link.SourceRect = animation[animationFrame];
        UpdateAnimation();
        OffsetCollisionBox();
    }

    private void UpdateAnimation()
    {
        AnimationFrame++;
        if (AnimationFrame >= MaxAnimationFrames)
        {
            link.Position = new Vector2(link.Position.X + dimensions.X, link.Position.Y);
            link.LinkState = new LinkWalkingLeftState(link);
        }
    }

    private void AddHitBox()
    {
        float width = animation[AnimationFrame].Width * link.Scale;
        // tbh, idk why using link.collisionRectangle.Height works, it should be
        // link.collisionRectangle.Width, but it works soooo
        float hitboxWidth = Math.Abs(this.dimensions.X - link.collisionRectangle.Height);
        float hitboxHeight = 5;
        Vector2 hitBoxDimensions = new Vector2(hitboxWidth, hitboxHeight);

        float x = link.Position.X;
        float y = link.Position.Y + (link.Dimensions.Y / 2) - hitBoxDimensions.Y;
        Vector2 hitboxPosition = new Vector2(x, y);

        link.LinkProjManager.AddHitBox(hitboxPosition, hitBoxDimensions, true);
    }

    private void OffsetCollisionBox()
    {
        int x = (int)(link.Position.X + link.Dimensions.X - link.collisionRectangle.Height);
        int y = (int)(link.Position.Y);
        int width = link.collisionRectangle.Width;
        int height = link.collisionRectangle.Height;

        link.collisionRectangle = new Rectangle(x, y, width, height);
    }

    public void Idle()
    {
        // NO-OP
    }

    public void MoveUp()
    {
        // NO-OP
    }

    public void MoveRight()
    {
        // NO-OP
    }

    public void MoveDown()
    {
        // NO-OP
    }

    public void MoveLeft()
    {
        // NO-OP
    }

    public void Hit()
    {
        // NO-OP
    }

    public void Sword()
    {
        // NO-OP
    }

    public void Wand()
    {
        // NO-OP
    }

    public void TakeDamage()
    {
        link.LinkState = new LinkTakeDamageState(link);
    }

    public void PickupItem()
    {
        // Fill
    }

    public void UseArrow()
    {
        // NO-OP
    }

    public void UseFireball()
    {
        // NO-OP
    }

    public void UseBoomerang()
    {
        // NO-OP
    }
}
