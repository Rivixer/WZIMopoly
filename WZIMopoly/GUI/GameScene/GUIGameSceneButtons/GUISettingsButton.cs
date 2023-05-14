using Microsoft.Xna.Framework;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents a settings button view.
    /// </summary>
    internal sealed class GUISettingsButton : GUIButton<SettingsButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUISettingsButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the settings button.
        /// </param>
        internal GUISettingsButton(SettingsButtonModel model)
            : base(model, new Rectangle(60, 200, 160, 160))
        {
            SetButtonHoverArea(5, 0.7f);
        }
    }
}
