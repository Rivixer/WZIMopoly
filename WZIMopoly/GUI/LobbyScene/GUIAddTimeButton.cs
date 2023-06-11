using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.GUI.LobbyScene
{
    /// <summary>
    /// Represents the add time button view.
    /// </summary>
    internal class GUIAddTimeButton : GUIButton<AddTimeButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIAddTimeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the add time button.
        /// </param>
        public GUIAddTimeButton(AddTimeButtonModel model)
            : base(model, new Rectangle(1282, 734, 50, 50), GUIStartPoint.Left, false, false) { }
    }
}
