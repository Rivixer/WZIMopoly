using System.Xml;

namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Represents a base class for tile classes that
    /// a player can purchase for the appropriate amount of ECTS.
    /// </summary>
    /// <remarks>
    /// If player steps on this field, they have to pay
    /// a rent to the person who owns this tile.
    /// </remarks>
    internal abstract class PurchasableTileModel : TileModel
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
        internal PlayerModel? Owner;
#nullable disable

        /// <summary>
        /// Initializes a new instance of the <see  cref="PurchasableTileModel"/> class.
        /// </summary>
        /// <param name="id">
        /// The id of the tile.
        /// </param>
        /// <param name="price">
        /// The price of the tile.
        /// </param>
        internal PurchasableTileModel(int id, int price) : base(id)
        {
            Price = price;
            Owner = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchasableTileModel"/> class,
        /// loading the data from the xml node.
        /// </summary>
        /// <param name="node">
        /// The XML node to load the data from.
        /// </param>
        /// <returns>
        /// The <see cref="PurchasableTileModel"/> instance.
        /// </returns>
        public static PurchasableTileModel LoadFromXml(XmlNode node)
        {
            int id = int.Parse(node.SelectSingleNode("id").InnerText);
            int price = int.Parse(node.SelectSingleNode("price").InnerText);
            var tile = new PurchasableTileModel(id);
            tile.LoadNamesFromXml(node, price);
            return tile;
        }

        /// <summary>
        /// Purchases the tile.
        /// </summary>
        /// <param name="player">
        /// The player who purchases the tile.
        /// </param>
        internal virtual void Purchase(PlayerModel player)
        {
            Owner = player;
            Owner.Money -= Price;
            Owner.PurchasedTiles.Add(this);
        }

        /// <summary>
        /// Determines whether the specified player can purchase the tile.
        /// </summary>
        /// <param name="player">
        /// The player who wants to purchase the tile.
        /// </param>
        /// <returns>
        /// True if the player can purchase the tile, false otherwise.
        /// </returns>
        internal bool CanPurchase(PlayerModel player)
        {
            return Owner == null && player.Money >= Price;
        }
    }
}