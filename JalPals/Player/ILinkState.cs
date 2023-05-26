namespace JalPals.Player;

public interface ILinkState
{
    public int AnimationFrame { get; set; }
    public int MaxAnimationFrames { get; }
    void Update();
    void Idle();
    void MoveUp();
    void MoveRight();
    void MoveDown();
    void MoveLeft();
    void Hit();
    void Sword();
    void Wand();
    void TakeDamage();
    void PickupItem();
    void UseArrow();
    void UseFireball();
    void UseBoomerang();
}

