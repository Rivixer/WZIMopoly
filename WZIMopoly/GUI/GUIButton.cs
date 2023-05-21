using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models;

#nullable enable

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents a view of a button.
    /// </summary>
    internal class GUIButton : GUIElement
    {
        /// <summary>
        /// The texture of the button when is enabled.
        /// </summary>
        protected readonly GUITexture Texture;

        /// <summary>
        /// The texture of the button when is hovered.
        /// </summary>
        /// <remarks>
        /// If null, <see cref="Texture"/> will be used instead.
        /// </remarks>
        protected readonly GUITexture? TextureHovered;

        /// <summary>
        /// The texture of the button when is disabled.
        /// </summary>
        /// <remarks>
        /// If null, the disabled button will not be displayed.
        /// </remarks>
        protected readonly GUITexture? TextureDisabled;

        /// <summary>
        /// The model of the button.
        /// </summary>
        protected readonly ButtonModel Model;

        /// <summary>
        /// The default destination rectangle of the button.
        /// </summary>
        /// <remarks>
        /// It specifies the position and size of the button.<br/>
        /// The X and Y coordinates refer to the top-left corner of the button.
        /// </remarks>
        private readonly Rectangle _defDstRect;

        /// <summary>
        /// The place where <see cref="_defDstRect"/> has been specified.
        /// </summary>
        private readonly GUIStartPoint _startPoint;

        /// <summary>
        /// The method that determines whether the button is hovered.
        /// </summary>
        private Func<Point, bool> _isInHoverArea;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the button.
        /// </param>
        /// <param name="defDstRect">
        /// The default destination rectangle of the button.<br/>
        /// It specifies the position and size of the button.
        /// </param>
        /// <param name="startPoint">
        /// The starting position of the element for which <paramref name="defDstRect"/> has been specified.<br/>
        /// Defaults to <see cref="GUIStartPoint.TopLeft"/>.
        /// </param>
        /// <param name="hoverTexture">
        /// Whether the button has a texture when is hovered.<br/>
        /// If false, <see cref="Texture"/> will be used instead.<br/>
        /// Defaults to true.
        /// </param>
        /// <param name="disableTexture">
        /// Whether the button has a texture when is disabled.<br/>
        /// If false, the disabled button will not be displayed.<br/>
        /// Default to true.
        /// </param>
        public GUIButton(
            ButtonModel model,
            Rectangle defDstRect,
            GUIStartPoint startPoint = GUIStartPoint.TopLeft,
            bool hoverTexture = true,
            bool disableTexture = true)
        {
            Model = model;
            _defDstRect = defDstRect;
            _startPoint = startPoint;

            var buttonPath = $"Images/Buttons/{model.Name}";
            Texture = new GUITexture(buttonPath, _defDstRect, _startPoint);

            if (hoverTexture)
            {
                TextureHovered = new GUITexture($"{buttonPath}Hovered", _defDstRect, _startPoint);
            }

            if (disableTexture)
            {
                TextureDisabled = new GUITexture($"{buttonPath}Disabled", _defDstRect, _startPoint);
            }
            
            _isInHoverArea = (Point p) => Texture.DestinationRect.Contains(p);
        }

        /// <summary>
        /// Whether the mouse cursor is in the button's hover area.
        /// </summary>
        internal bool IsHovered => MouseController.IsHover(_isInHoverArea);

        /// <summary>
        /// Creates a rounded rectangle hover area for the button
        /// and assigns it to the <see cref="IsHovered"/> as method.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Uses the formula (if the button's rectangle is a square):<br/>
        /// <c>|x^factor| + |y^factor| &lt;=
        /// |(Width / 2 * scale)^factor|</c>,<br/>
        /// where <c>x</c> and <c>y</c> are the distance
        /// from the center of the button to the mouse position.
        /// </para>
        /// <para>
        /// If the button's rectangle is not a square,
        /// it simulates the squares on the sides of the rectangle
        /// and uses the above formula for each one.<br/>
        /// It also checks if the mouse is inside the
        /// button's inner rectangle, where the
        /// left and right borders have been cut off
        /// by the half of the height of the button.
        /// </para>
        /// <para>
        /// This method may be not working properly
        /// if the height of the button is greater than its width.
        /// </para>
        /// </remarks>
        /// <param name="factor">
        /// The factor of the formula. The larger it is,
        /// the more rounded the square hover area is.
        /// </param>
        /// <param name="scale">
        /// The scale of the formula.<br/>
        /// Specifies the height of the visible button
        /// to the height of its graphic.<br/>
        /// Should be between 0 and 1.
        /// </param>
        /// <param name="onlyIfInRect">
        /// Whether the button should be hovered only
        /// if the mouse is also in the
        /// <see cref="GUIElement.DestinationRect"/> of the button.<br/>
        /// If <c>false</c> the button will be hovered even if the mouse
        /// is outside of destination rectangle but in the hover area.
        /// This may affect performance.<br/>
        /// If <c>true</c> the button will be hovered only if the mouse
        /// is in the destination rectangle and in the hover area.<br/>
        /// Defaults to <c>true</c>.
        /// </param>
        internal void SetButtonHoverArea(int factor, float scale, bool onlyIfInRect = true)
        {
            _isInHoverArea = (Point p) =>
            {
                bool InRoundedSquareArea(Point squareCenter, float radius)
                {
                    // The distance vector from squareCenter to the mouse position
                    var vector = new Vector2(
                        squareCenter.X - p.X,
                        squareCenter.Y - p.Y);

                    // The absolute values of the vector components
                    // raised to the certain factor
                    double x = Math.Abs(Math.Pow(vector.X, factor));
                    double y = Math.Abs(Math.Pow(vector.Y, factor));

                    // The border value based on the radius and scaling factor
                    double border = Math.Pow(radius * scale, factor);

                    // Check if the mouse cursor is in the rounded square area
                    return x + y <= border;
                }

                Rectangle DestinationRect = Texture.DestinationRect;

                // Check if the point is outside the destination rectangle
                // when 'onlyIfInRect' flag is enabled
                if (onlyIfInRect && !DestinationRect.Contains(p))
                {
                    return false;
                }

                // Check if the destination rectangle is a square
                if (DestinationRect.Width == DestinationRect.Height)
                {
                    // Check if the point is within the rounded square area
                    // defined by the destination rectangle
                    return InRoundedSquareArea(
                        DestinationRect.Center,
                        DestinationRect.Height / 2);
                }

                // The distance vector from the center of
                // the button to the mouse position
                var vector = new Vector2(
                    DestinationRect.Center.X - p.X,
                    DestinationRect.Center.Y - p.Y);

                // The distance vector from the center of
                // the button to the top and left borders
                var innerRectangle = new Vector2(
                    (DestinationRect.Width - DestinationRect.Height) / 2,
                    DestinationRect.Height / 2);

                // Check if the mouse cursor is within the inner rectangle area
                if (Math.Abs(vector.X) <= Math.Abs(innerRectangle.X) * scale
                    && Math.Abs(vector.Y) <= Math.Abs(innerRectangle.Y) * scale)
                {
                    return true;
                }

                // Define the extreme points on the sides of the button
                var extremePointLeft = new Point(
                    DestinationRect.Left + DestinationRect.Height / 2,
                    DestinationRect.Center.Y);
                var extremePointRight = new Point(
                    DestinationRect.Right - DestinationRect.Height / 2,
                    DestinationRect.Center.Y);

                // Check if the point is within the rounded square
                // areas around the extreme points
                return InRoundedSquareArea(extremePointLeft, DestinationRect.Height / 2)
                    || InRoundedSquareArea(extremePointRight, DestinationRect.Height / 2);                           
            };
        }

        /// <summary>
        /// Resets the hover area of the button
        /// to the <see cref="GUIElement.DestinationRect"/> of the button.
        /// </summary>
        internal void ResetButtonHoverArea()
        {
            _isInHoverArea = (Point p) => Texture.DestinationRect.Contains(p);
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            Texture.Load(content);
            TextureHovered?.Load(content);
            TextureDisabled?.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            Texture.Recalculate();
            TextureHovered?.Recalculate();
            TextureDisabled?.Recalculate();
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            GUITexture? texture;
            if (!Model.IsActive)
            {
                texture = TextureDisabled;
            }
            else if (IsHovered)
            {
                texture = TextureHovered ?? Texture;
            }
            else
            {
                texture = Texture;
            }
            texture?.Draw(spriteBatch);
        }
    }

    /// <summary>
    /// Represents a view of a button with a specific model.
    /// </summary>
    /// <typeparam name="M">
    /// The type of the model of the button.
    /// </typeparam>
    internal class GUIButton<M> : GUIButton
        where M : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIButton{M}"/> class.
        /// </summary>
        /// <inheritdoc cref="GUIButton(ButtonModel, Rectangle, GUIStartPoint)"/>
        public GUIButton(
            M model,
            Rectangle defDstRect,
            GUIStartPoint startPoint = GUIStartPoint.TopLeft,
            bool hoverTexture = true,
            bool disableTexture = true
        ) : base(model, defDstRect, startPoint, hoverTexture, disableTexture) { }

        /// <summary>
        /// Gets the model of the button.
        /// </summary>
        protected new M Model => (M)base.Model;
    }
}
