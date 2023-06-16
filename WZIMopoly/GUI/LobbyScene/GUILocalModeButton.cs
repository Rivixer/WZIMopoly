using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.GUI.LobbyScene
{
    /// <summary>
    /// Represents the local mode button view.
    /// </summary>
    internal class GUILocalModeButton : GUIButton<LocalModeButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUILocalModeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the local mode button.
        /// </param>
        public GUILocalModeButton(LocalModeButtonModel model)
            : base(model, new Rectangle(826, 250, 249, 73), GUIStartPoint.Center, hoverTexture: false, disableTexture: false)
        {
            SetButtonHoverArea(5, 0.85f);
        }
    }
}