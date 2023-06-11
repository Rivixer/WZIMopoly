namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the add time button model.
    /// </summary>
    internal class AddTimeButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddTimeButtonModel"/> class.
        /// </summary>
        public AddTimeButtonModel()
            : base("Plus") { }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            IsActive = GameSettings.MaxGameTime is not null;
        }
    }
}
