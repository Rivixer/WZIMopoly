using System;
using System.Collections.Generic;
using WZIMopoly.Enums;
using WZIMopoly.Models;

namespace WZIMopoly.NetworkData
{
    /// <summary>
    /// Represents the data structure for lobby information.
    /// </summary>
    [Serializable]
    internal class LobbyData : NetData
    {
        /// <summary>
        /// Gets or sets the list of players in the lobby.
        /// </summary>
        public List<PlayerModel> Players { get; set; }

        /// <summary>
        /// Gets or sets the game end type.
        /// </summary>
        public GameEndType GameEndType { get; set; }

        /// <summary>
        /// Gets or sets the maximum game time.
        /// </summary>
        public int? MaxGameTime { get; set; }
    }
}
