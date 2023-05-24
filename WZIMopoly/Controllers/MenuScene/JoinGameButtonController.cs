using WZIMopoly.GUI.MenuScene;
using WZIMopoly.Models.MenuScene;

namespace WZIMopoly.Controllers.MenuScene
{
    /// <summary>
    /// Represents the join game button controller.
    /// </summary>
    internal class JoinGameButtonController : ButtonController<JoinGameButtonModel, GUIJoinGameButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinGameButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the join game button.
        /// </param>
        /// <param name="view">
        /// The view of the join game button.
        /// </param>
        public JoinGameButtonController(JoinGameButtonModel model, GUIJoinGameButton view)
            : base(model, view) { }
    }
}
