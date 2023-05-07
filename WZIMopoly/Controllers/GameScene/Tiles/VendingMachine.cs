#region Using Statements
using System.Xml;
using WZIMopoly.Models;
#endregion

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Vending Machine' tile.<br/>
    /// </summary>
    /// <remarks>
    /// <para>
    /// When a counter lands on this tile, a card is drawn. It can have a positive or negative 
    /// effect on player's turn. Sometimes this card can be saved for later (e.g. Get out of 
    /// the Must-have Lecture card).
    /// In case of taking a card from the 'Vending Machine' tile that moves counters, 
    /// money is also rewarded for crossing the start tile in the case of moving a pawn, 
    /// unless it says 'go back'. <br/>
    /// </para>
    /// <para>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Chance">'Chance'</see> tile in Monopoly.
    /// </para>
    /// <remarks>

    class VendingMachine : ChanceTile
    {
        /// <summary>
        /// Initializes a new instance of the <see  cref="VendingMachine"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        public VendingMachine(XmlNode node) : base(node)
        {

        }
        
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
