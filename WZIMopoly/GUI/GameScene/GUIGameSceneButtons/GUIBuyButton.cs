using Microsoft.Xna.Framework;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the buy button view.
    /// </summary>
    internal sealed class GUIBuyButton : GUIButton<BuyButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIBuyButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the buy button.
        /// </param>
        public GUIBuyButton(BuyButtonModel model)
            : base(model, new Rectangle(1012, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }
    }
}
