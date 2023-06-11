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
        /// Gets the game end time.
        /// </summary>
        public DateTime? EndTime { get; private set; }

        /// <summary>
        /// Gets the time to end the game.
        /// </summary>
        public TimeSpan? TimeToEnd => EndTime - StartTime.Add(ActualTime);

        /// <summary>
        /// Sets <see cref="StartTime"/> to the current time.
        /// </summary>
        public void SetStartTime()
        {
            StartTime = DateTime.Now;
        }

        /// <summary>
        /// Sets <see cref="EndTime"/> to the current
        /// time plus <see cref="GameSettings.MaxGameTime"/>.
        /// </summary>
        /// <remarks>
        /// If <see cref="GameSettings.MaxGameTime"/:> is null,
        /// <see cref="EndTime"/> is set to null.
        /// </remarks>
        public void SetEndTime()
        {
            if (GameSettings.MaxGameTime is not null)
            {
                EndTime = StartTime.AddMinutes((double)GameSettings.MaxGameTime);
            }
            else
            {
                EndTime = null;
            }
        }

#if DEBUG
        /// <summary>
        /// Increases <see cref="GameSettings.MaxGameTime"/> by 1.
        /// </summary>
        public void IncreaseGameTime()
        {
            if (GameSettings.MaxGameTime != null)
            {
                GameSettings.MaxGameTime++;
                SetEndTime();
            }

        }

        /// <summary>
        /// Decreases <see cref="GameSettings.MaxGameTime"/> by 1.
        /// </summary>
        public void DecreaseGameTime()
        {
            if (GameSettings.MaxGameTime != null)
            {
                GameSettings.MaxGameTime--;
                SetEndTime();
            }
        }
#endif

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
