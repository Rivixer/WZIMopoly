using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.GameButtonModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the mortgage button view.
    /// </summary>
    internal sealed class GUIMortgageButton : GUIGameButton<MortgageButtonModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIMortgageButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the mortgage button.
        /// </param>
        internal GUIMortgageButton(MortgageButtonModel model)
            : base(model, new Rectangle(622, 930, 160, 160))
        {
            SetButtonHoverArea(5, 0.8f);
        }

        /// <inheritdoc/>
        public override void Update()
        {
            AuxText.Text = WZIMopoly.Language switch
            {
                Language.Polish => $"Zastaw pole lub sprzedaj ocenę przedmiotu.",
                Language.English => $"Mortgage a tile or sell a subject grade.",
                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
#nullable enable
            GUITexture? texture;
#nullable disable

            if (!Model.IsActive)
            {
                texture = TextureDisabled;
            }
            else if (IsHovered)
            {
                texture = TextureHovered ?? Texture;
                if (GameSettings.CurrentPlayer.PlayerStatus != PlayerStatus.MortgagingTiles)
                    AuxText.Draw(spriteBatch);
            }
            else
            {
                texture = Texture;
            }
            texture?.Draw(spriteBatch);
        }
    }
}
