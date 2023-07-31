using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly;

internal static class MathUtils
{
    public static int GreatestCommonDivisor(int x, int y)
    {
        return y == 0 ? x : GreatestCommonDivisor(y, x % y);
    }

    public static Point Clamp(Point point, Point min, Point max)
    {
        return new Point(
            Math.Clamp(point.X, min.X, max.X),
            Math.Clamp(point.Y, min.Y, max.Y));
    }
}
