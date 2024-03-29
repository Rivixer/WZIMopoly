﻿using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene.GameButtonModels
{
    /// <summary>
    /// Represents the dice button model.
    /// </summary>
    internal sealed class DiceButtonModel : ButtonModel, IGameUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiceButtonModel"/> class.
        /// </summary>
        internal DiceButtonModel()
            : base("Dice") { }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            IsActive = player.PlayerStatus == PlayerStatus.BeforeRollingDice
                && (WZIMopoly.GameType == GameType.Online && player == GameSettings.Client || WZIMopoly.GameType == GameType.Local);
        }
    }
}
