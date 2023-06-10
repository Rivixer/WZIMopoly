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
        /// Gets or sets the game status.
        /// </summary>
        public GameStatus GameStatus { get; set; }

        /// <summary>
        /// Gets or privately sets the game start time.
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// Gets or privately sets the game end time.
        /// </summary>
        public DateTime EndTime { get; private set; }

        /// <summary>
        /// Gets the game time since the game started.
        /// </summary>
        public TimeSpan ActualTime => DateTime.Now - EndTime;

        /// <summary>
        /// Sets <see cref="StartTime"/> to the current time.
        /// </summary>
        public void SetStartTime()
        {
            StartTime = DateTime.Now;
            SetEndTime();
        }

        /// <summary>
        /// Sets <see cref="EndTime"/> to the ending time.
        /// </summary>
        private void SetEndTime()
        {
            EndTime = StartTime.AddMinutes(GameSettings.MatchDuration);
        }
    }
}
