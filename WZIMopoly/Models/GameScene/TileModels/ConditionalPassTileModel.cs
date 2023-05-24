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
        public readonly int Tax;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalPassTileModel"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        public ConditionalPassTileModel(XmlNode node) : base(node)
        {
            Tax = int.Parse(node.SelectSingleNode("tax").InnerText);
        }

        /// <inheritdoc/>
        public override void OnStand(PlayerModel player)
        {
            player.LoseMoney(Tax);
        }
    }
}
