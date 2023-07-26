using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal class UITransformRelative : UITransform
{
    private Vector2 _relativeOffset = Vector2.Zero;
    private Vector2 _relativeSize = Vector2.One;
    private Alignment _alignment = Alignment.TopLeft;

    public UITransformRelative(UIComponent component)
        : base(component)
    {
        if (component.Parent is null)
        {
            throw new ArgumentNullException("Component",
                "Component must have a parent. Use UITransformAbsolute instead.");
        }
        Reference.OnRecalculated += Reference_OnRecalculated;
    }

    public UITransformRelative(UITransform transform)
        : this(transform.Component) { }

    public UITransform Reference => Component.Parent!.Transform;

    public Vector2 RelativeSize
    {
        get { return _relativeSize; }
        set
        {
            _relativeSize = value;
            NeedsRecalculate = true;
        }
    }

    public Vector2 RelativeOffset
    {
        get { return _relativeOffset; }
        set
        {
            _relativeOffset = value;
            NeedsRecalculate = true;
        }
    }

    public Alignment Alignment
    {
        get { return _alignment; }
        set
        {
            _alignment = value;
            NeedsRecalculate = true;
        }
    }

    public override void Recalculate()
    {
        Recalculate_Size();
        Recalculate_Ratio();
        Recalculate_Alignment();
        Recalculate_Offset();
        base.Recalculate();
    }

    private void Reference_OnRecalculated(object sender, EventArgs e)
    {
        Recalculate();
    }

    private void Recalculate_Offset()
    {
        Point nonScaledLocation = NonScaledLocation;
        nonScaledLocation += Reference.NonScaledDestinationRectangle.Location.Scale(_relativeOffset);
        NonScaledLocation = nonScaledLocation;
    }

    private void Recalculate_Size()
    {
        Point nonScaledSize = Reference.NonScaledDestinationRectangle.Size.Scale(_relativeSize);
        NonScaledSize = nonScaledSize;
    }

    private void Recalculate_Alignment()
    {
        Rectangle referenceRect = Reference.NonScaledDestinationRectangle;
        Rectangle currentRect;
        if (Component is UIText uiText)
        {
            Vector2 nonScaledFontSize = uiText.Font.MeasureString(uiText.Text).Scale(Vector2.One / ScreenSystem.Scale);
            currentRect = new(NonScaledLocation, nonScaledFontSize.ToPoint());
        }
        else
        {
            currentRect = new(NonScaledLocation, NonScaledSize);
        }

        Point nonScaledLocation = NonScaledLocation;
        switch (Alignment)
        {
            case Alignment.TopLeft:
                break;
            case Alignment.Top:
                nonScaledLocation.X = referenceRect.X + (referenceRect.Width - currentRect.Width) / 2;
                break;
            case Alignment.TopRight:
                nonScaledLocation.X = referenceRect.Right - currentRect.Width;
                break;
            case Alignment.Left:
                nonScaledLocation.Y = referenceRect.Y + (referenceRect.Height - currentRect.Height) / 2;
                break;
            case Alignment.Center:
                Point offset = referenceRect.Center - currentRect.Center;
                nonScaledLocation.X += offset.X;
                nonScaledLocation.Y += offset.Y;
                break;
            case Alignment.Right:
                nonScaledLocation.X = referenceRect.Right - currentRect.Width;
                nonScaledLocation.Y = referenceRect.Y + (referenceRect.Height - currentRect.Height) / 2;
                break;
            case Alignment.BottomLeft:
                nonScaledLocation.Y = referenceRect.Bottom - currentRect.Height;
                break;
            case Alignment.Bottom:
                nonScaledLocation.X = referenceRect.X + (referenceRect.Width - currentRect.Width) / 2;
                nonScaledLocation.Y = referenceRect.Bottom - currentRect.Height;
                break;
            case Alignment.BottomRight:
                nonScaledLocation.X = referenceRect.Right - currentRect.Width;
                nonScaledLocation.Y = referenceRect.Bottom - currentRect.Height;
                break;
        }
        NonScaledLocation = nonScaledLocation;
    }
}

internal class UITransformAbsolute : UITransform
{
    public UITransformAbsolute(UIComponent component)
        : base(component)
    {
        NonScaledDestinationRectangle = new(Point.Zero, ScreenSystem.DefaultSize);
    }

    public UITransformAbsolute(UITransform transform)
        : this(transform.Component)
    {
        NonScaledDestinationRectangle = transform.NonScaledDestinationRectangle;
    }

    public new Rectangle NonScaledDestinationRectangle
    {
        get { return base.NonScaledDestinationRectangle; }
        set { base.NonScaledDestinationRectangle = value; }
    }

    public new Rectangle DestinationRectangle
    {
        get { return base.DestinationRectangle; }
        set { base.DestinationRectangle = value; }
    }

    public override void Recalculate()
    {
        Recalculate_Ratio();
        base.Recalculate();
    }
}