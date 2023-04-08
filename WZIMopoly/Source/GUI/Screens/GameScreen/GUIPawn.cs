#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WZIMopoly.GUI
{
    internal class GUIPawn : GUIElement, IGUIDynamicPosition, IGUILoadable, IGUIDrawable
    {
        private readonly string _color;
        private Texture2D _texture;

        internal GUIPawn(string color) : base(new(0, 0, 1920, 1080))
        {
            _color = color;
            Offset = new(-DestinationRect.Height / 2,
                         -DestinationRect.Width / 2);
        }

        public void UpdateDestinationRect(Vector2 newPosition)
        {
            DestinationRect.X = Convert.ToInt32(newPosition.X);
            DestinationRect.Y = Convert.ToInt32(newPosition.Y);
        }

        public void UpdateDestinationRect(Rectangle newRectangle)
        {
            DestinationRect = newRectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Offset, Color.White);
        }

        public void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Images/Pawn" + _color);
        }
    }
}