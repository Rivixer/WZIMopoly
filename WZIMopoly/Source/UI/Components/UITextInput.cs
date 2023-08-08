using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using WZIMopoly.UI;

namespace WZIMopoly.Source.UI.Components;

internal class UITextInput : UIComponent
{
    private const int CURSOR_BLINK_TIME = 500; // In milliseconds

    private readonly UIText _text;
    private readonly UIImage _caret;
    private UIText? _placeholder;
    private int _caretPosition;

    private float? _cusorEnableTime;

    public UITextInput(Color color, Color caretColor)
    {
        _text = new UIText(string.Empty, color)
        {
            Parent = this,
            RelativeOffset = new(0.01f, 0.0f),
            Size = 0.7f,
        };
        _caret = new UIImage(caretColor)
        {
            Parent = this,
            RelativeOffset = new(0.01f, 0.0f),
            RelativeSize = new(0.85f),
            Ratio = new(1, 12),
        };
        IsEnabled = false;

        OnTextChange += UITextInput_OnTextChange;
    }

    private void UITextInput_OnTextChange(object? sender, EventArgs e)
    {
        if (IsEnabled)
        {
            _cusorEnableTime = 0.0f;
        }
    }

    public event EventHandler? OnTextChange;

    public Alignment TextAlignment
    {
        get { return _text.Alignment; }
        set
        {
            _text.Alignment = value;
            if (_placeholder != null)
            {
                _placeholder.Alignment = value;
            }   
        }
    }

    public float TextSize
    {
        get { return _text.Size; }
        set { _text.Size = value;  }
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
                    Alignment = _text.Alignment,
                    RelativeOffset = _text.Transform.RelativeOffset,
                    RelativeSize = _text.Transform.RelativeSize * new Vector2(0.8f),
                    Size = _text.Size,
                };
            }
        }
    }

    public bool Selected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override void Update(GameTime gameTime)
    {
        if (MouseSystem.WasLeftButtonClicked())
        {
            Point mousePosition = MouseSystem.Position;
            if (Transform.DestinationRectangle.Contains(mousePosition))
            {
                if (!IsEnabled)
                {
                    ScreenSystem.GameWindow.TextInput += GameWindow_TextInput;
                    ScreenSystem.GameWindow.KeyDown += GameWindow_KeyDown;
                }
                IsEnabled = true;
                SetCursorPositionBasedOnMouseClick();
            }
            else
            {
                if (IsEnabled)
                {
                    ScreenSystem.GameWindow.TextInput -= GameWindow_TextInput;
                    ScreenSystem.GameWindow.KeyDown -= GameWindow_KeyDown;
                }
                IsEnabled = false;
            }
        }

        if (IsEnabled)
        {
            _cusorEnableTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
    }

    private void GameWindow_KeyDown(object? sender, InputKeyEventArgs e)
    {
        switch (e.Key)
        {
            case Keys.Left:
                _caretPosition = Math.Max(_caretPosition - 1, 0);
                break;
            case Keys.Right:
                _caretPosition = Math.Min(_caretPosition + 1, _text.Text.Length);
                break;
            case Keys.Back:
                if (_caretPosition > 0)
                {
                    _text.Text = _text.Text.Remove(_caretPosition - 1, 1);
                    _caretPosition--;
                }
                break;
            case Keys.Home:
                _caretPosition = 0;
                break;
            case Keys.End:
                _caretPosition = _text.Text.Length;
                break;
            case Keys.Space:
                _text.Text = _text.Text.Insert(_caretPosition++, " ");
                break;
        }
        OnTextChange?.Invoke(this, EventArgs.Empty);
    }

    private void GameWindow_TextInput(object? sender, TextInputEventArgs e)
    {
        if (_text.Font.Characters.Contains(e.Character))
        {
            _text.Text = _text.Text.Insert(_text.Text.Length, e.Character.ToString());
            _caretPosition++;
            OnTextChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void Draw(GameTime gameTime)
    {
        if (_text.Text.Length == 0)
        {
            _placeholder?.Draw(gameTime);
        }
        else
        {
            _text.Draw(gameTime);
        }

        if (IsEnabled && _cusorEnableTime * 1000.0f % 1000 < CURSOR_BLINK_TIME)
        {
            _caret.Draw(gameTime);
        }
    }

    private void SetCursorPositionBasedOnMouseClick()
    {
        // TODO
    }
}
