using WZIMopoly.GUI.LobbyScene.PlayersList;
using WZIMopoly.Models.LobbyScene.PlayersList;

namespace WZIMopoly.Controllers.LobbyScene.PlayersList
{
    /// <summary>
    /// Represents a controller for the host label.
    /// </summary>
    internal class HostLabelController : Controller<HostLabelModel, GUIHostLabel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostLabelController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the host label.
        /// </param>
        /// <param name="view">
        /// The view of the host label.
        /// </param>
        public HostLabelController(HostLabelModel model, GUIHostLabel view)
            : base(model, view) { }
    }
}
