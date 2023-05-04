using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// The base class for the classes for tiles that a player can purchase for the appropriate amount of ECTS.<br/><br/>
    /// If player steps on this field, they have to pay a rent to the person who owns this tile.
    /// </summary>
    internal abstract class PurchasableTile : Tile
    {
        internal readonly int Price;

        internal Player owner;

        protected PurchasableTile(XmlNode node) : base(node)
        {
            Price = int.Parse(node.SelectSingleNode("price").InnerText);
            owner = null;
        }

        internal void Purchase(Player owner)
        {
            throw new System.NotImplementedException("Not implemented");
        }

        internal bool CanPurchase(Player owner)
        {
            throw new System.NotImplementedException("Not implemented");
        }

        internal override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}