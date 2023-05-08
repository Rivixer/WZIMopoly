using System;
using System.Diagnostics;
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
        /// The event that is invoked when the button is clicked.
        /// </summary>
        internal event EventHandler OnButtonClicked;

        /// <summary>
        /// The method called when the button is clicked.
        /// </summary>
        protected virtual void OnClick()
        {
            Debug.WriteLine($"{Model.Name} button has been clicked");
            OnButtonClicked?.Invoke(this, EventArgs.Empty);
        }

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
