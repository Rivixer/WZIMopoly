using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents an 'Elevator' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// After stopping on this tile, the player is teleported to another
    /// to <see cref="ElevatorTileController">Elevator</see>
    /// </para>
    /// </remarks>
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
