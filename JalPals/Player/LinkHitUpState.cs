using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkHitUpState : ILinkState
{
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private ILink link;

    private int stepDistance;
    private Vector2 dimensions;

    private List<Rectangle> animation = LinkAnimations.HitUp;

    public LinkHitUpState(ILink link)
    {
        this.link = link;
        animationFrame = 0;
        MaxAnimationFrames = animation.Count;
        stepDistance = link.StepDistance;

        float width = animation[animationFrame].Width * link.Scale;
        float height = animation[animationFrame].Height * link.Scale - link.Dimensions.Y;

        this.dimensions = new Vector2(width, height);

        link.Position = new Vector2(link.Position.X, link.Position.Y - dimensions.Y);
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
            link.Position = new Vector2(link.Position.X, link.Position.Y + dimensions.Y);
            link.LinkState = new LinkWalkingUpState(link);
        }
    }

    private void AddHitBox()
    {
        float hitboxWidth = 5;
        float hitboxHeight = 5;
        Vector2 hitBoxDimensions = new Vector2(hitboxWidth, hitboxHeight);

        float x = link.Position.X + (link.Dimensions.X / 2) - hitBoxDimensions.X;
        float y = link.Position.Y - hitboxHeight;
        Vector2 hitboxPosition = new Vector2(x, y);

        link.LinkProjManager.AddHitBox(hitboxPosition, hitBoxDimensions, true);
    }

    private void OffsetCollisionBox()
    {
        int x = (int)(link.Position.X);
        int y = (int)(link.Position.Y + link.Dimensions.Y - link.collisionRectangle.Height);
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

