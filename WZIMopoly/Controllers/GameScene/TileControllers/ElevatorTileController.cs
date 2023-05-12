using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents an 'Elevator' tile.
    /// </summary>
    internal class ElevatorTileController : TileController<ElevatorTileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElevatorTileController"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param> 
        internal ElevatorTileController(ElevatorTileModel model, GUITile view) 
            : base(model, view) { }
    }
}
