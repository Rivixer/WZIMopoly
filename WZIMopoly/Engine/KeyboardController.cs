using Microsoft.Xna.Framework.Input;

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
        private readonly static bool s_printWhenClicked = false;
#endif
        /// <summary>
        /// The current state of the keyboard.
        /// </summary>
        private static KeyboardState s_keyboard = Keyboard.GetState();

        /// <summary>
        /// The previous state of the keyboard.
        /// </summary>
        private static KeyboardState s_oldKeyboard;

        /// <summary>
        /// Updates the state of the keyboard.<br/>
        /// Saves the current state as the old state and gets the new state.
        /// </summary>
        public static void Update()
        {
            s_oldKeyboard = s_keyboard;
            s_keyboard = Keyboard.GetState();
#if DEBUG
            if (s_printWhenClicked)
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
            bool wasReleased = s_oldKeyboard.IsKeyUp(key);
            bool isPressed = s_keyboard.IsKeyDown(key);
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
            bool isPressed = s_keyboard.IsKeyDown(key);
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
            bool wasPressed = s_oldKeyboard.IsKeyDown(key);
            bool isReleased = s_keyboard.IsKeyUp(key);
            return wasPressed && isReleased;
        }

        /// <summary>
        /// Prints to the console, the keys that have been clicked.
        /// </summary>
        private static void PrintClickedKeys()
        {
            foreach (var key in s_keyboard.GetPressedKeys())
            {
                if (WasClicked(key))
                {
                    Debug.WriteLine($"Key '{key}' has been clicked");
                }
            }
        }
    }
}
