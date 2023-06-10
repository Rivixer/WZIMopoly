using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.GUI.LobbyScene
{
    /// <summary>
    /// Represents the last not bankrupt button view.
    /// </summary>
    internal class GUILastButton : GUIButton<LastButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUILastButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the last not bankrupt button.
        /// </param>
        public GUILastButton(LastButtonModel model)
            : base(model, new Rectangle(560, 640, 480, 60), GUIStartPoint.TopLeft, false, false)
        {
            SetButtonHoverArea(5, 0.75f);
        }
    }
}
