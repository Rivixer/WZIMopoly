using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal class UITransform
{
    #region Fields

    private Point _unscaledLocation;
    private Point _unscaledSize;
    private Point _scaledLocation;
    private Point _scaledSize;

    private TransformType _transformType;
    private bool _needsRecalculate;

    // Relative data
    private Vector2 _relativeOffset = Vector2.Zero;
    private Vector2 _relativeSize = Vector2.One;
    private Alignment _alignment = Alignment.TopLeft;

    private Ratio _ratio = Ratio.Unspecified;

    #endregion

    public UITransform(UIComponent component)
    {
        Component = component;
        component.OnParentChanged += Component_OnParentChanged;
        if (component.Parent is not null)
        {
            component.Parent.Transform.OnRecalculated += ParentTransform_OnRecalculated;
        }
        else
        {
            ScreenSystem.OnScreenChanged += Recalculate;
        }
        _needsRecalculate = true;
    }

    public static UITransform Default(UIComponent component)
    {
        UITransform transform = new(component)
        {
            TransformType = TransformType.Absolute,
            _unscaledLocation = Point.Zero,
            _unscaledSize = ScreenSystem.DefaultSize
        };
        return transform;
    }

    public event EventHandler? OnRecalculated;

    public UIComponent Component { get; private set; }

    #region Transform Data Properties

    public TransformType TransformType
    {
        get { return _transformType; }
        set
        {
            if (value == _transformType)
            {
                return;
            }
            if (_transformType is TransformType.Relative && Component.Parent is null)
            {
                throw new InvalidOperationException(
                    "Cannot set TransformType to Relative when Component has no parent.");
            }
            _transformType = value;
            _needsRecalculate = true;
        }
    }

    public Vector2 RelativeSize
    {
        get { return _relativeSize; }
        set
        {
            CheckAndThrowIfNotRelative();
            if (value == _relativeSize)
            {
                return;
            }
            if (value.X < 0 || value.Y < 0)
            {
                throw new ArgumentException(
                    "RelativeSize cannot have negative components.");
            }
            _relativeSize = value;
            _needsRecalculate = true;
        }
    }

    public Vector2 RelativeOffset
    {
        get { return _relativeOffset; }
        set
        {
            CheckAndThrowIfNotRelative();
            if (value == _relativeOffset)
            {
                return;
            }
            _relativeOffset = value;
            _needsRecalculate = true;
        }
    }

    public Alignment Alignment
    {
        get { return _alignment; }
        set
        {
            CheckAndThrowIfNotRelative();
            if (value == _alignment)
            {
                return;
            }
            _alignment = value;
            _needsRecalculate = true;
        }
    }

    public Ratio Ratio
    {
        get { return _ratio; }
        set
        {
            if (value == _ratio)
            {
                return;
            }
            _ratio = value;
            _needsRecalculate = true;
        }
    }

    public Rectangle DestinationRectangle
    {
        get { return new Rectangle(_scaledLocation, _scaledSize); }
        set
        {
            CheckAndThrowIfNotAbsolute();
            if (value == DestinationRectangle)
            {
                return;
            }
            _unscaledLocation = value.Location.Scale(Vector2.One / ScreenSystem.Scale);
            _unscaledSize = value.Size.Scale(Vector2.One / ScreenSystem.Scale);
            _needsRecalculate = true;
        }
    }

    public Rectangle UnscaledDestinationRectangle
    {
        get { return new Rectangle(_unscaledLocation, _unscaledSize); }
        set
        {
            CheckAndThrowIfNotAbsolute();
            if (value == UnscaledDestinationRectangle)
            {
                return;
            }
            _unscaledLocation = value.Location;
            _unscaledSize = value.Size;
            _needsRecalculate = true;
        }
    }

    #endregion

    #region Recalculate Methods

    public void RecalculateIfNeeded()
    {
        if (_needsRecalculate)
        {
            Recalculate();
        }
    }

    public void Recalculate()
    {
        switch (TransformType)
        {
            case TransformType.Relative:
                RecalculateRelative();
                break;
            case TransformType.Absolute:
                RecalculateAbsolute();
                break;
        }
        _scaledLocation = _unscaledLocation.Scale(ScreenSystem.Scale);
        _scaledSize = _unscaledSize.Scale(ScreenSystem.Scale);
        _needsRecalculate = false;
        OnRecalculated?.Invoke(this, EventArgs.Empty);
    }

    private void RecalculateRelative()
    {
        UITransform reference = Component.Parent!.Transform;

        _unscaledLocation = reference._unscaledLocation;
        _unscaledSize = reference._unscaledSize;

        // TODO: Refactor offset and size calculation
        if (Component.Parent is IUIPositionInfluencer parent)
        {
            Vector2 additionalOffset = parent.GetAdditionalRelativeOffsetForChildren();
            _unscaledLocation += reference._unscaledSize.Scale(additionalOffset);

            Vector2 additionalSize = parent.GetAdditionalRelativeSizeForChildren();
            _unscaledSize -= reference._unscaledSize.Scale(Vector2.One - additionalSize);
        }

        RecalculateSize();
        RecalculateRatio();
        RecalculateAlignment();
        RecalculateOffset();

        void RecalculateOffset()
        {
            _unscaledLocation += reference._unscaledSize.Scale(_relativeOffset);
        }

        void RecalculateSize()
        {
            _unscaledSize -= reference._unscaledSize.Scale(Vector2.One - _relativeSize);
        }

        void RecalculateAlignment()
        {
            Rectangle referenceRect = reference.UnscaledDestinationRectangle;
            Rectangle currentRect;
            if (Component is UIText uiText)
            {
                Vector2 unscaledFontSize = uiText.MeasureDimensions();
                currentRect = new(_unscaledLocation, unscaledFontSize.ToPoint());
            }
            else
            {
                currentRect = new(_unscaledLocation, _unscaledSize);
            }

            switch (Alignment)
            {
                case Alignment.TopLeft:
                    break;
                case Alignment.Top:
                    _unscaledLocation.X = referenceRect.X + (referenceRect.Width - currentRect.Width) / 2;
                    break;
                case Alignment.TopRight:
                    _unscaledLocation.X = referenceRect.Right - currentRect.Width;
                    break;
                case Alignment.Left:
                    _unscaledLocation.Y = referenceRect.Y + (referenceRect.Height - currentRect.Height) / 2;
                    break;
                case Alignment.Center:
                    Point offset = referenceRect.Center - currentRect.Center;
                    _unscaledLocation.X += offset.X;
                    _unscaledLocation.Y += offset.Y;
                    break;
                case Alignment.Right:
                    _unscaledLocation.X = referenceRect.Right - currentRect.Width;
                    _unscaledLocation.Y = referenceRect.Y + (referenceRect.Height - currentRect.Height) / 2;
                    break;
                case Alignment.BottomLeft:
                    _unscaledLocation.Y = referenceRect.Bottom - currentRect.Height;
                    break;
                case Alignment.Bottom:
                    _unscaledLocation.X = referenceRect.X + (referenceRect.Width - currentRect.Width) / 2;
                    _unscaledLocation.Y = referenceRect.Bottom - currentRect.Height;
                    break;
                case Alignment.BottomRight:
                    _unscaledLocation.X = referenceRect.Right - currentRect.Width;
                    _unscaledLocation.Y = referenceRect.Bottom - currentRect.Height;
                    break;
            }
        }
    }

    private void RecalculateAbsolute()
    {
        RecalculateRatio();
    }

    private void RecalculateRatio()
    {
        if (_ratio == Ratio.Unspecified)
        {
            return;
        }

        var currentRatio = _unscaledSize.ToRatio();
        if (currentRatio == _ratio)
        {
            return;
        }

        Point unscaledSize = _unscaledSize;
        bool heightIsOversized = currentRatio.ToFloat() < _ratio.ToFloat();
        if (heightIsOversized)
        {
            unscaledSize.Y = (int)(_unscaledSize.X / _ratio.ToFloat());
        }
        else
        {
            unscaledSize.X = (int)(_unscaledSize.Y * _ratio.ToFloat());
        }
        _unscaledSize = unscaledSize;
    }

    #endregion

    public void Destroy()
    {
        if (Component.Parent is null)
        {
            ScreenSystem.OnScreenChanged -= Recalculate;
        }
        else
        {
            Component.Parent.Transform.OnRecalculated -= ParentTransform_OnRecalculated;
        }
    }

    #region Event Handlers

    private void ParentTransform_OnRecalculated(object? sender, EventArgs args)
    {
        Recalculate();
    }

    private void Component_OnParentChanged(object? sender, ParentChangedEventArgs args)
    {
        if (args.OldParent is { } oldParent)
        {
            oldParent.Transform.OnRecalculated -= ParentTransform_OnRecalculated;
        }
        else
        {
            ScreenSystem.OnScreenChanged -= Recalculate;
        }

        if (args.NewParent is { } newParent)
        {
            newParent.Transform.OnRecalculated += ParentTransform_OnRecalculated;
        }
        else
        {
            ScreenSystem.OnScreenChanged += Recalculate;
        }
    }

    #endregion

    #region Validate Methods

    private void CheckAndThrowIfNotRelative()
    {
        if (TransformType != TransformType.Relative)
        {
            throw new InvalidOperationException(
                "Transform must be of type Relative.");
        }
    }

    private void CheckAndThrowIfNotAbsolute()
    {
        if (TransformType != TransformType.Absolute)
        {
            throw new InvalidOperationException(
                "Transform must be of type Absolute.");
        }
    }

    #endregion
}
