﻿using Microsoft.Xna.Framework;
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
        /// The vector that represents the position scaled to the current screen resolution.
        /// </summary>
        /// <remarks>
        /// The X and Y coordinates refer to the position <see cref="_startPoint"/> of the element.
        /// </remarks>
        protected Vector2 Position;

        /// <summary>
        /// The color of the text.
        /// </summary>
        protected Color Color;

        /// <summary>
        /// The scale of the font scaled to the current screen resolution.
        /// </summary>
        protected float Scale;

        /// <summary>
        /// The background of the text.
        /// </summary>
        /// <remarks>
        /// If the background is null, the text will be displayed without background.
        /// </remarks>
        protected GUITexture Background;

        /// <summary>
        /// The text to display.
        /// </summary>
        private string _text;

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
        /// <param name="defPosition">
        /// The position vector of the element specified for 1920x1080 resolution.
        /// </param>
        /// <param name="startPoint">
        /// The starting position of the element for which <paramref name="defPosition"/> has been specified.
        /// </param>
        /// <remarks>
        /// The <see cref="Color"/> property is set to white and the <see cref="Text"/> property is empty by default.
        /// </remarks>
        protected GUIText(Vector2 defPosition, GUIStartPoint startPoint = GUIStartPoint.TopLeft)
            : this(defPosition, startPoint, Color.White) { }

        /// <summary>
        /// Initializes a new instance of <see cref="GUIText"/> class.
        /// </summary>
        /// <param name="defPosition">
        /// The position vector of the element specified for 1920x1080 resolution.
        /// </param>
        /// <param name="startPoint">
        /// The starting position of the element for which <paramref name="defPosition"/> has been specified.
        /// </param>
        /// <param name="color">
        /// The color of the element.
        /// </param>
        /// <param name="text">
        /// The text to display.<br/>
        /// Defaults to empty string.
        /// </param>
        /// <param name="scale">
        /// The scale of the text. <br/>
        /// Defaults to 1.0f.
        /// </param>
        protected GUIText(Vector2 defPosition, GUIStartPoint startPoint, Color color, string text = "", float scale = 1.0f)
        {
            _defaultPosition = defPosition;
            _startPoint = startPoint;
            _defaultScale = scale;
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
            Background?.Draw(spriteBatch);
            if (Font is not null)
            {
                spriteBatch.DrawString(Font, Text, Position, Color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Scales <see cref="_defaultPosition"/> for the current screen resolution.<br/>
        /// Saves it to <see cref="Position"/> field.
        /// </summary>
        internal override void Recalculate()
        {
            if (Font is null)
            {
                return;
            }

            var x = _defaultPosition.X * ScreenController.Width / 1920;
            var y = _defaultPosition.Y * ScreenController.Height / 1080;

            float textWidth = Font.MeasureString(Text).X * Scale;
            float textHeight = Font.MeasureString(Text).Y * Scale;

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

            Position = new Vector2(x, y);
            Scale = _defaultScale * ScreenController.Height / 1080;
            Background?.Recalculate();
        }
    }
}
