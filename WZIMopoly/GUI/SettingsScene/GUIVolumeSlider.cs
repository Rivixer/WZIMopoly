using Microsoft.Xna.Framework;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Utils.PositionExtensions;

namespace WZIMopoly.GUI.SettingsScene
{
    /// <summary>
    /// Represents the resolution settings button view.
    /// </summary>
    internal class GUIVolumeSlider : GUIButton
    {
        internal Rectangle rectangleSlider1
        {
            get
            {
                return Texture.UnscaledDestinationRect;
            }
        }
        internal Rectangle rectangleSlider2
        {
            get
            {
                return TextureDisabled.UnscaledDestinationRect;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIResolutionButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the resolution settings button.
        /// </param>
        /// <param name="defDstRect">
        /// The default destination rectangle of the resolution settings button.
        /// </param>
        /// <param name="startPoint">
        /// The place where <paramref name="defDstRect"/> has been specified.
        /// </param>
        public GUIVolumeSlider(ButtonModel model, Rectangle defDstRect, GUIStartPoint startPoint = GUIStartPoint.TopLeft)
            : base(model, defDstRect, startPoint, false, true) { }

        public void SetNewArea(int howMany)
        {
            Rectangle rectangle = Texture.UnscaledDestinationRect;
            rectangle.X = howMany * ScreenController.Width / 1920;
            rectangle.ToCurrentResolution();
            //rectangle.ToCurrentResolution();
            //Texture.SetNewDefDstRectangle(rectangle);
            TextureDisabled.SetNewDefDstRectangle(rectangle);
        }
    }
}
