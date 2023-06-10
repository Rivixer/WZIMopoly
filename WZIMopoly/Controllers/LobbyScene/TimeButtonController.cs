using Microsoft.Xna.Framework;
using WZIMopoly.Engine;
using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models.LobbyScene;
using WZIMopoly.Utils.PositionExtensions;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents the time button controller.
    /// </summary>
    internal class TimeButtonController : ButtonController<TimeButtonModel, GUITimeButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the time button.
        /// </param>
        /// <param name="view">
        /// The view of the time button.
        /// </param>
        public TimeButtonController(TimeButtonModel model, GUITimeButton view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Update()
        {
            Model.Update();

            if (Model.Conditions()
                && MouseController.WasLeftBtnClicked()
                && MouseController.IsHover(new Rectangle(1050, 640, 304, 60).ToCurrentResolution()))
            {
                OnClick();
            }
        }
    }
}
