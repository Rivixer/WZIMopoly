using WZIMopoly.Controllers.LobbyScene;
using WZIMopoly.GUI;
using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.Scenes
{
    /// <summary>
    /// Represents the lobby scene.
    /// </summary>
    internal class LobbyScene : Scene<LobbyModel, LobbyView>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LobbyScene"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the lobby.
        /// </param>
        /// <param name="view">
        /// The view of the lobby.
        /// </param>
        public LobbyScene(LobbyModel model, LobbyView view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Initialize()
        {
            Model.InitializeChild<LobbyPlayersModel, GUILobbyPlayers, LobbyPlayersController>();
            Model.InitializeChild<ReturnButtonModel, GUIReturnButton, ReturnButtonController>();
            Model.InitializeChild<StartGameButtonModel, GUIStartGameButton, StartGameButtonController>();
            Model.InitializeChild<LocalModeButtonModel, GUILocalModeButton, LocalModeButtonController>();
            // Online mode button
            var onlineButton = Model.InitializeChild<OnlineModeButtonModel, GUIOnlineModeButton, OnlineModeButtonController>();
            onlineButton.OnButtonClicked += () =>
            {
                if (WZIMopoly.Network != null)
                {
                    WZIMopoly.Network.Send(new byte[] { (byte)WZIMopolyNetworkingLibrary.PacketType.NewLobby });
                    WZIMopoly.Network.OnMessage += (sender, e) =>
                    {
                        var code = System.Text.Encoding.ASCII.GetString(e.RawData);
                        lobbyCode.Model.Code = code;
                        NetworkService.SwitchToLobby(code);
                    };
                }
                
                for (int i = 1; i <= 3; i++)
                {
                    GameSettings.Players[i].PlayerType = PlayerType.None;
                    GameSettings.Players[i].ResetNick();
                };
            };
        }
    }
}
