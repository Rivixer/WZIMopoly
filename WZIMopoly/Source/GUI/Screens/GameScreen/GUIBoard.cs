#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WZIMopoly.GUI
{
    internal class GUIBoard : GUIElement
    {
        internal GUIBoard()
        {
            destinationRect = new(0, 0, 1920, 1080);
        }

        internal override void Load(ContentManager content)
        {
            texture = content.Load<Texture2D>("Images/Board");
        }
    }
}
