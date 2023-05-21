using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.GUI.LobbyScene
{
    /// <summary>
    /// Represents the start game button view.
    /// </summary>
    internal class GUIStartGameButton : GUIButton<StartGameButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIStartGameButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the start game button.
        /// </param>
        public GUIStartGameButton(StartGameButtonModel model)
            : base(model, new Rectangle(1420, 980, 541, 125), GUIStartPoint.Right)
        {
            SetButtonHoverArea(5, 0.75f);
        }
    }
}
