#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WindowsWZIMpoly.GUI
{
    public class GameScreen : Screen
    {
        private readonly GUIBoard _board;

        public GameScreen(BoardController boardController)
        {
            _board = new GUIBoard(boardController);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(_board.Texture, _board.Rectangle, Color.White);
        }

        public override void Load(ContentManager content)
        {
            // _board.Load(content);
        }
    }
}
