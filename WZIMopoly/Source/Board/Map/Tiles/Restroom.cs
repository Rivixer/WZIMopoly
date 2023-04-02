#region Using Statements
using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Enums;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{
    class Restroom : PurchasableTile
    {
        public readonly Dictionary<RestroomAmount, int> TaxPrices;
        public Restroom(XmlNode node) : base(node)
        {
            TaxPrices = new Dictionary<RestroomAmount, int>();
            foreach (XmlAttribute attribute in node.SelectSingleNode("tax_prices").Attributes)
            {
                if (!Enum.TryParse(attribute.Name, true, out RestroomAmount temp))
                {
                    throw new ArgumentException($"Invalid attribute name in tax_prices node in tile node with {Id} id");
                }
                TaxPrices.Add(temp, int.Parse(attribute.Value));
            }
        }
        public override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
