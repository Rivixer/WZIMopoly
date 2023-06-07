using WZIMopoly.Engine;
using WZIMopoly.GUI.SettingsScene;
using WZIMopoly.Models.SettingsScene;

namespace WZIMopoly.Controllers.SettingsScene
{
    /// <summary>
    /// Represents the resolution settings button controller.
    /// </summary>
    internal class VolumeSliderController : ButtonController<VolumeSliderModel, GUIVolumeSlider>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the resolution button.
        /// </param>
        /// <param name="view">
        /// The view of the resolution button.
        /// </param>
        public VolumeSliderController(VolumeSliderModel model, GUIVolumeSlider view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();

            // We have to override this, because we want to make
            // this button clickable even if it is inactive.

            if (Model.Conditions()
                && (MouseController.WasLeftBtnClicked()
                || MouseController.IsLeftBtnPressed()))
            {
                OnClick();
            }
        }
    }
}
