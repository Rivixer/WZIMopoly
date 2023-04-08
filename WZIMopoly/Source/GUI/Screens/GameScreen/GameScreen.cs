#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WZIMopoly.GUI
{
    /// <summary>
    /// The game screen.
    /// </summary>
    public class GameScreen : Screen
    {
        private readonly GUIBoard _board;

        public GameScreen()
        {
            _board = new GUIBoard(new(0,0,1920,1080));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _board.Draw(spriteBatch);
        }

        public override void Load(ContentManager content)
        {
            _board.Load(content);
        }
        public override void RecalculateAll()
        {
            _board.RecalculateAll();
        }
    }
}
