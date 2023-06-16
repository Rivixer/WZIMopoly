using WZIMopoly.GUI.MenuScene;
using WZIMopoly.Models.MenuScene;

namespace WZIMopoly.Controllers.MenuScene
{
    /// <summary>
    /// Represents the settings button controller.
    /// </summary>
    internal class MenuSettingsButtonController : ButtonController<MenuSettingsButtonModel, GUIMenuSettingsButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuSettingsButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the settings button.
        /// </param>
        /// <param name="view">
        /// The view of the settings button.
        /// </param>
        public MenuSettingsButtonController(MenuSettingsButtonModel model, GUIMenuSettingsButton view)
            : base(model, view) { }
    }
}
