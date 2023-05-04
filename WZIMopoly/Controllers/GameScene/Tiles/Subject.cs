using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Utils;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a subject tile.<br/>
    /// This is a purchasable tile. <br/>
    /// Each tile can have its own grade.
    /// In order to place grades or an exam exemptions, there is a need to buy all the tiles of a given 
    /// section. To place another element on the tile, it is required to have one on each
    /// of the tiles of that section, and so on. Scores and exemptions can be placed before player's move.
    /// If someone step on this tile, they have to pay a rent to the person who owns this tile.
    /// In case of having all of the tiles in the section, the rent is double.
    /// Player can decide to take a retake from an owned subject. In this case there is an obligation to
    /// sell the grades and exemption from the exam to the bank for half of the price 
    /// and pawn the card in the bank for a certain amount of the ECTS points.
    /// Moreover, money from someone entering this tile is no rewarded during this stage.
    /// In the future, player can buy this card again.<br/>
    /// Equivalent to Monopoly <see href="https://monopoly.fandom.com/wiki/Street">'streets'</see>. 
    /// </summary>
    internal class Subject : PurchasableTile
    {
        internal SubjectGrade Grade;

        internal readonly int UpgradePrice;

        internal readonly Dictionary<SubjectGrade, int> TaxPrices;

        internal readonly SubjectColor Color;

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

        internal override void OnStand(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
