using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Utils;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a subject tile.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a purchasable tile.
    /// </para>
    /// <para>
    /// In order to place grades or an exam exemptions,
    /// there is a need to buy all the tiles of a given 
    /// section. To place another element on the tile,
    /// it is required to have one on each of the tiles
    /// of that section, and so on. Scores and exemptions
    /// can be placed before player's move.
    /// </para>
    /// <para>
    /// If someone step on this tile, they have to pay
    /// a rent to the person who owns this tile.
    /// </para>
    /// <para>
    /// In case of having all of the tiles in
    /// the section, the rent is double.
    /// </para>
    /// <para>
    /// Player can decide to take a retake froman owned
    /// subject. In this case there is an obligation to
    /// sell the grades and exemption from the exam
    /// to the bank for half of the price and pawn the card
    /// in the bank for a certain amount of the ECTS points.
    /// Moreover, money from someone entering this tile
    /// is no rewarded during this stage. In the future,
    /// player can buy this card again.
    /// </para>
    /// <para>
    /// Equivalent to the
    /// <see href="https://monopoly.fandom.com/wiki/Street">'Street'</see>
    /// in Monopoly. 
    /// </para>
    /// </remarks>
    internal class Subject : PurchasableTile
    {
        /// <summary>
        /// The grade of the subject.
        /// </summary>
        internal SubjectGrade Grade;

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
        /// Initializes a new instance of the <see cref="Subject"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        internal Subject(XmlNode node) : base(node)
        {
            Grade = SubjectGrade.Two;
            UpgradePrice = int.Parse(node.SelectSingleNode("upgrade_price").InnerText);
            TaxPrices = new Dictionary<SubjectGrade, int>();

            foreach (XmlAttribute attribute in node.SelectSingleNode("tax_prices").Attributes)
            {
                if (!Enum.TryParse(attribute.Name, true, out SubjectGrade temp))
                {
                    throw new ArgumentException($"Invalid attribute name in tax_prices node: {attribute.Name};" +
                        $" in tile node with {Id} id");
                }
                TaxPrices.Add(temp, int.Parse(attribute.Value));
            }

            string rawColor = NamingConverter.ConvertSnakeCaseToPascalCase(node.SelectSingleNode("color").InnerText);
            if (!Enum.TryParse(rawColor, true, out Color))
            {
                throw new ArgumentException($"Invalid contents of color node: {rawColor}; in tile node with {Id} id");
            }
        }

        /// <inheritdoc/>
        internal override void OnStand(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
