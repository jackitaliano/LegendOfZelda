using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JalPals.Player;

public struct LinkAnimations
{
    public LinkAnimations() { }

    public static List<Rectangle> Idle = new List<Rectangle>() {
        new Rectangle(0, 0, 15, 16),
    };

    public static List<Rectangle> WalkingLeft = new List<Rectangle>(){
        new Rectangle(30, 0, 15, 16),
        new Rectangle(31, 30, 14, 15),
    };

    public static List<Rectangle> WalkingRight = new List<Rectangle>(){
        new Rectangle(90, 30, 15, 16),
        new Rectangle(91, 0, 14, 15),
    };

    public static List<Rectangle> WalkingUp = new List<Rectangle>() {
        new Rectangle(62, 30, 12, 16),
        new Rectangle(62, 0, 12, 16),
    };
    public static List<Vector2> WalkingUpOffset = new List<Vector2>()
    {
        new Vector2(0,0),
        new Vector2(1,0)
    };

    public static List<Rectangle> WalkingDown = new List<Rectangle>() {
        new Rectangle(0, 0, 15, 16),
        new Rectangle(1, 30, 13, 16),
    };

    public static List<Vector2> WalkingDownOffset = new List<Vector2>()
    {
        new Vector2(0,0),
        new Vector2(1,0)
    };

    public static List<Rectangle> HitUp = new List<Rectangle>() {
        new Rectangle(60, 60, 16, 16),
        new Rectangle(60, 60, 16, 16),
        new Rectangle(60, 60, 16, 16),
        new Rectangle(60, 60, 16, 16),
        new Rectangle(60, 60, 16, 16),
        new Rectangle(60, 60, 16, 16),
        new Rectangle(60, 60, 16, 16),
        new Rectangle(60, 60, 16, 16),
    };

    public static List<Rectangle> HitRight = new List<Rectangle>() {
        new Rectangle(90, 60, 15, 15),
        new Rectangle(90, 60, 15, 15),
        new Rectangle(90, 60, 15, 15),
        new Rectangle(90, 60, 15, 15),
        new Rectangle(90, 60, 15, 15),
        new Rectangle(90, 60, 15, 15),
        new Rectangle(90, 60, 15, 15),
        new Rectangle(90, 60, 15, 15),
    };

    public static List<Rectangle> HitDown = new List<Rectangle>() {
        new Rectangle(0, 60, 16, 15),
        new Rectangle(0, 60, 16, 15),
        new Rectangle(0, 60, 16, 15),
        new Rectangle(0, 60, 16, 15),
        new Rectangle(0, 60, 16, 15),
        new Rectangle(0, 60, 16, 15),
        new Rectangle(0, 60, 16, 15),
        new Rectangle(0, 60, 16, 15),
    };

    public static List<Rectangle> HitLeft = new List<Rectangle>() {
        new Rectangle(30, 60, 15, 15),
        new Rectangle(30, 60, 15, 15),
        new Rectangle(30, 60, 15, 15),
        new Rectangle(30, 60, 15, 15),
        new Rectangle(30, 60, 15, 15),
        new Rectangle(30, 60, 15, 15),
        new Rectangle(30, 60, 15, 15),
        new Rectangle(30, 60, 15, 15),
    };

    public static List<Rectangle> SwordUp = new List<Rectangle>() {
        new Rectangle(60, 84, 16, 28),
        new Rectangle(60, 84, 16, 28),
        new Rectangle(60, 84, 16, 28),
        new Rectangle(60, 84, 16, 28),
        new Rectangle(60, 84, 16, 28),
        new Rectangle(60, 84, 16, 28),
        new Rectangle(60, 84, 16, 28),
        new Rectangle(60, 84, 16, 28),
    };

    public static List<Rectangle> SwordRight = new List<Rectangle>() {
        new Rectangle(84, 90, 27, 15),
        new Rectangle(84, 90, 27, 15),
        new Rectangle(84, 90, 27, 15),
        new Rectangle(84, 90, 27, 15),
        new Rectangle(84, 90, 27, 15),
        new Rectangle(84, 90, 27, 15),
        new Rectangle(84, 90, 27, 15),
        new Rectangle(84, 90, 27, 15),
    };

