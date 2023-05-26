using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkWalkingLeftState : ILinkState
{
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private ILink link;
    private int stepDistance;

    private List<Rectangle> animation = LinkAnimations.WalkingLeft;

    public LinkWalkingLeftState(ILink link)
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
        animationFrame = 0;
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
        UpdateAnimation();

        Vector2 position = link.Position;

        position.X = position.X - stepDistance;

        link.Position = position;
    }

    public void Hit()
    {
        link.LinkState = new LinkHitLeftState(link);
    }

    public void Sword()
    {
        link.LinkState = new LinkSwordLeftState(link);
    }

    public void Wand()
    {
        link.LinkState = new LinkWandLeftState(link);
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
        float x = link.Position.X;
        float y = link.Position.Y + (link.Dimensions.Y / 2);
        Vector2 position = new Vector2(x, y);
        link.LinkProjManager.AddArrowLeft(position, link.Scale, true);
    }

    public void UseFireball()
    {
        link.LinkProjManager.AddFireball(link.Position, new Vector2(-5, 0), true);
    }

    public void UseBoomerang()
    {
        link.LinkProjManager.AddBoomerang(link.Position, new Vector2(-5, 0), true);
    }
}

