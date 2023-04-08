#region Using Statements
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace WZIMopoly.GUI
{
    internal interface IGUIDrawable
    {
        /// <summary>
        /// Draws the screen.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch object used for rendering.
        /// </param>
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
