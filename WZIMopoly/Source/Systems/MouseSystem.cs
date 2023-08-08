using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WZIMopoly;

internal static class MouseSystem
{
    private static MouseState s_previousMouseState;
    private static MouseState s_currentMouseState;

    public static void Update()
    {
        s_previousMouseState = s_currentMouseState;
        s_currentMouseState = Mouse.GetState();
    }

    public static bool WasLeftButtonClicked()
    {
        return s_previousMouseState.LeftButton == ButtonState.Released
            && s_currentMouseState.LeftButton == ButtonState.Pressed;
    }

    public static bool IsLeftButtonPressing()
    {
        return s_currentMouseState.LeftButton == ButtonState.Pressed;
    }

    public static Point Position => s_currentMouseState.Position;

    public static Point MouseDelta
        => s_currentMouseState.Position - s_previousMouseState.Position;

    public static int ScrollDelta
        => s_currentMouseState.ScrollWheelValue - s_previousMouseState.ScrollWheelValue;
}
