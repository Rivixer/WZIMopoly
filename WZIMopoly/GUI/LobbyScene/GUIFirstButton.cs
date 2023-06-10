using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.GUI.LobbyScene
{
    /// <summary>
    /// Represents the first bankruptcy button view.
    /// </summary>
    internal class GUIFirstButton : GUIButton<FirstButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIFirstButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the first bankruptcy button.
        /// </param>
        public GUIFirstButton(FirstButtonModel model)
            : base(model, new Rectangle(560, 720, 480, 60), GUIStartPoint.TopLeft, false, false)
        {
            SetButtonHoverArea(5, 0.75f);
        }
    }
}
