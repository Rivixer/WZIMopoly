using WZIMopoly.Enums;
using WZIMopoly.GUI.LobbyScene.PlayersList;
using WZIMopoly.Models.LobbyScene.PlayersList;

namespace WZIMopoly.Controllers.LobbyScene.PlayersList
{
    /// <summary>
    /// Represents the controller of the remove player button.
    /// </summary>
    internal class RemovePlayerButtonController : ButtonController<RemovePlayerButtonModel, GUIRemovePlayerButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemovePlayerButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the remove player button.
        /// </param>
        /// <param name="view">
        /// The view of the remove player button.
        /// </param>
        public RemovePlayerButtonController(RemovePlayerButtonModel model, GUIRemovePlayerButton view)
            : base(model, view)
        {
            OnButtonClicked += () =>
            {
                Model.Player.PlayerType = PlayerType.None;
            };
        }
    }
}
