using WZIMopoly.Engine;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameSceneButtonControllers
{
    /// <summary>
    /// Represents the exit button controller.
    /// </summary>
    internal class ExitButtonController : ButtonController<ExitButtonModel, GUIExitButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExitButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the exit button.
        /// </param>
        /// <param name="view">
        /// The view of the exit button.
        /// </param>
        public ExitButtonController(ExitButtonModel model, GUIExitButton view)
            : base(model, view) { }

        /// <summary>
        /// <inheritdoc cref="Controller{_M, _V}.Update"/><br/>
        /// </summary>
        /// <remarks>
        /// Calls <see cref="ButtonController.OnClick"/> method when the button 
        /// was clicked a second time while it is hovered.
        /// </remarks>
        public override void Update()
        {
            Model.Update();
            View.Update();

            var wasClicked =
                Model.IsActive
                && Model.Conditions()
                && MouseController.WasLeftBtnClicked()
                && View.IsHovered;

            if (wasClicked)
            {
                if (View.WasClickedOnce)
                    OnClick();
                else
                    View.WasClickedOnce = true;
            }

            if (!View.IsHovered)
                View.WasClickedOnce = false;
        }
    }
}
