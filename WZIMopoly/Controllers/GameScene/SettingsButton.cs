using System.Diagnostics;
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
        /// <param name="view">
        /// The view of the settings button controller.
        /// </param>
        /// <param name="model">
        /// The model of the settings button controller.
        /// </param>
        internal SettingsButton(GUIButton view, ButtonModel model)
            : base(view, model) { }

        /// <inheritdoc/>
        protected override void OnClick()
        {
            Debug.WriteLine($"{Model.Name} has been clicked.");
        }
    }
}
