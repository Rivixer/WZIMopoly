using Microsoft.Xna.Framework;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the exit button view.
    /// </summary>
    internal class GUIExitButton : GUIGameButton<ExitButtonModel>
    {
        /// <summary>
        /// Whether the button was clicked once while it was hovered.
        /// </summary>
        internal bool WasClickedOnce = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIExitButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the exit button.
        /// </param>
        public GUIExitButton(ExitButtonModel model)
            : base(model, new Rectangle(77, 320, 120, 120), disableTexture: false)
        {
            SetButtonHoverArea(5, 0.75f);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            if (WasClickedOnce)
            {
                AuxText.Color = Color.Red;
                AuxText.Text = WZIMopoly.Language switch
                {
                    Language.Polish => $"Kliknij ponownie jeśli jesteś pewien.",
                    Language.English => $"Click again if you are sure.",
                    _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                };
            }
            else
            {
                AuxText.Color = Color.Black;
                AuxText.Text = WZIMopoly.Language switch
                {
                    Language.Polish => $"Kliknij, aby wyjść z gry.",
                    Language.English => $"Click to exit the game.",
                    _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                };
            }
        }
    }
}
