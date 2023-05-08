using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents an 'Elevator' tile.
    /// </summary>
    internal class Elevator : Tile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Elevator"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param> 
        internal Elevator(XmlNode node) : base(node) { }
        
        /// <inheritdoc/>
        internal override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }   
    }
}
