#region Using Statements
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion


namespace WZIMopoly.GUI
{
    public abstract class Screen
    {
        /// <summary>
        /// Loads the content of the screen.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        public abstract void Load(ContentManager content);

        /// <summary>
        /// Draws the screen.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch object used for rendering.
        /// </param>
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
