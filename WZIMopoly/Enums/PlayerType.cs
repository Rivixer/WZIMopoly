namespace WZIMopoly.Enums
{
    /// <summary>
    /// Represents the player type.
    /// </summary>
    internal enum PlayerType
    {
        /// <summary>
        /// The player plays locally.
        /// </summary>
        Local,

        /// <summary>
        /// The player plays over the network.
        /// </summary>
        Online,

        /// <summary>
        /// The player is a bot.
        /// </summary>
        Bot,

        /// <summary>
        /// The player is not set.
        /// </summary>
        None,
    }
}
