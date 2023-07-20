namespace WZIMopoly.UI;

internal struct Padding
{
    public float Left;
    public float Top;
    public float Right;
    public float Bottom;

    public Padding(float left = 0.0f, float top = 0.0f, float right = 0.0f, float bottom = 0.0f)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public Padding(float padding)
    {
        Left = padding;
        Top = padding;
        Right = padding;
        Bottom = padding;
    }

    public static Padding Zero => new(0.0f);
}
