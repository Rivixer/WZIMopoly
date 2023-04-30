using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace WZIMopoly.Controllers
{
    /// <summary>
    /// Provides an interface for a controller that can be controlled by another controller.
    /// </summary>
    internal interface IControllerable
    {
        /// <summary>
        /// The list of child controllers.
        /// </summary>
        internal List<IControllerable> Children { get; }

        /// <summary>
        /// Loads the content for the controller.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        internal void Load(ContentManager content);

        /// <summary>
        /// Draws the view of this controller.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch used for drawing.
        /// </param>
        internal void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Updates the controller.
        /// </summary>
        internal void Update();

        /// <summary>
        /// Recalculates the view of this controller.
        /// </summary>
        internal void Recalculate();
    }
}
