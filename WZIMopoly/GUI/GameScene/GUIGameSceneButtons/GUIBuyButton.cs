using Microsoft.Xna.Framework;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the buy button view.
    /// </summary>
    internal sealed class GUIBuyButton : GUIGameButton<BuyButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIBuyButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the buy button.
        /// </param>
        internal GUIBuyButton(BuyButtonModel model)
            : base(model, new Rectangle(1012, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            AuxText.Text = WZIMopoly.Language switch
            {
                Language.Polish => $"Kup pole.",
                Language.English => $"Buy the tile.",
                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
            };
        }
    }
}
