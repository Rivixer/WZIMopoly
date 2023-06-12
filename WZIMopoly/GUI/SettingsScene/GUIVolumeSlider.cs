using Microsoft.Xna.Framework;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models;

namespace WZIMopoly.GUI.SettingsScene
{
    /// <summary>
    /// Represents the volume slider view.
    /// </summary>
    internal class GUIVolumeSlider : GUIButton
    {
        /// <summary>
        /// The delegate used to define the signature of methods 
        /// that can handle the SliderVolume event.
        /// </summary>
        /// <param name="volume">
        /// The volume of the element.
        /// </param>
        public delegate void SliderVolumeHandler(float volume);

        /// <summary>
        /// The event that is invoked when the slider is holding.
        /// </summary>
        public event SliderVolumeHandler OnSliderVolume;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIVolumeSlider"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the volume slider.
        /// </param>
        /// <param name="defDstRect">
        /// The default destination rectangle of the slider volume.
        /// </param>
        /// <param name="startPoint">
        /// The place where <paramref name="defDstRect"/> has been specified.
        /// </param>
        public GUIVolumeSlider(ButtonModel model, Rectangle defDstRect, GUIStartPoint startPoint = GUIStartPoint.TopLeft)
            : base(model, defDstRect, startPoint, false, false)
        {        }

        /// <summary>
        /// Gets the texture rectangle of the slider.
        /// </summary>
        public Rectangle RectangleSlider
        {
            get => Texture.UnscaledDestinationRect;
        }

        /// <summary>
        /// Change position of slider.
        /// Calls <see cref="OnSliderVolume"/> to change volume.
        /// </summary>
        public void MoveSlider()
        {
            var mouseX = MouseController.Position.X;
            var sliderMin = 1024 * ScreenController.Width / 1920;
            var sliderMax = 1330 * ScreenController.Width / 1920;
            if (sliderMin <= mouseX && mouseX <= sliderMax)
            {
                Rectangle rectangle = Texture.UnscaledDestinationRect;
                rectangle.X = mouseX * 1920 / ScreenController.Width;
                Texture.SetNewDefDstRectangle(rectangle, GUIStartPoint.Top);
                OnSliderVolume((float)(mouseX - sliderMin) / (sliderMax - sliderMin));
            }
        }

        public void SetVolume(float volume)
        {
            var sliderMin = 1024 * ScreenController.Width / 1920;
            var sliderMax = 1330 * ScreenController.Width / 1920;
            Rectangle rectangle = Texture.UnscaledDestinationRect;
            float cos = (sliderMax - sliderMin) * volume + sliderMin;
            rectangle.X = (int)cos * 1920 / ScreenController.Width;
            Texture.SetNewDefDstRectangle(rectangle, GUIStartPoint.Top);
            OnSliderVolume(volume);
        }
    }
}
