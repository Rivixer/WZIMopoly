using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal static class TransformExtensions
{
    public static Point Scale(this Point point, Vector2 scale)
    {
        return new Point((int)(point.X * scale.X + 0.5), (int)(point.Y * scale.Y + 0.5));
    }

    public static Point Scale(this Point point, float scale)
    {
        return new Point((int)(point.X * scale + 0.5), (int)(point.Y * scale + 0.5));
    }

    public static Vector2 Scale(this Vector2 vector, Vector2 scale)
    {
        return new Vector2(vector.X * scale.X, vector.Y * scale.Y);
    }

    public static Vector2 Scale(this Vector2 vector, float scale)
    {
        return vector * scale;
    }

    public static Rectangle Scale(this Rectangle rect, Vector2 scale)
    {
        return new Rectangle(rect.Location.Scale(scale), rect.Size.Scale(scale));
    }

    public static Rectangle Scale(this Rectangle rect, float scale)
    {
        return new Rectangle(rect.Location.Scale(scale), rect.Size.Scale(scale));
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
