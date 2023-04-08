#region Using Statements
using Microsoft.Xna.Framework;
using System.Collections.Generic;
#endregion


namespace WZIMopoly.GUI
{
    /// <summary>
    /// The base class for creating GUI elements.
    /// </summary>
    internal abstract class GUIElement
    {
        private Rectangle _defaultDestinationRect;
        protected List<GUIElement> Children;
        protected Rectangle DestinationRect;
        protected Vector2 Offset;
        /// <summary>
        /// Creates GUIElement with an empty list of children. 
        /// </summary>
        /// <param name="defDstRect">Default destination rectangle</param>
        protected GUIElement(Rectangle defDstRect) : this(defDstRect, new List<GUIElement>()) { }
        /// <summary>
        /// Creates a GUIElement.
        /// </summary>
        /// <param name="defDstRect">Default destination rectangle</param>
        /// <param name="children">List of GUIElement</param>
        protected GUIElement(Rectangle defDstRect, List<GUIElement> children)
        {
            Children = children;
            _defaultDestinationRect = defDstRect;
            Recalculate();
        }

        /// <summary>
        /// Sets <see cref="DestinationRect"/> for this object.
        /// </summary>
        private void Recalculate()
        {
            var x = _defaultDestinationRect.X * MainScreen.Width / 1920;
            var y = _defaultDestinationRect.Y * MainScreen.Height / 1080;
            var width = _defaultDestinationRect.Width * MainScreen.Width / 1920;
            var height = _defaultDestinationRect.Height * MainScreen.Height / 1080;
            DestinationRect = new(x, y, width, height);
        }

        /// <summary>
        /// Runs <see cref="Recalculate"/> method for this object and all his children.
        /// </summary>
        internal void RecalculateAll()
        {
            Recalculate();
            Children.ForEach(x => x.RecalculateAll());
        }
    }
}
