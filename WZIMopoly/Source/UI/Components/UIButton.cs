using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace WZIMopoly.UI;

internal class UIButton : UIComponent
{
    private UIImage? _background;
    private UIText? _text;

    public static bool WasClickedInThisFrame { get; set; }

    public event EventHandler? OnClick;

    public UIImage? Background
    {
        get { return _background; }
        set
        {
            _background = value;
            if (_background is not null)
            {
                _background.Parent = this;
                Transform.Ratio = _background.Texture.Width / (float)_background.Texture.Height;
            }
        }
    }

    public UIText? Text
    {
        get { return _text; }
        set
        {
            _text = value;
            if (_text is not null)
            {
                _text.Parent = this;
                _text.Transform.Alignment = Alignment.Center;
            }
        }
    }

    public bool IsHovered
    {
        get
        {
            Point cursorPostion = MouseSystem.Position;
            bool isInRect = Transform.DestinationRectangle.Contains(cursorPostion);

            if (_background is null || isInRect is false || _background.Color is not null)
            {
                return isInRect;
            }

            // Button is hovered if the mouse is over a non-transparent pixel.
            // TODO: Better naming and some comments would be nice.
            Texture2D texture = _background.Texture;
            Rectangle destinationRect = _background.Transform.DestinationRectangle;
            Point mouseOffset = cursorPostion - destinationRect.Location;
            int mouseOffsetX = mouseOffset.X * texture.Width / destinationRect.Size.X;
            int mouseOffsetY = mouseOffset.Y * texture.Height / destinationRect.Size.Y;
            int index = mouseOffsetY * texture.Width + mouseOffsetX;

            // TODO: Maybe instead of checking alpha value, we should keep
            // non-transparent pixels as a boolean array? That would be more efficient.
            Color[] texturePixels = _background.TexturePixels!;
            return index >= 0 && index < texturePixels.Length
                && texturePixels[index].A > 0;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (IsEnabled
            && !WasClickedInThisFrame
            && MouseSystem.WasLeftButtonClicked()
            && IsHovered)
        {
            WasClickedInThisFrame = true;
            OnClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
