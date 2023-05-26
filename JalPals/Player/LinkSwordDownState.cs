using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkSwordDownState : ILinkState
{
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private ILink link;

    private Vector2 dimensions;
    private int stepDistance;

    private List<Rectangle> animation = LinkAnimations.SwordDown;

    public LinkSwordDownState(ILink link)
    {
        this.link = link;
        animationFrame = 0;
        MaxAnimationFrames = animation.Count;
        stepDistance = link.StepDistance;

        float width = animation[animationFrame].Width * link.Scale;
        float height = Math.Abs(animation[animationFrame].Height * link.Scale - link.Dimensions.Y);

        this.dimensions = new Vector2(width, height);

        AddHitBox();
    }

    public void Update()
    {
        link.SourceRect = animation[animationFrame];
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        AnimationFrame++;
        if (AnimationFrame >= MaxAnimationFrames)
            link.LinkState = new LinkWalkingDownState(link);
    }

    //private void AddHitBox()
    //{
    //    Vector2 dimensions = new Vector2(5, 16);
    //    float x = link.Position.X + (link.Dimensions.X / 2) - (dimensions.X / 2);
    //    float y = link.Position.Y + link.Dimensions.Y;
    //    Vector2 position = new Vector2(x, y);
    //    link.LinkProjManager.AddHitBox(position, dimensions, true);
    //}

    private void AddHitBox()
    {
        float width = animation[AnimationFrame].Width * link.Scale;
        float hitboxWidth = 5;
        float hitboxHeight = Math.Abs(this.dimensions.Y - link.collisionRectangle.Height);
        Vector2 hitBoxDimensions = new Vector2(hitboxWidth, hitboxHeight);

        float x = link.Position.X + (link.Dimensions.X / 2) - hitBoxDimensions.X;
        float y = link.Position.Y + link.Dimensions.Y;
        Vector2 hitboxPosition = new Vector2(x, y);

        link.LinkProjManager.AddHitBox(hitboxPosition, hitBoxDimensions, true);
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

