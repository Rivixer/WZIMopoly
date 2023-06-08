using Microsoft.Xna.Framework;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the sell button view.
    /// </summary>
    internal sealed class GUISellButton : GUIButton<SellButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUISellButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the sell button.
        /// </param>
        internal GUISellButton(SellButtonModel model)
            : base(model, new Rectangle(1142, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }
    }
}
