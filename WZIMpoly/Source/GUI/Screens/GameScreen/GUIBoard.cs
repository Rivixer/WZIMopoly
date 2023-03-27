#region UsingStatements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WindowsWZIMpoly.GUI
{
    internal class GUIBoard : GUIElement
    {
        private readonly BoardController _boardController;
        private Texture2D _texture;
        // TODO: Specify rectangle
        private readonly Rectangle _rect = new();

        internal override Texture2D Texture => _texture;
        internal override Rectangle Rectangle => _rect;

        internal GUIBoard(BoardController boardController)
        {
            _boardController = boardController;
        }

        internal override void Load(ContentManager content)
        {
            // TODO: Add board texture
            // _texture = content.Load<Texture2D>("");
        }
    }
}
