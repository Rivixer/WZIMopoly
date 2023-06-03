using System.Linq;
using WZIMopoly.Controllers.LobbyScene.PlayersList;
using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.GUI.LobbyScene.PlayersList;
using WZIMopoly.Models;
using WZIMopoly.Models.LobbyScene;
using WZIMopoly.Models.LobbyScene.PlayersList;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents the controller of the players list in the lobby.
    /// </summary>
    internal class LobbyPlayersController : Controller<LobbyPlayersModel, GUILobbyPlayers>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LobbyPlayersController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the players list.
        /// </param>
        /// <param name="view">
        /// The view of the players list.
        /// </param>
        public LobbyPlayersController(LobbyPlayersModel model, GUILobbyPlayers view)
            : base(model, view)
        {
            foreach (PlayerModel player in GameSettings.Players)
            {
                Model.InitializeChild<LobbyPlayerModel, GUILobbyPlayer, LobbyPlayerController>(player);
            }
            Model.InitializeChild<HostLabelModel, GUIHostLabel, HostLabelController>();
        }

        /// <summary>
        /// Resets the players list.
        /// </summary>
        /// <remarks>
        /// Creates a new players list based on <see cref="GameSettings.Players"/>.
        /// </remarks>
        public void Reset()
        {
            var lobbyPlayerControllers = Model.GetAllControllers<LobbyPlayerController>();
            foreach (var (ctrl, player) in lobbyPlayerControllers.Zip(GameSettings.Players))
            {
                ctrl.UpdatePlayer(player);
            }
        }
    }
}
