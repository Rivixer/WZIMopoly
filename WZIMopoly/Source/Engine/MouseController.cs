#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace WZIMopoly
{
    public static class MouseController
    {
        private static MouseState _mouse = Mouse.GetState();
        private static MouseState _oldMouse;

        /// <summary>
        /// Updates the state of the mouse
        /// </summary>
        public static void Update()
        {
            _oldMouse = _mouse;
            _mouse = Mouse.GetState();
        }

        /// <summary>
        /// Checks if the left mouse button was clicked
        /// </summary>
        public static bool WasLeftBtnClicked()
        {
            bool wasRealeased = _oldMouse.LeftButton == ButtonState.Released;
            bool isPressed = _mouse.LeftButton == ButtonState.Pressed;
            return isPressed && wasRealeased;
        }

        /// <summary>
        /// Checks if the left mouse button is being pressed
        /// </summary>
        public static bool IsLeftBtnPressed()
        {
            bool isPressed = _mouse.LeftButton == ButtonState.Pressed;
            return isPressed;
        }

        /// <summary>
        /// Checks if the left mouse button was released
        /// </summary>
        public static bool WasLeftBtnReleased()
        {
            bool wasPressed = _oldMouse.LeftButton == ButtonState.Pressed;
            bool isReleased = _mouse.LeftButton == ButtonState.Released;
            return isReleased && wasPressed;
        }

        /// <summary>
        /// Checks if the mouse position is within a rectangle
        /// </summary>
        public static bool IsHover(Rectangle rect)
        {
            return rect.Contains(_mouse.Position);
        }
    }
}
