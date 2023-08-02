using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace WZIMopoly.UI;

internal class UIFrame : UIComponent
{
    private readonly UIImage[] _lines = new UIImage[4];
    private readonly UIContainer _innerRectangle;

    private int _thickness;
    private Color _color;

    public UIFrame(int thickness, Color color)
        : base()
    {
        _thickness = thickness;
        _color = color;

        _innerRectangle = new UIContainer()
        {
            Parent = this,
            TransformType = TransformType.Absolute,
        };

        UpdateLines(); // Force creation

        Transform.OnRecalculate += Transform_OnRecalculate;
        OnChildAdd += Component_OnChildAdd;
    }

    public int Thickness
    {
        get { return _thickness; }
        set
        {
            if (_thickness == value)
            {
                return;
            }    
            _thickness = value;
            UpdateLines();
        }
    }

    private void UpdateLines()
    {
        int updatedLines = 0;
        Rectangle referenceRect = Transform.UnscaledDestinationRectangle;

        void UpdateLine(Vector2 start, Vector2 end)
        {
            if (_lines[updatedLines] is null)
            {
                _lines[updatedLines] = new UIImage(_color)
                {
                    Parent = this,
                    TransformType = TransformType.Absolute,
                    Rotation = (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                    Origin = Vector2.Zero,
                };
            }

            float length = Vector2.Distance(start, end);
            _lines[updatedLines].Transform.UnscaledLocation = start.ToPoint();
            _lines[updatedLines++].Transform.UnscaledSize = new((int)length, _thickness);
        }

        UpdateLine( // Top line
            new(referenceRect.Left + _thickness, referenceRect.Top),
            new(referenceRect.Right - _thickness, referenceRect.Top));
        UpdateLine( // Bottom line
            new(referenceRect.Left, referenceRect.Bottom - _thickness),
            new(referenceRect.Right, referenceRect.Bottom - _thickness));
        UpdateLine( // Left line
            new(referenceRect.Left + _thickness, referenceRect.Top),
            new(referenceRect.Left + _thickness, referenceRect.Bottom - _thickness));
        UpdateLine( // Right line
            new(referenceRect.Right, referenceRect.Top),
            new(referenceRect.Right, referenceRect.Bottom - _thickness));

        _innerRectangle.Transform.UnscaledLocation = new(
            referenceRect.X + _thickness,
            referenceRect.Y + _thickness);
        _innerRectangle.Transform.UnscaledSize = new(
            referenceRect.Width - 2 * _thickness,
            referenceRect.Height - 2 * _thickness);
    }

    private void Transform_OnRecalculate(object? sender, EventArgs e)
    {
        UpdateLines();
    }

    /// <summary>
    /// Switches the parent of an added component to <see cref="_innerRectangle"/>.
    /// </summary>
    private void Component_OnChildAdd(object? sender, ChildChangeEventArgs e)
    {
        UIComponent child = e.Child;
        child.Parent = _innerRectangle;
    }
}
