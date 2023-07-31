using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal class UIFrame : UIComponent, IUIPositionInfluencer
{
    private readonly UIImage[] _lines = new UIImage[4];
    private int _unscaledThickness;
    private Point _scaledThickness;
    private UIContainer _innerRectangle;
    private Color _color;

    public UIFrame(int thickness, Color color)
        : base()
    {
        _unscaledThickness = thickness;
        _color = color;
        _innerRectangle = new UIContainer()
        {
            Parent = this,
            TransformType = TransformType.Absolute,
        };
        CreateLines();
        Transform.OnRecalculate += (s, e) =>
        {
            ScaleThickness();
            RecalculateLines();
        };

        OnParentChange += (s, e) =>
        {
            UIComponent child = (s as UIComponent)!;
            if (child != this && e.NewParent == this)
            {
                (s as UIComponent)!.Parent = _innerRectangle;
            }
        };
    }

    private void UIFrame_OnChildAdd(object? sender, EventArgs e)
    {
        UIComponent child = (sender as UIComponent)!;
        child.Parent = _innerRectangle;
    }

    public int Thickness
    {
        get { return _unscaledThickness; }
        set
        {
            if (_unscaledThickness != value)
            {
                _unscaledThickness = value;
                ScaleThickness();
                RecalculateLines();
            }
        }
    }

    public Vector2 GetAdditionalRelativeOffsetForChildren()
    {
        return Vector2.Zero;
    }

    public Vector2 GetAdditionalRelativeSizeForChildren()
    {
        return Vector2.One;
    }

    private void ScaleThickness()
    {
        _scaledThickness = new Point(
            (int)(_unscaledThickness * ScreenSystem.Scale.X + 0.5f),
            (int)(_unscaledThickness * ScreenSystem.Scale.Y + 0.5f));
    }

    private void CreateLines()
    {
        for (int i = 0; i < 4; i++)
        {
            _lines[i] = new UIImage(_color)
            {
                Parent = this,
                TransformType = TransformType.Absolute,
            };
        }
    }

    private void RecalculateLines()
    {
        Point referenceLocation = Transform.ScaledLocation;
        Point referenceSize = Transform.ScaledSize;

        // Left line
        _lines[0].Transform.ScaledLocation = new(
            referenceLocation.X,
            referenceLocation.Y + _scaledThickness.Y);
        _lines[0].Transform.ScaledSize = new(
            _scaledThickness.X,
            referenceSize.Y - 2 * _scaledThickness.Y);

        // Top line
        _lines[1].Transform.ScaledLocation = referenceLocation;
        _lines[1].Transform.ScaledSize = new(
            referenceSize.X,
            _scaledThickness.Y);

        // Right line
        _lines[2].Transform.ScaledLocation = new(
            referenceLocation.X + referenceSize.X - _scaledThickness.X,
            referenceLocation.Y + _scaledThickness.Y);
        _lines[2].Transform.ScaledSize = new(
            _scaledThickness.X,
            referenceSize.Y - 2 * _scaledThickness.Y);

        // Bottom line
        _lines[3].Transform.ScaledLocation = new(
            referenceLocation.X,
            referenceLocation.Y + referenceSize.Y - _scaledThickness.Y);
        _lines[3].Transform.ScaledSize = new(
            referenceSize.X,
            _scaledThickness.Y);

        /*_innerRectangle.Transform.ScaledLocation = new(
            referenceLocation.X + _scaledThickness.X,
            referenceLocation.Y + _scaledThickness.Y);
        _innerRectangle.Transform.ScaledLocation = new(
            referenceSize.X - 2 * _scaledThickness.X,
            referenceSize.Y - 2 * _scaledThickness.Y);*/
    }
}
