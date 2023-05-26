using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkTakeDamageState : ILinkState
{
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private ILink link;
    private int stepDistance;

    private List<Rectangle> animation = LinkAnimations.TakeDamage;

    public LinkTakeDamageState(ILink link)
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
        {
            link.LinkState = new LinkIdleState(link);
            AnimationFrame = 0;
        }

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
        link.LinkState = new LinkWalkingLeftState(link);
    }

    public void Hit()
    {
        //NO-OP
    }

    public void Sword()
    {
        //NO-OP
    }

    public void Wand()
    {
        //NO-OP
    }

    public void TakeDamage()
    {
        //NO-OP
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


