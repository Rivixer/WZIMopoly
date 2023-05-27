using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Conditional Pass tile model.
    /// </summary>
    internal class ConditionalPassTileModel : TileModel
    {
        /// <summary>
        /// The amount of ECTS to be paid.
        /// </summary>
        internal readonly int Tax;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalPassTileModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        /// <param name="tax">
        /// The tax for stepping no tile.
        /// </param>
        internal ConditionalPassTileModel(int id, int tax) : base(id)
        {
            Tax = tax;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalPassTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="ConditionalPassTileModel"/> instance.
        /// </returns>
        public static ConditionalPassTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.Attributes["id"].InnerText);
            int tax = int.Parse(node.SelectSingleNode("tax").InnerText);
            var tile = new ConditionalPassTileModel(id, tax);
            tile.LoadNamesFromXml(node);
            return tile;
        }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player) 
        {
            player.Money -= Tax;
        }
    }
}
