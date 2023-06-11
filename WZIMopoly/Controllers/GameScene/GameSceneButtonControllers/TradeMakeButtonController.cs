using WZIMopoly.Attributes;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameButtonControllers
{
    /// <summary>
    /// Represents the make trade button controller.
    /// </summary>
    [UpdatesNetwork]
    internal sealed class TradeMakeButtonController : ButtonController<TradeMakeButtonModel, GUITradeMakeButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeMakeButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the make trade button controller.
        /// </param>
        /// <param name="view">
        /// The view of the make trade button controller.
        /// </param>
        internal TradeMakeButtonController(TradeMakeButtonModel model, GUITradeMakeButton view)
            : base(model, view) { }
    }
}
