using System.Xml;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Represents a base class for tile classes that
    /// a player can purchase for the appropriate amount of ECTS.
    /// </summary>
    /// <remarks>
    /// If player steps on this field, they have to pay
    /// a rent to the person who owns this tile.
    /// </remarks>
    internal abstract class PurchasableTile : Tile
    {
        /// <summary>
        /// The price of the tile.
        /// </summary>
        internal readonly int Price;

#nullable enable
        /// <summary>
        /// The owner of the tile.
        /// </summary>
        /// <remarks>
        /// Can be <see langword="null"/> if the tile is not owned by anyone.
        /// </remarks>
        internal Player? owner;
#nullable disable

        /// <summary>
        /// Initializes a new instance of the <see  cref="PurchasableTile"/> class.
        /// </summary>
        /// <param name="node">
        /// The XML node containing the tile data.
        /// </param>
        protected PurchasableTile(XmlNode node) : base(node)
        {     
            Price = int.Parse(node.SelectSingleNode("price").InnerText);
            owner = null;
        }

        /// <summary>
        /// Purchases the tile.
        /// </summary>
        /// <param name="owner">
        /// The player who purchases the tile.
        /// </param>
        internal void Purchase(Player owner)
        {
            throw new System.NotImplementedException("Not implemented");
        }

        /// <summary>
        /// Determines whether the specified player can purchase the tile.
        /// </summary>
        /// <param name="owner">
        /// The player who wants to purchase the tile.
        /// </param>
        /// <returns>
        /// True if the player can purchase the tile, false otherwise.
        /// </returns>
        internal bool CanPurchase(Player owner)
        {
            throw new System.NotImplementedException("Not implemented");
        }

        /// <inheritdoc/>
        internal override void OnStand(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}