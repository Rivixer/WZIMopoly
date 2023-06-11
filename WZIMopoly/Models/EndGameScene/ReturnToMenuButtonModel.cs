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
            : base("SettingsExit")
        {
            IsActive = true;
        }
    }
}
