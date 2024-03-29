﻿using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene.GameSceneButtonModels
{
    /// <summary>
    /// Represents the end turn button model.
    /// </summary>
    internal class EndTurnButtonModel : ButtonModel, IGameUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndTurnButtonModel"/> class.
        /// </summary>
        internal EndTurnButtonModel()
            : base("EndTurn") { }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            IsActive = (player.PlayerStatus == PlayerStatus.AfterRollingDice)
                && (WZIMopoly.GameType == GameType.Online && player == GameSettings.Client || WZIMopoly.GameType == GameType.Local);
        }
    }
}
