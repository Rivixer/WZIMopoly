namespace WZIMopoly.Models.LobbyScene.PlayersList
{
    /// <summary>
    /// Represents the model of the player box in lobby.
    /// </summary>
    internal class LobbyPlayerModel : Model
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LobbyPlayerModel"/> class.
        /// </summary>
        /// <param name="player">
        /// The player model.
        /// </param>
        public LobbyPlayerModel(PlayerModel player)
        {
            Player = player;
        }

        /// <summary>
        /// Gets or sets the player model.
        /// </summary>
        public PlayerModel Player { get; set; }
    }
}
