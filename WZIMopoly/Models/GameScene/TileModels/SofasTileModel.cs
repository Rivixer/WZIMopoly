using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Sofas tile model.
    /// </summary>
    internal class SofasTileModel : TileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SofasTileModel"/> class.
        /// </summary>
        internal SofasTileModel() : base() { }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player) { }
    }
}
