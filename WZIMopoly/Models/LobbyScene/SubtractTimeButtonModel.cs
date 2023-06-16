﻿namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the subtract time button model.
    /// </summary>
    internal class SubtractTimeButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractTimeButtonModel"/> class.
        /// </summary>
        public SubtractTimeButtonModel()
            : base("Minus") { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            IsActive = GameSettings.MaxGameTime is not null;
        }
    }
}
