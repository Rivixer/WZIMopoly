using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene.GameSceneButtonModels
{
    /// <summary>
    /// Represents the model of the trade subtract money button.
    /// </summary>
    internal class TradeSubtractMoneyButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeSubtractMoneyButtonModel"/> class.
        /// </summary>
        public TradeSubtractMoneyButtonModel()
            : base("Minus") { }

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
