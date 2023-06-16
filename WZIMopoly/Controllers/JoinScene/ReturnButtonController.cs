using WZIMopoly.GUI.JoinScene;
using WZIMopoly.Models.JoinScene;

namespace WZIMopoly.Controllers.JoinScene
{
    /// <summary>
    /// Represents the return button controller.
    /// </summary>
    internal class ReturnButtonController : ButtonController<ReturnButtonModel, GUIReturnButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the return button.
        /// </param>
        /// <param name="view">
        /// The view of the return button.
        /// </param>
        public ReturnButtonController(ReturnButtonModel model, GUIReturnButton view)
            : base(model, view) { }
    }
}
