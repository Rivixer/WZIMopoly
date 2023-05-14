﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the dice button view.
    /// </summary>
    internal sealed class GUIDiceButton : GUIButton<DiceButtonModel>, ISoundable
    {
        /// <summary>
        /// The sound effect of a rolling dice.
        /// </summary>
        private SoundEffect _soundEffect;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIDiceButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the dice button.
        /// </param>
        internal GUIDiceButton(DiceButtonModel model)
            : base(model, new Rectangle(882, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }

        /// <inheritdoc/>
        public void PlaySound()
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
