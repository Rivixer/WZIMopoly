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
        /// Gets or sets the list of players.
        /// </summary>
        internal List<PlayerModel> Players { get; set; } = new();

        /// <summary>
        /// Gets the current player.
        /// </summary>
        internal PlayerModel CurrentPlayer => Players[_currentPlayerIndex];

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
            if (++_currentPlayerIndex >= Players.Count)
            {
                _currentPlayerIndex = 0;
            }
            CurrentPlayer.PlayerStatus = PlayerStatus.BeforeRollingDice;
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
