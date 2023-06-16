using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Controllers.LobbyScene;
using WZIMopoly.Engine;
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
            Model.InitializeChild<LobbyCodeModel, GUILobbyCode, LobbyCodeController>();
            Model.InitializeChild<LocalModeButtonModel, GUILocalModeButton, LocalModeButtonController>();
            Model.InitializeChild<OnlineModeButtonModel, GUIOnlineModeButton, OnlineModeButtonController>();
            Model.InitializeChild<ReturnButtonModel, GUIReturnButton, ReturnButtonController>();

            static bool CanUseButton()
            {
                return WZIMopoly.GameType == GameType.Local
                    || (WZIMopoly.GameType == GameType.Online && GameSettings.Client.PlayerType == PlayerType.OnlineHostPlayer);
            }

            var firstBankruptcyBtn = Model.InitializeChild<LastNotBankruptButtonModel, GUILastNotBankruptButton, LastNotBankruptButtonController>();
            firstBankruptcyBtn.Model.Conditions += CanUseButton;

            var lastNonBankruptBtn = Model.InitializeChild<FirstBankruptcyButtonModel, GUIFirstBankruptcyButton, FirstBankruptcyButtonController>();
            lastNonBankruptBtn.Model.Conditions += CanUseButton;

            var timeBtn = Model.InitializeChild<TimeButtonModel, GUITimeButton, TimeButtonController>();
            timeBtn.Model.Conditions += CanUseButton;

            var addTimeBtn = Model.InitializeChild<AddTimeButtonModel, GUIAddTimeButton, AddTimeButtonController>();
            addTimeBtn.Model.Conditions += CanUseButton;

            var subTimeBtn = Model.InitializeChild<SubtractTimeButtonModel, GUISubtractTimeButton, SubtractTimeButtonController>();
            subTimeBtn.Model.Conditions += CanUseButton;

            timeBtn.OnButtonClicked += () =>
            {
                if (timeBtn.Model.IsActive)
                {
                    GameSettings.MaxGameTime = null;
                }
                else
                {
                    GameSettings.MaxGameTime = 10;
                }
                GameSettings.SendLobbyData();
            };
        }

        public override void Update()
        {
            base.Update();

#if DEBUG
            // Click F1 to send the lobby data.
            if (KeyboardController.WasClicked(Keys.F1))
            {
                GameSettings.SendLobbyData();
            }
#endif
        }
    }
}
