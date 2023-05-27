namespace WZIMopoly.Enums
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
        /// The player is upgrading his tiles.
        /// </summary>
        UpgradingTiles,

        /// <summary>
        /// The player is mortgaging his tiles.
        /// </summary>
        MortgagingTiles,

        /// <summary>
        /// The player has already rolled the dice.
        /// </summary>
        AfterRollingDice,
    }
}
