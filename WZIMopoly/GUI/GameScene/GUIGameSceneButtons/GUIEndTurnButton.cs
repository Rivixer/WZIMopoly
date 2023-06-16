using Microsoft.Xna.Framework;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the end turn button view.
    /// </summary>
    internal sealed class GUIEndTurnButton : GUIGameButton<EndTurnButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIEndTurnButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the end turn button.
        /// </param>
        internal GUIEndTurnButton(EndTurnButtonModel model)
            : base(model, new Rectangle(882, 930, 160, 160), disableTexture: false)
        {
            SetButtonHoverArea(5, 0.8f);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            AuxText.Text = WZIMopoly.Language switch
            {
                Language.Polish => $"Zakończ swoją turę.",
                Language.English => $"End your turn.",
                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
            };
        }
    }
}
