using System;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a timer model.
    /// </summary>
    internal class TimerModel : Model
    {
        /// <summary>
        /// Gets or privately sets the time.
        /// </summary>
        /// <value>
        /// The time since the game started.
        /// </value>
        public TimeSpan Time { get; private set; } = TimeSpan.Zero;

        /// <summary>
        /// Updates <see cref="Time"/> with the current game time.
        /// </summary>
        /// <param name="actualTime">
        /// The current time.
        /// </param>
        public void UpdateTime(TimeSpan actualTime)
        {
            Time = actualTime;
        }
    }
}
