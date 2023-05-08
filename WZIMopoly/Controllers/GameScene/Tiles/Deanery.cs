using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Deanery' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// After stopping on this tile, the player enters a <see cref="Jail"/>.
    /// </para>
    /// <para>
    /// Equivalent to the
    /// <see href="https://monopoly.fandom.com/wiki/Go_to_Jail_(space)">
    /// 'Go To Jail'</see> tile in Monopoly.
    /// </para>
    /// </remarks>
    internal class Deanery : Tile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Deanery"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        internal Deanery(XmlNode node) : base(node) { }

        /// <inheritdoc/>
        internal override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
