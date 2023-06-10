using Microsoft.Xna.Framework;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the upgrade button view.
    /// </summary>
    internal sealed class GUIUpgradeButton : GUIGameButton<UpgradeButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIUpgradeButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the upgrade button.
        /// </param>
        internal GUIUpgradeButton(UpgradeButtonModel model)
            : base(model, new Rectangle(752, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            AuxText.Text = WZIMopoly.Language switch
            {
                Language.Polish => $"Ulepsz swoje przedmioty.",
                Language.English => $"Upgrade your subjects.",
                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
            };
        }
    }
}
