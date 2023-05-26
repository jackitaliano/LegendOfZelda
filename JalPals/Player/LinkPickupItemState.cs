using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkPickupItemState : ILinkState
{
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private ILink link;

    private int stepDistance;

    private List<Rectangle> animation = LinkAnimations.PickupItem;

    public LinkPickupItemState(ILink link)
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

    private void UpdateAnimation()
    {
        AnimationFrame++;
        if (AnimationFrame >= MaxAnimationFrames)
            link.LinkState = new LinkIdleState(link);
    }

    public void Idle()
    {
        //NO-OP
    }

    public void MoveUp()
    {
        link.LinkState = new LinkWalkingUpState(link);
    }

    public void MoveRight()
    {
        link.LinkState = new LinkWalkingRightState(link);
    }

    public void MoveDown()
    {
        link.LinkState = new LinkWalkingDownState(link);
    }

    public void MoveLeft()
    {
        link.LinkState = new LinkWalkingRightState(link);
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
        // NO-OP
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


