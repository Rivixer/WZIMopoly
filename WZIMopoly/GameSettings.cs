using System.Collections.Generic;
using WZIMopoly.Enums;
using WZIMopoly.Models;

namespace WZIMopoly
{
    /// <summary>
    /// Represents the game settings.
    /// </summary>
    internal static class GameSettings
    {
        /// <summary>
        /// Gets the players.
        /// </summary>
        /// <value>
        /// The list of all players. (4 players)
        /// </value>
        /// <remarks>
        /// This list of players contains all players,
        /// independently of their <see cref="PlayerType"/>.
        /// </remarks>
        public static List<PlayerModel> Players { get; } = new();

        /// <summary>
        /// Gets the active players.
        /// </summary>
        /// <value>
        /// The list of active players. (2 to 4 players)
        /// </value>
        /// <remarks>
        /// This list of players contains only the players
        /// that do not contain <see cref="PlayerType.None"/> type.
        /// </remarks>
        public static List<PlayerModel> ActivePlayers => Players.FindAll(x => x.PlayerType != PlayerType.None);
    }
}
