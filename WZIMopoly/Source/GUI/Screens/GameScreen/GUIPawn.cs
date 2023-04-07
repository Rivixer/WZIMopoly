#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WZIMopoly.GUI
{
    internal class GUIPawn : GUIElement, IGUIDynamicPosition
    {
        private readonly string _color;

        internal GUIPawn(string color)
        {
            _color = color;
            destinationRect = new(0, 0, 30, 30);
            offset = new(-destinationRect.Height / 2,
                         -destinationRect.Width / 2);
        }

        internal override void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Images/Pawn" + _color);
        }

        public void UpdateDestinationRect(Vector2 newPosition)
        {
            destinationRect.X = Convert.ToInt32(newPosition.X);
            destinationRect.Y = Convert.ToInt32(newPosition.Y);
        }

        public void UpdateDestinationRect(Rectangle newRectangle)
        {
            destinationRect = newRectangle;
        }
    }
}