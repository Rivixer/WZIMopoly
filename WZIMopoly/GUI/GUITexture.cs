﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Exceptions;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a texture GUI element.
    /// </summary>
    internal class GUITexture : GUIElement
    {
        #region Fields
        /// <summary>
        /// The destination rectangle of the element scaled to the current screen resolution.
        /// </summary>
        /// <remarks>
        /// The X and Y coordinates refer to the top-left corner of the element.
        /// </remarks>
        internal Rectangle DestinationRect;

        /// <summary>
        /// The destination rectangle of the element unscaled to the current screen resolution.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The rectangle is specified for 1920x1080 resolution.
        /// </para>
        /// <para>
        /// The X and Y coordinates refer to the top-left corner of the element.
        /// </para>
        /// </remarks>
        internal Rectangle UnscaledDestinationRect;

        /// <summary>
        /// The texture of the element.
        /// </summary>
        private Texture2D _texture;

        /// <summary>
        /// The destination rectangle of the element specified for 1920x1080 resolution.
        /// </summary>
        /// <remarks>
        /// The X and Y coordinates refer to the top-left corner of the element.
        /// </remarks>
        private Rectangle _defaultDestinationRect;

        /// <summary>
        /// The place where <see cref="_defaultDestinationRect"/> has been specified.
        /// </summary>
        private readonly GUIStartPoint _startPoint;

        private readonly string _path;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="GUITexture"/> class.
        /// </summary>
        /// <param name="path">
        /// The path to the texture that will be drawn.
        /// </param>
        /// <param name="defDstRect">
        /// The destination rectangle of the element specified for 1920x1080 resolution.
        /// </param>
        internal GUITexture(string path, Rectangle defDstRect)
            : this(path, defDstRect, GUIStartPoint.TopLeft) { }

        /// <inheritdoc cref="GUITexture(string, Rectangle)"/>
        /// <param name="startPoint">
        /// The starting position of the element for which <paramref name="defDstRect"/> has been specified.
        /// </param>
        internal GUITexture(string path, Rectangle defDstRect, GUIStartPoint startPoint)
        {
            _startPoint = startPoint;
            _defaultDestinationRect = defDstRect;
            _path = path;
            Recalculate();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets texture of the element.
        /// </summary>
        protected Texture2D Texture
        {
            get => _texture;
            set
            {
                _texture = value;
                Recalculate();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Shifts <see cref="_defaultDestinationRect"/> according to <see cref="_startPoint"/> field.
        /// </summary>
        /// <remarks>
        /// Saves it to <see cref="UnscaledDestinationRect"/> field.
        /// </remarks>
        private void ShiftRectangle()
        {
            var x = _defaultDestinationRect.X;
            var y = _defaultDestinationRect.Y;
            var width = _defaultDestinationRect.Width;
            var height = _defaultDestinationRect.Height;

            switch (_startPoint)
            {
                case GUIStartPoint.TopLeft:
                    break;
                case GUIStartPoint.Left:
                    y -= height / 2;
                    break;
                case GUIStartPoint.BottomLeft:
                    y -= height;
                    break;
                case GUIStartPoint.Top:
                    x -= width / 2;
                    break;
                case GUIStartPoint.Center:
                    x -= width / 2;
                    y -= height / 2;
                    break;
                case GUIStartPoint.Bottom:
                    x -= width / 2;
                    y -= height;
                    break;
                case GUIStartPoint.TopRight:
                    x -= width;
                    break;
                case GUIStartPoint.Right:
                    x -= width;
                    y -= height / 2;
                    break;
                case GUIStartPoint.BottomRight:
                    x -= width;
                    y -= height;
                    break;
            }

            UnscaledDestinationRect = new Rectangle(x, y, width, height);
        }
        #endregion

        #region GUIElement Methods
        /// <inheritdoc/> 
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Texture is not null)
            {
                spriteBatch.Draw(Texture, DestinationRect, Color.White);
            }
        }

        /// <summary>
        /// Scales and shifts <see cref="_defaultDestinationRect"/> for the current screen resolution.<br/>
        /// </summary>
        /// <remarks>
        /// Saves it to <see cref="DestinationRect"/> field.
        /// </remarks>
        public override void Recalculate()
        {
            ShiftRectangle();
            var x = UnscaledDestinationRect.X * ScreenController.Width / 1920;
            var y = UnscaledDestinationRect.Y * ScreenController.Height / 1080;
            var width = UnscaledDestinationRect.Width * ScreenController.Width / 1920;
            var height = UnscaledDestinationRect.Height * ScreenController.Height / 1080;
            DestinationRect = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Loads the texture of the element.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used to load the texture.
        /// </param>
        public override void Load(ContentManager content)
        {
            Texture = content.Load<Texture2D>(_path);
        }
        #endregion

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
        protected static void UpdateDefaultDestinationRect(GUITexture view, Rectangle defDstRect)
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
        protected static void UpdateDefaultDestinationRect(GUITexture view, Point point)
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
        protected static void UpdateDefaultDestinationRect(GUITexture view, Vector2 vector)
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
