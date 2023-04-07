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
        /// Updates the state of mouse
        /// </summary>
        public static void Update()
        {
            _oldMouse = _mouse;
            _mouse = Mouse.GetState();
        }

        /// <summary>
        /// Checks if left mouse button was clicked
        /// </summary>
        public static bool WasLeftBtnClicked()
        {
            bool wasClicked = (_oldMouse.LeftButton == ButtonState.Pressed);
            bool isClicked = (_mouse.LeftButton == ButtonState.Pressed);
            return wasClicked && !isClicked;
        }

        /// <summary>
        /// Checks if left mouse button is being pressed
        /// </summary>
        public static bool IsLeftBtnPressed()
        {
            bool wasClicked = (_oldMouse.LeftButton == ButtonState.Pressed);
            bool isClicked = (_mouse.LeftButton == ButtonState.Pressed);
            return wasClicked && isClicked;
        }

        /// <summary>
        /// Checks if left mouse button was released
        /// </summary>
        public static bool WasLeftBtnReleased()
        {

            bool wasClicked = (_oldMouse.LeftButton == ButtonState.Pressed);
            bool isClicked = (_mouse.LeftButton == ButtonState.Pressed);
            return wasClicked && !isClicked;
        }

        /// <summary>
        /// Checks if mouse position is within rectangle
        /// </summary>
        public static bool IsHover(Rectangle rect)
        {
            return rect.Contains(_mouse.Position);
        }
    }
}
