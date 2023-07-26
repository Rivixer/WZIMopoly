using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal enum TransformType
{
    Relative,
    Absolute,
}

internal abstract class UITransform
{
    private Ratio _ratio = Ratio.Unspecified;

    public UITransform(UIComponent component)
    {
        Component = component;
        if (component.Parent is not null)
        {
            component.Parent.Transform.OnRecalculated += ParentTransform_OnRecalculated;
        }
        else
        {
            ScreenSystem.OnScreenChanged += Recalculate;
        }
        NeedsRecalculate = true;
    }

    /*public UITransform(UIBaseComponent component, Rectangle rectangle)
        : this(component, rectangle.Location, rectangle.Size) { }

    public UITransform(UIBaseComponent component, Point location, Point size)
    {
        Component = component;
        _nonScaledLocation = location;
        _nonScaledSize = size;
        _needsRecalculcate = true;
        ScreenSystem.OnScreenChanged += Recalculate;
    }*/

    public delegate void OnRecalculatedEventHandler(object sender, EventArgs args);

    public event OnRecalculatedEventHandler? OnRecalculated;

    public UIComponent Component { get; set; }

    protected Point NonScaledLocation { get; set; }
    protected Point NonScaledSize { get; set; }

    protected Point ScaledLocation { get; set; }
    protected Point ScaledSize { get; set; }
    
    protected bool NeedsRecalculate { get; set; }

    public Rectangle DestinationRectangle
    {
        get { return new Rectangle(ScaledLocation, ScaledSize); }
        protected set
        {
            NonScaledLocation = value.Location.Scale(Vector2.One / ScreenSystem.Scale);
            NonScaledSize = value.Size.Scale(Vector2.One / ScreenSystem.Scale);
            NeedsRecalculate = true;
        }
    }

    public Rectangle NonScaledDestinationRectangle
    {
        get { return new Rectangle(NonScaledLocation, NonScaledSize); }
        protected set
        {
            NonScaledLocation = value.Location;
            NonScaledSize = value.Size;
            NeedsRecalculate = true;
        }
    }

    public Ratio Ratio
    {
        get { return _ratio; }
        set
        {
            _ratio = value;
            NeedsRecalculate = true;
        }
    }

    public void RecalculateIfNeeded()
    {
        if (NeedsRecalculate)
        {
            Recalculate();
        }
    }
    public virtual void Recalculate()
    {
        ScaledLocation = NonScaledLocation.Scale(ScreenSystem.Scale);
        ScaledSize = NonScaledSize.Scale(ScreenSystem.Scale);
        NeedsRecalculate = false;
        OnRecalculated?.Invoke(this, EventArgs.Empty);
        /*if (Reference is not null)
        {
            _nonScaledLocation = Parent._nonScaledLocation;
            _nonScaledSize = Parent._nonScaledSize.Scale(RelativeSize);

            RecalculateRatio();
            RecalculateAlignment();
            RecalculateMargin();
            RecalculateOffset();
        }
        
        _scaledLocation = _nonScaledLocation.Scale(ScreenSystem.Scale);
        _scaledSize = _nonScaledSize.Scale(ScreenSystem.Scale);*/
    }

    public void Component_OnParentChanged(UIComponent? oldParent, UIComponent? newParent)
    {
        if (oldParent is null)
        {
            ScreenSystem.OnScreenChanged -= Recalculate;
        }
        else
        {
            oldParent.Transform.OnRecalculated -= ParentTransform_OnRecalculated;
        }

        if (newParent is null)
        {
            ScreenSystem.OnScreenChanged += Recalculate;
        }
        else
        {
            newParent.Transform.OnRecalculated += ParentTransform_OnRecalculated;
        }
    }

    private void ParentTransform_OnRecalculated(object sender, EventArgs args)
    {
        Recalculate();
    }

    protected virtual void Recalculate_Ratio()
    {
        if (_ratio == Ratio.Unspecified)
        {
            return;
        }

        var currentRatio = NonScaledSize.ToRatio();
        if (currentRatio == _ratio)
        {
            return;
        }

        Point nonScaledSize = NonScaledSize;
        bool heightIsOversized = currentRatio.ToFloat() < _ratio.ToFloat();
        if (heightIsOversized)
        {
            nonScaledSize.Y = (int)(NonScaledSize.X / _ratio.ToFloat());
        }
        else
        {
            nonScaledSize.X = (int)(NonScaledSize.Y * _ratio.ToFloat());
        }
        NonScaledSize = nonScaledSize;
    }

    /*private void RecalculateAlignment()
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
    }*/
}
