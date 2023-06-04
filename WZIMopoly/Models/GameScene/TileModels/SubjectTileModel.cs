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
        internal SubjectTileModel(int id, int price, int upgradedPrice, Dictionary<SubjectGrade, int> taxPrices, SubjectColor color)
            : base(id, price)
        {
            Grade = SubjectGrade.Two;
            UpgradePrice = upgradedPrice;
            TaxPrices = taxPrices;
            Color = color;

            OnStand += (player) =>
            {
                if (Owner != null)
                {
                    player.TransferMoneyTo(Owner, TaxPrices[Grade]);
                }
            };
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
            int id = int.Parse(node.Attributes["id"].InnerText);
            int price = int.Parse(node.SelectSingleNode("price").InnerText);
            int upgradePrice = int.Parse(node.SelectSingleNode("upgrade_price").InnerText);
            var taxPrices = new Dictionary<SubjectGrade, int>();

            foreach (XmlAttribute attribute in node.SelectSingleNode("tax_prices").Attributes)
            {
                if (!Enum.TryParse(NamingConverter.ConvertSnakeCaseToPascalCase(attribute.Name), true, out SubjectGrade temp))
                {
                    throw new ArgumentException($"Invalid attribute name in tax_prices node: {attribute.Name};" +
                        $" in tile node with {id} id");
                }
                taxPrices.Add(temp, int.Parse(attribute.Value));
            }

            string rawColor = NamingConverter.ConvertSnakeCaseToPascalCase(node.SelectSingleNode("color").InnerText);
            if (!Enum.TryParse(rawColor, true, out SubjectColor color))
            {
                throw new ArgumentException($"Invalid contents of color node: {rawColor}; in tile node with {id} id");
            }

            var tile = new SubjectTileModel(id, price, upgradePrice, taxPrices, color);
            tile.LoadNamesFromXml(node);
            return tile;
        }

        /// <summary>
        /// Gets the price for selling the subject grade.
        /// </summary>
        public int SellGradePrice => UpgradePrice / 2;

        /// <summary>
        /// Gets the price for mortgaging the subject.
        /// </summary>
        public int MortgagePrice => Price / 2;

        /// <summary>
        /// Gets the value indicating whether
        /// the subject is currently mortgaged.
        /// </summary>
        public bool IsMortgaged { get; private set; } = false;

        /// <summary>
        /// Upgrades the subject tile.
        /// </summary>
        public void Upgrade()
        {
            Grade += 1;
            Owner.Money -= UpgradePrice;
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
                && Grade != SubjectGrade.Exemption
                && !IsMortgaged
                && NoMortgagedTilesInColorSet()
                && PlayerHasSetOfColor(player);
        }

        /// <summary>
        /// Checks if the player can mortgage the subject tile.
        /// </summary>
        /// <param name="player">
        /// The player to check if can mortgage the subject tile.
        /// </param>
        /// <returns>
        /// True if the player can mortgage the subject tile, otherwise false.
        /// </returns>
        /// <remarks>
        /// The player can mortgage the subject tile if the player owns the tile
        /// and the grade is <see cref="SubjectGrade.Three"/>.
        /// </remarks>
        public bool CanMortgage(PlayerModel player)
        {
            return player == Owner && !IsMortgaged && Grade == SubjectGrade.Three;
        }

        /// <summary>
        /// Checks if the player can unmortgage the subject tile.
        /// </summary>
        /// <param name="player">
        /// The player to check if can unmortgage the subject tile.
        /// </param>
        /// <returns>
        /// True if the player can unmortgage the subject tile, otherwise false.
        /// </returns>
        /// <remarks>
        /// The player can unmortgage the subject tile if the player owns the tile,
        /// the tile is mortgaged and the player has enough money to pay the mortgage price.
        /// </remarks>
        public bool CanUnmortgage(PlayerModel player)
        {
            return player == Owner && IsMortgaged && player.Money >= MortgagePrice;
        }

        /// <summary>
        /// Checks if the player can sell the subject tile grade.
        /// </summary>
        /// <param name="player">
        /// The player to check if can sell the subject tile grade.
        /// </param>
        /// <returns>
        /// True if the player can sell the subject tile grade, otherwise false.
        /// </returns>
        /// <remarks>
        /// The player can sell the subject tile grade if the player owns the tile
        /// and the grade is greater than <see cref="SubjectGrade.Three"/>.
        /// </remarks>
        public bool CanSellGrade(PlayerModel player)
        {
            return player == Owner && Grade > SubjectGrade.Three;
        }

        /// <summary>
        /// Mortgages the subject tile.
        /// </summary>
        public void Mortgage()
        {
            Owner.MortgagedTiles.Add(this);
            Owner.Money += MortgagePrice;
            IsMortgaged = true;
        }

        /// <summary>
        /// Unmortages the subject tile.
        /// </summary>
        public void Unmortgage()
        {
            Owner.Money -= MortgagePrice;
            Owner.MortgagedTiles.Remove(this);
            IsMortgaged = false;
        }

        /// <summary>
        /// Sells the subject tile grade.
        /// </summary>
        public void SellGrade()
        {
            Grade -= 1;
            Owner.Money += SellGradePrice;
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
            var subjectTiles = AllTiles.Where(x => (x as SubjectTileModel)?.Color == Color).Cast<SubjectTileModel>();
            var playerTiles = player.PurchasedTiles.Where(x => (x as SubjectTileModel)?.Color == Color).Cast<SubjectTileModel>();
            return subjectTiles.SequenceEqual(playerTiles);
        }

        /// <summary>
        /// Checks if there are no mortgaged tiles in the color set.
        /// </summary>
        /// <returns>
        /// True if there are no mortgaged tiles in the color set, otherwise false.
        /// </returns>
        private bool NoMortgagedTilesInColorSet()
        {
            var subjectTiles = AllTiles.Where(x => (x as SubjectTileModel)?.Color == Color).Cast<SubjectTileModel>();
            return !subjectTiles.Any(x => x.IsMortgaged);
        }
    }
}
