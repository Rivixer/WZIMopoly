using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models;

namespace WZIMopoly.GUI.SettingsScene
{
    /// <summary>
    /// Represents the return button view.
    /// </summary>
    internal class GUIReturnButton : GUIButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIReturnButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the return button.
        /// </param>
        public GUIReturnButton(ButtonModel model)
            : base(model, new Rectangle(960, 1000, 343, 110), GUIStartPoint.Center, disableTexture: false)
        {
            SetButtonHoverArea(5, 0.75f);
        }
    }
}
