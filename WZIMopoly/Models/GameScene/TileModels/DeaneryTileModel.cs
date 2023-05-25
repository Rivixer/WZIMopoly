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
        /// <param name="node">
        /// The XML node of the chance tile.
        /// </param>
        public DeaneryTileModel(XmlNode node) : base(node) { }

        /// <inheritdoc/>
        public override void OnStand(PlayerModel player) { }
    }
}
