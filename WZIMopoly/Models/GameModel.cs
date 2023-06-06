using System;
using WZIMopoly.Enums;

namespace WZIMopoly.Models
{
    /// <summary>
    /// Represents a game model.
    /// </summary>
    [Serializable]
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
        /// Gets the game time since the game started.
        /// </summary>
        public TimeSpan ActualTime => DateTime.Now - StartTime;

        /// <summary>
        /// Sets <see cref="StartTime"/> to the current time.
        /// </summary>
        public void SetStartTime()
        {
            StartTime = DateTime.Now;
        }
    }
}
