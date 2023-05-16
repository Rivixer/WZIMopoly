using Microsoft.Xna.Framework;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the end turn button view.
    /// </summary>
    internal class GUIEndTurnButton : GUIButton<EndTurnButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIEndTurnButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the end turn button.
        /// </param>
        internal GUIEndTurnButton(EndTurnButtonModel model)
            : base(model, new Rectangle(882, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }
    }
}
