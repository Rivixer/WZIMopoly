﻿#region Using Statements
using System.Xml;
#endregion

namespace WZIMpoly.Source.Board.Map.Tiles
{
    class Restroom : PurchasableTile
    {
        public Restroom(XmlNode node) : base(node)
        {

        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
