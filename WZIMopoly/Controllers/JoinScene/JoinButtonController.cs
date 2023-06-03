using WZIMopoly.GUI.JoinScene;
using WZIMopoly.Models.JoinScene;

namespace WZIMopoly.Controllers.JoinScene
{
    /// <summary>
    /// Represents the join button controller.
    /// </summary>
    internal class JoinButtonController : ButtonController<JoinButtonModel, GUIJoinButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the join button.
        /// </param>
        /// <param name="view">
        /// The view of the join button.
        /// </param>
        public JoinButtonController(JoinButtonModel model, GUIJoinButton view)
            : base(model, view) { }
    }
}
