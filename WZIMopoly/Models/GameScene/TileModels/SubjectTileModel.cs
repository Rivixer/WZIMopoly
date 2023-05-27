using System;
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
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        /// <param name="price">
        /// The price of the tile.
        /// </param>
        /// <param name="upgradedPrice">
        /// The price of the tile after upgrade.
        /// </param>
        /// <param name="taxPrices">
        /// The prices of the subjects.
        /// </param>
        /// <param name="rawColor">
        /// The color as a string.
        /// </param>
        internal SubjectTileModel(int id, int price, int upgradedPrice, Dictionary<SubjectGrade, int> taxPrices, string rawColor) : base(id, price)
        {
            Grade = SubjectGrade.Two;
            UpgradePrice = upgradedPrice;
            if (!Enum.TryParse(rawColor, true, out Color))
            {
                throw new ArgumentException($"Invalid contents of color node: {rawColor}; in tile node with {id} id");
            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="SubjectTileModel"/> instance.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the XML file data is invalid.
        /// </exception>
        public static SubjectTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.SelectSingleNode("id").InnerText);
            int price = int.Parse(node.SelectSingleNode("price").InnerText);
            int upgradePrice = int.Parse(node.SelectSingleNode("upgrade_price").InnerText);
            var taxPrices = new Dictionary<SubjectGrade, int>();

            foreach (XmlAttribute attribute in node.SelectSingleNode("tax_prices").Attributes)
            {
                if (!Enum.TryParse(attribute.Name, true, out SubjectGrade temp))
                {
                    throw new ArgumentException($"Invalid attribute name in tax_prices node: {attribute.Name};" +
                        $" in tile node with {id} id");
                }
                taxPrices.Add(temp, int.Parse(attribute.Value));
            }

            string rawColor = NamingConverter.ConvertSnakeCaseToPascalCase(node.SelectSingleNode("color").InnerText);
            var tile = new SubjectTileModel(id, price, upgradePrice, taxPrices, rawColor);
            tile.LoadNamesFromXml(node);
            return tile;
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
