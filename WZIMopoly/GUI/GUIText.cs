using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Engine;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a text GUI element.
    /// </summary>
    internal abstract class GUIText : GUIElement
    {
        /// <summary>
        /// The font of the text.
        /// </summary>
        protected SpriteFont Font;

        /// <summary>
        /// The text to display.
        /// </summary>
        protected string Text = string.Empty;

        /// <summary>
        /// The vector that represents the position scaled to the current screen resolution.
        /// </summary>
        /// <remarks>
        /// The X and Y coordinates refer to the top-left corner of the element.
        /// </remarks>
        protected Vector2 Position = Vector2.Zero;

        /// <summary>
        /// The color of the text.
        /// </summary>
        protected Color Color = Color.White;

        /// <summary>
        /// The vector that represents position specified for 1920x1080 resolution.
        /// </summary>
        /// <remarks>
        /// The X and Y coordinates refer to the top-left corner of the element.
        /// </remarks>
        private Vector2 _defaultPosition;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="GUIText"/> class
        /// </summary>
        /// <param name="defPosition">
        /// The position vector of the element specified for 1920x1080 resolution.
        /// </param>
        protected GUIText(Vector2 defPosition)
        {
            _defaultPosition = defPosition;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="GUIText"/> class.
        /// </summary>
        /// <param name="defPosition">
        /// The position vector of the element specified for 1920x1080 resolution.
        /// </param>
        /// <param name="text">
        /// The text to display.
        /// </param>
        /// <param name="color">
        /// The color of the text.
        /// </param>
        protected GUIText(Vector2 defPosition, string text, Color color)
            : this(defPosition)
        {
            Text = text;
            Color = color;
        }
        #endregion

        /// <inheritdoc/>        
        internal override void Draw(SpriteBatch spriteBatch)
        {
            if (Font is not null)
            {
                spriteBatch.DrawString(Font, Text, Position, Color);
            }
        }

        /// <summary>
        /// Scales <see cref="_defaultPosition"/> for the current screen resolution.<br/>
        /// Saves it to <see cref="Position"/> field.
        /// </summary>
        internal sealed override void Recalculate()
        {
            var x = _defaultPosition.X * ScreenController.Width / 1920;
            var y = _defaultPosition.Y * ScreenController.Height / 1080;
            Position = new Vector2(x, y);
        }
    }
}
