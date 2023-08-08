using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WZIMopoly.UI;

/// <summary>
/// Represents a srollable list box for containing UI elements.
/// </summary>
internal class UIListBox : UIComponent
{
    private readonly List<UIComponent> _elements = new();
    private readonly UIContainer _container;

    private int _elementSpacing;
    private int _scrollOffset;
    private float _totalLength;

    private Orientation _orientation;
    private UIScrollBar? _scrollBar;

    private bool _needsRecalculation;

    /// <summary>
    /// Initializes a new instance of the <see cref="UIListBox"/> class.
    /// </summary>
    public UIListBox()
        : base()
    {
        _container = new() { Parent = this };
        Transform.OnRecalculate += Transform_OnRecalculate;
    }

    /// <summary>
    /// Gets the list of elements in the list box.
    /// </summary>
    public IEnumerable<UIComponent> Elements => _elements;

    /// <summary>
    /// Gets or sets the spacing between elements.
    /// </summary>
    public int ElementSpacing
    {
        get { return _elementSpacing; }
        set
        {
            _elementSpacing = value;
            _needsRecalculation = true;
        }
    }

    /// <summary>
    /// Gets or sets the orientation of the list box.
    /// </summary>
    public Orientation Orientation
    {
        get { return _orientation; }
        set
        {
            _orientation = value;
            _needsRecalculation = true;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the list box is scrollable.
    /// </summary>
    public bool IsScrollable
    {
        get { return _scrollBar is not null; }
        set
        {
            bool hasScrollBar = _scrollBar is not null;
            if (hasScrollBar == value)
            {
                return;
            }

            if (value)
            {
                CalculateTotalLength();
                CreateScrollBar();
            }
            else
            {
                _scrollBar!.Destroy();
                _scrollBar = null;
            }

            AdjustContainerSize();

            _needsRecalculation = true;
        }
    }

    /// <summary>
    /// Adds an element to the list box.
    /// </summary>
    /// <param name="element">The element to add.</param>
    public void AddElement(UIComponent element)
    {
        element.Parent = _container;
        element.OnParentChange += Element_OnParentChange;
        element.Transform.OnRecalculate += ElementTransform_OnRecalculate;
        _elements.Add(element);

        UpdateScrollBarIfExists();
        _needsRecalculation = true;
    }

    private void Element_OnParentChange(object? sender, ParentChangeEventArgs e)
    {
        if (e.NewParent != this)
        {
            RemoveElement((UIComponent)sender!);
        }
    }

    private void ElementTransform_OnRecalculate(object? sender, EventArgs e)
    {
        _needsRecalculation = true;
    }

    public void RemoveElement(UIComponent element)
    {
        _elements.Remove(element);
        element.OnParentChange -= Element_OnParentChange;
        element.Transform.OnRecalculate -= ElementTransform_OnRecalculate;
        element.Parent = null;

        UpdateScrollBarIfExists();
        _needsRecalculation = true;
    }

    public override void Update(GameTime gameTime)
    {
        if (Transform.DestinationRectangle.Contains(MouseSystem.Position))
        {
            HandleScroll();
        }

        if (_needsRecalculation)
        {
            RecalculateElements();
            UpdateScrollBarIfExists();
            _needsRecalculation = false;
        }

        base.Update(gameTime);
    }

    public void JumpToElement(UIComponent component)
    {
        float distanceToShift = 0;
        foreach (UIComponent element in _elements)
        {
            if (component == element)
            {
                break;
            }
            distanceToShift += GetComponentLength(element);
            distanceToShift += _elementSpacing;
        }

        if (distanceToShift == 0)
        {
            throw new ArgumentException("The provided component was not found in the list.");
        }

        _scrollOffset = (int)Math.Clamp(distanceToShift, 0.0f, _totalLength - Transform.UnscaledDestinationRectangle.Height);
        _needsRecalculation = true;
    }

    public override void Draw(GameTime gameTime)
    {
        Rectangle scissorRect = _container.Transform.DestinationRectangle;

        using (ContentSystem.SpriteBatch.UseScissorRectangle(scissorRect))
        {
            foreach (UIComponent element in _elements)
            {
                if (scissorRect.ContainsAny(element.Transform.DestinationRectangle))
                {
                    element.Draw(gameTime);
                }
            }
        }

        _scrollBar?.Draw(gameTime);
    }

    /// <summary>
    /// Handles scrolling based on mouse input.
    /// </summary>
    private void HandleScroll()
    {
        int scrollDelta = MouseSystem.ScrollDelta;
        if (scrollDelta != 0)
        {
            int maxScrollOffset = (int)_elements.Sum(element => GetComponentLength(element) + _elementSpacing);
            if (Orientation is Orientation.Vertical)
            {
                maxScrollOffset -= Transform.UnscaledDestinationRectangle.Height;
            }
            else
            {
                maxScrollOffset -= Transform.UnscaledDestinationRectangle.Width;
            }
            
            _scrollOffset = MathHelper.Clamp(_scrollOffset - scrollDelta, 0, maxScrollOffset);
            _needsRecalculation = true;
        }
    }

    private void RecalculateElements()
    {
        CalculateTotalLength();
        float currentOffset = 0;
        foreach (UIComponent element in _elements)
        {
            SetElementOffset(element, currentOffset);
            currentOffset += GetComponentLength(element);
            currentOffset += _elementSpacing;
        }
    }

    private void SetElementOffset(UIComponent element, float absoluteOffset)
    {
        int length;
        if (_orientation is Orientation.Vertical)
        {
            length = element.Parent?.Transform.UnscaledDestinationRectangle.Height ?? 1;
            element.Transform.RelativeOffset = new(0.0f, (absoluteOffset - _scrollOffset) / length);
        }
        if (_orientation is Orientation.Horizontal)
        {
            length = element.Parent?.Transform.UnscaledDestinationRectangle.Width ?? 1;
            element.Transform.RelativeOffset = new((absoluteOffset - _scrollOffset) / length, 0.0f);
        }
        
    }

    private float GetComponentLength(UIComponent component)
    {
        component.Transform.RecalculateIfNeeded();
        Vector2 componentDimensions = component.Transform.UnscaledDimensions;
        return _orientation switch
        {
            Orientation.Vertical => componentDimensions.Y,
            Orientation.Horizontal => componentDimensions.X,
            _ => throw new NotImplementedException(),
        };
    }

    private void UpdateScrollBarIfExists()
    {
        if (_scrollBar is not null)
        {
            _scrollBar.TotalLength = (int)_totalLength;
            _scrollBar.CurrentLength = _scrollOffset;
        }
    }

    private void CalculateTotalLength()
    {
        _totalLength = -_elementSpacing;
        foreach (UIComponent element in _elements)
        {
            _totalLength += _elementSpacing;
            _totalLength += GetComponentLength(element);
        }
    }

    private void CreateScrollBar()
    {
        _scrollBar = new(
            scrollBarType: _orientation is Orientation.Vertical
                ? Orientation.Vertical
                : Orientation.Horizontal,
            frameColor: Color.Gray,
            thumbColor: Color.DarkGray)
            { Parent = this };
        _scrollBar.OnScroll += _scrollBar_OnScroll;
    }

    private void _scrollBar_OnScroll(object? sender, ScrollEventArgs e)
    {
        _scrollOffset = _scrollBar.CurrentLength;
        RecalculateElements();
    }

    /// <summary>
    /// Updates the size of the container based on the scroll bar's presence.
    /// </summary>
    private void AdjustContainerSize()
    {
        if (_scrollBar is null)
        {
            return;
        }

        _scrollBar.Transform.RecalculateIfNeeded();

        Rectangle scrollBarRect = _scrollBar.UnscaledDestinationRectangle;
        Rectangle containerRect = Transform.UnscaledDestinationRectangle;

        int scrollBarLength;
        int containerLength;
        switch (_orientation)
        {
            case Orientation.Vertical:
                scrollBarLength = scrollBarRect.Width;
                containerLength = containerRect.Width;
                _container.RelativeSize = new(
                    1 - scrollBarLength / (float)containerLength,
                    _container.RelativeSize.Y);
                break;
            case Orientation.Horizontal:
                scrollBarLength = scrollBarRect.Height;
                containerLength = containerRect.Height - Transform.UnscaledDestinationRectangle.Y;
                _container.RelativeSize = new(
                    _container.RelativeSize.X,
                    1 - scrollBarLength / (float)containerLength);
                break;
        }
    }

    private void Transform_OnRecalculate(object? sender, EventArgs e)
    {
        AdjustContainerSize();
    }
}
