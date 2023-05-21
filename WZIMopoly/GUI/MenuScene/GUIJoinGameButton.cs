using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.MenuScene;

namespace WZIMopoly.GUI.MenuScene
{
    /// <summary>
    /// Represents the join game button view.
    /// </summary>
    internal class GUIJoinGameButton : GUIButton<JoinGameButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIJoinGameButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the join game button.
        /// </param>
        public GUIJoinGameButton(JoinGameButtonModel model)
            : base(model, new Rectangle(960, 630, 600, 125), GUIStartPoint.Center)
        {
            SetButtonHoverArea(5, 0.75f);
        }
    }
}
