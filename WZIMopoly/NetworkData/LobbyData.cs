using System;
using System.Collections.Generic;
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
    }
}
