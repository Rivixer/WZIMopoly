using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models;

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
        protected readonly GUITexture TextureHovered;

        /// <summary>
        /// The texture of the button when is disabled.
        /// </summary>
        protected readonly GUITexture TextureDisabled;

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
        internal GUIButton(ButtonModel model, Rectangle defDstRect, GUIStartPoint startPoint = GUIStartPoint.TopLeft)
        {
            Model = model;
            _defDstRect = defDstRect;
            _startPoint = startPoint;

            var buttonPath = $"Images/Buttons/{model.Name}";
            Texture = new GUITexture(buttonPath, _defDstRect, _startPoint);
            TextureHovered = new GUITexture($"{buttonPath}Hovered", _defDstRect, _startPoint);
            TextureDisabled = new GUITexture($"{buttonPath}Disabled", _defDstRect, _startPoint);
            ResetButtonHoverArea();
        }

        /// <summary>
        /// Whether the mouse cursor is in the button's hover area.
        /// </summary>
        internal bool IsHovered => MouseController.IsHover(_isInHoverArea);

        /// <summary>
        /// Creates a rounded square hover area for the button
        /// and assigns it to the <see cref="IsHovered"/> as method.
        /// </summary>
        /// <remarks>
        /// Uses the formula: <c>|x^factor| + |y^factor| &lt;=
        /// |(Width / 2 * scale)^factor|</c>,<br/>
        /// where <c>x</c> and <c>y</c> are the distance
        /// from the center of the button to the mouse position.
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
                Rectangle DestinationRect = Texture.DestinationRect;
                if (onlyIfInRect && !DestinationRect.Contains(p))
                {
                    return false;
                }

                var vector = new Vector2(DestinationRect.Center.X - p.X, DestinationRect.Center.Y - p.Y);
                double x = Math.Abs(Math.Pow(vector.X, factor));
                double y = Math.Abs(Math.Pow(vector.Y, factor));
                double border = Math.Pow(DestinationRect.Width / 2 * scale, factor);
                return x + y <= border;
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
        internal override void Load(ContentManager content)
        {
            Texture.Load(content);
            TextureHovered.Load(content);
            TextureDisabled.Load(content);
        }

        /// <inheritdoc/>
        internal override void Recalculate()
        {
            Texture.Recalculate();
            TextureHovered.Recalculate();
            TextureDisabled.Recalculate();
        }

        /// <inheritdoc/>
        internal override void Draw(SpriteBatch spriteBatch)
        {
            GUITexture texture;
            if (!Model.IsActive)
            {
                texture = TextureDisabled;
            }
            else if (IsHovered)
            {
                texture = TextureHovered;
            }
            else
            {
                texture = Texture;
            }
            texture.Draw(spriteBatch);
        }
    }
}
