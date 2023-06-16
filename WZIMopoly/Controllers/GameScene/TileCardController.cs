using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents the tile card controller.
    /// </summary>
    internal class TileCardController : Controller<TileCardModel, GUITileCard>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TileCardController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the tile card.
        /// </param>
        /// <param name="view">
        /// The view of the tile card.
        /// </param>
        public TileCardController(TileCardModel model, GUITileCard view)
            : base(model, view) { }
    }
}
