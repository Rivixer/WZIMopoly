#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WZIMopoly.GUI
{
    internal class GUIBoard : GUIElement
    {
        private readonly BoardController _boardController;
        private Texture2D _texture;
        // TODO: Specify rectangle
        private readonly Rectangle _destinationRect = new(0, 0, 1920, 1080);

        internal override Texture2D Texture => _texture;
        internal override Rectangle DestinationRect => _destinationRect;

        internal GUIBoard(BoardController boardController)
        {
            _boardController = boardController;
        }

        internal override void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Images/Board");
        }
    }
}
