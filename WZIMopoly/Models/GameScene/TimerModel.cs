using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using WZIMopoly.Enums;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a timer model.
    /// </summary>
    internal class TimerModel : GameModel
    {
        /// <summary>
        /// Gets or privately sets the time.
        /// </summary>
        internal TimeSpan Time { get; private set; }

        /// <summary>
        /// Gets or privately sets the default rectangle of the timer background texture.
        /// </summary>
        internal Rectangle DefRectangle { get; private set; }

        /// <summary>
        /// Gets or privately sets the GUI start point of the timer texture.
        /// </summary>
        /// <remarks>
        /// Used to determine the place for which <see cref="DefRectangle"/> is specified.
        /// </remarks>
        internal GUIStartPoint StartPoint { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerModel"/> class.
        /// </summary>
        /// <param name="time">
        /// The player model.
        /// </param>
        /// <param name="defRect">
        /// The default rectangle of the timer background texture.
        /// </param>
        /// <param name="startPoint">
        /// The start point that determines the place for which <paramref name="defRect"> is specified.
        /// </param>
        internal TimerModel(TimeSpan time, Rectangle defRect, GUIStartPoint startPoint)
        {
            Time = time;
            DefRectangle = defRect;
            StartPoint = startPoint;
        }

        /// <summary>
        /// Updates the current game time.
        /// </summary>
        internal void UpdateTime()
        {
            Time = ActualTime;
        }
    }
}