    public static List<Rectangle> SwordDown = new List<Rectangle>() {
        new Rectangle(0, 84, 16, 27),
        new Rectangle(0, 84, 16, 27),
        new Rectangle(0, 84, 16, 27),
        new Rectangle(0, 84, 16, 27),
        new Rectangle(0, 84, 16, 27),
        new Rectangle(0, 84, 16, 27),
        new Rectangle(0, 84, 16, 27),
        new Rectangle(0, 84, 16, 27),
    };

    public static List<Rectangle> SwordLeft = new List<Rectangle>() {
        new Rectangle(24, 90, 27, 15),
        new Rectangle(24, 90, 27, 15),
        new Rectangle(24, 90, 27, 15),
        new Rectangle(24, 90, 27, 15),
        new Rectangle(24, 90, 27, 15),
        new Rectangle(24, 90, 27, 15),
        new Rectangle(24, 90, 27, 15),
        new Rectangle(24, 90, 27, 15),
    };
    public static List<Rectangle> WandUp = new List<Rectangle>() {
        new Rectangle(60, 114, 16, 28),
        new Rectangle(60, 114, 16, 28),
        new Rectangle(60, 114, 16, 28),
        new Rectangle(60, 114, 16, 28),
        new Rectangle(60, 114, 16, 28),
        new Rectangle(60, 114, 16, 28),
        new Rectangle(60, 114, 16, 28),
        new Rectangle(60, 114, 16, 28),
    };

    public static List<Rectangle> WandRight = new List<Rectangle>() {
        new Rectangle(84, 120, 27, 15),
        new Rectangle(84, 120, 27, 15),
        new Rectangle(84, 120, 27, 15),
        new Rectangle(84, 120, 27, 15),
        new Rectangle(84, 120, 27, 15),
        new Rectangle(84, 120, 27, 15),
        new Rectangle(84, 120, 27, 15),
        new Rectangle(84, 120, 27, 15),
    };

    public static List<Rectangle> WandDown = new List<Rectangle>() {
        new Rectangle(0, 114, 16, 28),
        new Rectangle(0, 114, 16, 28),
        new Rectangle(0, 114, 16, 28),
        new Rectangle(0, 114, 16, 28),
        new Rectangle(0, 114, 16, 28),
        new Rectangle(0, 114, 16, 28),
        new Rectangle(0, 114, 16, 28),
        new Rectangle(0, 114, 16, 28),
    };

    public static List<Rectangle> WandLeft = new List<Rectangle>() {
        new Rectangle(24, 120, 27, 15),
        new Rectangle(24, 120, 27, 15),
        new Rectangle(24, 120, 27, 15),
        new Rectangle(24, 120, 27, 15),
        new Rectangle(24, 120, 27, 15),
        new Rectangle(24, 120, 27, 15),
        new Rectangle(24, 120, 27, 15),
        new Rectangle(24, 120, 27, 15),
    };

    public static List<Rectangle> TakeDamage = new List<Rectangle>() {
        new Rectangle(241, 150, 13, 16),
        new Rectangle(241, 150, 13, 16),
        new Rectangle(241, 150, 13, 16),
        new Rectangle(241, 150, 13, 16),
        new Rectangle(241, 150, 13, 16),
        new Rectangle(241, 150, 13, 16),
        new Rectangle(241, 150, 13, 16),
        new Rectangle(241, 150, 13, 16),
        new Rectangle(241, 150, 13, 16),
    };

    public static List<Rectangle> PickupItem = new List<Rectangle>() {
        new Rectangle(31, 150, 14, 16),
        new Rectangle(31, 150, 14, 16),
        new Rectangle(31, 150, 14, 16),
        new Rectangle(31, 150, 14, 16),
        new Rectangle(31, 150, 14, 16),
        new Rectangle(31, 150, 14, 16),
        new Rectangle(31, 150, 14, 16),
        new Rectangle(31, 150, 14, 16),
        new Rectangle(31, 150, 14, 16),
        new Rectangle(31, 150, 14, 16),
        new Rectangle(31, 150, 14, 16),
    };

}


