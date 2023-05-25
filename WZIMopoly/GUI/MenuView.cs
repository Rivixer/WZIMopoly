using Microsoft.Xna.Framework;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a menu view.
    /// </summary>
    internal class MenuView : GUITexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuView"/> class.
        /// </summary>
        public MenuView()
            : base("Images/MenuScreen", new Rectangle(0, 0, 1920, 1080)) { }
    }
}
