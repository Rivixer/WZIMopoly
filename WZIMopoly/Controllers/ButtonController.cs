using WZIMopoly.Engine;
using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers
{
    /// <summary>
    /// Represents a button controller with 
    /// a <see cref="GUIButton"/> view.
    /// </summary>
    internal abstract class ButtonController : Controller<ButtonModel, GUIButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the button controller.
        /// </param>
        /// <param name="view">
        /// The view of the button controller.
        /// </param>
        protected ButtonController(ButtonModel model, GUIButton view) 
            : base(model, view) { }

        /// <summary>
        /// The delegate used to define the signature of methods 
        /// that can handle the ButtonClick event.
        /// </summary>
        internal delegate void ButtonClickedHandler();

        /// <summary>
        /// The event that is invoked when the button is clicked.
        /// </summary>
        internal event ButtonClickedHandler OnButtonClicked;

        /// <summary>
        /// The method called when the button is clicked.
        /// </summary>
        protected virtual void OnClick()
        {
            Debug.WriteLine($"{Model.Name} button has been clicked");
            (View as ISoundable)?.PlaySound();
            OnButtonClicked?.Invoke();
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

    /// <summary>
    /// Represents a button controller with a specific view type.
    /// </summary>
    internal abstract class ButtonController<V> : ButtonController
        where V : GUIButton
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
        internal ButtonController(ButtonModel model, V view)
            : base(model, view) { }

        /// <summary>
        /// Gets the view of the button.
        /// </summary>
        /// <value>
        /// The specified view of the button.
        /// </value>
        internal new V View => (V)base.View;

        /// <summary>
        /// <inheritdoc/><br/>
        /// Calls <see cref="ButtonController.OnClick"/> method when the button is clicked.
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
