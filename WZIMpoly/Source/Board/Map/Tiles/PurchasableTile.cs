using System.Xml;

namespace WindowsWZIMpoly.Source.Board.Map.Tiles
{
    abstract class PurchasableTile
    {
        public int Price;
        public Player? owner;
        protected PurchasableTile(XmlNode node):base(node)
        {

        }
        public void Purchase(Player owner)
        {
            throw System.NotImplementedException
        }
        public bool CanPurchase(Player owner)
        {
            throw System.NotImplementedException
        }

    }
}