using System.Collections.Generic;
using System;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Enums;

namespace WZIMopoly.Models
{
    /// <summary>
    /// Represents a game model.
    /// </summary>
    internal class GameModel : Model
    {
        /// <summary>
        /// Gets or sets the map controller.
        /// </summary>
        internal MapController MapController { get; set; }

        /// <summary>
        /// Gets or sets the game status.
        /// </summary>
        internal GameStatus GameStatus { get; set; }

        /// <summary>
        /// Gets or sets the list of players.
        /// </summary>
        internal List<Player> Players { get; set; } = new();

        /// <summary>
        /// Gets the game start time.
        /// </summary>
        internal DateTime StartTime { get; private set; }

        /// <summary>
        /// Sets <see cref="StartTime"/> to the current time.
        /// </summary>
        internal void SetStartTime()
        {
            StartTime = DateTime.Now;
        }
    }
}
