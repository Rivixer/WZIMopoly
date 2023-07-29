using System;

namespace WZIMopoly.UI;

internal struct Ratio : IEquatable<Ratio>
{
    public int Numerator;
    public int Denumerator;

    public Ratio(int numerator, int denominator)
    {
        Numerator = numerator;
        Denumerator = denominator;
    }

    public static Ratio Unspecified => new(0, 0);

    public static bool operator ==(Ratio left, Ratio right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Ratio left, Ratio right)
    {
        return !(left == right);
    }

    public static implicit operator Ratio(float value)
    {
        return value.ToRatio();
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is Ratio ratio && Equals(ratio);
    }

    public override readonly int GetHashCode()
    {
        return Numerator.GetHashCode() ^ Denumerator.GetHashCode();
    }

    public readonly bool Equals(Ratio other)
    {
        return Numerator == other.Numerator
            && Denumerator == other.Denumerator;
    }

    public readonly float ToFloat()
    {
        return Numerator / (float)Denumerator;
    }

    public override readonly string ToString()
    {
        if (this == Unspecified)
        {
            return nameof(Unspecified);
        }
        return $"{Numerator}:{Denumerator}";
    }
}
