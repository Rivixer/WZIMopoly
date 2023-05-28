using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents the settings view in the game scene.
    /// </summary>
    internal class GUISettings : GUITexture
    {
        /// <summary>
        /// The model of the settings.
        /// </summary>
        private readonly SettingsModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUISettings"/> class.
        /// </summary>
        public GUISettings(SettingsModel model)
            : base("Images/SettingsScreen", new Rectangle(0, 0, 1920, 1080))
        {
            _model = model;
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
