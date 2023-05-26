using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkIdleState : ILinkState
{
    private ILink link;
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private List<Rectangle> animation = LinkAnimations.Idle;

    public LinkIdleState(ILink link)
    {
        this.link = link;
        animationFrame = 0;
        MaxAnimationFrames = animation.Count;
    }

    public void Update()
    {
        link.SourceRect = animation[animationFrame];
    }

    public void ChangeDirection()
    {
        // NO-OP
    }

    public void Idle()
    {

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
        link.LinkState = new LinkWalkingLeftState(link);
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
        link.LinkState = new LinkPickupItemState(link);
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

