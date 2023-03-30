#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    class Canteen : ChanceTile
    {
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
        public Canteen(XmlNode node) : base(node)
        {

        }
    }
}
