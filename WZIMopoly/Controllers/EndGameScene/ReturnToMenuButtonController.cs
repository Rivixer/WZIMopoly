using WZIMopoly.GUI.EndGameScene;
using WZIMopoly.Models.EndGameScene;

namespace WZIMopoly.Controllers.EndGameScene
{
    /// <summary>
    /// Represents the return to menu button controller.
    /// </summary>
    internal class ReturnToMenuButtonController : ButtonController<ReturnToMenuButtonModel, GUIReturnToMenuButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnToMenuButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the return to menu button.
        /// </param>
        /// <param name="view">
        /// The view of the return to menu button.
        /// </param>
        public ReturnToMenuButtonController(ReturnToMenuButtonModel model, GUIReturnToMenuButton view)
            : base(model, view) { }
    }
}
