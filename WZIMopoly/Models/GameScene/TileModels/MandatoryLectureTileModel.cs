using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Mandatory Lecture tile model.
    /// </summary>
    internal class MandatoryLectureTileModel : TileModel
    {
        /// <summary>
        /// The amount of money that the player has to pay to leave the jail.
        /// </summary>
        private readonly int _payForLeave;

        /// <summary>
        /// Initializes a new instance of the <see cref="MandatoryLectureTileModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        internal MandatoryLectureTileModel(int id) : base(id) { }
            _payForLeave = payForLeave;

        /// <summary>
        /// Gets the amount of money that the player has to pay to leave the jail.
        /// </summary>
        public int PayForLeave => _payForLeave;

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
            int id = int.Parse(node.Attributes["id"].InnerText);
            int payForLeave = int.Parse(node["pay_for_leave"].InnerText);
            var tile = new MandatoryLectureTileModel(id, payForLeave);
            tile.LoadNamesFromXml(node);
            return tile;
        }
    }
}
