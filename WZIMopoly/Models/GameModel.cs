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
        /// Whether the time was stopped in prevoius frame.
        /// </summary>
        private bool _wasTimeStopped;

        /// <summary>
        /// Gets or sets the game status.
        /// </summary>
        public GameStatus GameStatus { get; set; }

        /// <summary>
        /// Gets or privately sets the game start time.
        /// </summary>
        /// <remarks>
        /// The time of pauses is added to the start time.
        /// </remarks>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// Gets the game time since the game started, not including pauses.
        /// </summary>
        public TimeSpan ActualTime { get; private set; }

        /// <summary>
        /// Sets <see cref="StartTime"/> to the current time.
        /// </summary>
        public void SetStartTime()
        {
            StartTime = DateTime.Now;
            SetEndTime();
        }

        /// <inheritdoc/>
        public override void Update()
        {
            if (GameStatus == GameStatus.Running)
            {
                if (_wasTimeStopped)
                {
                    StartTime = DateTime.Now - ActualTime;
                    _wasTimeStopped = false;
                }
                else
                {
                    ActualTime = DateTime.Now - StartTime;
                }
            }
            else
            {
                _wasTimeStopped = true;
            }
        }
    }
}
