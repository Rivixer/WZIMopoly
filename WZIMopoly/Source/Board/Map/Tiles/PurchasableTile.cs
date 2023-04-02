﻿#region Using Statements
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    /// <summary>
    /// The base class for the classes for tiles that a player can purchase for the appropriate amount of ECTS.<br/><br/>
    /// If player steps on this field, they have to pay a rent to the person who owns this tile.
    /// </summary>
    abstract class PurchasableTile : Tile
    {
        public int Price;
        public Player? owner;
        protected PurchasableTile(XmlNode node) : base(node)
        {

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