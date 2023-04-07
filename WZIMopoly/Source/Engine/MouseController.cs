#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            throw new System.NotImplementedException();
        }
        public static bool IsLeftBtnPressed()
        {
            throw new System.NotImplementedException();
        }
        public static bool WasLeftBtnReleased()
        {
            throw new System.NotImplementedException();
        }
        public static bool IsHover(Rectangle rect)
        {
            return rect.Contains(_mouse.Position);
        }
    }
}
