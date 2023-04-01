#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    class ConditionalPass : Tile
    {
        public int Tax;
        public ConditionalPass(XmlNode node) : base(node)
        {
            Tax = 0;  
        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
