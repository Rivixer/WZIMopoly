using Microsoft.Xna.Framework;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a map view.
    /// </summary>
    internal sealed class MapView : GUITexture
    {
        /// <sumary>
        /// Initializes a new instance of the <see cref="MapView"/> class.
        /// </summary>
        internal MapView()
            : base("Images/Board", new Rectangle(0, 0, 1920, 1080)) { }
    }
}
