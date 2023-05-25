using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a map view.
    /// </summary>
    internal sealed class GUIMap : GUIElement
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

        /// <sumary>
        /// Initializes a new instance of the <see cref="GUIMap"/> class.
        /// </summary>
        public GUIMap() { 
            _background = new GUITexture("Images/Board", new Rectangle(0, 0, 1920, 1080));
            _names = new GUITexture("Images/TileNamesPL", new Rectangle(0, 0, 1920, 1080));
            _prices = new GUITexture("Images/TilePrices", new Rectangle(0, 0, 1920, 1080));
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            _names.Draw(spriteBatch);
            _prices.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _background.Load(content);
            _names.Load(content);
            _prices.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _background.Recalculate();
            _names.Recalculate();
            _prices.Recalculate();
        }
    }
}
