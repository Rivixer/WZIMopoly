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
        /// The array of default player models.
        /// </summary>
        private static readonly PlayerModel[] s_defaultPlayers = new PlayerModel[4]
        {
            new PlayerModel("Player1", "Red", PlayerType.Local),
            new PlayerModel("Player2", "Blue", PlayerType.None),
            new PlayerModel("Player3", "Green", PlayerType.None),
            new PlayerModel("Player4", "Yellow", PlayerType.None),
        };

        /// <summary>
        /// The current player index.
        /// </summary>
        private static int _currentPlayerIndex = 0;

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
        /// Gets the current player.
        /// </summary>
        public static PlayerModel CurrentPlayer => Players[_currentPlayerIndex];

        /// <summary>
        /// Changes the current player to the next one.
        /// </summary>
        public static void NextPlayer()
        {
            if (++_currentPlayerIndex >= ActivePlayers.Count)
            {
                _currentPlayerIndex = 0;
            }
        }

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

#nullable enable

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <remarks>
        /// The client is the player that is currently playing from this computer
        /// and it is used to identify the player in the online game.
        /// </remarks>
        public static int? ClientIndex { get; set; } = null;

        /// <summary>
        /// Gets the client.
        /// </summary>
        public static PlayerModel? Client => ClientIndex.HasValue ? Players[ClientIndex.Value] : Players[0];

#nullable disable

        /// <summary>
        /// Creates the players with default values.
        /// </summary>
        public static void CreatePlayers()
        {
            Players.Clear();
            foreach(var defPlayer in s_defaultPlayers)
            {
                Players.Add(new PlayerModel(defPlayer));
            }
        }

        /// <summary>
        /// Resets the players to default values.
        /// </summary>
        public static void ResetPlayers()
        {
            for (int i = 0; i < 4; i++)
            {
                Players[i].Reset();
            }
        }
    }
}
