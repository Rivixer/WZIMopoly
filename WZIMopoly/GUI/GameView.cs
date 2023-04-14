using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a game view.
    /// </summary>
    internal class GameView : GUITexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameView"/> class.
        /// </summary>
        public GameView() : base(new Rectangle(0, 0, 1920, 1080)) { }

        /// <inheritdoc/>
        internal override void Load(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Images/board");
        }
    }
}
