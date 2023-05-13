using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Provides methods for a GUI element.
    /// </summary>
    internal interface IGUIable
    {
        /// <summary>
        /// Updates the element.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame.
        /// </remarks>
        abstract void Update();

        /// <summary>
        /// Loads the content of the element.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        abstract void Load(ContentManager content);

        /// <summary>
        /// Draws the element.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch object used for rendering.
        /// </param>
        abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Scales a GUI element for the current screen resolution.
        /// </summary>
        abstract void Recalculate();
    }
}
