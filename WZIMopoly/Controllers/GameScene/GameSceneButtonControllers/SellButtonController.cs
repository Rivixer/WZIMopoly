using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameButtonControllers
{
    /// <summary>
    /// Represents the sell button controller.
    /// </summary>
    internal sealed class UpgradeButtonController : ButtonController<UpgradeButtonModel, GUIUpgradeButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the sell button controller.
        /// </param>
        /// <param name="view">
        /// The view of the sell button controller.
        /// </param>
        public UpgradeButtonController(UpgradeButtonModel model, GUIUpgradeButton view)
            : base(model, view) { }
    }
}
