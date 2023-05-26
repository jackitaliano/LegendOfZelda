using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace JalPals.Collision;

public class CollisionDetection : ICollisionDetection
{
    public CollisionDetection() { }

    public void DetectCollisions(List<Rectangle> list1, List<Rectangle> list2)
    {
        for (int i = 0; i < list1.Count; i++)
        {
            DetectCollision(list1[i], list2);
        }

    }
    public void DetectCollision(Rectangle rect1, List<Rectangle> list1)
    {
        for (int i = 0; i < list1.Count; i++)
        {
            DetectCollision(rect1, list1[i]);
        }
    }

    public int DetectCollision(Rectangle rect1, Rectangle rect2)
    {
        if (overlap(rect1, rect2))
        {
            return collisionSide(rect1, rect2);
        }
        return 0;
    }

    private static bool overlap(Rectangle rect1, Rectangle rect2)
    {

        if (rect1.Left > rect2.Right || rect2.Left > rect1.Right)
        {
            return false; // Rectangles are not intersecting horizontally
        }
        if (rect1.Top > rect2.Bottom || rect2.Top > rect1.Bottom)
        {
            return false; // Rectangles are not intersecting vertically
        }
        return true;

    }

    private static int collisionSide(Rectangle rect1, Rectangle rect2)
    {
        // Calculate horizontal and vertical distances between centers of the two rectangles
        int dx = (rect1.Left + rect1.Right) / 2 - (rect2.Left + rect2.Right) / 2;
        int dy = (rect1.Top + rect1.Bottom) / 2 - (rect2.Top + rect2.Bottom) / 2;

        // Calculate minimum distances between edges of the two rectangles
        int leftDist = Math.Abs(rect1.Left - rect2.Right);
        int rightDist = Math.Abs(rect1.Right - rect2.Left);
        int topDist = Math.Abs(rect1.Top - rect2.Bottom);
        int bottomDist = Math.Abs(rect1.Bottom - rect2.Top);

        // Determine closest edge
        int minDist = Math.Min(leftDist, Math.Min(rightDist, Math.Min(topDist, bottomDist)));

        if (minDist == topDist)
        {
            return 1;
        }
        else if (minDist == rightDist)
        {
            return 2;
        }
        else if (minDist == bottomDist)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }
}

