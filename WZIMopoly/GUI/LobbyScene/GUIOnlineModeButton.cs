using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.GUI.LobbyScene
{
    /// <summary>
    /// Represents the online mode button view.
    /// </summary>
    internal class GUIOnlineModeButton : GUIButton<OnlineModeButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIOnlineModeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the online mode button.
        /// </param>
        public GUIOnlineModeButton(OnlineModeButtonModel model)
            : base(model, new Rectangle(1093, 269, 249, 73), GUIStartPoint.Center, hoverTexture: false, disableTexture: false)
        {
            SetButtonHoverArea(5, 0.85f);
        }
    }
}