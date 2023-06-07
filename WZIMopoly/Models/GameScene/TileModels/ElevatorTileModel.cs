using System;
using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Elevator tile model.
    /// </summary>
    [Serializable]
    internal class ElevatorTileModel : TileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElevatorTileModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        internal ElevatorTileModel(int id) : base(id) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElevatorTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="ElevatorTileModel"/> instance.
        /// </returns>
        public static ElevatorTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.Attributes["id"].InnerText);
            var tile = new ElevatorTileModel(id);
            tile.LoadNamesFromXml(node);
            return tile;
        }
    }
}
