using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal class UITransform
{
    private Rectangle _nonScaledDestinationRectangle;
    private Rectangle _scaledDestinationRectangle;
    private Point _nonScaledLocation;
    private Point _scaledLocation;
    private Point _nonScaledSize;
    private Point _scaledSize;

    private Vector2 _relativeOffset = Vector2.Zero;
    private Vector2 _relativeSize = Vector2.One;
    private Margin _relativeMargin = Margin.Zero;
    private Padding _relativePadding = Padding.Zero;
    private Alignment _alignment = Alignment.TopLeft;
    private Ratio _ratio = Ratio.Unspecified;

    private bool _needsRecalculcate;

    public UITransform(UIComponent component)
    {
        UITransform parentTransform = component.Parent.Transform
            ?? throw new ArgumentException("Parent of component must have a transform. ", nameof(component));

        Component = component;

        _nonScaledLocation = parentTransform._nonScaledDestinationRectangle.Location;
        _nonScaledSize = parentTransform._nonScaledDestinationRectangle.Size;

        parentTransform.OnRecalculated += ParentTransform_OnRecalculated;
        _needsRecalculcate = true;
    }

    public UITransform(UIBaseComponent baseParent, Rectangle rectangle)
        : this(baseParent, rectangle.Location, rectangle.Size) { }

    public UITransform(UIBaseComponent baseComponent, Point location, Point size)
    {
        if (baseComponent is UIComponent)
        {
            throw new ArgumentException(
                "Parent must be a UIBaseComponent. " +
                "Use UITransform(UIComponent) constructor instead.",
                nameof(baseComponent));
        }

        Component = baseComponent;
        _nonScaledLocation = location;
        _nonScaledSize = size;
        _needsRecalculcate = true;
        ScreenSystem.OnScreenChanged += Recalculate;
    }

    private delegate void OnRecalculatedEventHandler(UITransform sender, EventArgs args);
    private event OnRecalculatedEventHandler? OnRecalculated;

    public UIBaseComponent Component { get; private set; }

    public Rectangle DestinationRectangle
    {
        get { return _scaledDestinationRectangle; }
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

    public Padding RelativePadding
    {
        get { return _relativePadding; }
        set
        {
            _relativePadding = value;
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
        if (Component is UIComponent component)
        {
            UITransform parentTransform = component.Parent.Transform;
            _nonScaledLocation = parentTransform._nonScaledLocation;
            _nonScaledSize = parentTransform._nonScaledSize.Scale(RelativeSize);

            RecalculateMargin(parentTransform);
            RecalculatePadding(parentTransform);
            RecalculateOffset(parentTransform);
            RecalculateRatio();
            RecalculateAlignment(parentTransform);
        }

        _scaledLocation = _nonScaledLocation.Scale(ScreenSystem.Scale);
        _scaledSize = _nonScaledSize.Scale(ScreenSystem.Scale);
        _scaledDestinationRectangle = new Rectangle(_scaledLocation, _scaledSize);
        _nonScaledDestinationRectangle = new Rectangle(_nonScaledLocation, _nonScaledSize);
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

    private void RecalculateOffset(UITransform parentTransform)
    {
        _nonScaledLocation += parentTransform._nonScaledSize.Scale(RelativeOffset);
    }

    private void RecalculateMargin(UITransform parentTransform)
    {
        int offsetLeft = (int)(parentTransform._nonScaledSize.X * _relativeMargin.Left);
        int offsetTop = (int)(parentTransform._nonScaledSize.Y * _relativeMargin.Top);
        int offsetRight = -((int)(parentTransform._nonScaledSize.X * _relativeMargin.Right) + offsetLeft);
        int offsetBottom = -((int)(parentTransform._nonScaledSize.Y * _relativeMargin.Bottom) + offsetTop);
        _nonScaledLocation += new Point(offsetLeft, offsetTop);
        _nonScaledSize += new Point(offsetRight, offsetBottom);
    }

    private void RecalculatePadding(UITransform parentTransform)
    {
        if (parentTransform.Component is UIContainer container && Component == container.Background)
        {
            return;
        }

        UITransform? grandparentTransform = (parentTransform.Component as UIComponent)?.Parent.Transform;
        Point grandparentSize = grandparentTransform != null
            ? grandparentTransform._nonScaledSize
            : ScreenSystem.DefaultSize;
        int offsetLeft = (int)(grandparentSize.X * parentTransform._relativePadding.Left);
        int offsetTop = (int)(grandparentSize.Y * parentTransform._relativePadding.Top);
        int offsetRight = -(int)((grandparentSize.X * parentTransform._relativePadding.Right) + offsetLeft);
        int offsetBottom = -((int)(grandparentSize.Y * parentTransform._relativePadding.Bottom) + offsetTop);
        _nonScaledLocation += new Point(offsetLeft, offsetTop);
        _nonScaledSize += new Point(offsetRight, offsetBottom);
    }

    private void RecalculateAlignment(UITransform parentTransform)
    {
        Rectangle parentRect = parentTransform._nonScaledDestinationRectangle;
        Rectangle currentRect;
        if (Component is UIText uiText)
        {
            Vector2 scaledFontSize = uiText.Font.MeasureString(uiText.Text).Scale(Vector2.One / ScreenSystem.Scale);
            currentRect = new(_nonScaledLocation, scaledFontSize.ToPoint());
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