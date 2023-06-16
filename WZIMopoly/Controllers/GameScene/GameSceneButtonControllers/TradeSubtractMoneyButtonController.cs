using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameSceneButtonControllers
{
    /// <summary>
    /// Represents the trade subtract money button controller.
    /// </summary>
    internal class TradeSubtractMoneyButtonController : ButtonController<TradeSubtractMoneyButtonModel, GUITradeSubtractMoneyButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeSubtractMoneyButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the trade subtract money button.
        /// </param>
        /// <param name="view">
        /// The view of the trade subtract money button.
        /// </param>
        public TradeSubtractMoneyButtonController(TradeSubtractMoneyButtonModel model, GUITradeSubtractMoneyButton view)
            : base(model, view) { }
    }
}
