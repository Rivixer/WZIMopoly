#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
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
