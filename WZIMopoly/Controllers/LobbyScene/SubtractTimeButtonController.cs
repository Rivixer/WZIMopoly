using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents the subtract time button controller.
    /// </summary>
    internal class SubtractTimeButtonController : ButtonController<SubtractTimeButtonModel, GUISubtractTimeButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractTimeButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the subtract time button.
        /// </param>
        /// <param name="view">
        /// The view of the subtract time button.
        /// </param>
        public SubtractTimeButtonController(SubtractTimeButtonModel model, GUISubtractTimeButton view)
            : base(model, view)
        {
            OnButtonClicked += () =>
            {
                if(GameSettings.MatchDuration > 1)
                { 
                    GameSettings.MatchDuration--;
                }
            };
        }
    }
}
