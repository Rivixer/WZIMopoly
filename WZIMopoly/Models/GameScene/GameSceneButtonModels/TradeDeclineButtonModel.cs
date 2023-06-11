using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene.GameButtonModels
{
    /// <summary>
    /// Represents the decline trade button model.
    /// </summary>
    internal sealed class TradeDeclineButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeDeclineButtonModel"/> class.
        /// </summary>
        internal TradeDeclineButtonModel()
            : base("Decline") { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            var player = GameSettings.CurrentPlayer;
            IsActive = (WZIMopoly.GameType == GameType.Online && player == GameSettings.Client || WZIMopoly.GameType == GameType.Local)
                && player.PlayerStatus == PlayerStatus.ReceivingTrade
                && Conditions();
        }
    }
}
