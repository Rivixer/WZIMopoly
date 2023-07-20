using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal static class TransformExtenstions
{
    public static Point Scale(this Point point, Vector2 scale)
    {
        return new Point((int)(point.X * scale.X), (int)(point.Y * scale.Y));
    }    
    
    public static Vector2 Scale(this Vector2 point, Vector2 scale)
    {
        return new Vector2(point.X * scale.X, point.Y * scale.Y);
    }

    public static Ratio ToRatio(this Point point)
    {
        var gcd = MathUtils.GreatestCommonDivisor(point.X, point.Y);
        return new Ratio(point.X / gcd, point.Y / gcd);
    }

    public static Ratio ToRatio(this float value, double epsilon = 1.0e-2, int maxDenominator = 1000)
    {
        // Metody numeryczne się do czegoś przydały XD
        // Teraz Jankowski już nie powie, że jego syn - ósmoklasista,
        // znający składnię C# - też mógłby WZIMopoly zaprogramować XD
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
