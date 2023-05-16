using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.Controllers
{
    /// <summary>
    /// Allows a class to be a primary controller.
    /// </summary>
    internal interface IPrimaryController
    {
        /// <summary>
        /// Loads the content of the controller and all its children.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        void LoadAll(ContentManager content);

        /// <summary>
        /// Draws the view of the controller and all its children.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch object used for rendering.
        /// </param>
        void DrawAll(SpriteBatch spriteBatch);

        /// <summary>
        /// Updates the controller and all its children
        /// before the main update loop.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame,
        /// before the <see cref="UpdateAll"/> method.
        /// </remarks>
        void BeforeUpdateAll();

        /// <summary>
        /// Updates the controller and all its children.
        /// </summary>
        void UpdateAll();

        /// <summary>
        /// Updates the controller and all its children
        /// after the main update loop.
        /// </summary>
        /// <remarks>
        /// This method is called once per frame,
        /// after the <see cref="UpdateAll"/> method.
        /// </remarks>
        void AfterUpdateAll();

        /// <summary>
        /// Recalculates the view of the controller and all its children.
        /// </summary>
        void RecalculateAll();
    }
}
