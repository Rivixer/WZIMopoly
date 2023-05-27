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
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        internal VendingMachineTileModel(int id) : base(id) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VendingMachineTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="VendingMachineTileModel"/> instance.
        /// </returns>
        public static VendingMachineTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.SelectSingleNode("id").InnerText);
            var tile = new VendingMachineTileModel(id);
            tile.LoadNamesFromXml(node);
            return tile;
        }
    }
}
