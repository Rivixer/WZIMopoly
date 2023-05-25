using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameSceneButtonControllers
{
    /// <summary>
    /// Represents the settings button controller.
    /// </summary>
    internal sealed class SettingsButtonController : ButtonController<SettingsButtonModel, GUISettingsButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the settings button controller.
        /// </param>
        /// <param name="view">
        /// The view of the settings button controller.
        /// </param>
        public SettingsButtonController(SettingsButtonModel model, GUISettingsButton view)
            : base(model, view) { }
    }
}
