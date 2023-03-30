#region Using Statements
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
#endregion

namespace WZIMopoly
{
    public static class KeyboardController
    {

#if DEBUG
        private readonly static bool _printWhenClicked = false;
#endif

        private static KeyboardState _keyboard = Keyboard.GetState();
        private static KeyboardState _oldKeyboard;

        public static void Update()
        {
            _oldKeyboard = _keyboard;
            _keyboard = Keyboard.GetState();
#if DEBUG
            if(_printWhenClicked)
            {
                foreach(var key in _keyboard.GetPressedKeys())
                {
                    if (WasClicked(key))
                    {
                        Debug.WriteLine($"Key '{key}' has been clicked");
                    }
                }
            }
#endif
        }

        public static bool WasClicked(Keys key)
        {
            bool wasClicked = _oldKeyboard.IsKeyDown(key);
            bool isClicked = _keyboard.IsKeyDown(key);
            return !wasClicked && isClicked;
        }

        public static bool IsPressed(Keys key)
        {
            bool isClicked = _keyboard.IsKeyDown(key);
            return isClicked;
        }

        public static bool WasReleased(Keys key)
        {
            bool wasClicked = _oldKeyboard.IsKeyDown(key);
            bool isClicked = _keyboard.IsKeyDown(key);
            return wasClicked && !isClicked;
        }
     }
}
