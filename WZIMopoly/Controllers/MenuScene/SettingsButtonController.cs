using WZIMopoly.GUI.MenuScene;
using WZIMopoly.Models.MenuScene;

namespace WZIMopoly.Controllers.MenuScene
{
    /// <summary>
    /// Represents the settings button controller.
    /// </summary>
    internal class SettingsButtonController : ButtonController<SettingsButtonModel, GUISettingsButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the settings button.
        /// </param>
        /// <param name="view">
        /// The view of the settings button.
        /// </param>
        public SettingsButtonController(SettingsButtonModel model, GUISettingsButton view)
            : base(model, view) { }
    }
}
