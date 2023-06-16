using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.JoinScene;

namespace WZIMopoly.GUI.JoinScene
{
    /// <summary>
    /// Represents the return button view.
    /// </summary>
    internal class GUIReturnButton : GUIButton<ReturnButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIReturnButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the return button.
        /// </param>
        public GUIReturnButton(ReturnButtonModel model)
            : base(model, new Rectangle(500, 980, 380, 125), GUIStartPoint.Left, disableTexture: false)
        {
            SetButtonHoverArea(5, 0.75f);
        }
    }
}
