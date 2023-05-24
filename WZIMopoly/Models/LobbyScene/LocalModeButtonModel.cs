namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the local model button model.
    /// </summary>
    internal class LocalModeButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalModeButtonModel"/> class.
        /// </summary>
        public LocalModeButtonModel()
            : base("LobbyLocal")
        {
            IsActive = true;
        }
    }
}
