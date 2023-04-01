#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    class Start : Tile, ICrossable
    {
        private int _reward;
        public Start(XmlNode node) : base(node)
        {
            _reward = 0;
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
