#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion


namespace WindowsWZIMpoly.GUI
{
    /// <summary>
    /// Base class for creating GUI elements.
    /// </summary>
    internal abstract class GUIElement
    {
        /// <value>
        /// The texture of the GUI element.
        /// </value>
        internal abstract Texture Texture { get; }

        /// <value>
        /// The drawing bounds on screen.
        /// </value>
        internal abstract Rectangle DestinationRect { get; }

        /// <summary>
        /// Loads the content of the GUI element.
        /// </summary>
        /// <param name="content">
        /// The content manager used for loading content.
        /// </param>
        internal abstract void Load(ContentManager content);
    }
}
