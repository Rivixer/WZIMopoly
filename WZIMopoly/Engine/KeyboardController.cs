using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace WZIMopoly.Engine
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
            if (_printWhenClicked)
            {
                UpdateForeach();    
            }
#endif
        }
        private static void UpdateForeach()
        {
            foreach (var key in _keyboard.GetPressedKeys())
            {
                if (WasPressed(key))
                {
                    Debug.WriteLine($"Key '{key}' has been clicked");
                }
            }
        }
        #region Key Methods
        /// <summary>Checks if the key has been clicked.</summary>
        /// <remarks>
        /// The key has been clicked if it was released and then pressed.
        /// </remarks>
        /// <returns>
        /// True if the key has been clicked, otherwise false.
        /// </returns>
        public static bool WasPressed(Keys key)
        {
            bool wasClicked = _oldKeyboard.IsKeyDown(key);
            bool isClicked = _keyboard.IsKeyDown(key);
            return !wasClicked && isClicked;
        }

        /// <summary>
        /// Checks if the key is being pressed.
        /// </summary>
        /// <returns>
        /// True if the key is being pressed, otherwise false.
        /// </returns>
        public static bool IsPressed(Keys key)
        {
            bool isPressed = _keyboard.IsKeyDown(key);
            return isPressed;
        }

        /// <summary>
        /// Checks if the key has been released.
        /// The key has been released if it was pressed and then released.
        /// </summary>
        /// <returns>
        /// True if the key has been released, otherwise false.
        /// </returns>
        public static bool WasReleased(Keys key)
        {
            bool wasPressed = _oldKeyboard.IsKeyDown(key);
            bool isPressed = _keyboard.IsKeyDown(key);
            return wasPressed && !isPressed;
        }
        #endregion  
    }
}
