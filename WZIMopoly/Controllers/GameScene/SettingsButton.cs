using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a settings button controller.
    /// </summary>
    internal class SettingsButton : ButtonController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the settings button controller.
        /// </param>
        /// <param name="view">
        /// The view of the settings button controller.
        /// </param>
        internal SettingsButton(ButtonModel model, GUIButton view)
            : base(model, view) { }
    }
}
