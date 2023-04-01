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
        /// </value>
        internal Rectangle DestinationRect => destinationRect;
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
