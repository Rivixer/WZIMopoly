using WZIMopoly.Attributes;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameButtonControllers
{
    /// <summary>
    /// Represents the decline trade button controller.
    /// </summary>
    [UpdatesNetwork]
    internal sealed class TradeDeclineButtonController : ButtonController<TradeDeclineButtonModel, GUITradeDeclineButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeDeclineButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the decline trade button controller.
        /// </param>
        /// <param name="view">
        /// The view of the decline trade button controller.
        /// </param>
        internal TradeDeclineButtonController(TradeDeclineButtonModel model, GUITradeDeclineButton view)
            : base(model, view) { }
    }
}
