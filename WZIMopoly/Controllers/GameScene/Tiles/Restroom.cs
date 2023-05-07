#region Using Statements
using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Enums;
using WZIMopoly.Models;
#endregion

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Restroom' tile.<br/>
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a purchasable tile. <br/>
    /// </para>
    /// <para>
    /// If player steps on this field, they have to pay a rent to the person who owns this tile.
    /// There is a chance to pop an owned tile. It's about pawning the card in the bank for a 
    /// certain amount of the ECTS points.
    /// </para>
    /// <para>
    /// Money from someone entering this tile is no rewarded during this stage.
    /// </para>
    /// <para>
    /// the equivalent of one the of the Monopoly <br/>
    /// <see href="https://monopoly.fandom.com/wiki/Railroads">'Railroads'</see>.<br/>
    /// <para>
    /// <remarks>
    class Restroom : PurchasableTile
    {
        public readonly Dictionary<RestroomAmount, int> TaxPrices;
        /// <summary>
        /// Initializes a new instance of the <see  cref="Restroom"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        public Restroom(XmlNode node) : base(node)
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
        public override void OnStand(Player player)
        {
            throw new NotImplementedException();
        }
    }
}