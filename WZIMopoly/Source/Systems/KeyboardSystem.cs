﻿using Microsoft.Xna.Framework.Input;
using System;

namespace WZIMopoly;

internal static class KeyboardSystem
{
    private static KeyboardState s_previousKeyboard;
    private static KeyboardState s_currentKeyboard;

    public static void Update()
    {
        s_previousKeyboard = s_currentKeyboard;
        s_currentKeyboard = Keyboard.GetState();
    }

    public static Func<Keys, bool> IsKeyUp => s_currentKeyboard.IsKeyUp;
    public static Func<Keys, bool> IsKeyDown => s_currentKeyboard.IsKeyDown;

    public static bool WasClicked(this Keys key)
    {
        return s_previousKeyboard.IsKeyUp(key)
            && s_currentKeyboard.IsKeyDown(key);
    }

    public static bool WasReleased(this Keys key)
    {
        return s_previousKeyboard.IsKeyDown(key)
            && s_currentKeyboard.IsKeyDown(key);
    }
}