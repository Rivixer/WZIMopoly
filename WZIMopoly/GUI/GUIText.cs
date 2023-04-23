﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Engine;
using WZIMopoly.Enums;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a text GUI element.
    /// </summary>
    internal abstract class GUIText : IGUIable
    {
        #region Fields
        /// <summary>
        /// The position of element.
        /// </summary>
        private GUIStartPoint _startPoint;

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
        /// The X and Y coordinates refer to the top-left corner of the element.
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
        /// The X and Y coordinates refer to the top-left corner of the element.
        /// </remarks>
        private Vector2 _defaultPosition;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="GUIText"/> class.
        /// </summary>
        /// <param name="defPosition">
        /// The position vector of the element specified for 1920x1080 resolution.
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
        /// The text to display.
        /// </param>
        protected GUIText(Vector2 defPosition, GUIStartPoint startPoint)
            : this(defPosition, startPoint, string.Empty, Color.White) { }


        protected GUIText(Vector2 defPosition, GUIStartPoint startPoint, string text, Color color)
        {
            _startPoint = startPoint;
            Text = text;
            Color = color;
        }

        #endregion


        protected string Text { get => _text; set { _text = value; Update(Position.X, Position.Y); } }

        /// <inheritdoc/>        
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Font is not null)
            {
                spriteBatch.DrawString(Font, Text, Position, Color);
            }
        }

        public void Load(ContentManager contentManager) { }

        /// <summary>
        /// Scales <see cref="_defaultPosition"/> for the current screen resolution.<br/>
        /// Saves it to <see cref="Position"/> field.
        /// </summary>
        public void Recalculate()
        {
            var x = _defaultPosition.X * ScreenController.Width / 1920;
            var y = _defaultPosition.Y * ScreenController.Height / 1080;

            Update(x, y);

            Position = new Vector2(x, y);
        }

        private void Update(double X, double Y)
        {
            switch (_startPoint)
            {
                case GUIStartPoint.TopLeft:
                    break;
                case GUIStartPoint.Left:
                    Position.Y -= Font.MeasureString(Text).Y / 2;
                    break;
                case GUIStartPoint.BottomLeft:
                    Position.Y -= Font.MeasureString(Text).Y;
                    break;
                case GUIStartPoint.Top:
                    Position.X -= Font.MeasureString(Text).X / 2;
                    break;
                case GUIStartPoint.Center:
                    Position.X -= Font.MeasureString(Text).X / 2;
                    Position.Y -= Font.MeasureString(Text).Y / 2;
                    break;
                case GUIStartPoint.Bottom:
                    Position.X -= Font.MeasureString(Text).X / 2;
                    Position.Y -= Font.MeasureString(Text).Y;
                    break;
                case GUIStartPoint.TopRight:
                    Position.X -= Font.MeasureString(Text).X;
                    break;
                case GUIStartPoint.Right:
                    Position.X -= Font.MeasureString(Text).X;
                    Position.Y -= Font.MeasureString(Text).Y / 2;
                    break;
                case GUIStartPoint.BottomRight:
                    Position.X -= Font.MeasureString(Text).X;
                    Position.Y -= Font.MeasureString(Text).Y;
                    break;
            }
        }
    }
}
