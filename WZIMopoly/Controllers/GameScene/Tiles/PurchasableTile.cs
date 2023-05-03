using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// The base class for the classes for tiles that a player can purchase for the appropriate amount of ECTS.<br/><br/>
    /// If player steps on this field, they have to pay a rent to the person who owns this tile.
    /// </summary>
    abstract class PurchasableTile : Tile
    {
        public readonly int Price;

        public Player owner;

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