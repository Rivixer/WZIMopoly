using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a map view.
    /// </summary>
    internal class GUIMap : GUIElement
    {
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
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch) { }

        /// <inheritdoc/>
        public override void Load(ContentManager content) { }

        /// <inheritdoc/>
        public override void Recalculate() { }

        /// <summary>
        /// Updates positions of all pawns.
        /// </summary>
        public void UpdatePawnPositions()
        {
            foreach (var tile in _model.GetAllControllers<TileController>())
            {
                List<Point> pawnPosition = tile.View.GetPawnPositions();
                foreach (var (player, position) in tile.Model.Players.Zip(pawnPosition, (p1, p2) => (p1, p2)))
                {
                    var ctrl = _model.GetController<PawnController>((x) => x.Model.Color == player.Color);
                    var rect = new Rectangle(position, ctrl.View.UnscaledDestinationRect.Size);
                    ctrl.View.SetNewDefDstRectangle(rect, GUIStartPoint.Center);
                }
            }
        }
    }
}
