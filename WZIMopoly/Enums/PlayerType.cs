using System;

namespace WZIMopoly.Enums
{
    /// <summary>
    /// Represents the player type.
    /// </summary>
    [Flags]
    internal enum PlayerType
    {
        /// <summary>
        /// The player is not set.
        /// </summary>
        None = 1 << 0,

        /// <summary>
        /// The player plays locally.
        /// </summary>
        Local = 1 << 1,

        /// <summary>
        /// The player plays over the network but is not the host.
        /// </summary>
        OnlinePlayer = 1 << 2,

        /// <summary>
        /// The player is the host of network game.
        /// </summary>
        OnlineHostPlayer = 1 << 3,

        /// <summary>
        /// The player plays over the network.
        /// </summary>
        Online = OnlinePlayer | OnlineHostPlayer,

        /// <summary>
        /// The player is not a bot.
        /// </summary>
        NotBot = Local | Online,

        /// <summary>
        /// The player is a bot.
        /// </summary>
        Bot = 1 << 4,        
    }
}
