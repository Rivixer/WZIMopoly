using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents an 'Elevator' tile.
    /// </summary>
    internal sealed class ElevatorTileController : TileController<ElevatorTileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElevatorTileController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the Elevator tile.
        /// </param>
        /// <param name="view">
        /// The view of the Elevator tile.
        /// </param>
        internal ElevatorTileController(ElevatorTileModel model, GUITile view) 
            : base(model, view) { }
    }
}
