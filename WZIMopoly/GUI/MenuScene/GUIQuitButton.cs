using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.MenuScene;

namespace WZIMopoly.GUI.MenuScene
{
    /// <summary>
    /// Represents the quit button view.
    /// </summary>
    internal class GUIQuitButton : GUIButton<QuitButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIQuitButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the quit button.
        /// </param>
        public GUIQuitButton(QuitButtonModel model)
            : base(model, new Rectangle(1260, 780, 450, 125), GUIStartPoint.Right)
        {
            SetButtonHoverArea(5, 0.75f);
        }
    }
}
