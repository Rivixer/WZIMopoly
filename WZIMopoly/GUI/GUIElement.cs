using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a GUI element.
    /// </summary>
    public abstract class GUIElement : IGUIable
    {
        /// <inheritdoc/>
        public virtual void BeforeUpdate() { }

        /// <inheritdoc/>
        public virtual void Update() { }

        /// <inheritdoc/>
        public virtual void AfterUpdate() { }

        /// <inheritdoc/>
        public abstract void Load(ContentManager content);

        /// <inheritdoc/>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <inheritdoc/>
        public abstract void Recalculate();
    }
}
