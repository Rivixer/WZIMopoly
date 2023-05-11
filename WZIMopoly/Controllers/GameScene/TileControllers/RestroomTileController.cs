using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Enums;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.TileControllers
{
    /// <summary>
    /// Represents a 'Restroom' tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a purchasable tile.
    /// </para>
    /// <para>
    /// If player steps on this field, they have to pay a rent
    /// to the person who owns this tile.
    /// </para>
    /// <para>
    /// There is a chance to pop an owned tile. It's about
    /// pawning the card in the bank for a certain amount
    /// of the ECTS points. Money from someone entering
    /// this tile is no rewarded during this stage.
    /// </para>
    /// <para>
    /// Equivalent to one of the Monopoly
    /// <see href="https://monopoly.fandom.com/wiki/Railroads">'Railroads'</see>.
    /// </para>
    /// </remarks>
    internal class RestroomTileController : PurchasableTileController
    {
        /// <summary>
        /// A dictionary containing the tax prices for each restroom amount.
        /// </summary>
        internal readonly Dictionary<RestroomAmount, int> TaxPrices;

        /// <summary>
        /// Initializes a new instance of the <see  cref="RestroomTileController"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        internal RestroomTileController(XmlNode node) : base(node)
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

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player)
        {
            throw new NotImplementedException();
        }
    }
}