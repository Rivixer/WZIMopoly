using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene.GameSceneButtonModels
{
    /// <summary>
    /// Represents the model of the trade add money button.
    /// </summary>
    internal class TradeAddMoneyButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeAddMoneyButtonModel"/> class.
        /// </summary>
        public TradeAddMoneyButtonModel()
            : base("Plus") { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            var player = GameSettings.CurrentPlayer;
            IsActive = (WZIMopoly.GameType == GameType.Online && player == GameSettings.Client || WZIMopoly.GameType == GameType.Local)
                && player.PlayerStatus == PlayerStatus.Trading
                && Conditions();
        }
    }
}
