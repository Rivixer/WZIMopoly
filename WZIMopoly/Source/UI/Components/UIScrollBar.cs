using Microsoft.Xna.Framework;
using System;

namespace WZIMopoly.UI;

internal class ScrollEventArgs : EventArgs
{
    public ScrollEventArgs(int currentLength, int totalLength)
    {
        CurrentLength = currentLength;
        TotalLength = totalLength;
        Percentage = currentLength / (float)totalLength;
    }
    public int TotalLength { get; }
    public int CurrentLength { get; }
    public float Percentage { get; }
}

internal class UIScrollBar : UIComponent
{
    private readonly Orientation _scrollBarType;
    private readonly Color _frameColor;
    private readonly Color _thumbColor;
    private readonly int _absoluteSize;

    private int _currentLength;
    private int _totalLength;

    private UIFrame _frame = default!;
    private UIImage _thumb = default!;

    private bool _needsRecalculation;

    private bool _isDragging;
    
    public UIScrollBar(Orientation scrollBarType, Color frameColor, Color thumbColor, int absoluteSize = 30)
    {
        _scrollBarType = scrollBarType;
        _absoluteSize = absoluteSize;
        _frameColor = frameColor;
        _thumbColor = thumbColor;
        OnParentChange += UIScrollBar_OnParentChange;
    }

    public event EventHandler<ScrollEventArgs>? OnScroll;

    public int CurrentLength
    {
        get { return _currentLength; }
        set
        {
            if (_currentLength == value)
            {
                return;
            }
            _currentLength = value;
            _needsRecalculation = true;
        }
    }

