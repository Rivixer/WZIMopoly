using WZIMopoly.Enums;
using WZIMopoly.GUI.LobbyScene.PlayersList;
using WZIMopoly.Models.LobbyScene.PlayersList;

namespace WZIMopoly.Controllers.LobbyScene.PlayersList
{
    /// <summary>
    /// Represents the controller of the add player button.
    /// </summary>
    internal class AddPlayerButtonController : ButtonController<AddPlayerButtonModel, GUIAddPlayerButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddPlayerButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the add player button.
        /// </param>
        /// <param name="view">
        /// The view of the add player button.
        /// </param>
        public AddPlayerButtonController(AddPlayerButtonModel model, GUIAddPlayerButton view)
            : base(model, view)
        {
            OnButtonClicked += () =>
            {
                Model.Player.PlayerType = PlayerType.Local;
            };
        }
    }
}
