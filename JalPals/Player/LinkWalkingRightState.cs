using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkWalkingRightState : ILinkState
{
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private ILink link;

    private int stepDistance;

    private List<Rectangle> animation = LinkAnimations.WalkingRight;

    public LinkWalkingRightState(ILink link)
    {
        this.link = link;
        animationFrame = 0;
        MaxAnimationFrames = animation.Count;
        stepDistance = link.StepDistance;
    }

    public void Update()
    {
        link.SourceRect = animation[animationFrame];
    }

    private void UpdateAnimation()
    {
        AnimationFrame++;
        if (AnimationFrame >= MaxAnimationFrames)
            AnimationFrame = 0;
    }

    public void Idle()
    {
        AnimationFrame = 0;
    }

    public void MoveUp()
    {
        link.LinkState = new LinkWalkingUpState(link);
    }

    public void MoveRight()
    {
        UpdateAnimation();
        Vector2 position = link.Position;

        position.X = position.X + stepDistance;

        link.Position = position;
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
        link.LinkState = new LinkHitRightState(link);
    }

    public void Sword()
    {
        link.LinkState = new LinkSwordRightState(link);
    }

    public void Wand()
    {
        link.LinkState = new LinkWandRightState(link);
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
        //Play sound effect of getting the Arrow/Boomerang
        link.Game.content.soundManager.Play("Arrow_Boomerang");
        float x = link.Position.X + link.Dimensions.X;
        float y = link.Position.Y + (link.Dimensions.Y / 2);
        Vector2 position = new Vector2(x, y);
        link.LinkProjManager.AddArrowRight(position, link.Scale, true);
    }

    public void UseFireball()
    {
        link.LinkProjManager.AddFireball(link.Position, new Vector2(5, 0), true);
    }

    public void UseBoomerang()
    {
        link.LinkProjManager.AddBoomerang(link.Position, new Vector2(5, 0), true);
    }

}

