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
        /// Updates the element before the main update loop.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame
        /// and before <see cref="Update"/> method.
        /// </remarks>
        void BeforeUpdate();

        /// <summary>
        /// Updates the element.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame.
        /// </remarks>
        void Update();

        /// <summary>
        /// Updates the element after the main update loop.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame
        /// and after <see cref="Update"/> method.
        /// </remarks>
        void AfterUpdate();

        /// <summary>
        /// Loads the content of the element.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        void Load(ContentManager content);

        /// <summary>
        /// Draws the element.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch object used for rendering.
        /// </param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Scales a GUI element for the current screen resolution.
        /// </summary>
        void Recalculate();
    }
}
