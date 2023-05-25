using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;
using System;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a controller for game time.
    /// </summary>
    /// <remarks>
    /// Used to display timer such as
    /// time in the game scene.
    /// </remarks>
    internal class TimerController : Controller<TimerModel, GUITimer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimerController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the timer information.
        /// </param>
        /// <param name="view">
        /// The view of the timer information.
        /// </param>
        public TimerController(TimerModel model, GUITimer view)
            : base(model, view) { }

        /// <summary>
        /// Updates the current game time.
        /// </summary>
        /// <param name="actualTime">
        /// The actual time in the game.
        /// </param>
        public void UpdateTime(TimeSpan actualTime)
        {
            Model.UpdateTime(actualTime);
        }
    }
}
