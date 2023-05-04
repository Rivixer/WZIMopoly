﻿using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Enums;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a 'Restroom' tile.<br/>
    /// This is a purchasable tile. <br/>
    /// If player steps on this field, they have to pay a rent to the person who owns this tile.
    /// There is a chance to pop an owned tile. It's about pawning the card in the bank for a 
    /// certain amount of the ECTS points.
    /// Money from someone entering this tile is no rewarded during this stage. <br/>
    /// In our game, bathroom is the equivalent of one of the Monopoly 
    /// <see href="https://monopoly.fandom.com/wiki/Railroads">'Railroads'</see>.<br/>
    /// </summary>
    internal class Restroom : PurchasableTile
    {
        internal readonly Dictionary<RestroomAmount, int> TaxPrices;

        internal Restroom(XmlNode node) : base(node)
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

        internal override void OnStand(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
