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
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        internal CanteenTileModel(int id) : base(id) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CanteenTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="CanteenTileModel"/> instance.
        /// </returns>
        public static CanteenTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.Attributes["id"].InnerText);
            var tile = new CanteenTileModel(id);
            tile.LoadNamesFromXml(node);
            return tile;
        }
    }
}
