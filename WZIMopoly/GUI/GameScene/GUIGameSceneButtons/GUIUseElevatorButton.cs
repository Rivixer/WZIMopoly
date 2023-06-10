using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the use elevator button view.
    /// </summary>
    internal class GUIUseElevatorButton : GUIGameButton<UseElevatorButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIUseElevatorButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the use elevator button.
        /// </param>
        public GUIUseElevatorButton(UseElevatorButtonModel model) 
            : base(model, new Rectangle(860, 923, 256, 88), GUIStartPoint.Right, disableTexture: false)
        {
            SetButtonHoverArea(5, 0.75f);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (IsHovered && Model.IsActive)
                AuxText.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            base.Recalculate();

            AuxText.Recalculate();
            AuxText.Text = WZIMopoly.Language switch
            {
                Language.Polish => $"Użyj magicznych właściwości windy, teleportuj się do drugiej windy.",
                Language.English => $"Use magic powers of the elevator and teleport yourself to the second elevator.",
                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
            };
        }
    }
}
