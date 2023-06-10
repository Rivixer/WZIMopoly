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
            : base("Upgrade")
        {
            IsActive = true;
        }
    }
}
