using WZIMopoly.GUI.MenuScene;
using WZIMopoly.Models.MenuScene;

namespace WZIMopoly.Controllers.MenuScene
{
    /// <summary>
    /// Represents the new game button controller.
    /// </summary>
    internal class NewGameButtonController : ButtonController<NewGameButtonModel, GUINewGameButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewGameButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the new game button.
        /// </param>
        /// <param name="view">
        /// The view of the new game button.
        /// </param>
        public NewGameButtonController(NewGameButtonModel model, GUINewGameButton view)
            : base(model, view) { }
    }
}
