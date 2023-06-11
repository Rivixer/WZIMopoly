using WZIMopoly.Attributes;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameButtonControllers
{
    /// <summary>
    /// Represents the accept trade button controller.
    /// </summary>
    [UpdatesNetwork]
    internal sealed class TradeAcceptButtonController : ButtonController<TradeAcceptButtonModel, GUITradeAcceptButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeAcceptButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the accept trade button controller.
        /// </param>
        /// <param name="view">
        /// The view of the accept trade button controller.
        /// </param>
        internal TradeAcceptButtonController(TradeAcceptButtonModel model, GUITradeAcceptButton view)
            : base(model, view) { }
    }
}
