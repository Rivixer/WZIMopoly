﻿#region Using Statements
using System.Xml;
#endregion

namespace WZIMpoly.Source.Board.Map.Tiles
{
    class Jail : Tile
    {
        public Jail(XmlNode node) : base(node)
        {}
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
