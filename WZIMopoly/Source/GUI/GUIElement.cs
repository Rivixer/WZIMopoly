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
        /// <value>
        /// The texture of the GUI element.
        /// </value>
        protected Texture2D texture;
        internal Texture2D Texture { get; }

        /// <value>
        /// The drawing bounds on screen.
        /// </value>
        protected Rectangle destinationRect;
        internal Rectangle DestinationRect=> destinationRect;
        /// <value>
        /// Center point of DestinationRect
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
