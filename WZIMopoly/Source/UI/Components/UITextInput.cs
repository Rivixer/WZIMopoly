using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using WZIMopoly.UI;

namespace WZIMopoly.Source.UI.Components;

internal class UITextInput : UIComponent
{
    private readonly UIText _text;
    private readonly UIImage _caret;
    private UIText? _placeholder;
    private int _caretPosition;

    public UITextInput(Color color, Color caretColor)
    {
        _text = new UIText(string.Empty, color)
        {
            Parent = this,
            RelativeOffset = new(0.01f, 0.0f),
        };
        _caret = new UIImage(caretColor)
        {
            Parent = this,
            Alignment = Alignment.Left,
            RelativeOffset = new(0.01f, 0.0f),
            RelativeSize = new(0.9f),
            Ratio = new(1, 20),
        };
        IsEnabled = false;
    }

    public string? Placeholder
    {
        get
        {
            return _placeholder?.Text;
        }
        set
        {
            if (_placeholder?.Text == value)
            {
                return;
            }
            if (value is null)
            {
                _placeholder = null;
            }
            else
            {
                _placeholder = new(value, _text.Color * 0.3f, _text.Font)
                {
                    Parent = this,
                    Alignment = Alignment.Left,
                    RelativeOffset = _text.Transform.RelativeOffset,
                    RelativeSize = _text.Transform.RelativeSize * new Vector2(0.8f),
                    Size = _text.Size * 0.6f,
                };
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (MouseSystem.WasLeftButtonClicked())
        {
            Point mousePosition = MouseSystem.Position;
            if (Transform.DestinationRectangle.Contains(mousePosition))
            {
                IsEnabled ^= true;
                if (IsEnabled)
                {
                    SetCursorPositionBasedOnMouseClick();
                }
            }
        }

        if (IsEnabled)
        {
            if (Keys.Escape.WasClicked())
            {
                IsEnabled = false;
            }
            else
            {
                CheckAndMoveCursor();
            }

            base.Update(gameTime);
        }
    }

    public override void Draw(GameTime gameTime)
    {
        if (!IsEnabled)
        {
            return;
        }

        _placeholder?.Draw(gameTime);
        if (DateTime.Now.Millisecond / 500 < 1)
        {
            base.Draw(gameTime);
        }
    }

    private void CheckAndMoveCursor()
    {
        foreach(Keys key in KeyboardSystem.GetClickedKeys())
        {
            switch(key)
            {
                case Keys.Left:
                    // Move the cursor one character left
                    break;
                case Keys.Right:
                    // Move the cursor one character right
                    break;
                case Keys.Escape:
                    IsEnabled = false;
                    break;
                case Keys.Home:
                    // Move the cursor to the beginning
                    break;
                case Keys.End:
                    // Move the cursor to the end
                    break;
            }
        }
    }

    private void SetCursorPositionBasedOnMouseClick()
    {
        // TODO
    }
}
