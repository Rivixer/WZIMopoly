namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the start game button model.
    /// </summary>
    internal class StartGameButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartGameButtonModel"/> class.
        /// </summary>
        public StartGameButtonModel()
            : base("LobbyStart") { }

        /// <inheritdoc/>
        public override void Update()
        {
            IsActive = GameSettings.ActivePlayers.Count >= 2;
        }
    }
}
