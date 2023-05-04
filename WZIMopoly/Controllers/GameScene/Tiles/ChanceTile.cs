using System.Xml;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a chance tile.
    /// </summary>
    /// <remarks>
    /// A change tile is a tile that when the player lands on it, they draw a chance card.
    /// </remarks>
    internal abstract class ChanceTile : Tile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChanceTile"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node of the chance tile.
        /// </param>
        internal ChanceTile(XmlNode node) : base(node) { }

        /// <summary>
        /// Draws a chance card.
        /// </summary>
        /// <returns>
        /// The chance card drawn.
        /// </returns>
        internal ChanceCard Draw()
        {
            throw new System.NotImplementedException();
        }    
    }
}
