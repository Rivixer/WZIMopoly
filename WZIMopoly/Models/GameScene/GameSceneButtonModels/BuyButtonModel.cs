using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene.GameButtonModels
{
    /// <summary>
    /// Represents the buy button model.
    /// </summary>
    internal sealed class BuyButtonModel : ButtonModel, IGameUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuyButtonModel"/> class.
        /// </summary>
        internal BuyButtonModel()
            : base("Buy") { }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            IsActive = player.PlayerStatus == Enums.PlayerStatus.AfterRollingDice
                && tile is PurchasableTileModel t
                && t.CanPurchase(player);
        }
    }
}
