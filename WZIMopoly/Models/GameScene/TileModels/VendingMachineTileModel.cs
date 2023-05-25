using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Vending Machine tile model.
    /// </summary>
    internal class VendingMachineTileModel : ChanceTileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VendingMachineTileModel"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node of the chance tile.
        /// </param>
        internal VendingMachineTileModel(XmlNode node) : base(node) { }
    }
}
