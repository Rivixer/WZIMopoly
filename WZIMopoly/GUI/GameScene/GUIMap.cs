using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a map view.
    /// </summary>
    internal class GUIMap : GUIElement
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
        /// The model of the map.
        /// </summary>
        private readonly MapModel _model;

        /// <sumary>
        /// Initializes a new instance of the <see cref="GUIMap"/> class.
        /// </summary>
        public GUIMap(MapModel model)
        {
            _model = model;
            _background = new GUITexture("Images/Board", new Rectangle(0, 0, 1920, 1080));
            _names = new GUITexture("Images/TileNames", new Rectangle(0, 0, 1920, 1080));
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

        /// <summary>
        /// Updates positions of all pawns.
        /// </summary>
        public void UpdatePawnPositions()
        {
            foreach (var tile in _model.GetAllControllers<TileController>())
            {
                List<Point> pawnPosition = tile.View.GetPawnPositions();
                foreach (var (Player, Position) in tile.Model.Players.Zip(pawnPosition, (p1, p2) => (p1, p2)))
                {
                    var ctrl = _model.GetController<PawnController>((x) => x.Model.Color == Player.Color);
                    ctrl.View.UpdatePosition(Position);
                }
            }
        }
    }
}
