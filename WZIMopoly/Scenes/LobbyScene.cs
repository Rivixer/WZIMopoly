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
            Model.InitializeChild<ReturnButtonModel, GUIReturnButton, ReturnButtonController>();
            Model.InitializeChild<StartGameButtonModel, GUIStartGameButton, StartGameButtonController>();
            Model.InitializeChild<LocalModeButtonModel, GUILocalModeButton, LocalModeButtonController>();
        }
    }
}
