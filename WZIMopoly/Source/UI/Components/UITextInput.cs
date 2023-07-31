using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using WZIMopoly.UI;

namespace WZIMopoly.Source.UI.Components;

internal class UITextInput : UIComponent
{
    private readonly UIText _text;
    private readonly UIImage _cursor;
    private int _cursorPosition;

    public UITextInput(Color color)
        : this(color, Color.Black) { }

    public UITextInput(Color color, Color cursorColor)
    {
        _text = new UIText(string.Empty, color)
        {
            Parent = this,
            RelativeOffset = new(0.01f, 0.0f),
        };
        _cursor = new UIImage(cursorColor)
        {
            Parent = this,
            RelativeOffset = new(0.01f, 0.0f),
            RelativeSize = new(0.96f),
            Ratio = new(1, 10),
        };
        IsEnabled = false;
    }

    public string Placeholder { get; set; } = string.Empty;

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