    public int TotalLength
    {
        get { return _totalLength; }
        set
        {
            if (_totalLength == value)
            {
                return;
            }
            _totalLength = value;
            _needsRecalculation = true;
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (MouseSystem.IsLeftButtonPressing()
            && _thumb.Transform.DestinationRectangle.Contains(MouseSystem.Position))
        {
            _isDragging = true;
        }
        else if (!MouseSystem.IsLeftButtonPressing())
        {
            _isDragging = false;
        }

        if (MouseSystem.WasLeftButtonClicked()
            && _frame.Transform.DestinationRectangle.Contains(MouseSystem.Position)
            && !_thumb.Transform.DestinationRectangle.Contains(MouseSystem.Position))
        {
            bool wasClickedAboveThumb = _scrollBarType switch
            {
                Orientation.Vertical => MouseSystem.Position.Y < _thumb.Transform.DestinationRectangle.Y,
                Orientation.Horizontal => MouseSystem.Position.X < _thumb.Transform.DestinationRectangle.X,
                _ => throw new NotImplementedException(),
            };

            int newCurrentLenght;
            if (wasClickedAboveThumb)
            {
                newCurrentLenght = _scrollBarType switch
                {
                    Orientation.Vertical => (int)(CurrentLength - _thumb.Transform.UnscaledDestinationRectangle.Height / (float)_frame.Transform.UnscaledDestinationRectangle.Height * TotalLength),
                    Orientation.Horizontal => (int)(CurrentLength - _thumb.Transform.UnscaledDestinationRectangle.Width / (float)_frame.Transform.UnscaledDestinationRectangle.Width * TotalLength),
                    _ => throw new NotImplementedException(),
                };
            }
            else
            {
                newCurrentLenght = _scrollBarType switch
                {
                    Orientation.Vertical => (int)(CurrentLength + _thumb.Transform.UnscaledDestinationRectangle.Height / (float)_frame.Transform.UnscaledDestinationRectangle.Height * TotalLength),
                    Orientation.Horizontal => (int)(CurrentLength + _thumb.Transform.UnscaledDestinationRectangle.Width / (float)_frame.Transform.UnscaledDestinationRectangle.Width * TotalLength),
                    _ => throw new NotImplementedException(),
                };
            }

            CurrentLength = (int)Math.Clamp(newCurrentLenght, 0, TotalLength * (1 - _thumb.Transform.UnscaledDestinationRectangle.Height / (float)_frame.Transform.UnscaledDestinationRectangle.Height));
            OnScroll?.Invoke(this, new ScrollEventArgs(CurrentLength, TotalLength));
        }

        if (_isDragging)
        {
            Point mouseDelta = MouseSystem.MouseDelta;
            switch (_scrollBarType)
            {
                case Orientation.Vertical:
                    if (mouseDelta.Y != 0)
                    {
                        int frameHeightWithoutThumb = _frame.UnscaledDestinationRectangle.Height - _thumb.UnscaledDestinationRectangle.Height;
                        float percentage = mouseDelta.Y / (float)(_frame.UnscaledDestinationRectangle.Height * ScreenSystem.Scale.Y);
                        float additionalThumbOffset = percentage * frameHeightWithoutThumb / frameHeightWithoutThumb;
                        float maxRelativeOffset = frameHeightWithoutThumb / (float)_frame.Transform.UnscaledDestinationRectangle.Height;
                        _thumb.RelativeOffset = new(0.0f, Math.Clamp(_thumb.RelativeOffset.Y + additionalThumbOffset, 0.0f, maxRelativeOffset));
                        _currentLength = (int)(_thumb.RelativeOffset.Y * _totalLength);
                    }
                    break;
                case Orientation.Horizontal:
                    if (mouseDelta.X != 0)
                    {
                        int frameWidthWithoutThumb = _frame.UnscaledDestinationRectangle.Width - _thumb.UnscaledDestinationRectangle.Width;
                        float percentage = mouseDelta.X / (float)(_frame.UnscaledDestinationRectangle.Width * ScreenSystem.Scale.X);
                        float additionalThumbOffset = percentage * frameWidthWithoutThumb / frameWidthWithoutThumb;
                        float maxRelativeOffset = frameWidthWithoutThumb / (float)_frame.Transform.UnscaledDestinationRectangle.Width;
                        _thumb.RelativeOffset = new(Math.Clamp(_thumb.RelativeOffset.X + additionalThumbOffset, 0.0f, maxRelativeOffset), 0.0f);
                        _currentLength = (int)(_thumb.RelativeOffset.X * _totalLength);   
                    }
                    break;
            }
            OnScroll?.Invoke(this, new ScrollEventArgs(CurrentLength, TotalLength));
        }
        else
        {
            if (!_needsRecalculation)
            {
                return;
            }

            float percentageOfShift = _currentLength / (float)_totalLength;
            switch (_scrollBarType)
            {
                case Orientation.Vertical:
                    int frameHeight = _frame.UnscaledDestinationRectangle.Height;
                    float newRelativeThumbHeight = Math.Clamp(frameHeight / (float)_totalLength, 0.0f, 1.0f);
                    _thumb.RelativeSize = new Vector2(1.0f, newRelativeThumbHeight);
                    _thumb.Transform.Recalculate();

                    int frameHeightWithoutThumb = frameHeight - _thumb.UnscaledDestinationRectangle.Height;
                    int newAbsoluteThumbOffsetY = (int)Math.Clamp(frameHeightWithoutThumb * percentageOfShift, 0.0f, frameHeightWithoutThumb);
                    _thumb.RelativeOffset = new(0.0f, newAbsoluteThumbOffsetY / (float)frameHeightWithoutThumb);
                    break;
                case Orientation.Horizontal:
                    int frameWidth = _frame.UnscaledDestinationRectangle.Width;
                    float newRelativeThumbWidth = Math.Clamp(frameWidth / (float)_totalLength, 0.0f, 1.0f);
                    _thumb.RelativeSize = new Vector2(newRelativeThumbWidth, 1.0f);
                    _thumb.Transform.Recalculate();

                    int frameWidthWithoutThumb = frameWidth - _thumb.UnscaledDestinationRectangle.Width;
                    int newAbsoluteThumbOffsetX = (int)Math.Clamp(frameWidthWithoutThumb * percentageOfShift, 0.0f, frameWidthWithoutThumb);
                    _thumb.RelativeOffset = new(newAbsoluteThumbOffsetX / (float)frameWidthWithoutThumb, 0.0f);
                    break;
            }
        }

        base.Update(gameTime);
        _needsRecalculation = false;
    }

    private void UIScrollBar_OnParentChange(object? sender, ParentChangeEventArgs e)
    {
        if (e.NewParent is not { } newParent)
        {
            throw new InvalidOperationException("Cannot remove parent from UIScrollbar.");
        }

        switch (_scrollBarType)
        {
            case Orientation.Vertical:
                Transform.Alignment = Alignment.Right;
                Transform.RelativeSize = new Vector2(_absoluteSize / (float)Parent!.Transform.UnscaledDestinationRectangle.Width, 1.0f);
                break;
            case Orientation.Horizontal:
                Transform.Alignment = Alignment.Bottom;
                Transform.RelativeSize = new Vector2(1.0f, _absoluteSize / (float)Parent!.Transform.UnscaledDestinationRectangle.Height);
                break;
        }

        _frame = new(thickness: 3, _frameColor) { Parent = this };
        _thumb = new(_thumbColor) { Parent = _frame };
        _thumb.Transform.MinSize = new Point(3);
    }
}
