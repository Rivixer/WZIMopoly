using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WZIMopoly.GUI
{
    /// <summary>
    /// Represents the settings view in the game scene.
    /// </summary>
    internal class SettingsView : GUITexture
    {
        /// <summary>
        /// The background of the settings view.
        /// </summary>
        private readonly GUITexture _background;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsView"/> class.
        /// </summary>
        public SettingsView()
            : base("Images/SettingsScreen", new Rectangle(0, 0, 1920, 1080))
        {
            _background = new GUITexture("Images/MenuScreen", new Rectangle(0, 0, 1920, 1080));
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            _background.Load(content);
            base.Load(content);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            _background?.Recalculate();
            base.Recalculate();
        }
    }
}
