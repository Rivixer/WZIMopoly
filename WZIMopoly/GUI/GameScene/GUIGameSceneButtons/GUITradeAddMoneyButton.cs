using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents a view of the trade add money button.
    /// </summary>
    internal class GUITradeAddMoneyButton : GUIButton<TradeAddMoneyButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUITradeAddMoneyButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the trade add money button.
        /// </param>
        public GUITradeAddMoneyButton(TradeAddMoneyButtonModel model)
            : base(model, new Rectangle(1045, 372, 40, 40), GUIStartPoint.Center, false, false) { }
    }
}
