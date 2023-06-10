using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene.GameButtonModels
{
    /// <summary>
    /// Represents the make trade button model.
    /// </summary>
    internal sealed class TradeMakeButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeMakeButtonModel"/> class.
        /// </summary>
        internal TradeMakeButtonModel()
            : base("MakeTrade") { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            var player = GameSettings.CurrentPlayer;
            IsActive = (WZIMopoly.GameType == GameType.Online && player == GameSettings.Client || WZIMopoly.GameType == GameType.Local)
                && Conditions()
                && (player.PlayerStatus == PlayerStatus.Trading
                && player.PurchasedTiles.Count > 0
                || player.PlayerStatus == PlayerStatus.Trading);
        }
    }
}
