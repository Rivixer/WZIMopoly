using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameButtonControllers
{
    /// <summary>
    /// Represents the trade button controller.
    /// </summary>
    internal sealed class TradeButtonController : ButtonController<TradeButtonModel, GUITradeButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the trade button controller.
        /// </param>
        /// <param name="view">
        /// The view of the trade button controller.
        /// </param>
        internal TradeButtonController(TradeButtonModel model, GUITradeButton view)
            : base(model, view) { }
    }
}
