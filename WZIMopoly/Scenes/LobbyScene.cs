using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Controllers.LobbyScene;
using WZIMopoly.Enums;
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
            Model.InitializeChild<StartGameButtonModel, GUIStartGameButton, StartGameButtonController>();
            Model.InitializeChild<LastNotBankruptButtonModel, GUILastNotBankruptButton, LastNotBankruptButtonController>();
            Model.InitializeChild<FirstBankruptcyButtonModel, GUIFirstBankruptcyButton, FirstBankruptcyButtonController>();
            Model.InitializeChild<LobbyCodeModel, GUILobbyCode, LobbyCodeController>();
            Model.InitializeChild<LocalModeButtonModel, GUILocalModeButton, LocalModeButtonController>();
            Model.InitializeChild<OnlineModeButtonModel, GUIOnlineModeButton, OnlineModeButtonController>();
            Model.InitializeChild<ReturnButtonModel, GUIReturnButton, ReturnButtonController>();

            var timeBtn = Model.InitializeChild<TimeButtonModel, GUITimeButton, TimeButtonController>();
            var addTimeBtn = Model.InitializeChild<AddTimeButtonModel, GUIAddTimeButton, AddTimeButtonController>();
            var subTimeBtn = Model.InitializeChild<SubtractTimeButtonModel, GUISubtractTimeButton, SubtractTimeButtonController>();
            timeBtn.OnButtonClicked += () =>
            {
                if (timeBtn.Model.IsActive)
                {
                    GameSettings.MaxGameTime = null;
                    timeBtn.Model.IsActive = false;
                }
                else
                {
                    GameSettings.MaxGameTime = 10;
                    timeBtn.Model.IsActive = true;
                }
                addTimeBtn.Model.IsActive = timeBtn.Model.IsActive;
                subTimeBtn.Model.IsActive = timeBtn.Model.IsActive;
            };
        }
    }
}
