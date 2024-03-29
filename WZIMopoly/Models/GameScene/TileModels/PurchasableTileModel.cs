using System;
using System.Linq;

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
    [Serializable]
    internal abstract class PurchasableTileModel : TileModel, IMortgageable
    {
        /// <summary>
        /// The price of the tile.
        /// </summary>
        internal readonly int Price;

        /// <summary>
        /// Whether the tile is mortgaged.
        /// </summary>
        private bool _isMortgaged = false;

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
        /// Initializes a new instance of the <see cref="PurchasableTileModel"/> class.
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

        /// <inheritdoc/>
        public int MortgagePrice => Price / 2;

        /// <inheritdoc/>
        public bool IsMortgaged => _isMortgaged;

        /// <summary>
        /// Returns the value of the tile.
        /// </summary>
        /// <returns>
        /// The value of the tile.
        /// </returns>
        public abstract int GetValue();

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

        /// <inheritdoc/>
        public virtual void Mortgage()
        {
            Owner.MortgagedTiles.Add(this);
            Owner.Money += MortgagePrice;
            _isMortgaged = true;
        }

        /// <inheritdoc/>
        public virtual void Unmortgage()
        {
            Owner.Money -= MortgagePrice;
            Owner.MortgagedTiles.Remove(this);
            _isMortgaged = false;
        }

        /// <inheritdoc/>
        public virtual bool CanMortgage(PlayerModel player)
        {
            return player.Equals(Owner) && !IsMortgaged;
        }

        /// <inheritdoc/>
        public virtual bool CanUnmortgage(PlayerModel player)
        { 
            return player.Equals(Owner) && IsMortgaged && player.Money >= MortgagePrice;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Sets the owner of the tile to the owner of the model.
        /// </remarks>
        public override void Update(TileModel model)
        {
            base.Update(model);
            if (model is PurchasableTileModel t)
            {
                var owner = GameSettings.ActivePlayers.FirstOrDefault(x => x.Equals(t.Owner));
                Owner = owner;
                _isMortgaged = t._isMortgaged;
            }
        }

        /// <inheritdoc/>
        public override void Reset()
        {
            base.Reset();
            Owner = null;
            _isMortgaged = false;
        }
    }
}