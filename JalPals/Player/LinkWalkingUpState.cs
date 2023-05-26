using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkWalkingUpState : ILinkState
{
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private ILink link;
    private int stepDistance;

    private List<Rectangle> animation = LinkAnimations.WalkingUp;

    public LinkWalkingUpState(ILink link)
    {
        this.link = link;
        animationFrame = 0;
        MaxAnimationFrames = animation.Count;
        stepDistance = link.StepDistance;
        this.link.SourceRect = animation[animationFrame]; ;
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
        UpdateAnimation();

        Vector2 position = link.Position;

        position.Y = position.Y - stepDistance;

        link.Position = position;
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
        link.LinkState = new LinkHitUpState(link);
    }

    public void Sword()
    {
        link.LinkState = new LinkSwordUpState(link);
    }

    public void Wand()
    {
        link.LinkState = new LinkWandUpState(link);
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
        float x = link.Position.X + (link.Dimensions.X / 2);
        float y = link.Position.Y;
        Vector2 position = new Vector2(x, y);
        link.LinkProjManager.AddArrowUp(position, link.Scale, true);
    }

    public void UseFireball()
    {
        link.LinkProjManager.AddFireball(link.Position, new Vector2(0, -5), true);
    }

    public void UseBoomerang()
    {
        link.LinkProjManager.AddBoomerang(link.Position, new Vector2(0, -5), true);
    }
}


