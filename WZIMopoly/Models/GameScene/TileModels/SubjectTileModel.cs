using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="node">
        /// The XML node of the chance tile.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the XML file data is invalid.
        /// </exception>
        internal SubjectTileModel(XmlNode node) : base(node)
        {
            Grade = SubjectGrade.Two;
            UpgradePrice = int.Parse(node.SelectSingleNode("upgrade_price").InnerText);
            TaxPrices = new Dictionary<SubjectGrade, int>();

            foreach (XmlAttribute attribute in node.SelectSingleNode("tax_prices").Attributes)
            {
                if (!Enum.TryParse(NamingConverter.ConvertSnakeCaseToPascalCase(attribute.Name), true, out SubjectGrade temp))
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

            OnStand += (player) => PayTaxToOwner(player);
        }

        /// <summary>
        /// Upgrades the subject tile.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the subject cannot be upgraded.
        /// </exception>
        public void Upgrade()
        {
            Owner.Money -= UpgradePrice;
            Grade = Grade switch
            {
                SubjectGrade.Three => SubjectGrade.ThreeHalf,
                SubjectGrade.ThreeHalf => SubjectGrade.Four,
                SubjectGrade.Four => SubjectGrade.FourHalf,
                SubjectGrade.FourHalf => SubjectGrade.Five,
                SubjectGrade.Five => SubjectGrade.Exemption,
                _ => throw new InvalidOperationException("Cannot upgrade subject with grade: " + Grade),
            };
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

        /// <summary>
        /// Pays the tax to the owner of the subject.
        /// </summary>
        /// <param name="player">
        /// The player who stands on the subject.
        /// </param>
        /// <remarks>
        /// If the owner is null or the owner is the same
        /// as the player, do nothing.
        /// </remarks>
        private void PayTaxToOwner(PlayerModel player)
        {
            if (Owner != null && player != Owner)
            {
                int tax = TaxPrices[Grade];
                player.Money -= tax;
                Owner.Money += tax;
            }
        }

        /// <summary>
        /// Checks if the player can upgrade the subject.
        /// </summary>
        /// <param name="player">
        /// The player to check.
        /// </param>
        /// <returns>
        /// True if the player can upgrade the subject, otherwise false.
        /// </returns>
        public bool CanUpgrade(PlayerModel player)
        {
            return player.Money >= UpgradePrice
                && PlayerHasSetOfColor(player)
                && Grade != SubjectGrade.Exemption;
        }

        /// <summary>
        /// Checks if the player has all tiles of the same color.
        /// </summary>
        /// <param name="player">
        /// The player to check.
        /// </param>
        /// <returns>
        /// True if the player has all tiles of the same color, otherwise false.
        /// </returns>
        private bool PlayerHasSetOfColor(PlayerModel player)
        {
            var subjectTiles = AllTiles.Where(x => x is SubjectTileModel).Cast<SubjectTileModel>();
            var subjectTilesColor = subjectTiles.Where(x => x.Color == Color);
            var playerSubjectTiles = player.PurchasedTiles.Where(x => x is SubjectTileModel).Cast<SubjectTileModel>();
            var playerSubjectTilesWithColor = playerSubjectTiles.Where(x => x.Color == Color);
            return subjectTilesColor.SequenceEqual(playerSubjectTilesWithColor);
        }
    }
}
