#region Using Statements
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion


namespace WZIMopoly.GUI
{
    /// <summary>
    /// The base class for screens.
    /// </summary>
    public abstract class Screen : IGUIDrawable, IGUILoadable
    {
        /// <inheritdoc/>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <inheritdoc/>
        public abstract void Load(ContentManager content);

        public abstract void RecalculateAll();

    }
}
