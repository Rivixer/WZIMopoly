using System.Xml;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents a base controller for a chance tile.
    /// </summary>
    /// <remarks>
    /// A change tile is a tile when the player lands on it, they draw a chance card.
    /// </remarks>
    internal abstract class ChanceTileController : TileController<ChanceTileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChanceTileController"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node of the chance tile.
        /// </param>
        internal ChanceTileController(XmlNode node) : base(node) { }

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
