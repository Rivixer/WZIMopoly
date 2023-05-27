using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        /// <param name="price">
        /// The price of the tile.
        /// </param>
        /// <param name="taxPrices">
        /// The dictiopnary of prices for restrooms.
        /// </param>
        internal RestroomTileModel(int id, int price, Dictionary<RestroomAmount, int> taxPrices) : base(id,price)
        {
            TaxPrices = taxPrices;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestroomTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="RestroomTileModel"/> instance.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the XML file data is invalid.
        /// </exception>
        public static RestroomTileModel LoadFromXml(XmlNode node)
        {
            var taxPrices = new Dictionary<RestroomAmount, int>();
            int id = int.Parse(node.Attributes["id"].InnerText);
            foreach (XmlAttribute attribute in node.SelectSingleNode("tax_prices").Attributes)
            {
                if (!Enum.TryParse(attribute.Name, true, out RestroomAmount temp))
                {
                    throw new ArgumentException($"Invalid attribute name in tax_prices node: {attribute.Name};" +
                        $" in tile node with {id} id");
                }
                taxPrices.Add(temp, int.Parse(attribute.Value));
            }
            int price = int.Parse(node.SelectSingleNode("price").InnerText);
            var tile = new RestroomTileModel(id, price,taxPrices);
            tile.LoadNamesFromXml(node);
            return tile;
        }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player)
        {
            if (Owner != null && Owner != player)
            {
                RestroomAmount ownerRestroomAmount = GetOwnerRestroomAmonut();
                int tax = TaxPrices[ownerRestroomAmount];
                player.Money -= tax;
                Owner.Money += tax;
            }
        }

        /// <summary>
        /// Returns the amount of restrooms the owner has.
        /// </summary>
        /// <returns>
        /// The amount of restrooms the owner has 
        /// as the <see cref="RestroomAmount"/> enum.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// The owner has an invalid amount of restrooms.
        /// </exception>
        private RestroomAmount GetOwnerRestroomAmonut()
        {
            int? amount = Owner?.PurchasedTiles.Where(x => x is RestroomTileModel).Count();
            return amount switch
            {
                1 => RestroomAmount.One,
                2 => RestroomAmount.Two,
                3 => RestroomAmount.Three,
                4 => RestroomAmount.Four,
                _ => throw new ArgumentException($"RestroomAmount with specified amount not exist: {amount}"),
            };
        }
    }
}
