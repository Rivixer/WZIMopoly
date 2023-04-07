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
        public static void Update()
        {
            _oldMouse = _mouse;
            _mouse = Mouse.GetState();
        }
        public static bool WasLeftBtnClicked()
        {
            bool wasClicked = (_oldMouse.LeftButton == ButtonState.Pressed);
            bool isClicked = (_mouse.LeftButton == ButtonState.Pressed);
            return wasClicked && !isClicked;
        }
        public static bool IsLeftBtnPressed()
        {
            bool wasClicked = (_oldMouse.LeftButton == ButtonState.Pressed);
            bool isClicked = (_mouse.LeftButton == ButtonState.Pressed);
            return wasClicked && isClicked;
        }
        public static bool WasLeftBtnReleased()
        {

            bool wasClicked = (_oldMouse.LeftButton == ButtonState.Pressed);
            bool isClicked = (_mouse.LeftButton == ButtonState.Pressed);
            return wasClicked && !isClicked;
        }
        public static bool IsHover(Rectangle rect)
        {
            return rect.Contains(_mouse.Position);
        }
    }
}
