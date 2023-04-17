using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace WZIMopoly.Engine
{
    /// <summary>
    /// Represents a controller for the keyboard.
    /// </summary>
    public static class KeyboardController
    {
#if DEBUG
        /// <summary>
        /// Defines if the clicked key is about to be print.
        /// </summary>
        /// <remarks>
        /// Works only for debug.
        /// </remarks>
        private readonly static bool _printWhenClicked = false;
#endif
        /// <summary>
        /// The current state of the keyboard.
        /// </summary>
        private static KeyboardState _keyboard = Keyboard.GetState();

        /// <summary>
        /// The previous state of the keyboard.
        /// </summary>
        private static KeyboardState _oldKeyboard;

        /// <summary>
        /// Updates the state of the keyboard.<br/>
        /// Saves the current state as the old state and gets the new state.
        /// </summary>
        public static void Update()
        {
            _oldKeyboard = _keyboard;
            _keyboard = Keyboard.GetState();
#if DEBUG
            if (_printWhenClicked)
            {
                PrintClickedKeys();    
            }
#endif
        }

        /// <summary>Checks if the key has been clicked.</summary>
        /// <remarks>
        /// The key has been clicked if it was released and then pressed.
        /// </remarks>
        /// <param name="key">
        /// The key to check if it has been released.
        /// </param>
        /// <returns>
        /// True if the key has been clicked, otherwise false.
        /// </returns>
        public static bool WasClicked(Keys key)
        {
            bool wasReleased = _oldKeyboard.IsKeyDown(key);
            bool isPressed = _keyboard.IsKeyDown(key);
            return wasReleased && isPressed;
        }

        /// <summary>
        /// Checks if the key is being pressed.
        /// </summary>
        /// <param name="key">
        /// The key to check if it has been released.
        /// </param>
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
        /// </summary>
        /// <remarks>
        /// The key has been released if it was pressed and then released.
        /// </remarks>
        /// <param name="key">
        /// The key to check if it has been released.
        /// </param>
        /// <returns>
        /// True if the key has been released, otherwise false.
        /// </returns>
        public static bool WasReleased(Keys key)
        {
            bool wasPressed = _oldKeyboard.IsKeyDown(key);
            bool isReleased = _keyboard.IsKeyUp(key);
            return wasPressed && isReleased;
        }

        /// <summary>
        /// Prints to the console, the keys that have been clicked.
        /// </summary>
        private static void PrintClickedKeys()
        {
            foreach (var key in _keyboard.GetPressedKeys())
            {
                if (WasClicked(key))
                {
                    Debug.WriteLine($"Key '{key}' has been clicked");
                }
            }
        }
    }
}
