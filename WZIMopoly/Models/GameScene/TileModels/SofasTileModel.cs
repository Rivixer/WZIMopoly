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
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        internal SofasTileModel(int id) : base(id) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SofasTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="SofasTileModel"/> instance.
        /// </returns>
        public static SofasTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.Attributes["id"].InnerText);
            var tile = new SofasTileModel(id);
            tile.LoadNamesFromXml(node);
            return tile;
        }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player) { }
    }
}
