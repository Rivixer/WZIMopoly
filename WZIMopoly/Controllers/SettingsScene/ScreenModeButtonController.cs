using WZIMopoly.Engine;
using WZIMopoly.GUI.SettingsScene;
using WZIMopoly.Models.SettingsScene;

namespace WZIMopoly.Controllers.SettingsScene
{
    internal class ScreenModeButtonController : ButtonController<ScreenModeButtonModel, GUIScreenModeButton>
    {
        /// <summary>
        /// Represents the screen mode button controller.
        /// </summary>
        /// <param name="model">
        /// The model of the screen mode button.
        /// </param>
        /// <param name="view">
        /// The view of the screen mode button.
        /// </param>
        public ScreenModeButtonController(ScreenModeButtonModel model, GUIScreenModeButton view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            
            // We have to override this, because we want to make
            // this button clickable even if it is inactive.

            if (Model.Conditions()
                && MouseController.WasLeftBtnClicked()
                && View.IsHovered)
            {
                OnClick();
            }
        }
    }
}
