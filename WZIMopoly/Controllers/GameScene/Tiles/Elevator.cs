#region Using Statements
using System.Xml;
using WZIMopoly.Models;
#endregion

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents an 'Elevator' tile.<br/>
    /// </summary>
    class Elevator : Tile
    {
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Initializes a new instance of the <see  cref="Elevator"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param> 
        public Elevator(XmlNode node) : base(node)
        {
            
        }
    }
}
