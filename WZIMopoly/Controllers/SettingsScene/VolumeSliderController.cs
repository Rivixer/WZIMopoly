using WZIMopoly.Engine;
using WZIMopoly.GUI.SettingsScene;
using WZIMopoly.Models.SettingsScene;
using WZIMopoly.Utils.PositionExtensions;

namespace WZIMopoly.Controllers.SettingsScene
{
    /// <summary>
    /// Represents the volume slider controller.
    /// </summary>
    internal class VolumeSliderController : ButtonController<VolumeSliderModel, GUIVolumeSlider>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VolumeSliderController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the volume slider.
        /// </param>
        /// <param name="view">
        /// The view of the volume slider.
        /// </param>
        public VolumeSliderController(VolumeSliderModel model, GUIVolumeSlider view)
            : base(model, view) { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();

            if (MouseController.WasLeftBtnClicked()
                && MouseController.IsHover(View.RectangleSlider.ToCurrentResolution()))
            {
                OnButtonClicked += View.MoveSlider;
            }
            if(MouseController.IsLeftBtnPressed())
            {
                OnClick();
            }
            else if (MouseController.WasLeftBtnReleased())
            {
                OnButtonClicked -= View.MoveSlider;
            }
        }
    }
}
