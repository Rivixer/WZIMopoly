﻿using Microsoft.Xna.Framework;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the trade button view.
    /// </summary>
    internal sealed class GUITradeButton : GUIGameButton<TradeButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUITradeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the trade button.
        /// </param>
        internal GUITradeButton(TradeButtonModel model)
            : base(model, new Rectangle(1142, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            AuxText.Text = WZIMopoly.Language switch
            {
                Language.Polish => $"Wymień się z innym graczem.",
                Language.English => $"Trade with another player.",
                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
            };
        }
    }
}
