using Microsoft.Xna.Framework;

namespace JalPals.Projectiles
{
    public struct ProjectileAnimations
    {

        public static Rectangle GreenArrowUp = new Rectangle(185, 195, 5, 16);
        public static Rectangle GreenArrowRight = new Rectangle(210, 200, 16, 5);
        public static Rectangle GreenArrowDown = new Rectangle(125, 195, 5, 16);
        public static Rectangle GreenArrowLeft = new Rectangle(150, 200, 16, 5);

        public static Rectangle BowRight = new Rectangle(424, 255, 8, 16);

        public static Rectangle[] FireBall = {
            new Rectangle(101, 14, 8, 10),
            new Rectangle(110, 14, 8, 10),
            new Rectangle(119, 14, 8, 10),
            new Rectangle(128, 14, 8, 10)
        };

        public static Rectangle[] BoomerangRight = {
            new Rectangle(291, 15, 5, 8),
            new Rectangle(299, 15, 8, 8),
            new Rectangle(308, 17, 8, 5),
        };

        public static Rectangle[] BoomerangLeft = {
            new Rectangle(291, 33, 5, 8),
            new Rectangle(299, 33, 8, 8),
            new Rectangle(308, 35, 8, 5)
        };
    }
}

