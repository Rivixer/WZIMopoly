using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using WZIMopoly.Models;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents a dice button view.
    /// </summary>
    internal sealed class GUIDiceButton : GUIButton, ISoundable
    {
        /// <summary>
        /// The sound effect of a rolling dice.
        /// </summary>
        private SoundEffect _soundEffect;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIDiceButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the button.
        /// </param>
        internal GUIDiceButton(ButtonModel model)
            : base(model, new Rectangle(882, 930, 160, 160)) { }

        /// <inheritdoc/>
        void ISoundable.PlaySound()
        {
            _soundEffect.Play();
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            base.Load(content);
            _soundEffect = content.Load<SoundEffect>($"Sounds/{Model.Name}");
        }
    }
}
