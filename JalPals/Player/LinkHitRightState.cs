using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkHitRightState : ILinkState
{
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private ILink link;

    private int stepDistance;

    private List<Rectangle> animation = LinkAnimations.HitRight;

    public LinkHitRightState(ILink link)
    {
        this.link = link;
        animationFrame = 0;
        MaxAnimationFrames = animation.Count;
        stepDistance = link.StepDistance;
    }

    public void Update()
    {
        link.SourceRect = animation[animationFrame];
        UpdateAnimation();
    }

    private void AddHitBox()
    {
        float hitboxWidth = 5;
        float hitboxHeight = 5;
        Vector2 hitBoxDimensions = new Vector2(hitboxWidth, hitboxHeight);

        float x = link.Position.X + link.Dimensions.X + hitboxWidth;
        float y = link.Position.Y + (link.Dimensions.Y / 2) - hitBoxDimensions.Y;
        Vector2 hitboxPosition = new Vector2(x, y);

        link.LinkProjManager.AddHitBox(hitboxPosition, hitBoxDimensions, true);
    }

    private void UpdateAnimation()
    {
        AnimationFrame++;
        if (AnimationFrame >= MaxAnimationFrames)
            link.LinkState = new LinkWalkingRightState(link);
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

