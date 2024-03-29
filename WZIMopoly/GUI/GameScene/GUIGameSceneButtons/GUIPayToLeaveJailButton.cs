﻿using Microsoft.Xna.Framework;
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
    /// Represents the leave jail button.
    /// </summary>
    internal class GUIPayToLeaveJailButton : GUIGameButton<PayToLeaveJailButtonModel>, IGUIGameUpdate
    {
        /// <summary>
        /// The tile that the player is currently on.
        /// </summary>
        private TileModel _currentTile;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIPayToLeaveJailButton"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the pay to leave jail button.
        /// </param>
        public GUIPayToLeaveJailButton(PayToLeaveJailButtonModel model)
            : base(model, new Rectangle(860, 923, 256, 88), GUIStartPoint.Right)
        {
            SetButtonHoverArea(5, 0.70f);
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
                    AuxText.Draw(spriteBatch);
                }
            }
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
                if (!t.CanPrisonerPayForRelease(player))
                {
                    AuxText.Text = WZIMopoly.Language switch
                    {
                        Language.Polish => $"Biednego studenta nie stać, potrzebujesz {t.PayForLeave}ECTS.",
                        Language.English => $"You cannot afford the deposit, you need {t.PayForLeave}ECTS.",
                        _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                    };
                }
                else
                {
                    AuxText.Text = WZIMopoly.Language switch
                    {
                        Language.Polish => $"Zapłać {t.PayForLeave}ECTS, aby wymknąć się z wykładu.",
                        Language.English => $"Pay {t.PayForLeave}ECTS to get out of mandatory lecture.",
                        _ => throw new ArgumentException($"Language not implemented: {WZIMopoly.Language}")
                    };
                }
            }
            else
            {
                AuxText.Text = string.Empty;
            }
        }
    }
}
