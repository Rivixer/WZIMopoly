using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WZIMopoly.Source.UI.Components;
using WZIMopoly.UI;

namespace WZIMopoly;

internal static class DebugConsole
{
    private static UIFrame _frame = default!;
       
    public static bool IsOpen { get; private set; }

    public delegate void OpenCloseEventHandler();

    public static event OpenCloseEventHandler? OnOpen;
    public static event OpenCloseEventHandler? OnClose;

    private static UIComponent f1;
    private static UIComponent f2;

    public static void Initialize()
    {
        _frame = new(thickness: 5, Color.Black) { UnscaledDestinationRectangle = new(50, 50, 1400, 800) };

        UIImage background = new(Color.Gray) { Parent = _frame, Opacity = 0.8f};
        UIText text = new("Tu będzie konsola do debugowania :)", Color.White) { Parent = _frame };

        UIFrame textInputFrame = new(thickness: 3, Color.DarkGray)
        {
            Parent = _frame,
            Alignment = Alignment.Bottom,
            RelativeSize = new(0.98f, 0.08f),
            RelativeOffset = new(0.0f, -0.02f),
        };
        f1 = textInputFrame;
        UIImage textInputBackground = new(Color.LightGray) { Parent = textInputFrame, Opacity = 0.99f };
        f2 = textInputBackground;
        UITextInput textInput = new(Color.White) { Parent = textInputFrame };
        OnClose += () => textInput.IsEnabled = false;
    }

    public static void Update(GameTime gameTime)
    {
        _frame.Update(gameTime);
        if (Keys.OemTilde.WasClicked())
        {
            IsOpen ^= true;
            (IsOpen ? OnOpen : OnClose)?.Invoke();
        }
        Debug.WriteLine(f1.UnscaledDestinationRectangle + " " + f1.Transform.DestinationRectangle);
        Debug.WriteLine(f2.UnscaledDestinationRectangle + " " + f2.Transform.DestinationRectangle);
        Debug.WriteLine("");
    }

    public static void Draw(GameTime gameTime)
    {
        if (IsOpen)
        {
            _frame.Draw(gameTime);
        }
    }

    public static void Dispose()
    {
        _frame.Destroy();
    }
}
