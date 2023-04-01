#region Using Statements
using System.Collections.Generic;
using System.Xml;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    enum RestroomsAmount
    {
        one,
        two,
        three,
        four
    }
    class Restroom : PurchasableTile
    {
        public readonly Dictionary<RestroomsAmount, int> TaxPrices;
        public Restroom(XmlNode node) : base(node)
        {

        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
