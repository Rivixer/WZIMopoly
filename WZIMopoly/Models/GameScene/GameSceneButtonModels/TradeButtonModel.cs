using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene.GameButtonModels
{
    /// <summary>
    /// Represents the trade button model.
    /// </summary>
    internal sealed class TradeButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeButtonModel"/> class.
        /// </summary>
        internal TradeButtonModel()
            : base("Trade") { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            var player = GameSettings.CurrentPlayer;
            IsActive = (WZIMopoly.GameType == GameType.Online && player == GameSettings.Client || WZIMopoly.GameType == GameType.Local)
                && (player.PlayerStatus == PlayerStatus.BeforeRollingDice
                && player.PurchasedTiles.Count > 0
                || player.PlayerStatus == PlayerStatus.Trading);
        }
    }
}
