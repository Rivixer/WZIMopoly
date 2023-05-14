using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameButtonControllers
{
    /// <summary>
    /// Represents the sell button controller.
    /// </summary>
    internal sealed class SellButtonController : ButtonController<SellButtonModel, GUISellButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SellButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the sell button controller.
        /// </param>
        /// <param name="view">
        /// The view of the sell button controller.
        /// </param>
        internal SellButtonController(SellButtonModel model, GUISellButton view)
            : base(model, view) { }
    }
}
