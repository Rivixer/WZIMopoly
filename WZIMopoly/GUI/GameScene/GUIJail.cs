using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WZIMopoly.Controllers.GameScene.GameSceneButtonControllers;
using WZIMopoly.Enums;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Represents the view of the jail.
    /// </summary>
    internal class GUIJail : GUIText, IGUIGameUpdate
    {
        /// <summary>
        /// The pay to leave jail button.
        /// </summary>
        private readonly PayToLeaveJailButtonController _payToLeaveJailBtn;

        /// <summary>
        /// The use card to leave jail button.
        /// </summary>
        private readonly UseCardToLeaveJailButtonController _useCardToLeaveJailBtn;

        /// <summary>
        /// The tile that the player is currently on.
        /// </summary>
        private TileModel _currentTile;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIJail"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the jail.
        /// </param>
        internal GUIJail(JailModel model)
            : base("Fonts/WZIMFont", new Vector2(960, 720), Color.Black, GUIStartPoint.Center, scale: 0.3f)
        {
            _payToLeaveJailBtn = model.GetController<PayToLeaveJailButtonController>();
            _useCardToLeaveJailBtn = model.GetController<UseCardToLeaveJailButtonController>();
        }

        /// <inheritdoc/>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (WZIMopoly.GameType == GameType.Online
                && !GameSettings.CurrentPlayer.Equals(GameSettings.Client))
            {
                return;
            }
            if (_currentTile is MandatoryLectureTileModel t
                && (!_payToLeaveJailBtn.View.IsHovered
                && !_useCardToLeaveJailBtn.View.IsHovered
                || !t.IsPrisoner(GameSettings.CurrentPlayer)))
            {
                base.Draw(spriteBatch);
            }
        }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            _currentTile = tile;
        }

        /// <remarks>
        /// <para>
        /// This method is called one per frame.
        /// </para>
        /// <para>
        /// Sets the auxiliary text informing
        /// the player about the situation in the jail.
        /// </para>
        /// </remarks>
        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            var player = GameSettings.CurrentPlayer;
            if (player.PlayerStatus == PlayerStatus.BeforeRollingDice && _currentTile is MandatoryLectureTileModel t)
            {
                if (t.IsPrisoner(player))
                {
                    var turnsLeft = t.GetRemainingTurns(player);
                    Text = WZIMopoly.Language switch
                    {
                        Language.Polish => turnsLeft == 1 ? "Została 1 kolejka czekania." : $"Zostały {turnsLeft} kolejki czekania.",
                        Language.English => $"{turnsLeft} turns left.",
                        _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                    };
                    Text += WZIMopoly.Language switch
                    {
                        Language.Polish => " Wyrzuć dublet, aby wyjść wcześniej.",
                        Language.English => " Roll a double to leave early.",
                        _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                    };
                }
                else
                {
                    Text = WZIMopoly.Language switch
                    {
                        Language.Polish => "Koniec wykładu!",
                        Language.English => $"The lecture has been ended!",
                        _ => string.Empty,
                    };
                }
            }
            else
            {
                Text = string.Empty;
            }
        }
    }
}
