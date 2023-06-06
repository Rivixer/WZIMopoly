using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the use elevator button view.
    /// </summary>
    internal class GUIUseElevatorButton : GUIButton<UseElevatorButtonModel>
    {
        /// <summary>
        /// The auxiliary text informing the player about what the button does.
        /// </summary>
        private readonly GUIText _text;

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
            _text = new GUIText("Fonts/WZIMFont", new Vector2(960, 720), Color.Black, GUIStartPoint.Center, scale: 0.3f);
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (IsHovered && Model.IsActive)
                _text.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            base.Load(content);

            _text.Load(content);
        }

        /// <inheritdoc/>
        public override void Recalculate()
        {
            base.Recalculate();

            _text.Recalculate();
            _text.Text = WZIMopoly.Language switch
            {
                Language.Polish => $"Użyj magicznych właściwości windy, teleportuj się do drugiej windy.",
                Language.English => $"Use magic powers of the elevator and teleport yourself to the second elevator.",
                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
            };
        }
    }
}
