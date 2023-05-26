using System;
using Microsoft.Xna.Framework;
namespace JalPals.Rooms;

public struct SurvivalWaves
{
    public static int[][] enemies = new int[][] {
        new int[] {0, 0, 0, 0},
        new int[] { 0, 0, 0, 0},
        new int[] { 1, 1, 1, 1 },
        new int[] { 2, 2, 2, 2 },
        new int[] { 4, 4, 4, 4 },
        new int[] { 7, 7, 7, 7 },
        new int[] { 5, 7, 4, 2 },
        new int[] { 5, 6, 0, 5 },
        new int[] { 1, 1, 1, 1, 1, 1, 0, 5},
    };
    public static Vector2[] spawnLocations = new Vector2[] {
        new Vector2(100, 300),
        new Vector2(150, 350),
        new Vector2(200, 350),
        new Vector2(250, 400),
        new Vector2(300, 400),
        new Vector2(350, 400),
        new Vector2(400, 450),
        new Vector2(450, 450),
        new Vector2(500, 500),
        new Vector2(550, 500),
        new Vector2(600, 550),
    };
}

