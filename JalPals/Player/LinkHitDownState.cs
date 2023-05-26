using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkHitDownState : ILinkState
{
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private ILink link;

    private int stepDistance;

    private List<Rectangle> animation = LinkAnimations.HitDown;

    public LinkHitDownState(ILink link)
    {
        this.link = link;
        animationFrame = 0;
        MaxAnimationFrames = animation.Count;
        stepDistance = link.StepDistance;
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

    private void AddHitBox()
    {
        float hitboxWidth = 5;
        float hitboxHeight = 5;
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

