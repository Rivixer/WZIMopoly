#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    abstract class PurchasableTile : Tile
    {
        public readonly int Price;
        public Player? owner;
        protected PurchasableTile(XmlNode node) : base(node)
        {
            Price = int.Parse(node.SelectSingleNode("price").InnerText);
            owner = null;
        }
        public void Purchase(Player owner)
        {
            throw new System.NotImplementedException("Not implemented");
        }
        public bool CanPurchase(Player owner)
        {
            throw new System.NotImplementedException("Not implemented");
        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }

    }
}