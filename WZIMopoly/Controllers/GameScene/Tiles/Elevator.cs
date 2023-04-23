#region Using Statements
using System.Xml;
using WZIMopoly.Models;
#endregion

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents an 'Elevator' tile.<br/>
    /// </summary>
    /// <para>
    /// <remarks>
    /// The player who lands who lands on such a tile is able to decide whether to move to the second Elevator tile
    /// </remarks> 
    /// </para>
    

    class Elevator : Tile
    {
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
        public Elevator(XmlNode node) : base(node)
        {
            
            /// <summary>
            /// Initializes a new instance of the <see  cref="Elevator"/> class.
            /// </summary>
            /// <param name="node">
            /// The XML node containing the tile data.
            /// </param>
            
        }
    }
}
