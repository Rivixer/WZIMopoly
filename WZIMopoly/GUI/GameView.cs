using Microsoft.Xna.Framework;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a game view.
    /// </summary>
    internal class GameView : GUITexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameView"/> class.
        /// </summary>
        public GameView()
            : base("Images/Background", new Rectangle(0, 0, 1920, 1080)) { }
    }
}
