using WZIMopoly.Attributes;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameButtonControllers
{
    /// <summary>
    /// Represents the buy button controller.
    /// </summary>
    [UpdatesNetwork]
    internal sealed class BuyButtonController : ButtonController<BuyButtonModel, GUIBuyButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuyButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the buy button controller.
        /// </param>
        /// <param name="view">
        /// The view of the buy button controller.
        /// </param>
        internal BuyButtonController(BuyButtonModel model, GUIBuyButton view)
            : base(model, view) { }
    }
}
