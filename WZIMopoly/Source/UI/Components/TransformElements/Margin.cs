namespace WZIMopoly.UI;

internal struct Margin
{
    public float Left;
    public float Top;
    public float Right;
    public float Bottom;

    public Margin(float left = 0.0f, float top = 0.0f, float right = 0.0f, float bottom = 0.0f)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public Margin(float margin)
    {
        Left = margin;
        Top = margin;
        Right = margin;
        Bottom = margin;
    }

    public static Margin Zero => new(0.0f);
}
