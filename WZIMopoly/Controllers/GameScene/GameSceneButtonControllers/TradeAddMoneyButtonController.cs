using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameSceneButtonControllers
{
    /// <summary>
    /// Represents the trade add money button controller.
    /// </summary>
    internal class TradeAddMoneyButtonController : ButtonController<TradeAddMoneyButtonModel, GUITradeAddMoneyButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeAddMoneyButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the trade add money button.
        /// </param>
        /// <param name="view">
        /// The view of the trade add money button.
        /// </param>
        public TradeAddMoneyButtonController(TradeAddMoneyButtonModel model, GUITradeAddMoneyButton view)
            : base(model, view) { }
    }
}
