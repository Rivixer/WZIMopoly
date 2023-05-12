using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Engine;
using WZIMopoly.Enums;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a text GUI element.
    /// </summary>
    internal class GUIText : GUIElement
    {
        #region Fields
        /// <summary>
        /// The font of the text.
        /// </summary>
        protected SpriteFont Font;

        /// <summary>
        /// The color of the text.
        /// </summary>
        protected Color Color;

        /// <summary>
        /// The scale of the font scaled to the current screen resolution.
        /// </summary>
        protected float Scale;

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
        private Vector2 _position;

        /// <summary>
        /// The path to the font.
        /// </summary>
        private readonly string _fontPath;

        /// <summary>
        /// The scale of the text specified for 1920x1080 resolution.
        /// </summary>
        private readonly float _defaultScale;

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
        /// <param name="fontPath">
        /// The path to the font that will be used to display the text.
        /// </param>
        /// <param name="defPosition">
        /// The position vector of the element specified for 1920x1080 resolution.
        /// </param>
        /// <param name="startPoint">
        /// The starting position of the element for which <paramref name="defPosition"/> has been specified.
        /// </param>
        /// <param name="text">
        /// The text to display.<br/>
        /// Defaults to empty string.
        /// </param>
        /// <param name="scale">
        /// The scale of the text. <br/>
        /// Defaults to 1.0f.
        /// </param>
        /// <remarks>
        /// The <see cref="Color"/> property is set to white and the <see cref="Text"/> property is empty by default.
        /// </remarks>
        internal GUIText(string fontPath, Vector2 defPosition, GUIStartPoint startPoint = GUIStartPoint.TopLeft, string text = "", float scale = 1.0f)
            : this(fontPath, defPosition, Color.White, startPoint, text, scale) { }

        /// <summary>
        /// Initializes a new instance of <see cref="GUIText"/> class.
        /// </summary>
        /// <param name="fontPath">
        /// The path to the font that will be used to display the text.
        /// </param>
        /// <param name="defPosition">
        /// The position vector of the element specified for 1920x1080 resolution.
        /// </param>
        /// <param name="color">
        /// The color of the element.
        /// </param>
        /// <param name="startPoint">
        /// The starting position of the element for which <paramref name="defPosition"/> has been specified.
        /// </param>
        /// <param name="text">
        /// The text to display.<br/>
        /// Defaults to empty string.
        /// </param>
        /// <param name="scale">
        /// The scale of the text. <br/>
        /// Defaults to 1.0f.
        /// </param>
        internal GUIText(string fontPath, Vector2 defPosition, Color color, GUIStartPoint startPoint, string text = "", float scale = 1.0f)
        {
            _fontPath = fontPath;
            _defaultPosition = defPosition;
            _startPoint = startPoint;
            _defaultScale = scale;
            Text = text;
            Color = color;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the text of the element.
        /// </summary>
        /// <value>
        /// The text to display.
        /// </value>
        protected string Text
        {
            get => _text;
            set
            {
                _text = value;
                Recalculate();
            }
        }

        /// <summary>
        /// Gets the position of the text.
        /// </summary>
        /// <value>
        /// The position of the text relative to the top-left corner.
        /// </value>
        protected Vector2 Position => _position;
        #endregion

        #region GUIElement Methods
        /// <inheritdoc/>
        internal override void Draw(SpriteBatch spriteBatch)
        {
            if (Font is not null)
            {
                spriteBatch.DrawString(Font, Text, _position, Color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Scales <see cref="_defaultPosition"/> and <see cref="_defaultScale"/> for the current screen resolution.<br/>
        /// </summary>
        /// <remarks>
        /// Saves it to <see cref="_position"/> and <see cref="Scale"/> field.
        /// </remarks>
        internal override void Recalculate()
        {
            if (Font is null)
            {
                return;
            }

            Scale = _defaultScale * ScreenController.Height / 1080;
            int textWidth = (int)(Font.MeasureString(Text).X * Scale);
            int textHeight = (int)(Font.MeasureString(Text).Y * Scale);
            float x = _defaultPosition.X * ScreenController.Width / 1920;
            float y = _defaultPosition.Y * ScreenController.Height / 1080;

            switch (_startPoint)
            {
                case GUIStartPoint.TopLeft:
                    break;
                case GUIStartPoint.Left:
                    y -= textHeight / 2;
                    break;
                case GUIStartPoint.BottomLeft:
                    y -= textHeight;
                    break;
                case GUIStartPoint.Top:
                    x -= textWidth / 2;
                    break;
                case GUIStartPoint.Center:
                    x -= textWidth / 2;
                    y -= textHeight / 2;
                    break;
                case GUIStartPoint.Bottom:
                    x -= textWidth / 2;
                    y -= textHeight;
                    break;
                case GUIStartPoint.TopRight:
                    x -= textWidth;
                    break;
                case GUIStartPoint.Right:
                    x -= textWidth;
                    y -= textHeight / 2;
                    break;
                case GUIStartPoint.BottomRight:
                    x -= textWidth;
                    y -= textHeight;
                    break;
            }

            _position = new Vector2(x, y);
        }

        /// <summary>
        /// Loads the font.
        /// </summary>
        /// <remarks>
        /// After loading the font, <see cref="Recalculate"/> is called.
        /// </remarks>
        /// <param name="content">
        /// The ContentManager used to load the font.
        /// </param>
        internal override void Load(ContentManager content)
        {
            Font = content.Load<SpriteFont>(_fontPath);
            Recalculate();
        }
        #endregion
    }
}
