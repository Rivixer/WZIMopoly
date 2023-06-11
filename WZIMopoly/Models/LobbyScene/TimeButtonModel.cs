namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the time button model.
    /// </summary>
    internal class TimeButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeButtonModel"/> class.
        /// </summary>
        public TimeButtonModel()
            : base("LobbyTime") { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            IsActive = GameSettings.MaxGameTime is not null;
        }
    }
}
