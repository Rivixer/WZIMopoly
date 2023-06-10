using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents a view of the trade subtract money button.
    /// </summary>
    internal class GUITradeSubtractMoneyButton : GUIButton<TradeSubtractMoneyButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUITradeSubtractMoneyButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the trade subtract money button.
        /// </param>
        public GUITradeSubtractMoneyButton(TradeSubtractMoneyButtonModel model)
            : base(model, new Rectangle(875, 372, 40, 40), GUIStartPoint.Center, false, false) { }
    }
}
