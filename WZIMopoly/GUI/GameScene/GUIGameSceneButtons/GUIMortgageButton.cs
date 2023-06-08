using Microsoft.Xna.Framework;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the unmortgage button view.
    /// </summary>
    internal sealed class GUIUnmortgageButton : GUIButton<UnmortgageButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIUnmortgageButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the unmortgage button.
        /// </param>
        internal GUIUnmortgageButton(UnmortgageButtonModel model)
            : base(model, new Rectangle(492, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }
    }
}
