using System.Collections.Generic;
using System;
using WZIMopoly.Enums;

namespace WZIMopoly.Models
{
    /// <summary>
    /// Represents a game model.
    /// </summary>
    internal class GameModel : Model
    {
        /// <summary>
        /// The current player index.
        /// </summary>
        private short _currentPlayerIndex = 0;

        /// <summary>
        /// Gets or sets the game status.
        /// </summary>
        internal GameStatus GameStatus { get; set; }

        /// <summary>
        /// Gets the current player.
        /// </summary>
        internal PlayerModel CurrentPlayer => GameSettings.Players[_currentPlayerIndex];

        /// <summary>
        /// Gets or privately sets the game start time.
        /// </summary>
        internal DateTime StartTime { get; private set; }

        /// <summary>
        /// Gets the game time since the game started.
        /// </summary>
        internal TimeSpan ActualTime => DateTime.Now - StartTime;

        /// <summary>
        /// Changes the current player to the next one.
        /// </summary>
        internal void NextPlayer()
        {
            if (++_currentPlayerIndex >= GameSettings.ActivePlayers.Count)
            {
                _currentPlayerIndex = 0;
            }
        }

        /// <summary>
        /// Sets <see cref="StartTime"/> to the current time.
        /// </summary>
        internal void SetStartTime()
        {
            StartTime = DateTime.Now;
        }
    }
}
