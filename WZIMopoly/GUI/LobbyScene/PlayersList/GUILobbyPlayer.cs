using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using WZIMopoly.Engine;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene.PlayersList;

namespace WZIMopoly.GUI.LobbyScene.PlayersList
{
    /// <summary>
    /// Represents the view of player box in players list.
    /// </summary>
    internal class GUILobbyPlayer : GUIElement
    {
        #region Fields
        /// <summary>
        /// The background of player box.
        /// </summary>
        private readonly GUITexture _background;

        /// <summary>
        /// The editable text of player nick.
        /// </summary>
        private readonly GUIEditableText _nickText;

        /// <summary>
        /// The model of player box.
        /// </summary>
        private readonly LobbyPlayerModel _model;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GUILobbyPlayer"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the player box.
        /// </param>
        public GUILobbyPlayer(LobbyPlayerModel model)
        {
            string color = model.Player.Color;
            Point position = Position[color];

            var backgroundRect = new Rectangle(position.X, position.Y, 392, 73);
            _background = new GUITexture($"Images/Buttons/LobbyPlayer{color}", backgroundRect, GUIStartPoint.Center);

            var textPosition = new Vector2(position.X - 135, position.Y);
            _nickText = new GUIEditableText("Fonts/WZIMFont", textPosition, Color.Black, GUIStartPoint.Left, text: model.Player.Nick, scale: 0.65f, maxChars: 10);

            _model = model;
        }

        /// <summary>
        /// Gets the position of player box.
        /// </summary>
        /// <value>
        /// A dictionary containing the position
        /// of the player box for each color.
        /// </value>
        /// <remarks>
        /// The key is the color of player.
        /// The value is the position of player box.
        /// </remarks>
        public static Dictionary<string, Point> Position { get; } = new()
        {
            { "Red", new Point(750, 450) },
            { "Blue", new Point(1170, 450) },
            { "Green", new Point(750, 550) },
            { "Yellow", new Point(1170, 550) },
        };

        /// <summary>
        /// Gets the view of editable text of player nick.
        /// </summary>
        public GUIEditableText NickText => _nickText;

        /// <summary>
        /// Gets whether the player box is hovered.
        /// </summary>
        public bool IsHovered => MouseController.IsHover(_background.DestinationRect);

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_model.Player.PlayerType != PlayerType.None)
            {
                _background.Draw(spriteBatch);
                _nickText.Draw(spriteBatch);
            }
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _background.Load(content);
            _nickText.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _background.Recalculate();
            _nickText.Recalculate();
        }

        /// <inheritdoc/>
        public override void Update()
        {
            if (!NickText.IsSelected)
            {
                NickText.Text = _model.Player.Nick;
            }
        }
    }
}
