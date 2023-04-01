#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    class Start : Tile, ICrossable
    {
        private readonly int _reward;
        public Start(XmlNode node) : base(node)
        {
            _reward = int.Parse(node.SelectSingleNode("reward").InnerText);
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
