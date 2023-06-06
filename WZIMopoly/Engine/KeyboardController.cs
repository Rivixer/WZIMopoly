using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
            bool wasReleased = _oldKeyboard.IsKeyUp(key);
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
        /// Returns all the keys that have been clicked.
        /// </summary>
        /// <returns>
        /// The keys that have been clicked.
        /// </returns>
        public static List<Keys> GetAllClickedKeys()
        {
            List<Keys> result = new();
            foreach (var key in _keyboard.GetPressedKeys())
            {
                if (WasClicked(key))
                {
                    result.Add(key);
                }
            }
            return result;
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

#nullable enable

        /// <summary>
        /// Returns the first key that has been clicked.
        /// </summary>
        /// <returns>
        /// The first key that has been clicked.
        /// </returns>
        public static char? GetClickedKey()
        {
            GetClickedKey(out char? c);
            return c;
        }

        /// <summary>
        /// Retrieves the currently pressed key and determines the corresponding character.
        /// </summary>
        /// <param name="c">
        /// The output parameter that will contain the retrieved character
        /// if a valid key is pressed, or null if no key is pressed.
        /// </param>
        public static void GetClickedKey(out char? c)
        {
            bool caps = Keyboard.GetState().CapsLock;
            bool shift = IsPressed(Keys.LeftShift) || IsPressed(Keys.RightShift);
            bool isUp = caps ^ shift;

            var clickedKeys = GetAllClickedKeys();
            if (clickedKeys.Count == 0)
            {
                c = null;
                return;
            }

            char? letter = null;
            // TODO: Add support to other languages
            switch (clickedKeys[0])
            {
                case Keys.A:
                    letter = isUp ? 'A' : 'a';
                    break;
                case Keys.B:
                    letter = isUp ? 'B' : 'b';
                    break;
                case Keys.C:
                    letter = isUp ? 'C' : 'c';
                    break;
                case Keys.D:
                    letter = isUp ? 'D' : 'd';
                    break;
                case Keys.E:
                    letter = isUp ? 'E' : 'e';
                    break;
                case Keys.F:
                    letter = isUp ? 'F' : 'f';
                    break;
                case Keys.G:
                    letter = isUp ? 'G' : 'g';
                    break;
                case Keys.H:
                    letter = isUp ? 'H' : 'h';
                    break;
                case Keys.I:
                    letter = isUp ? 'I' : 'i';
                    break;
                case Keys.J:
                    letter = isUp ? 'J' : 'j';
                    break;
                case Keys.K:
                    letter = isUp ? 'K' : 'k';
                    break;
                case Keys.L:
                    letter = isUp ? 'L' : 'l';
                    break;
                case Keys.M:
                    letter = isUp ? 'M' : 'm';
                    break;
                case Keys.N:
                    letter = isUp ? 'N' : 'n';
                    break;
                case Keys.O:
                    letter = isUp ? 'O' : 'o';
                    break;
                case Keys.P:
                    letter = isUp ? 'P' : 'p';
                    break;
                case Keys.Q:
                    letter = isUp ? 'Q' : 'q';
                    break;
                case Keys.R:
                    letter = isUp ? 'R' : 'r';
                    break;
                case Keys.S:
                    letter = isUp ? 'S' : 's';
                    break;
                case Keys.T:
                    letter = isUp ? 'T' : 't';
                    break;
                case Keys.U:
                    letter = isUp ? 'U' : 'u';
                    break;
                case Keys.V:
                    letter = isUp ? 'V' : 'v';
                    break;
                case Keys.W:
                    letter = isUp ? 'W' : 'w';
                    break;
                case Keys.X:
                    letter = isUp ? 'X' : 'x';
                    break;
                case Keys.Y:
                    letter = isUp ? 'Y' : 'y';
                    break;
                case Keys.Z:
                    letter = isUp ? 'Z' : 'z';
                    break;
                case Keys.D0:
                    letter = '0';
                    break;
                case Keys.D1:
                    letter = '1';
                    break;
                case Keys.D2:
                    letter = '2';
                    break;
                case Keys.D3:
                    letter = '3';
                    break;
                case Keys.D4:
                    letter = '4';
                    break;
                case Keys.D5:
                    letter = '5';
                    break;
                case Keys.D6:
                    letter = '6';
                    break;
                case Keys.D7:
                    letter = '7';
                    break;
                case Keys.D8:
                    letter = '8';
                    break;
                case Keys.D9:
                    letter = '9';
                    break;
            }
            c = letter;
        }
    }
}
