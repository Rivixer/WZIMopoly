using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace WZIMopoly.Engine
{
    /// <summary>
    /// Represents a controller for the mouse.
    /// </summary>
    public static class MouseController
    {
        /// <summary>
        /// The current state of the mouse.
        /// </summary>
        private static MouseState s_mouse = Mouse.GetState();

        /// <summary>
        /// The previous state of the mouse.
        /// </summary>
        private static MouseState s_oldMouse;

        /// <summary>
        /// Updates the state of the mouse.<br/>
        /// Saves the current state as the old state and gets the new state.
        /// </summary>
        public static void Update()
        {
            s_oldMouse = s_mouse;
            s_mouse = Mouse.GetState();
        }

        #region Left Button Methods
        /// <summary>Checks if the left mouse button has been clicked.</summary>
        /// <remarks>
        /// The button has been clicked if it was released and then pressed.
        /// </remarks>
        /// <returns>
        /// True if the left mouse button has been clicked, otherwise false.
        /// </returns>
        public static bool WasLeftBtnClicked()
        {
            bool wasRealeased = s_oldMouse.LeftButton == ButtonState.Released;
            bool isPressed = s_mouse.LeftButton == ButtonState.Pressed;
            return isPressed && wasRealeased;
        }

        /// <summary>
        /// Checks if the left mouse button is being pressed.
        /// </summary>
        /// <returns>
        /// True if the left mouse button is being pressed, otherwise false.
        /// </returns>
        public static bool IsLeftBtnPressed()
        {
            bool isPressed = s_mouse.LeftButton == ButtonState.Pressed;
            return isPressed;
        }

        /// <summary>
        /// Checks if the left mouse button has been released.
        /// The button has been released if it was pressed and then released.
        /// </summary>
        /// <returns>
        /// True if the left mouse button has been released, otherwise false.
        /// </returns>
        public static bool WasLeftBtnReleased()
        {
            bool wasPressed = s_oldMouse.LeftButton == ButtonState.Pressed;
            bool isReleased = s_mouse.LeftButton == ButtonState.Released;
            return isReleased && wasPressed;
        }
        #endregion

        #region Hover Methods
        /// <summary>
        /// Checks if the mouse position is within a rectangle.
        /// </summary>
        /// <returns>
        /// True if the mouse is in the rectangle, otherwise false.
        /// </returns>
        public static bool IsHover(Rectangle rect)
        {
            return rect.Contains(s_mouse.Position);
        }

        /// <summary>
        /// Checks if the mouse position is within an area defined by a function.
        /// </summary>
        /// <param name="func">
        /// The function that determines whether the mouse is in the area.
        /// </param>
        /// <returns>
        /// True if the mouse is in the area, otherwise false.
        /// </returns>
        public static bool IsHover(Func<Point, bool> func)
        {
            return func(s_mouse.Position);
        }
        #endregion
    }
}
