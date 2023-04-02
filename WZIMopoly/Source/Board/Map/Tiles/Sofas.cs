#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    /// <summary>
    /// Represents a 'Sofas' tile. <br/>
    /// Sofas tile is a safe tile in the game. It doesn't have a special function.<br/>
    /// Equivalent to the <see href="https://monopoly.fandom.com/wiki/Free_Parking">'Free Parking'</see> tile in Monopoly.
    /// </summary>
    class Sofas : Tile
    {
        public Sofas(XmlNode node) : base(node)
        {
            
        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
