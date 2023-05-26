using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Player;

public class LinkWalkingDownState : ILinkState
{
    public int MaxAnimationFrames { get; }
    public int AnimationFrame { get { return animationFrame; } set { animationFrame = value; } }

    private int animationFrame;

    private ILink link;
    private int stepDistance;

    private List<Rectangle> animation = LinkAnimations.WalkingDown;

    public LinkWalkingDownState(ILink link)
    {
        this.link = link;
        animationFrame = 0;
        MaxAnimationFrames = animation.Count;
        stepDistance = link.StepDistance;
    }

    public void Update()
    {
        link.SourceRect = animation[animationFrame];
        if (animationFrame == 1)
        {
            link.destRectangle = new Rectangle((int)link.Position.X - 20, (int)link.Position.Y, link.SourceRect.Width, link.SourceRect.Height);
        }

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
        UpdateAnimation();
        Vector2 position = link.Position;

        position.Y = position.Y + stepDistance;

        link.Position = position;
    }

    public void MoveLeft()
    {
        link.LinkState = new LinkWalkingLeftState(link);
    }

    public void Hit()
    {
        link.LinkState = new LinkHitDownState(link);
    }

    public void Sword()
    {
        link.LinkState = new LinkSwordDownState(link);
    }

    public void Wand()
    {
        link.LinkState = new LinkWandDownState(link);
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
        float y = link.Position.Y + link.Dimensions.Y;
        Vector2 position = new Vector2(x, y);
        link.LinkProjManager.AddArrowDown(position, link.Scale, true);
    }

    public void UseFireball()
    {
        link.LinkProjManager.AddFireball(link.Position, new Vector2(0, 5), true);
    }

    public void UseBoomerang()
    {
        link.LinkProjManager.AddBoomerang(link.Position, new Vector2(0, 5), true);
    }
}

