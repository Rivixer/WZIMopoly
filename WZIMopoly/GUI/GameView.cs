using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a game view.
    /// </summary>
    internal class GameView : GUIElement
    {
        /// <summary>
        /// The background texture.
        /// </summary>
        private readonly GUITexture _background;

        /// <summary>
        /// The graphic with names of tiles.
        /// </summary>
        private readonly GUITexture _names;

        /// <summary>
        /// The graphic with prices of tiles.
        /// </summary>
        private readonly GUITexture _prices;

        /// <summary>
        /// The board texture.
        /// </summary>
        private readonly GUITexture _board;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameView"/> class.
        /// </summary>
        public GameView()
        {
            _background = new GUITexture("Images/Background", new Rectangle(0, 0, 1920, 1080));
            _names = new GUITexture("Images/TileNames", new Rectangle(0, 0, 1920, 1080));
            _prices = new GUITexture("Images/TilePrices", new Rectangle(0, 0, 1920, 1080));
            _board = new GUITexture("Images/Board", new Rectangle(0, 0, 1920, 1080));
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            _board.Draw(spriteBatch);
            _names.Draw(spriteBatch);
            _prices.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _background.Load(content);
            _board.Load(content);
            _names.Load(content);
            _prices.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _background.Recalculate();
            _board.Recalculate();
            _names.Recalculate();
            _prices.Recalculate();
        }
    }
}
