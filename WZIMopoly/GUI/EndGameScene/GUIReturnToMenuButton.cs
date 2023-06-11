using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.EndGameScene;

namespace WZIMopoly.GUI.EndGameScene
{
    /// <summary>
    /// Rperesents the return to menu button view.
    /// </summary>
    internal class GUIReturnToMenuButton : GUIButton<ReturnToMenuButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIReturnToMenuButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the return to menu button.
        /// </param>
        public GUIReturnToMenuButton(ReturnToMenuButtonModel model)
            : base(model, new Rectangle(960, 1000, 541, 125), GUIStartPoint.Center, disableTexture: false) { }
    }
}
