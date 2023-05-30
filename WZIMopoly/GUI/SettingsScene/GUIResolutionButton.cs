using Microsoft.Xna.Framework;
using WZIMopoly.Enums;
using WZIMopoly.Models;

namespace WZIMopoly.GUI.SettingsScene
{
    /// <summary>
    /// Represents the resolution settings button view.
    /// </summary>
    internal class GUIResolutionButton : GUIButton
    {
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
        public GUIResolutionButton(ButtonModel model, Rectangle defDstRect, GUIStartPoint startPoint = GUIStartPoint.TopLeft)
            : base(model, defDstRect, startPoint, false, false) { }
    }
}
