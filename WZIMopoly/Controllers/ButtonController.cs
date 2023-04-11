using WZIMopoly.Engine;
using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers
{
    /// <summary>
    /// Represents a button controller.
    /// </summary>
    internal abstract class ButtonController : Controller
    {
        /// <summary>
        /// Gets the view of the button controller.
        /// </summary>
        protected new GUIButton View => (GUIButton)base.View;

        /// <summary>
        /// Gets the model of the button controller.
        /// </summary>
        protected new ButtonModel Model => (ButtonModel)base.Model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonController"/> class.
        /// </summary>
        /// <param name="view">
        /// The view of the button controller.
        /// </param>
        /// <param name="model">
        /// The model of the button controller.
        /// </param>
        internal ButtonController(GUIButton view, ButtonModel model)
            : base(view, model, false) { }

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
