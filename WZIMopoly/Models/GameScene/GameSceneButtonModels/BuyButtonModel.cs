using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene.GameButtonModels
{
    /// <summary>
    /// Represents the buy button model.
    /// </summary>
    internal sealed class BuyButtonModel : ButtonModel, IGameButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuyButtonModel"/> class.
        /// </summary>
        public BuyButtonModel()
            : base("Buy") { }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            IsActive = tile is PurchasableTileModel t && t.CanPurchase(player);
        }
    }
}
