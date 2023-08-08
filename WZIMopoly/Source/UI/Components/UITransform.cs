﻿using Microsoft.Xna.Framework;
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
    private bool _needsRecalculation;

    // Relative data
    private Vector2 _relativeOffset = Vector2.Zero;
    private Vector2 _relativeSize = Vector2.One;
    private Alignment _alignment = Alignment.TopLeft;

    private Point _minSize = new(1);
    private Point _maxSize = new(int.MaxValue);

    private Ratio _ratio = Ratio.Unspecified;

    #endregion

    public UITransform(UIComponent component)
    {
        Component = component;
        component.OnParentChange += Component_OnParentChange;
        if (component.Parent is not null)
        {
            component.Parent.Transform.OnRecalculate += ParentTransform_OnRecalculate;
        }
        else
        {
            ScreenSystem.OnScreenChange += Recalculate;
        }
        _needsRecalculation = true;
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

    public event EventHandler? OnRecalculate;

    public UIComponent Component { get; private set; }

    #region Transform Data Properties

    public TransformType TransformType
    {
        get { return _transformType; }
        set
        {
            if (_transformType == value)
            {
                return;
            }
            if (value is TransformType.Relative && Component.Parent is null)
            {
                throw new InvalidOperationException(
                    "Cannot set TransformType to Relative when Component has no parent.");
            }
            _transformType = value;
            _needsRecalculation = true;
        }
    }

    public Vector2 RelativeSize
    {
        get { return _relativeSize; }
        set
        {
            if (_relativeSize == value)
            {
                return;
            }
            if (value.X < 0 || value.Y < 0)
            {
                throw new ArgumentException(
                    "RelativeSize cannot have negative components.",
                    nameof(value));
            }
            _relativeSize = value;
            _needsRecalculation = true;
        }
    }

    public Vector2 RelativeOffset
    {
        get { return _relativeOffset; }
        set
        {
            if (_relativeOffset == value)
            {
                return;
            }
            _relativeOffset = value;
            _needsRecalculation = true;
        }
    }

    public Alignment Alignment
    {
        get { return _alignment; }
        set
        {
            if (_alignment == value)
            {
                return;
            }
            _alignment = value;
            _needsRecalculation = true;
        }
    }

    public Point MinSize
    {
        get { return _minSize; }
        set
        {
            if (_minSize == value)
            {
                return;
            }
            if (value.X < 0 || value.Y < 0)
            {
                throw new ArgumentException(
                    "MinSize cannot have negative components.",
                    nameof(value));
            }
            _minSize = value;
            _needsRecalculation = true;
        }
    }

    public Point MaxSize
    {
        get { return _maxSize; }
        set
        {
            if (_maxSize == value)
            {
                return;
            }
            if (value.X < _minSize.X || value.Y < _minSize.Y)
            {
                throw new ArgumentException(
                    "MaxSize cannot be smaller than MinSize.",
                    nameof(value));
            }
            _maxSize = value;
            _needsRecalculation = true;
        }
    }

    public Ratio Ratio
    {
        get { return _ratio; }
        set
        {
            if (_ratio == value)
            {
                return;
            }
            _ratio = value;
            _needsRecalculation = true;
        }
    }

    public Point ScaledLocation
    {
        get { return _scaledLocation; }
        set
        {
            if (ScaledLocation == value)
            {
                return;
            }
            _unscaledLocation = new((int)Math.Ceiling(value.X * (1 / ScreenSystem.Scale.X)), (int)Math.Ceiling(value.Y * (1 / ScreenSystem.Scale.Y)));
            _needsRecalculation = true;
        }
    }

    public Point UnscaledLocation
    {
        get { return _unscaledLocation; }
        set
        {
            if (_unscaledLocation == value)
            {
                return;
            }
            _unscaledLocation = value;
            _needsRecalculation = true;
        }
    }

    public Point ScaledSize
    {
        get { return _scaledSize; }
        set
        {
            if (_scaledSize == value)
            {
                return;
            }
            _unscaledSize = new((int)Math.Ceiling(value.X * (1 / ScreenSystem.Scale.X)), (int)Math.Ceiling(value.Y * (1 / ScreenSystem.Scale.Y)));
            _needsRecalculation = true;
        }
    }

    public Point UnscaledSize
    {
        get { return _unscaledSize; }
        set
        {
            if (_unscaledSize == value)
            {
                return;
            }
            _unscaledSize = value;
            _needsRecalculation = true;
        }
    }

    public Rectangle DestinationRectangle
        => new(_scaledLocation, _scaledSize);

    public Rectangle UnscaledDestinationRectangle
        => new(_unscaledLocation, _unscaledSize);

    public Vector2 UnscaledDimensions
    {
        get
        {
            if (Component is UIText uiText)
            {
                return uiText.MeasureDimensions();
            }
            return _unscaledSize.ToVector2();
        }
    }

    public Vector2 ScaledDimensions
        => UnscaledDimensions.Scale(ScreenSystem.Scale);

    #endregion

    #region Recalculate Methods

    public void RecalculateIfNeeded()
    {
        if (_needsRecalculation)
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
        _scaledSize = MathUtils.Clamp(_unscaledSize.Scale(ScreenSystem.Scale), _minSize, _maxSize);
        _needsRecalculation = false;
        OnRecalculate?.Invoke(this, EventArgs.Empty);
    }

    private void RecalculateRelative()
    {
        UITransform reference = Component.Parent!.Transform;

        _unscaledLocation = reference._unscaledLocation;
        _unscaledSize = reference._unscaledSize;

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
            _unscaledSize = reference._unscaledSize.Scale(_relativeSize);
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
                    _unscaledLocation.X += (referenceRect.Center - currentRect.Center).X;
                    _unscaledLocation.Y += (referenceRect.Center - currentRect.Center).Y;
                    break;
                case Alignment.Right:
                    _unscaledLocation.X = referenceRect.Right - currentRect.Width;
                    _unscaledLocation.Y = referenceRect.Y + (int)(referenceRect.Height - currentRect.Height) / 2;
                    break;
                case Alignment.BottomLeft:
                    _unscaledLocation.Y = referenceRect.Bottom - currentRect.Height;
                    break;
                case Alignment.Bottom:
                    _unscaledLocation.X = referenceRect.X + (int)(referenceRect.Width - currentRect.Width) / 2;
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
            ScreenSystem.OnScreenChange -= Recalculate;
        }
        else
        {
            Component.Parent.Transform.OnRecalculate -= ParentTransform_OnRecalculate;
        }
    }

    #region Event Handlers

    private void ParentTransform_OnRecalculate(object? sender, EventArgs args)
    {
        Recalculate();
    }

    private void Component_OnParentChange(object? sender, ParentChangeEventArgs args)
    {
        if (args.OldParent is { } oldParent)
        {
            oldParent.Transform.OnRecalculate -= ParentTransform_OnRecalculate;
        }
        else
        {
            ScreenSystem.OnScreenChange -= Recalculate;
        }

        if (args.NewParent is { } newParent)
        {
            newParent.Transform.OnRecalculate += ParentTransform_OnRecalculate;
        }
        else
        {
            ScreenSystem.OnScreenChange += Recalculate;
        }
    }

    #endregion
}
