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
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        internal DeaneryTileModel(int id) : base(id) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeaneryTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="DeaneryTileModel"/> instance.
        /// </returns>
        public static DeaneryTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.Attributes["id"].InnerText);
            var tile = new DeaneryTileModel(id);
            tile.LoadNamesFromXml(node);
            return tile;
        }
    }
}
