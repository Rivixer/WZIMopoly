using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Utils.PositionExtensions;

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
        private Rectangle _defaultDestinationRect;

        /// <summary>
        /// The place where <see cref="_defaultDestinationRect"/> has been specified.
        /// </summary>
        private GUIStartPoint _startPoint;

        /// <summary>
        /// The opacity of the element.
        /// </summary>
        /// <remarks>
        /// Value between 0f and 1f.
        /// </remarks>
        private readonly float _opacity;

        /// <summary>
        /// The path to the texture that will be drawn.
        /// </summary>
        private readonly string _path;
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="GUITexture"/> class.
        /// </summary>
        /// <param name="path">
        /// The path to the texture that will be drawn.
        /// </param>
        /// <param name="defDstRect">
        /// The destination rectangle of the element specified for 1920x1080 resolution.
        /// </param>
        /// <param name="startPoint">
        /// The starting position of the element for which <paramref name="defDstRect"/> has been specified.
        /// Defaults to <see cref="GUIStartPoint.TopLeft"/>.
        /// </param>
        /// <param name="opacity">
        /// The opacity of the element. Must be between 0f and 1f. Defaults to 1f.
        /// </param>
        internal GUITexture(string path, Rectangle defDstRect, GUIStartPoint startPoint = GUIStartPoint.TopLeft, float opacity = 1f)
        {
            _startPoint = startPoint;
            _defaultDestinationRect = defDstRect;
            _path = path;
            _opacity = opacity;
            Recalculate();
        }

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
        /// Sets new <see cref="_defaultDestinationRect"/>
        /// and recalculates <see cref="DestinationRect"/>
        /// and <see cref="UnscaledDestinationRect"/> fields.
        /// </summary>
        /// <param name="defDstRect">
        /// The destination rectangle of the element
        /// specified for 1920x1080 resolution.
        /// </param>
        /// <param name="startPoint">
        /// The starting position of the element for which
        /// <paramref name="defDstRect"/> has been specified.
        /// </param>
        public void SetNewDefDstRectangle(Rectangle defDstRect, GUIStartPoint startPoint = GUIStartPoint.TopLeft)
        {
            _defaultDestinationRect = defDstRect;
            _startPoint = startPoint;
            Recalculate();
        }

        /// <summary>
        /// Sets new <see cref="_defaultDestinationRect"/>
        /// and recalculates <see cref="DestinationRect"/>
        /// and <see cref="UnscaledDestinationRect"/> fields,
        /// based on <paramref name="dstRect"/> scaled to 
        /// the current resolution.
        /// </summary>
        /// <param name="dstRect">
        /// The rectangle scaled to the current resolution.
        /// </param>
        /// <param name="startPoint">
        /// The starting position of the element for which
        /// <paramref name="dstRect"/> has been specified.
        /// </param>
        public void SetNewDstRectangle(Rectangle dstRect, GUIStartPoint startPoint = GUIStartPoint.TopLeft)
        {
            int posX = dstRect.X * 1920 / ScreenController.Width;
            int posY = dstRect.Y * 1080 / ScreenController.Height;
            int width = dstRect.Width * 1920 / ScreenController.Width;
            int height = dstRect.Height * 1080 / ScreenController.Height;
            _defaultDestinationRect = new Rectangle(posX, posY, width, height);
            _startPoint = startPoint;
            Recalculate();
        }

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
                spriteBatch.Draw(Texture, DestinationRect, new Color(255, 255, 255, (int)(_opacity * 255)));
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
            DestinationRect = UnscaledDestinationRect.ToCurrentResolution();
        }

        /// <summary>
        /// Loads the texture of the element.
        /// </summary>
        /// <param name="content">
        /// The ContentManager used to load the texture.
        /// </param>
        /// <remarks>
        /// After loading the texture, <see cref="Recalculate"/> is called
        /// to scale and shift the element to the current screen resolution.
        /// </remarks>
        public override void Load(ContentManager content)
        {
            // TODO: Standardize this after creating the game language selection
            try
            {
                Texture = content.Load<Texture2D>(_path + "PL");
            }
            catch (ContentLoadException)
            {
                Texture = content.Load<Texture2D>(_path);
            }

            Recalculate();
        }
        #endregion
    }
}
