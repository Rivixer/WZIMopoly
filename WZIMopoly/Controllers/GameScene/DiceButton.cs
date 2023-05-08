using WZIMopoly.Models;
using WZIMopoly.GUI;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a dice button controller.
    /// </summary>
    internal sealed class DiceButton : ButtonController
    {
        /// <summary>
        /// The sound effect of a rolling dice.
        /// </summary>
        SoundEffect soundEffect;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiceButton"/> class.
        /// </summary>
        /// <param name="view">
        /// The view of the dice button controller.
        /// </param>
        /// <param name="model">
        /// The model of the dice button controller.
        /// </param>
        internal DiceButton(ButtonModel model, GUIButton view)
            : base(model, view) { }

        /// <inheritdoc/>
        protected override void OnClick()
        {
            soundEffect.Play();
            base.OnClick();
        }

        /// <inheritdoc/>
        protected override void Load(ContentManager content)
        {
            base.Load(content);
            soundEffect = content.Load<SoundEffect>("Sounds/dice");
        }
    }
}
