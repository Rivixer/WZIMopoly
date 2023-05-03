using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Engine;
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
        private readonly GUITexture _texture;

        /// <summary>
        /// The texture of the button when is hovered.
        /// </summary>
        private readonly GUITexture _textureHovered;

        /// <summary>
        /// The texture of the button when is disabled.
        /// </summary>
        private readonly GUITexture _textureDisabled;

        /// <summary>
        /// The method that determines whether the button is hovered.
        /// </summary>
        private Func<Point, bool> _isInHoverArea;

        /// <summary>
        /// The model of the button.
        /// </summary>
        private readonly ButtonModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the button.
        /// </param>
        internal GUIButton(ButtonModel model)
        {
            _model = model;

            var buttonPath = $"Images/Buttons/{model.Name}";
            _texture = InitializeTexture(buttonPath, model);
            _textureHovered = InitializeTexture($"{buttonPath}Hovered", model);
            _textureDisabled = InitializeTexture($"{buttonPath}Disabled", model);
            ResetButtonHoverArea();
        }

        /// <summary>
        /// Whether the mouse cursor is in the button's hover area.
        /// </summary>
        internal bool IsHovered => MouseController.IsHover(_isInHoverArea);

        /// <inheritdoc/>
        internal override void Draw(SpriteBatch spriteBatch)
        {
            GUITexture texture;
            if (!_model.IsActive)
            {
                texture = _textureDisabled;
            }
            else if (IsHovered)
            {
                texture = _textureHovered;
            }
            else
            {
                texture = _texture;
            }
            texture.Draw(spriteBatch);

            // We don't call base.Draw(SpriteBatch)
            // because we don't want to draw
            // the children of the button.
        }

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
                Rectangle DestinationRect = _texture.DestinationRect;
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
            _isInHoverArea = (Point p) => _texture.DestinationRect.Contains(p);
        }

        /// <summary>
        /// Initializes a texture for the button.
        /// </summary>
        /// <remarks>
        /// The initialized texture is added as a child of the button.
        /// </remarks>
        /// <param name="path">
        /// The path to the texture.
        /// </param>
        /// <param name="model">
        /// The model of the button to get the destination rectangle and the start point.
        /// </param>
        /// <returns>
        /// The initialized texture.
        /// </returns>
        private GUITexture InitializeTexture(string path, ButtonModel model)
        {
            var button = new GUITexture(path, model.DefDstRect, model.StartPoint);
            AddChild(button);
            return button;
        }
    }
}
