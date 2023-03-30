#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
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
