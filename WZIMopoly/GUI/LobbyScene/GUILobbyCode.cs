using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models.LobbyScene;

namespace WZIMopoly.GUI.LobbyScene
{
    /// <summary>
    /// Represents a view of the lobby code.
    /// </summary>
    internal class GUILobbyCode : GUIElement
    {
        /// <summary>
        /// The background of the lobby code.
        /// </summary>
        private readonly GUITexture _background;

        /// <summary>
        /// The text of the lobby code.
        /// </summary>
        private readonly GUIText _text;

        /// <summary>
        /// The auxiliary text of the lobby code.
        /// </summary>
        private readonly GUIText _infoText;

        /// <summary>
        /// The model of the lobby code.
        /// </summary>
        private readonly LobbyCodeModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUILobbyCode"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the lobby code.
        /// </param>
        public GUILobbyCode(LobbyCodeModel model)
        {
            _model = model;
            _background = new GUITexture("Images/LobbyCode", new Rectangle(960, 822, 321, 74), GUIStartPoint.Center);
            _text = new GUIText("Fonts/WZIMFont", new Vector2(960, 822), Color.Black, GUIStartPoint.Center, scale: 0.45f);
            _infoText = new GUIText("Fonts/WZIMFont", new Vector2(960, 795), Color.Black, GUIStartPoint.Center, scale: 0.35f);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (WZIMopoly.GameType == GameType.Online)
            {
                if (_model.Code != string.Empty)
                {
                    _background.Draw(spriteBatch);
                    _text.Draw(spriteBatch);
                }
                _infoText.Draw(spriteBatch);
            }
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _background.Load(content);
            _text.Load(content);
            _infoText.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _background.Recalculate();
            _text.Recalculate();
            _infoText.Recalculate();
        }

        /// <inheritdoc/>
        public override void Update()
        {
            if (_model.Code != string.Empty)
            {
                _text.Text = _model.Code;
            }

            if (WZIMopoly.Network != null)
            {
                _infoText.Text = WZIMopoly.Language switch
                {
                    Language.Polish => "Kod lobby",
                    Language.English => "Lobby code",
                    _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}"),
                };
            }
            else
            {
                _infoText.Text = WZIMopoly.Language switch
                {
                    Language.Polish => "Nie udalo sie polaczyc z serwerem WZIMopoly!",
                    Language.English => "Cannot connect to the WZIMopoly's server!",
                    _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}"),
                };
            }
        }
    }
}
