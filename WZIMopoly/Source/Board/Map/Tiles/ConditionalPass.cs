#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    class ConditionalPass : Tile
    {
        public int Value;
        public ConditionalPass(XmlNode node) : base(node)
        {
            Value = 0;  
        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
