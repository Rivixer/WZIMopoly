using WZIMopoly.GUI.MenuScene;
using WZIMopoly.Models.MenuScene;

namespace WZIMopoly.Controllers.MenuScene
{
    /// <summary>
    /// Represents the quit button controller.
    /// </summary>
    internal class QuitButtonController : ButtonController<QuitButtonModel, GUIQuitButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuitButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the quit button.
        /// </param>
        /// <param name="view">
        /// The view of the quit button.
        /// </param>
        public QuitButtonController(QuitButtonModel model, GUIQuitButton view)
            : base(model, view) { }
    }
}
