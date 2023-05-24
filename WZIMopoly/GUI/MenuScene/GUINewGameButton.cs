using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.MenuScene;

namespace WZIMopoly.GUI.MenuScene
{
    /// <summary>
    /// Represents the new game button view.
    /// </summary>
    internal class GUINewGameButton : GUIButton<NewGameButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUINewGameButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the new game button.
        /// </param>
        public GUINewGameButton(NewGameButtonModel model)
            : base(model, new Rectangle(960, 480, 600, 125), GUIStartPoint.Center, disableTexture: false)
        {
            SetButtonHoverArea(5, 0.75f);
        }
    }
}
