using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents the start game button controller.
    /// </summary>
    internal class StartGameButtonController : ButtonController<StartGameButtonModel, GUIStartGameButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartGameButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the start game button.
        /// </param>
        /// <param name="view">
        /// The view of the start game button.
        /// </param>
        public StartGameButtonController(StartGameButtonModel model, GUIStartGameButton view)
            : base(model, view) { }
    }
}
