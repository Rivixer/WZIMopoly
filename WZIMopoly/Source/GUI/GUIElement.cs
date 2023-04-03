#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion


namespace WZIMopoly.GUI
{
    /// <summary>
    /// Base class for creating GUI elements.
    /// </summary>
    internal abstract class GUIElement
    {
        protected Texture2D texture;
        protected Rectangle destinationRect;

        /// <value>
        /// The texture of the GUI element.
        /// </value>
        internal Texture2D Texture => texture;

        /// <value>
        /// The drawing bounds on screen.
        /// DestinationRect scales depending on current Main Screen's resolution.
        /// </value>
        internal Rectangle DestinationRect => new(
            destinationRect.X * MainScreen.Width / 1920,
            destinationRect.Y * MainScreen.Height / 1080,
            destinationRect.Width * MainScreen.Width / 1920,
            destinationRect.Height * MainScreen.Height / 1080
        );

        /// <value>
        /// The center position of the GUI element.
        /// </value>
        internal Point CenterPosition => DestinationRect.Center;


        /// <summary>
        /// Loads the content of the GUI element.
        /// </summary>
        /// <param name="content">
        /// The content manager used for loading content.
        /// </param>
        internal abstract void Load(ContentManager content);
    }
}
