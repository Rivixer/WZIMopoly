using System.Xml;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a ConditionalPass tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The player, who lands on such a tile have to pay the amount of ECTS indicated on the tile to the bank,
    /// depending on the tile on which he stood.
    /// </para>
    /// <para>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Luxury_Tax">'Luxury Tax'</see>
    /// and <see href="https://monopoly.fandom.com/wiki/Income_Tax">'Income Tax'</see> tiles in Monopoly.
    /// </para>
    /// </remarks>
    internal class ConditionalPass : Tile
    {
        /// <summary>
        /// The amount of ECTS to be paid.
        /// </summary>
        public readonly int Tax;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalPass"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        public ConditionalPass(XmlNode node) : base(node)
        {
            Tax = int.Parse(node.SelectSingleNode("tax").InnerText);
        }

        /// <inheritdoc/>
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
