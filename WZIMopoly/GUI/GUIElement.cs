using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Engine;
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
        /// <remarks>
        /// The X and Y coordinates refer to the top-left corner of the element.
        /// </remarks>
        protected Rectangle DestinationRect;

        /// <summary>
        /// The texture of the element.
        /// </summary>
        protected Texture2D Texture;

        /// <summary>
        /// The destination rectangle of the element specified for 1920x1080 resolution.
        /// </summary>
        /// <remarks>
        /// The X and Y coordinates refer to the top-left corner of the element.
        /// </remarks>
        private Rectangle _defaultDestinationRect;
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
        /// Initializes a new GUI element.
        /// </summary>
        /// <param name="defDstRect">
        /// The destination rectangle of the element specified for 1920x1080 resolution.
        /// </param>
        protected GUIElement(Rectangle defDstRect)
        {
            _defaultDestinationRect = defDstRect;
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

            DestinationRect = new(x, y, width, height);
        }

        #region IGUIDynamicPosition Static Methods
        /// <summary>
        /// Updates <see cref="_defaultDestinationRect"/> of a GUIElement
        /// and recalculates <see cref="DestinationRect"/> based on the new values.
        /// </summary>
        /// <para>
        /// Replaces the default destination rectangle with a new one.<br/>
        /// The X and Y coordinates of rectangle refer to the top left corner of the element.
        /// </para>
        /// <para>
        /// The GUIElement must implement <see cref="IGUIDynamicPosition"/> interface.
        /// </para>
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
        /// <para>
        /// Updates only the X and Y coordinates of the default destination rectangle.<br/>
        /// The X and Y coordinates refer to the top left corner of the element.<br/>
        /// The width and height of the default destination rectangle are not changed.
        /// </para>
        /// <para>
        /// The GUIElement must implement <see cref="IGUIDynamicPosition"/> interface.
        /// </para>
        /// </remarks>
        /// <param name="view">
        /// The GUIElement instance to be updated.
        /// </param>
        /// <param name="point">
        /// A point to be set as the new X and Y coordinates of the default destination rectangle.
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
        /// Updates <see cref="_defaultDestinationRect"/> of a GUIElement
        /// and recalculates <see cref="DestinationRect"/> based on the new values.
        /// </summary>
        /// <para>
        /// Moves the default destination rectangle by the specified vector.<br/>
        /// The width and height of the default destination rectangle are not changed.
        /// </para>
        /// <para>
        /// The GUIElement must implement <see cref="IGUIDynamicPosition"/> interface.
        /// </para>
        /// <param name="view">
        /// The GUIElement instance to be updated.
        /// </param>
        /// <param name="vector">
        /// A vector to move the default destination rectangle.
        /// </param>
        /// <exception cref="InvalidTypeException">
        /// Thrown if the GUIElement does not implement <see cref="IGUIDynamicPosition"/> interface.
        /// </exception>
        protected static void UpdateDefaultDestinationRect(GUIElement view, Vector2 vector)
        {
            var defDstRect = view._defaultDestinationRect;
            var rectangle = new Rectangle(
                defDstRect.X + (int)vector.X,
                defDstRect.Y + (int)vector.Y,
                defDstRect.Width, defDstRect.Height);
            UpdateDefaultDestinationRect(view, rectangle);
        }
        #endregion
    }
}
