using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Mandatory Lecture tile model.
    /// </summary>
    internal class MandatoryLectureTileModel : TileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MandatoryLectureTileModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        internal MandatoryLectureTileModel(int id) : base(id) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MandatoryLectureTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="MandatoryLectureTileModel"/> instance.
        /// </returns>
        public static MandatoryLectureTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.SelectSingleNode("id").InnerText);
            var tile = new MandatoryLectureTileModel(id);
            tile.LoadNamesFromXml(node);
            return tile;
        }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player) { }
    }
}
