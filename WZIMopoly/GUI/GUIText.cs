using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Engine;
using WZIMopoly.Enums;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a text GUI element.
    /// </summary>
    internal abstract class GUIText : GUIElement
    {
        #region Fields
        /// <summary>
        /// The font of the text.
        /// </summary>
        protected SpriteFont Font;

        /// <summary>
        /// The text to display.
        /// </summary>
        private string _text;

        /// <summary>
        /// The vector that represents the position scaled to the current screen resolution.
        /// </summary>
        /// <remarks>
        /// The X and Y coordinates refer to the position <see cref="_startPoint"/> of the element.
        /// </remarks>
        protected Vector2 Position;

        /// <summary>
        /// The color of the text.
        /// </summary>
        protected Color Color = Color.White;

        /// <summary>
        /// The vector that represents position specified for 1920x1080 resolution.
        /// </summary>
        /// <remarks>
        /// The X and Y coordinates refer to the position <see cref="_startPoint"/> of the element.
        /// </remarks>
        private readonly Vector2 _defaultPosition;

        /// <summary>
        /// The place where <see cref="_defaultPosition"/> has been specified.
        /// </summary>
        private readonly GUIStartPoint _startPoint;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="GUIText"/> class.
        /// </summary>
        /// <param name="defPosition">
        /// The position vector of the element specified for 1920x1080 resolution. It refers to the top left corner.
        /// </param>
        protected GUIText(Vector2 defPosition)
            : this(defPosition, GUIStartPoint.TopLeft, string.Empty, Color.White) { }

        /// <summary>
        /// Initializes a new instance of <see cref="GUIText"/> class.
        /// </summary>
        /// <param name="defPosition">
        /// The position vector of the element specified for 1920x1080 resolution.
        /// </param>
        /// <param name="startPoint">
        /// The starting position of the element for which <paramref name="defPosition"/> has been specified.
        /// </param>
        /// <remarks>
        /// The <see cref="Text"/> property is empty and the <see cref="Color"/> property is set to white by default.
        /// </remarks>
        protected GUIText(Vector2 defPosition, GUIStartPoint startPoint)
            : this(defPosition, startPoint, string.Empty, Color.White) { }

        /// <summary>
        /// Initializes a new instance of <see cref="GUIText"/> class.
        /// </summary>
        /// <param name="defPosition">
        /// The position vector of the element specified for 1920x1080 resolution.
        /// </param>
        /// <param name="startPoint">
        /// The starting position of the element for which <paramref name="defPosition"/> has been specified.
        /// </param>
        /// <param name="text">
        /// The text to display.
        /// </param>
        /// <param name="color">
        /// The color of the element.
        /// </param>
        protected GUIText(Vector2 defPosition, GUIStartPoint startPoint, string text, Color color)
        {
            _defaultPosition = defPosition;
            _startPoint = startPoint;
            Text = text;
            Color = color;
        }
        #endregion

        /// <summary>
        /// Gets or sets the text of the element.
        /// </summary>
        protected string Text
        {
            get => _text;
            set
            {
                _text = value;
                Recalculate();
            }
        }

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
        internal override void Recalculate()
        {
            var x = _defaultPosition.X * ScreenController.Width / 1920;
            var y = _defaultPosition.Y * ScreenController.Height / 1080;

            switch (_startPoint)
            {
                case GUIStartPoint.TopLeft:
                    break;
                case GUIStartPoint.Left:
                    y -= Font.MeasureString(Text).Y / 2;
                    break;
                case GUIStartPoint.BottomLeft:
                    y -= Font.MeasureString(Text).Y;
                    break;
                case GUIStartPoint.Top:
                    x -= Font.MeasureString(Text).X / 2;
                    break;
                case GUIStartPoint.Center:
                    x -= Font.MeasureString(Text).X / 2;
                    y -= Font.MeasureString(Text).Y / 2;
                    break;
                case GUIStartPoint.Bottom:
                    x -= Font.MeasureString(Text).X / 2;
                    y -= Font.MeasureString(Text).Y;
                    break;
                case GUIStartPoint.TopRight:
                    x -= Font.MeasureString(Text).X;
                    break;
                case GUIStartPoint.Right:
                    x -= Font.MeasureString(Text).X;
                    y -= Font.MeasureString(Text).Y / 2;
                    break;
                case GUIStartPoint.BottomRight:
                    x -= Font.MeasureString(Text).X;
                    y -= Font.MeasureString(Text).Y;
                    break;
            }

            Position = new Vector2(x, y);
        }
    }
}
