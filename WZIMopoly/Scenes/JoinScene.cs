using WZIMopoly.Controllers.JoinScene;
using WZIMopoly.GUI;
using WZIMopoly.GUI.JoinScene;
using WZIMopoly.Models;
using WZIMopoly.Models.JoinScene;

namespace WZIMopoly.Scenes
{
    /// <summary>
    /// Represents the join scene.
    /// </summary>
    internal class JoinScene : Scene<JoinModel, JoinView>
    {
        /// <summary>
        /// The player nick controller.
        /// </summary>
        private PlayerNickController _playerNickController;

        /// <summary>
        /// The lobby code controller.
        /// </summary>
        private LobbyCodeController _lobbyCodeController;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinScene"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the join scene.
        /// </param>
        /// <param name="view">
        /// The view of the join scene.
        /// </param>
        public JoinScene(JoinModel model, JoinView view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Initialize()
        {
            _playerNickController = Model.InitializeChild<PlayerNickModel, GUIPlayerNick, PlayerNickController>();
            _lobbyCodeController = Model.InitializeChild<LobbyCodeModel, GUILobbyCode, LobbyCodeController>();

            Model.InitializeChild<ReturnButtonModel, GUIReturnButton, ReturnButtonController>();
            Model.InitializeChild<JoinButtonModel, GUIJoinButton, JoinButtonController>();
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            var joinButton = Model.GetModel<JoinButtonModel>();
            joinButton.UpdateActivity(_lobbyCodeController.Model, _playerNickController.Model);
        }
    }
}
