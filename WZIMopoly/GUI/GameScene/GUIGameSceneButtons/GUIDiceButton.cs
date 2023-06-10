using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.GameButtonModels;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the dice button view.
    /// </summary>
    internal sealed class GUIDiceButton : GUIGameButton<DiceButtonModel>, IGUIGameUpdate, ISoundable
    {
        /// <summary>
        /// The sound effect of a rolling dice.
        /// </summary>
        private SoundEffect _soundEffect;

        /// <summary>
        /// The player who is now taking a turn.
        /// </summary>
        private PlayerModel _currentPlayer;

        /// <summary>
        /// Whether the auxiliary text is visible.
        /// </summary>
        private bool _isAuxTextVisible;

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

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            GUITexture texture;
            if (WZIMopoly.GameType == GameType.Online
                && (!_currentPlayer?.Equals(GameSettings.Client) ?? false))
            {
                texture = TextureDisabled;
            }
            else
            {
                texture = _currentPlayer?.PlayerStatus switch
                {
                    PlayerStatus.BeforeRollingDice => IsHovered ? TextureHovered : Texture,
                    PlayerStatus.DuringRollingDice => TextureDisabled,
                    PlayerStatus.UpgradingTiles => TextureDisabled,
                    PlayerStatus.MortgagingTiles => TextureDisabled,
                    _ => null,
                };
            }
            texture?.Draw(spriteBatch);
            if (Model.IsActive && IsHovered && _isAuxTextVisible)
                AuxText.Draw(spriteBatch);
        }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            _currentPlayer = player;
            _isAuxTextVisible = !(tile is MandatoryLectureTileModel t && t.IsPrisoner(_currentPlayer));

            AuxText.Text = WZIMopoly.Language switch
            {
                Language.Polish => $"Rzuć kostkami.",
                Language.English => $"Roll the dice.",
                _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
            };
        }
    }
}
