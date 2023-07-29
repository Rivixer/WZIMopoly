using Microsoft.Xna.Framework;

namespace WZIMopoly.UI;

internal class UIFrame : UIComponent, IUIPositionInfluencer
{
    private readonly UIImage[] _lines = new UIImage[4];
    private int _thickness;
    private Color _color;

    public UIFrame(int thickness, Color color)
    {
        _thickness = thickness;
        _color = color;
        Transform.OnRecalculated += (s, e) => CreateLines();
    }

    public int Thickness
    {
        get { return _thickness; }
        set
        {
            _thickness = value;
            CreateLines();
        }
    }

    public Color Color
    {
        get { return _color; }
        set
        {
            _color = value;
            CreateLines();
        }
    }

    public Vector2 GetAdditionalRelativeOffsetForChildren()
    {
        Point size = Transform.UnscaledDestinationRectangle.Size;
        return new(Thickness / (float)size.X, Thickness / (float)size.Y);
    }
    
    public Vector2 GetAdditionalRelativeSizeForChildren()
    {
        return Vector2.One - 2 * GetAdditionalRelativeOffsetForChildren();
    }

    private void CreateLines()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_lines[i] is null)
            {
                _lines[i] = new UIImage(_color)
                {
                    Parent = this,
                    TransformType = TransformType.Absolute,
                };
            }

            // TODO: Refactor
            Rectangle rect = Transform.UnscaledDestinationRectangle;
            Vector2 thickness = new(_thickness);
            switch (i)
            {
                case 0: // Left line
                    rect.Width = (int)thickness.X;
                    rect.Height -= 2 * (int)thickness.Y;
                    rect.Y += (int)thickness.Y;
                    break;
                case 1: // Top line
                    rect.Height = (int)thickness.X;
                    break;
                case 2: // Right line
                    rect.X += rect.Width - (int)thickness.X;
                    rect.Y += (int)thickness.Y;
                    rect.Width = (int)thickness.X;
                    rect.Height -= 2 * (int)thickness.Y;
                    break;
                case 3: // Bottom line
                    rect.Y += rect.Height - (int)thickness.Y;
                    rect.Height = (int)thickness.Y;
                    break;
            }

            _lines[i].UnscaledDestinationRectangle = rect;
        }
    }
}
