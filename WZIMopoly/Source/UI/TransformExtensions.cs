using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal static class TransformExtensions
{
    public static Point Scale(this Point point, Vector2 scale)
    {
        return new Point((int)(point.X * scale.X), (int)(point.Y * scale.Y));
    }

    public static Point Scale(this Point point, float scale)
    {
        return new Point((int)(point.X * scale), (int)(point.Y * scale));
    }

    public static Point Unscale(this Point point, Vector2 scale)
    {
        return new Point(
            (int)Math.Ceiling(point.X / scale.X),
            (int)Math.Ceiling(point.Y / scale.Y));
    }

    public static Point Unscale(this Point point, float scale)
    {
        return new Point(
            (int)Math.Ceiling(point.X / scale),
            (int)Math.Ceiling(point.Y / scale));
    }

    public static Vector2 Scale(this Vector2 vector, Vector2 scale)
    {
        return new Vector2(vector.X * scale.X, vector.Y * scale.Y);
    }

    public static Vector2 Scale(this Vector2 vector, float scale)
    {
        return vector * scale;
    }

    public static Vector2 Unscale(this Vector2 vector, Vector2 scale)
    {
        return new Vector2(vector.X / scale.X, vector.Y / scale.Y);
    }

    public static Vector2 Unscale(this Vector2 vector, float scale)
    {
        return vector / scale;
    }

    public static bool ContainsAny(this Rectangle rect, Rectangle value)
    {
        return rect.Contains(value.Location)
            || rect.Contains(value.Location + value.Size)
            || rect.Contains(new Point(value.X, value.Y + value.Height))
            || rect.Contains(new Point(value.X + value.Width, value.Y));
    }

    public static Ratio ToRatio(this Point point)
    {
        var gcd = MathUtils.GreatestCommonDivisor(point.X, point.Y);
        return new Ratio(point.X / gcd, point.Y / gcd);
    }

    public static Ratio ToRatio(this float value, double epsilon = 1.0e-2, int maxDenominator = 1000)
    {
        for (int denominator = 1; denominator <= maxDenominator; denominator++)
        {
            int numerator = (int)Math.Round(value * denominator);
            if (Math.Abs(value - numerator / (double)denominator) < epsilon)
            {
                return new Ratio(numerator, denominator);
            }
        }
        throw new ArgumentException($"Unable to represent {value} as a ratio.");
    }
}
