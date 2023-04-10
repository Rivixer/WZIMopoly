using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Exceptions;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a GUI element.
    /// </summary>
    public abstract class GUIElement
    {
        #region Fields
        /// <summary>
        /// The destination rectangle of the element scaled to the current screen resolution.
        /// </summary>
        protected Rectangle DestinationRect;

        /// <summary>
        /// The texture of the element.
        /// </summary>
        protected Texture2D Texture;

        /// <summary>
        /// The destination rectangle of the element specified for 1920x1080 resolution.
        /// </summary>
        private Rectangle _defaultDestinationRect;

        /// <summary>
        /// The start point of the element.
        /// </summary>
        private GUIStartPoint _startPoint;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new GUI element.
        /// <para>
        /// Doesn't initialize <see cref="_defaultDestinationRect"/> and <see cref="_startPoint"/>.
        /// </para>
        /// <para>
        /// Should be only used if the class overrites <see cref="Draw(SpriteBatch)"/> and <see cref="Recalculate()"/> methods.
        /// </para>
        /// </summary>
        protected GUIElement() { }

        /// <summary>
        /// Initializes a new GUI element with <see cref="GUIStartPoint.TopLeft"/> start point.
        /// </summary>
        /// <param name="defDstRect">
        /// The destination rectangle of the element specified for 1920x1080 resolution.
        /// </param>
        protected GUIElement(Rectangle defDstRect) : this(defDstRect, GUIStartPoint.TopLeft) { }

        /// <summary>
        /// Initializes a new GUI element.
        /// </summary>
        /// <param name="defDstRect">
        /// The destination rectangle of the element specified for 1920x1080 resolution.
        /// </param>
        /// <param name="startPoint">
        /// The point of the element for which <paramref name="defDstRect"/> has been specified.
        /// </param>
        protected GUIElement(Rectangle defDstRect, GUIStartPoint startPoint)
        {
            _defaultDestinationRect = defDstRect;
            _startPoint = startPoint;
            Recalculate();
        }
        #endregion

        /// <summary>
        /// Loads the content of the element.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used for loading content.
        /// </param>
        internal virtual void Load(ContentManager content) { }

        /// <summary>
        /// Draws the element.
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch object used for rendering.
        /// </param>
        internal virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRect, Color.White);
        }

        /// <summary>
        /// Scales <see cref="_defaultDestinationRect"/> for the current screen resolution.<br/>
        /// Moves the start position according to the specified <see cref="GUIStartPoint"/>.<br/>
        /// Saves it to <see cref="DestinationRect"/> field.
        /// </summary>
        public void Recalculate()
        {
            var x = _defaultDestinationRect.X * ScreenController.Width / 1920;
            var y = _defaultDestinationRect.Y * ScreenController.Height / 1080;
            var width = _defaultDestinationRect.Width * ScreenController.Width / 1920;
            var height = _defaultDestinationRect.Height * ScreenController.Height / 1080;

            switch (_startPoint)
            {
                case GUIStartPoint.TopLeft:
                    break;
                case GUIStartPoint.Top:
                    x -= width / 2;
                    break;
                case GUIStartPoint.TopRight:
                    x -= width;
                    break;
                case GUIStartPoint.Center:
                    x -= width / 2;
                    y -= height / 2;
                    break;
            }

            DestinationRect = new(x, y, width, height);
        }

        #region IGUIDynamicPosition Methods
        /// <summary>
        /// Updates <see cref="_defaultDestinationRect"/> of a GUIElement
        /// and recalculates <see cref="DestinationRect"/> based on the new values.
        /// </summary>
        /// <remarks>
        /// The GUIElement must implement <see cref="IGUIDynamicPosition"/> interface.
        /// </remarks>
        /// <param name="view">
        /// The GUIElement instance to be updated.
        /// </param>
        /// <param name="defDstRect">
        /// A new default destination rectangle to be set.
        /// </param>
        /// <exception cref="InvalidTypeException">
        /// Thrown if the GUIElement does not implement <see cref="IGUIDynamicPosition"/> interface.
        /// </exception>
        protected static void UpdateDefaultDestinationRect(GUIElement view, Rectangle defDstRect)
        {
            if (view is not IGUIDynamicPosition)
            {
                throw new InvalidTypeException(
                    $"{view.GetType()} must implements IGUIDynamicPosition"
                    + " to change the default destination rectangle.");
            }

            view._defaultDestinationRect = defDstRect;
            view.Recalculate();
        }

        /// <summary>
        /// Updates <see cref="_defaultDestinationRect"/> of a GUIElement
        /// and recalculates <see cref="DestinationRect"/> based on the new values.
        /// </summary>
        /// <remarks>
        /// The GUIElement must implement <see cref="IGUIDynamicPosition"/> interface.
        /// </remarks>
        /// <param name="view">
        /// The GUIElement instance to be updated.
        /// </param>
        /// <param name="point">
        /// A point of the element to update the default destination rectangle.
        /// </param>
        /// <exception cref="InvalidTypeException">
        /// Thrown if the GUIElement does not implement <see cref="IGUIDynamicPosition"/> interface.
        /// </exception>
        protected static void UpdateDefaultDestinationRect(GUIElement view, Point point)
        {
            var defDstRect = view._defaultDestinationRect;
            var rectangle = new Rectangle(point.X, point.Y, defDstRect.Width, defDstRect.Height);
            UpdateDefaultDestinationRect(view, rectangle);
        }

        /// <summary>
        /// Updates <see cref="_startPoint"/> of a GUIElement
        /// and recalculates <see cref="DestinationRect"/> based on the new value.
        /// </summary>
        /// <remarks>
        /// The GUIElement must implement <see cref="IGUIDynamicPosition"/> interface.
        /// </remarks>
        /// <param name="view">
        /// The GUIElement instance to be updated.
        /// </param>
        /// <param name="startPoint">
        /// A new start point to be set.
        /// </param>
        /// <exception cref="InvalidTypeException">
        /// Thrown if the GUIElement does not implement <see cref="IGUIDynamicPosition"/> interface.
        /// </exception>
        protected static void UpdateStartPoint(GUIElement view, GUIStartPoint startPoint)
        {
            if (view is not IGUIDynamicPosition)
            {
                throw new InvalidTypeException(
                    $"{view.GetType()} must implements IGUIDynamicPosition"
                    + " to change the start point.");
            }

            view._startPoint = startPoint;
            view.Recalculate();
        }
        #endregion
    }
}
