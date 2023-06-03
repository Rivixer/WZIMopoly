using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.JoinScene;

namespace WZIMopoly.GUI.JoinScene
{
    /// <summary>
    /// Represents the return button view.
    /// </summary>
    internal class GUIJoinButton : GUIButton<JoinButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIReturnButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the join button.
        /// </param>
        public GUIJoinButton(JoinButtonModel model)
            : base(model, new Rectangle(1420, 980, 561, 125), GUIStartPoint.Right)
        {
            SetButtonHoverArea(5, 0.75f);
        }
    }
}
