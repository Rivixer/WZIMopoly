using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Elevator tile model.
    /// </summary>
    internal class ElevatorTileModel : TileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElevatorTileModel"/> class.
        /// </summary>
        internal ElevatorTileModel() : base() { }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player) { }
    }
}
