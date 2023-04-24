using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a GUI element.
    /// </summary>
    public abstract class GUIElement
    {
        /// <summary>
        /// Loads the content of the element.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        internal abstract void Load(ContentManager content);

        /// <summary>
        /// Draws the element.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch object used for rendering.
        /// </param>
        internal abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Scales a GUI element for the current screen resolution.<br/>
        /// </summary>
        internal abstract void Recalculate();
    }
}