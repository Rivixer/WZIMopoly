using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.GUI.GameScene.GUIGameSceneButtons
{
    /// <summary>
    /// Represents the use card to leave jail button.
    /// </summary>
    internal class GUIUseCardToLeaveJailButton : GUIButton<UseCardToLeaveJailButtonModel>, IGUIGameUpdate
    internal class GUIUseCardToLeaveJailButton : GUIGameButton<UseCardToLeaveJailButtonModel>, IGUIGameUpdate
    {
        /// <summary>
        /// The auxiliary text informing the player about the action of the button.
        /// </summary>
        private readonly GUIText _text;

        /// <summary>
        /// The tile that the player is currently on.
        /// </summary>
        private TileModel _currentTile;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIUseCardToLeaveJailButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the use card to leave jail button.
        /// </param>
        public GUIUseCardToLeaveJailButton(UseCardToLeaveJailButtonModel model)
            : base(model, new Rectangle(1060, 923, 256, 88), GUIStartPoint.Left)
        {
            SetButtonHoverArea(5, 0.70f);
            _text = new GUIText("Fonts/WZIMFont", new Vector2(960, 720), Color.Black, GUIStartPoint.Center, scale: 0.3f);
        }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            _currentTile = tile;
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (WZIMopoly.GameType == GameType.Online
                && !GameSettings.CurrentPlayer.Equals(GameSettings.Client))
            {
                return;
            }
            if (_currentTile is MandatoryLectureTileModel t)
            {
                if (t.IsPrisoner(GameSettings.CurrentPlayer))
                {
                    base.Draw(spriteBatch);
                }
                if (IsHovered)
                {
                    _text.Draw(spriteBatch);
                }
            }
        }

        /// <inheritdoc/>
        public override void Load(ContentManager content)
        {
            base.Load(content);
            _text.Load(content);
        }

        /// <remarks>
        /// <para>
        /// This method is called one per frame.
        /// </para>
        /// <para>
        /// Sets the auxiliary text informing
        /// the player about the action of the button.
        /// </para>
        /// </remarks>
        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            var player = GameSettings.CurrentPlayer;
            if (player.PlayerStatus == PlayerStatus.BeforeRollingDice
                && _currentTile is MandatoryLectureTileModel t
                && IsHovered && t.IsPrisoner(player))
            {
                if (player.NumberOfLeaveJailCards == 0)
                {
                    _text.Text = WZIMopoly.Language switch
                    {
                        Language.Polish => $"Nie masz żadnego usprawiedliwienia, siedź na wykładzie!",
                        Language.English => $"You have no excuse, stay at the lecture!",
                        _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                    };
                }
                else
                {
                    _text.Text = WZIMopoly.Language switch
                    {
                        Language.Polish => $"Skorzystaj z usprawiedliwienia i wyjdź z wykładu (pozostało {player.NumberOfLeaveJailCards})",
                        Language.English => $"Use an excuse and leave the lecture. {player.NumberOfLeaveJailCards}",
                        _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                    };
                }
            }
            else
            {
                _text.Text = string.Empty;
            }
        }
    }
}
