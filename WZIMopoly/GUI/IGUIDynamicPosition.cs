using Microsoft.Xna.Framework;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Provides a dynamic position for GUI elements.
    /// </summary>
    internal interface IGUIDynamicPosition
    {
        /// <summary>
        /// Moves the element by the specified offset.
        /// </summary>
        /// <param name="offset">
        /// The offset that the element should be moved by.
        /// </param>
        public abstract void UpdatePosition(Vector2 offset);

        /// <summary>
        /// Sets the position of the element to the specified point.
        /// </summary>
        /// <param name="point">
        /// The point that the element should be moved to.
        /// </param>
        public abstract void UpdatePosition(Point point);
    }
}
