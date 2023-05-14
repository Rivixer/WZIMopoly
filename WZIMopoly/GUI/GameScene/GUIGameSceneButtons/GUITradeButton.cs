using Microsoft.Xna.Framework;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the trade button view.
    /// </summary>
    internal sealed class GUITradeButton : GUIButton<TradeButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUITradeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the trade button.
        /// </param>
        internal GUITradeButton(TradeButtonModel model)
            : base(model, new Rectangle(1142, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }
    }
}
