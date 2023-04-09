#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Board
{
    /// <summary>
    /// Represents a 'Condition' tile. <br/>    
    /// The player, who lands on such a tile have to pay the amount of ECTS indicated on the tile to the bank,
    /// depending on the tile on which he stood.<br/>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Luxury_Tax">'Luxury Tax'</see>
    /// and <see href="https://monopoly.fandom.com/wiki/Income_Tax">'Income Tax'</see> tiles in Monopoly.
    /// </summary>
    class ConditionalPass : Tile
    {
        public readonly int Tax;
        public ConditionalPass(XmlNode node) : base(node)
        {
            Tax = int.Parse(node.SelectSingleNode("tax").InnerText);
        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
