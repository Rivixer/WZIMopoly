using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Sofas' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Sofas tile is a safe tile in the game.
    /// It doesn't have a special function.
    /// </para>
    /// <para>
    /// Equivalent to the
    /// <see href="https://monopoly.fandom.com/wiki/Free_Parking">'Free Parking'</see>
    /// tile in Monopoly.
    /// </para>
    /// </remarks>
    internal class Sofas : Tile
    {
        /// <summary>
        /// Initializes a new instance of the <see  cref="Sofas"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        internal Sofas(XmlNode node) : base(node) { }
        
        /// <inheritdoc/>
        internal override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
