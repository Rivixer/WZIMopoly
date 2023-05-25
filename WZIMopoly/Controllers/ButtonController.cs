using WZIMopoly.Engine;
using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers
{
    /// <summary>
    /// Represents a button controller with 
    /// a <see cref="ButtonModel"/> model and
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
        public delegate void ButtonClickedHandler();

        /// <summary>
        /// The event that is invoked when the button is clicked.
        /// </summary>
        public event ButtonClickedHandler OnButtonClicked;

        /// <summary>
        /// The method called when the button is clicked.
        /// </summary>
        protected virtual void OnClick()
        {
            Debug.WriteLine($"{Model.Name} button has been clicked");
            (View as ISoundable)?.PlaySound();
            OnButtonClicked?.Invoke();
            Model.WasClickedInThisFrame = true;
        }

        /// <summary>
        /// <inheritdoc/><br/>
        /// Calls <see cref="OnClick"/> method when the button is clicked.
        /// </summary>
        public override void Update()
        {
            var canBeClicked = 
                Model.IsActive
                && Model.Conditions()
                && MouseController.WasLeftBtnClicked()
                && View.IsHovered;

            if (canBeClicked)
            {
                OnClick();
            }
        }
    }

    /// <summary>
    /// Represents a button controller with specific view and model types.
    /// </summary>
    internal abstract class ButtonController<M, V> : ButtonController
        where M : ButtonModel
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
        public ButtonController(M model, V view)
            : base(model, view) { }

        /// <summary>
        /// Gets the model of the button.
        /// </summary>
        /// <value>
        /// The specified model of the button.
        /// </value>
        public new M Model => (M)base.Model;

        /// <summary>
        /// Gets the view of the button.
        /// </summary>
        /// <value>
        /// The specified view of the button.
        /// </value>
        public new V View => (V)base.View;
    }
}
