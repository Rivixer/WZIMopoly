namespace WZIMopoly.Models.SettingsScene
{
    /// <summary>
    /// Represents the return button model.
    /// </summary>
    internal class ReturnButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnButtonModel"/> class.
        /// </summary>
        public ReturnButtonModel()
            : base("Return")
        {
            IsActive = true;
        }
    }
}
