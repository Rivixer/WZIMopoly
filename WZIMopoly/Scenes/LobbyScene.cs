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
            Model.InitializeChild<ReturnButtonModel, GUIReturnButton, ReturnButtonController>();
            Model.GetController<ReturnButtonController>().OnButtonClicked += () =>
            {
                Model.GetModel<AddTimeButtonModel>().IsActive = true;
                Model.GetModel<SubtractTimeButtonModel>().IsActive = true;
                Model.GetModel<TimeButtonModel>().IsActive = false;
                GameSettings.gameEndType = GameEndType.LastNotBankrupt;
                GameSettings.startTime = 10;
                Model.GetView<GUITimeButton>().Update();
            };

            Model.InitializeChild<StartGameButtonModel, GUIStartGameButton, StartGameButtonController>();
            Model.InitializeChild<LastButtonModel, GUILastButton, LastButtonController>();
            Model.InitializeChild<FirstButtonModel, GUIFirstButton, FirstButtonController>();
            Model.InitializeChild<LobbyCodeModel, GUILobbyCode, LobbyCodeController>();
            Model.InitializeChild<LocalModeButtonModel, GUILocalModeButton, LocalModeButtonController>();
            Model.InitializeChild<OnlineModeButtonModel, GUIOnlineModeButton, OnlineModeButtonController>();
            Model.InitializeChild<AddTimeButtonModel, GUIAddTimeButton, AddTimeButtonController>();
            Model.GetController<AddTimeButtonController>().OnButtonClicked += () =>
            {
                Model.GetView<GUITimeButton>().Update();
            };

            Model.InitializeChild<SubtractTimeButtonModel, GUISubtractTimeButton, SubtractTimeButtonController>();
            Model.GetController<SubtractTimeButtonController>().OnButtonClicked += () =>
            {
                Model.GetView<GUITimeButton>().Update();
            };

            Model.InitializeChild<TimeButtonModel, GUITimeButton, TimeButtonController>();
            Model.GetController<TimeButtonController>().OnButtonClicked += () =>
            {
                if (Model.GetModel<TimeButtonModel>().IsActive)
                {
                    Model.GetModel<AddTimeButtonModel>().IsActive = true;
                    Model.GetModel<SubtractTimeButtonModel>().IsActive = true;
                    Model.GetModel<TimeButtonModel>().IsActive = false;
                    GameSettings.startTime = 10;
                    Model.GetView<GUITimeButton>().Update();
                }
                else
                {
                    Model.GetModel<AddTimeButtonModel>().IsActive = false;
                    Model.GetModel<SubtractTimeButtonModel>().IsActive = false;
                    Model.GetModel<TimeButtonModel>().IsActive = true;
                    GameSettings.startTime = 0;
                }
            };
        }
    }
}
