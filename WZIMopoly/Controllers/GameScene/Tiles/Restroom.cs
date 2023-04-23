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
    /// This is a purchasable tile. <br/>
    /// </summary>
    /// <remarks>
    /// If player steps on this field, they have to pay a rent to the person who owns this tile.
    /// </remarks>
    /// <para>
    /// <see href="https://monopoly.fandom.com/wiki/Railroads">'Railroads'</see>.<br/>
    /// </para>
   
    class Restroom : PurchasableTile
    {
        public readonly Dictionary<RestroomAmount, int> TaxPrices;
        public Restroom(XmlNode node) : base(node)
        {
            /// <summary>
            /// Initializes a new instance of the <see  cref="Restroom"/> class.
            /// </summary>
            /// <param name="node">
            /// The XML node containing the tile data.
            /// </param>
            
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