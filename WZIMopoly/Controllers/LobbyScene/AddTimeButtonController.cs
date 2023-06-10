using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents the add time button controller.
    /// </summary>
    internal class AddTimeButtonController : ButtonController<AddTimeButtonModel, GUIAddTimeButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddTimeButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the add time button.
        /// </param>
        /// <param name="view">
        /// The view of the add time button.
        /// </param>
        public AddTimeButtonController(AddTimeButtonModel model, GUIAddTimeButton view)
            : base(model, view)
        {
            OnButtonClicked += () =>
            {
                if (GameSettings.MatchDuration < 60)
                {
                    GameSettings.MatchDuration++;
                }
            };
        }
    }
}
