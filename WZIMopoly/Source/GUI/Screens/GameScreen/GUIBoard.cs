#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WZIMopoly.GUI
{
    internal class GUIBoard : GUIElement, IGUIDrawable, IGUILoadable
    {
        private Texture2D _texture;

        public GUIBoard(Rectangle defDstRect) : base(defDstRect)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, DestinationRect, Color.White);
        }

        public void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Images/Board");
        }
    }
}
