using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents an 'Elevator' tile.
    /// </summary>
    internal class ElevatorTileController : TileController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElevatorTileController"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param> 
        internal ElevatorTileController(XmlNode node) : base(node) { }
        
        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player)
        {
            throw new System.NotImplementedException();
        }   
    }
}
