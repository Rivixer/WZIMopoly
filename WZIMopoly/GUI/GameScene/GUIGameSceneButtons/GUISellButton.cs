using Microsoft.Xna.Framework;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the sell button view.
    /// </summary>
    internal sealed class GUIUpgradeButton : GUIButton<UpgradeButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIUpgradeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the sell button.
        /// </param>
        internal GUIUpgradeButton(UpgradeButtonModel model)
            : base(model, new Rectangle(752, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }
    }
}
