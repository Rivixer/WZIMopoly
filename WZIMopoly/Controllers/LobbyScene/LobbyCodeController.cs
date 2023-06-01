using WZIMopoly.GUI.LobbyScene;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.Controllers.LobbyScene
{
    /// <summary>
    /// Represents a controller for the lobby code.
    /// </summary>
    internal class LobbyCodeController : Controller<LobbyCodeModel, GUILobbyCode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LobbyCodeController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the lobby code.
        /// </param>
        /// <param name="view">
        /// The view of the lobby code.
        /// </param>
        public LobbyCodeController(LobbyCodeModel model, GUILobbyCode view)
            : base(model, view) { }
    }
}
