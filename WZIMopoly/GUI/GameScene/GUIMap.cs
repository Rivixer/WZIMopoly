using Microsoft.Xna.Framework;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a map view.
    /// </summary>
    internal sealed class GUIMap : GUITexture
    {
        /// <sumary>
        /// Initializes a new instance of the <see cref="GUIMap"/> class.
        /// </summary>
        internal GUIMap()
            : base("Images/Board", new Rectangle(0, 0, 1920, 1080)) { }
    }
}
