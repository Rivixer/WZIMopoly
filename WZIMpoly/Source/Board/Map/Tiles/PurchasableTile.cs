﻿#region Using Statements
using System.Xml;
#endregion

namespace WZIMpoly.Source.Board.Map.Tiles
{
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