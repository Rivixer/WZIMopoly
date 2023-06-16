using System;

namespace WZIMopoly.Models.EndGameScene
{
    /// <summary>
    /// Represents the return to menu button model.
    /// </summary>
    internal class ReturnToMenuButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnToMenuButtonModel"/> class.
        /// </summary>
        public ReturnToMenuButtonModel()
            : base("SettingsExit") { }

        /// <summary>
        /// Gets or sets the time to enter the end game scene.
        /// </summary>
        public DateTime? EnterTime { get; set; } = null;

        /// <inheritdoc/>
        public override void Update()
        {
            IsActive = EnterTime is not null && EnterTime?.AddSeconds(3) < DateTime.Now;
        }
    }
}
