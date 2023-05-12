using System.Collections.Generic;
using System;
using WZIMopoly.Enums;
using Microsoft.Xna.Framework;

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
        /// Changes the current player to the next one.
        /// </summary>
        public void NextPlayer()
        {
            if (++_currentPlayerIndex >= Players.Count)
            {
                _currentPlayerIndex = 0;
            }
        }

        /// <summary>
        /// Gets the game start time.
        /// </summary>
        internal DateTime StartTime { get; private set; }

        /// <summary>
        /// Gets the game time since the application launched.
        /// </summary>
        internal TimeSpan ActualTime { get; private set; }

        /// <summary>
        /// Sets <see cref="StartTime"/> to the current time.
        /// </summary>
        internal void SetStartTime()
        {
            StartTime = DateTime.Now;
        }

        /// <summary>
        /// Updates the game current time.
        /// </summary>
        /// <param name="gameTime">
        /// The time since the application launched.
        /// </param>
        internal void UpdateTime(GameTime gameTime)
        {
            ActualTime = gameTime.TotalGameTime;
        }
    }
}
