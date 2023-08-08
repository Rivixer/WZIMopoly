using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using WZIMopoly.Source.UI.Components;
using WZIMopoly.UI;

namespace WZIMopoly;

internal static class DebugConsole
{
    private static UIFrame _frame = default!;
    private static UIListBox _messages = default!;

    public delegate void OpenCloseEventHandler();
    public static event OpenCloseEventHandler? OnOpen;
    public static event OpenCloseEventHandler? OnClose;

    public static bool IsOpen { get; private set; }

    public static void Create()
    {
        _frame = new(thickness: 5, Color.Black) { UnscaledDestinationRectangle = new(50, 50, 1400, 800) };

        UIImage background = new(Color.Black) { Parent = _frame, Opacity = 0.9f };

        UIFrame textInputFrame = new(thickness: 3, new Color(60, 60, 60, 255))
        {
            Parent = _frame,
            Alignment = Alignment.Bottom,
            RelativeSize = new(0.98f, 0.06f),
            RelativeOffset = new(0.0f, -0.02f),
        };
        UIImage textInputBackground = new(Color.Gray) { Parent = textInputFrame, Opacity = 0.5f };
        UITextInput textInput = new(Color.White, caretColor: Color.Black)
        {
            Parent = textInputFrame,
            Alignment = Alignment.Left,
            Placeholder = "Tu jest miejsce na wpisanie komendy...",
            TextAlignment = Alignment.Left,
            TextSize = 0.7f,
        };

        OnClose += () => textInput.IsEnabled = false;

        UIFrame messagesFrame = new(thickness: 3, new Color(60, 60, 60, 255))
        {
            Parent = _frame,
            Alignment = Alignment.Top,
            RelativeSize = new(0.98f, 0.88f),
            RelativeOffset = new(0.0f, 0.02f),
        };
        UIImage messagesBackground = new(Color.Gray) { Parent = messagesFrame, Opacity = 0.15f };

        _messages = new UIListBox()
        {
            Parent = messagesFrame,
            Orientation = Orientation.Vertical,
            IsScrollable = true,
            Alignment = Alignment.Center,
            RelativeSize = new(0.99f),
            ElementSpacing = 8,
        };
        OnOpen += () => _messages.JumpToElement(_messages.Elements.Last());
    }

    public static void Update(GameTime gameTime)
    {
        _frame.Update(gameTime);
        if (Keys.OemTilde.WasClicked())
        {
            IsOpen ^= true;
            (IsOpen ? OnOpen : OnClose)?.Invoke();
        }
    }

    public static void Draw(GameTime gameTime)
    {
        if (IsOpen)
        {
            _frame.Draw(gameTime);
        }
    }

    public static void Error(string message)
    {
        var text = new UIWrappedText(message, Color.Red)
        {
            Size = 0.51f,
            Alignment = Alignment.TopLeft
        };
        _messages.AddElement(text);
        IsOpen = true;
    }

    public static void Warning(string message)
    {
        var text = new UIWrappedText(message, Color.Yellow)
        {
            Size = 0.51f,
            Alignment = Alignment.TopLeft,
        };
        _messages.AddElement(text);
    }
}
