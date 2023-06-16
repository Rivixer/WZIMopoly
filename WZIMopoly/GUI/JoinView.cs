using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents the join view.
    /// </summary>
    internal class JoinView : GUIElement
    {
        /// <summary>
        /// The background of the join scene.
        /// </summary>
        private readonly GUITexture _background;

        /// <summary>
        /// The texture with settings of the game.
        /// </summary>
        private readonly GUITexture _joinSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinView"/> class.
        /// </summary>
        public JoinView()
        {
            _background = new GUITexture("Images/MenuScreen", new Rectangle(0, 0, 1920, 1080));
            _joinSettings = new GUITexture("Images/JoinScreen", new Rectangle(0, 0, 1920, 1080));
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            _joinSettings.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _background.Load(content);
            _joinSettings.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _background.Recalculate();
            _joinSettings.Recalculate();
        }
    }
}
