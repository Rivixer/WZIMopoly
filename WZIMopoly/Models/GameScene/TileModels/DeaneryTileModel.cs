using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Deanery tile model.
    /// </summary>
    internal class DeaneryTileModel : TileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeaneryTileModel"/> class.
        /// </summary>
        internal DeaneryTileModel() : base() { }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player) { }
    }
}
