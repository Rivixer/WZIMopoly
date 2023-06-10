using Microsoft.Xna.Framework;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the mortgage button view.
    /// </summary>
    internal sealed class GUIMortgageButton : GUIButton<MortgageButtonModel>
    internal sealed class GUIMortgageButton : GUIGameButton<MortgageButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIMortgageButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the mortgage button.
        /// </param>
        internal GUIMortgageButton(MortgageButtonModel model)
            : base(model, new Rectangle(622, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }
    }
}
