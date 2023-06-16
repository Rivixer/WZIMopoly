using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents the lobby view.
    /// </summary>
    internal class LobbyView : GUIElement
    {
        /// <summary>
        /// The background of the lobby scene.
        /// </summary>
        private readonly GUITexture _background;

        /// <summary>
        /// The texture with settings of the game.
        /// </summary>
        private readonly GUITexture _lobbySettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="LobbyView"/> class.
        /// </summary>
        public LobbyView()
        {
            _background = new GUITexture("Images/MenuScreen", new Rectangle(0, 0, 1920, 1080));
            _lobbySettings = new GUITexture("Images/LobbyScreen", new Rectangle(0, 0, 1920, 1080));
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            _lobbySettings.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _background.Load(content);
            _lobbySettings.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _background.Recalculate();
            _lobbySettings.Recalculate();
        }
    }
}
