using WZIMopoly.Engine;
using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers
{
    /// <summary>
    /// Represents a button controller.
    /// </summary>
    internal abstract class ButtonController : Controller<ButtonModel, GUIButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonController"/> class.
        /// </summary>
        /// <param name="view">
        /// The view of the button controller.
        /// </param>
        /// <param name="model">
        /// The model of the button controller.
        /// </param>
        internal ButtonController(ButtonModel model, GUIButton view)
            : base(model, view) { }

        /// <summary>
        /// The method called when the button is clicked.
        /// </summary>
        protected abstract void OnClick();

        /// <summary>
        /// <inheritdoc/><br/>
        /// Calls <see cref="OnClick"/> method when the button is clicked.
        /// </summary>
        protected override void Update()
        {
            if (Model.IsActive && MouseController.WasLeftBtnClicked() && View.IsHovered)
            {
                OnClick();
            }
        }
    }
}
