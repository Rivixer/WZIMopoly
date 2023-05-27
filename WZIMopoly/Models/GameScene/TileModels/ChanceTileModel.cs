using System;
using System.Xml;
using WZIMopoly.Controllers.GameScene;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents a base controller for a chance tile model.
    /// </summary>
    /// <remarks>
    /// A chance tile is a tile when the player lands on it, they draw a chance card.
    /// </remarks>
    internal class ChanceTileModel : TileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChanceTileModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        internal ChanceTileModel(int id) : base(id) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="StartTileModel"/> instance.
        /// </returns>
        public static ChanceTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.Attributes["id"].InnerText);
            var tile = new ChanceTileModel(id);
            tile.LoadNamesFromXml(node);
            return tile;
        }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player) { }

        /// <summary>
        /// Draws a chance card.
        /// </summary>
        /// <returns>
        /// The chance card drawn.
        /// </returns>
        internal ChanceCard Draw()
        {
            throw new NotImplementedException();
        }
    }
}
