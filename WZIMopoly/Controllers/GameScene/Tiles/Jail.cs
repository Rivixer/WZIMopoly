#region Using Statements
using System.Xml;
using WZIMopoly.Models;
#endregion

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Mandatory Lecture' tile.<br/>
    /// This is the tile on which player stands during the mandatory lecture. 
    /// </summary>
    /// <remarks>
    /// In case of not throwing a double dice in the third round, there is an obligation to pay pay the amount of ECTS indicated on the tile to be free.
    /// </remarks>
    /// <para>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Jail">'Jail'</see> tile in Monopoly.
    /// </para>
    
    class Jail : Tile
    {
        public Jail(XmlNode node) : base(node)
        { 
            /// <summary>
            /// Initializes a new instance of the <see  cref="Jail"/> class.
            /// </summary>
            /// <param name="node">
            /// The XML node containing the tile data.
            /// </param>
            
        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
