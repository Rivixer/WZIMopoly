#region Using Statements
using Microsoft.Xna.Framework;
using System.Collections.Generic;
#endregion


namespace WZIMopoly.GUI
{
    /// <summary>
    /// Base class for creating GUI elements.
    /// </summary>
    internal abstract class GUIElement
    {
        private Rectangle _defaultDestinationRect;
        protected List<GUIElement> Children;
        protected Rectangle DestinationRect;
        /// <value>
        /// The offset will be used to move the position of the pawn so that the drawing coordinates refer to the center of the field, not the upper left corner.
        /// </value>
        protected Vector2 offset;
        /// <summary>
        /// Creates GUIElement with empty list of children. 
        /// </summary>
        /// <param name="defDstRect">Receives default destination rectangle</param>
        protected GUIElement(Rectangle defDstRect) : this(defDstRect, new List<GUIElement>()) { }
        /// <summary>
        /// Creates GUIElement
        /// </summary>
        /// <param name="defDstRect">Receives default destination rectangle</param>
        /// <param name="children">Receives list of GUIElement</param>
        protected GUIElement(Rectangle defDstRect, List<GUIElement> children)
        {
            Children = children;
            _defaultDestinationRect = defDstRect;
            Recalculate();
        }

        /// <summary>
        /// Sets DestinationRect for this object
        /// </summary>
        private void Recalculate()
        {
            var x = _defaultDestinationRect.X * MainScreen.Width / 1920;
            var y = _defaultDestinationRect.X * MainScreen.Height / 1080;
            var width = _defaultDestinationRect.Width * MainScreen.Width / 1920;
            var height = _defaultDestinationRect.Height * MainScreen.Height / 1080;
            DestinationRect = new(x, y, width, height);
        }

        /// <summary>
        /// Runs Recalculate() method for this object and all his childern
        /// </summary>
        internal void RecalculateAll()
        {
            Recalculate();
            Children.ForEach(x => x.RecalculateAll());
        }
    }
}
