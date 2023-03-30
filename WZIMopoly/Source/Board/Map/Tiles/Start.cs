#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    class Start : Tile, ICrossable
    {
        private int _money;
        public Start(XmlNode node) : base(node)
        {
            _money = 0;
        }
        public void OnCross(Player player)
        {
            throw new System.NotImplementedException();
        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
