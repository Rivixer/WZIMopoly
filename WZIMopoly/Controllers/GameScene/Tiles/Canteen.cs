using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary> 
    /// Represents a Canteen tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When a counter lands on this tile, a card is drawn.
    /// It can have a positive or negative effect on player's turn.<br/>
    /// Sometimes this card can be saved for later (e.g. Get out of the Must-have Lecture card).<br/>
    /// In case of taking a card from the 'Canteen' tile that moves counters,
    /// money is also rewarded for crossing the start tile in the case of moving a pawn, 
    /// unless it says 'go back'.
    /// </para>
    /// <para>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Chance">'Chance'</see> tile in Monopoly. 
    /// </para>
    /// </remarks>
    class Canteen : ChanceTile
    {
        public Canteen(XmlNode node) : base(node)
        {

        }

        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
        
    }
}
