﻿namespace WZIMopoly.Enums
{
    /// <summary>
    /// Represents the player status.
    /// </summary>
    internal enum PlayerStatus
    {
        /// <summary>
        /// The player is waiting for his turn.
        /// </summary>
        WaitingForTurn,

        /// <summary>
        /// The player has not rolled the dice yet.
        /// </summary>
        BeforeRollingDice,

        /// <summary>
        /// The player is rolling the dice.
        /// </summary>
        DuringRollingDice,

        /// <summary>
        /// The player is upgrading his fields.
        /// </summary>
        UpgradingFields,

        /// <summary>
        /// The player has already rolled the dice.
        /// </summary>
        AfterRollingDice,
    }
}
