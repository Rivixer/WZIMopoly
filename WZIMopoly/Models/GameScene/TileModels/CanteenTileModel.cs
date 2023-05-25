using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Canteen tile model.
    /// </summary>
    internal class CanteenTileModel : ChanceTileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanteenTileModel"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node of the chance tile.
        /// </param>
        internal CanteenTileModel(XmlNode node) : base(node) { }
    }
}
