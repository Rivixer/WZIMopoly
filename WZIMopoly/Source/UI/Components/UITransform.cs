using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal class UITransform
{
    private Point _nonScaledLocation;
    private Point _scaledLocation;
    private Point _nonScaledSize;
    private Point _scaledSize;

    private Vector2 _relativeOffset = Vector2.Zero;
    private Vector2 _relativeSize = Vector2.One;
    private Margin _relativeMargin = Margin.Zero;
    private Alignment _alignment = Alignment.TopLeft;
    private Ratio _ratio = Ratio.Unspecified;

    private bool _needsRecalculcate;

    public UITransform(UIComponent component)
    {
        Parent = component.Parent.Transform;
        Component = component;
        Parent.OnRecalculated += ParentTransform_OnRecalculated;
        _needsRecalculcate = true;
    }

    public UITransform(UIBaseComponent component, Rectangle rectangle)
        : this(component, rectangle.Location, rectangle.Size) { }

    public UITransform(UIBaseComponent component, Point location, Point size)
    {
        Component = component;
        _nonScaledLocation = location;
        _nonScaledSize = size;
        _needsRecalculcate = true;
        ScreenSystem.OnScreenChanged += Recalculate;
    }

    private delegate void OnRecalculatedEventHandler(UITransform sender, EventArgs args);
    private event OnRecalculatedEventHandler? OnRecalculated;

    public UIBaseComponent Component { get; set; }

    public UITransform Parent { get; set; }


    public Rectangle DestinationRectangle
    {
        get { return new Rectangle(_scaledLocation, _scaledSize); }
        set
        {
            _nonScaledLocation = value.Location.Scale(Vector2.One / ScreenSystem.Scale);
            _nonScaledSize = value.Size.Scale(Vector2.One / ScreenSystem.Scale);
            _needsRecalculcate = true;
        }
    }

    public Rectangle NonScaledDestinationRectangle
    {
        get { return new Rectangle(_nonScaledLocation, _nonScaledSize); }
        set
        {
            _nonScaledLocation = value.Location;
            _nonScaledSize = value.Size;
            _needsRecalculcate = true;
        }
    }

    public Vector2 RelativeSize
    {
        get { return _relativeSize; }
        set
        {
            _relativeSize = value;
            _needsRecalculcate = true;
        }
    }

    public Vector2 RelativeOffset
    {
        get { return _relativeOffset; }
        set
        {
            _relativeOffset = value;
            _needsRecalculcate = true;
        }
    }

    public Margin RelativeMargin
    {
        get { return _relativeMargin; }
        set
        {
            _relativeMargin = value;
            _needsRecalculcate = true;
        }
    }

    public Alignment Alignment
    {
        get { return _alignment; }
        set
        {
            _alignment = value;
            _needsRecalculcate = true;
        }
    }

    public Ratio Ratio
    {
        get { return _ratio; }
        set
        {
            _ratio = value;
            _needsRecalculcate = true;
        }
    }

    public void RecalculateIfNeeded()
    {
        if (_needsRecalculcate)
        {
            Recalculate();
        }
    }
    protected void Recalculate()
{
        if (Parent is not null)
        {
            _nonScaledLocation = Parent._nonScaledLocation;
            _nonScaledSize = Parent._nonScaledSize.Scale(RelativeSize);

            RecalculateMargin();
            RecalculateOffset();
            RecalculateRatio();
            RecalculateAlignment();
        }
        
        _scaledLocation = _nonScaledLocation.Scale(ScreenSystem.Scale);
        _scaledSize = _nonScaledSize.Scale(ScreenSystem.Scale);
        OnRecalculated?.Invoke(this, EventArgs.Empty);
        _needsRecalculcate = false;
    }

    private void ParentTransform_OnRecalculated(UITransform sender, EventArgs args)
    {
        Recalculate();
    }

    private void RecalculateRatio()
    {
        if (_ratio == Ratio.Unspecified)
        {
            return;
        }

        var currentRatio = _nonScaledSize.ToRatio();
        if (currentRatio == _ratio)
        {
            return;
        }

        bool heightIsOversized = currentRatio.ToFloat() < _ratio.ToFloat();
        if (heightIsOversized)
        {
            _nonScaledSize.Y = (int)(_nonScaledSize.X / _ratio.ToFloat());
        }
        else
        {
            _nonScaledSize.X = (int)(_nonScaledSize.Y * _ratio.ToFloat());
        }
    }

    private void RecalculateOffset()
    {
        _nonScaledLocation += Parent._nonScaledSize.Scale(RelativeOffset);
    }

    private void RecalculateMargin()
    {
        int offsetLeft = (int)(Parent._nonScaledSize.X * _relativeMargin.Left);
        int offsetTop = (int)(Parent._nonScaledSize.Y * _relativeMargin.Top);
        int offsetRight = -((int)(Parent._nonScaledSize.X * _relativeMargin.Right) + offsetLeft);
        int offsetBottom = -((int)(Parent._nonScaledSize.Y * _relativeMargin.Bottom) + offsetTop);
        _nonScaledLocation += new Point(offsetLeft, offsetTop);
        _nonScaledSize += new Point(offsetRight, offsetBottom);
    }

    private void RecalculateAlignment()
    {
        Rectangle parentRect = Parent.NonScaledDestinationRectangle;
        Rectangle currentRect;
        if (Component is UIText uiText)
        {
            Vector2 nonScaledFontSize = uiText.Font.MeasureString(uiText.Text).Scale(Vector2.One / ScreenSystem.Scale);
            currentRect = new(_nonScaledLocation, nonScaledFontSize.ToPoint());
        }
        else
        {
            currentRect = new(_nonScaledLocation, _nonScaledSize);
        }

        switch (Alignment)
        {
            case Alignment.TopLeft:
                break;
            case Alignment.Top:
                _nonScaledLocation.X = parentRect.X + (parentRect.Width - currentRect.Width) / 2;
                break;
            case Alignment.TopRight:
                _nonScaledLocation.X = parentRect.Right - currentRect.Width;
                break;
            case Alignment.Left:
                _nonScaledLocation.Y = parentRect.Y + (parentRect.Height - currentRect.Height) / 2;
                break;
            case Alignment.Center:
                Point offset = parentRect.Center - currentRect.Center;
                _nonScaledLocation.X += offset.X;
                _nonScaledLocation.Y += offset.Y;
                break;
            case Alignment.Right:
                _nonScaledLocation.X = parentRect.Right - currentRect.Width;
                _nonScaledLocation.Y = parentRect.Y + (parentRect.Height - currentRect.Height) / 2;
                break;
            case Alignment.BottomLeft:
                _nonScaledLocation.Y = parentRect.Bottom - currentRect.Height;
                break;
            case Alignment.Bottom:
                _nonScaledLocation.X = parentRect.X + (parentRect.Width - currentRect.Width) / 2;
                _nonScaledLocation.Y = parentRect.Bottom - currentRect.Height;
                break;
            case Alignment.BottomRight:
                _nonScaledLocation.X = parentRect.Right - currentRect.Width;
                _nonScaledLocation.Y = parentRect.Bottom - currentRect.Height;
                break; 
        }
    }
}
