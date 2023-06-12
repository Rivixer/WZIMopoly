using Microsoft.Xna.Framework;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents a settings button view.
    /// </summary>
    internal class GUISettingsButton : GUIGameButton<SettingsButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUISettingsButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the settings button.
        /// </param>
        internal GUISettingsButton(SettingsButtonModel model)
            : base(model, new Rectangle(77, 200, 120, 120), disableTexture: false)
        {
            SetButtonHoverArea(5, 0.7f);
            AuxText.SetNewDefPosition(new Vector2(AuxText.DefaultPosition.X, AuxText.DefaultPosition.Y - 30), AuxText.StartPoint);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            AuxText.Text = WZIMopoly.Language switch
            {
                Language.Polish => $"Otwórz ustawienia.",
                Language.English => $"Open settings.",
                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
            };
        }
    }
}
