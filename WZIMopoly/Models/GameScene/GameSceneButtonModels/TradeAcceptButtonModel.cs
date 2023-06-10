using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene.GameButtonModels
{
    /// <summary>
    /// Represents the accept trade button model.
    /// </summary>
    internal sealed class TradeAcceptButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeAcceptButtonModel"/> class.
        /// </summary>
        internal TradeAcceptButtonModel()
            : base("Accept") { }

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
