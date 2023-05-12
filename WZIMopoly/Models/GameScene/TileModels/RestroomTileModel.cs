using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene.TileModels
{
    internal class RestroomTileModel : PurchasableTileModel
    {
        /// <summary>
        /// A dictionary containing the tax prices for each restroom amount.
        /// </summary>
        internal readonly Dictionary<RestroomAmount, int> TaxPrices;

        internal RestroomTileModel(XmlNode node) : base(node)
        {
            TaxPrices = new Dictionary<RestroomAmount, int>();
            foreach (XmlAttribute attribute in node.SelectSingleNode("tax_prices").Attributes)
            {
                if (!Enum.TryParse(attribute.Name, true, out RestroomAmount temp))
                {
                    throw new ArgumentException($"Invalid attribute name in tax_prices node: {attribute.Name};" +
                        $" in tile node with {Id} id");
                }
                TaxPrices.Add(temp, int.Parse(attribute.Value));
            }
        }
    }
}
