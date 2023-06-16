using WZIMopoly.Enums;
using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents the return button controller.
    /// </summary>
    internal class ReturnButtonController : ButtonController<ReturnButtonModel, GUIReturnButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the return button.
        /// </param>
        /// <param name="view">
        /// The view of the return button.
        /// </param>
        public ReturnButtonController(ReturnButtonModel model, GUIReturnButton view)
            : base(model, view)
        {
            OnButtonClicked += () =>
            {
                GameSettings.Players[0].ResetNick();
                for (int i = 1; i <= 3; i++)
                {
                    GameSettings.Players[i].PlayerType = PlayerType.None;
                    GameSettings.Players[i].ResetNick();
                }
            };
        }
    }
}
