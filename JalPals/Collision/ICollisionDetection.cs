using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Collision;

public interface ICollisionDetection
{
    void DetectCollisions(List<Rectangle> list1, List<Rectangle> list2);
    void DetectCollision(Rectangle rect1, List<Rectangle> list1);
    int DetectCollision(Rectangle rect1, Rectangle rect2);
}

