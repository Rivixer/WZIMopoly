using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Restroom tile model.
    /// </summary>
    internal class RestroomTileModel : PurchasableTileModel
    {
        /// <summary>
        /// A dictionary containing the tax prices for each restroom amount.
        /// </summary>
        internal readonly Dictionary<RestroomAmount, int> TaxPrices;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestroomTileModel"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node of the chance tile.
        /// </param>
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
