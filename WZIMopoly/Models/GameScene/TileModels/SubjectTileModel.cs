﻿using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Enums;
using WZIMopoly.Utils;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents the Subject tile model.
    /// </summary>
    internal class SubjectTileModel : PurchasableTileModel
    {
        /// <summary>
        /// The price for upgrading subject.
        /// </summary>
        internal readonly int UpgradePrice;

        /// <summary>
        /// A dictionary containing the tax prices for each subject grade.
        /// </summary>
        internal readonly Dictionary<SubjectGrade, int> TaxPrices;

        /// <summary>
        /// The color representing the section of the tile.
        /// </summary>
        internal readonly SubjectColor Color;

        /// <summary>
        /// The grade of the subject.
        /// </summary>
        internal SubjectGrade Grade;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectTileModel"/> class.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the XML file data is invalid.
        /// </exception>
        internal SubjectTileModel() : base()
        {
            Grade = SubjectGrade.Two;
            UpgradePrice = int.Parse(MapModel.XmlNode.SelectSingleNode("upgrade_price").InnerText);
            TaxPrices = new Dictionary<SubjectGrade, int>();

            foreach (XmlAttribute attribute in MapModel.XmlNode.SelectSingleNode("tax_prices").Attributes)
            {
                if (!Enum.TryParse(attribute.Name, true, out SubjectGrade temp))
                {
                    throw new ArgumentException($"Invalid attribute name in tax_prices node: {attribute.Name};" +
                        $" in tile node with {Id} id");
                }
                TaxPrices.Add(temp, int.Parse(attribute.Value));
            }

            string rawColor = NamingConverter.ConvertSnakeCaseToPascalCase(MapModel.XmlNode.SelectSingleNode("color").InnerText);
            if (!Enum.TryParse(rawColor, true, out Color))
            {
                throw new ArgumentException($"Invalid contents of color node: {rawColor}; in tile node with {Id} id");
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Sets the <see cref="Grade"/> of the subject
        /// to <see cref="SubjectGrade.Three"/>.
        /// </remarks>
        internal override void Purchase(PlayerModel player)
        {
            base.Purchase(player);
            Grade = SubjectGrade.Three;
        }

        /// <inheritdoc/>
        internal override void OnStand(PlayerModel player)
        {
            if (Owner != null && player != Owner)
            {
                int tax = TaxPrices[Grade];
                player.Money -= tax;
                Owner.Money += tax;
            }
        }
    }
}
