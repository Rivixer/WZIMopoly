namespace WZIMopoly.Models.JoinScene
{
    /// <summary>
    /// Represents the join button model.
    /// </summary>
    internal class JoinButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinButtonModel"/> class.
        /// </summary>
        public JoinButtonModel()
            : base("MenuJoin") { }

        /// <summary>
        /// Updates the activity of the join button.
        /// </summary>
        /// <param name="lobbyCodeModel">
        /// The lobby code model.
        /// </param>
        /// <param name="playerNickModel">
        /// The player nick model.
        /// </param>
        /// <remarks>
        /// The join button is active when the lobby code is
        /// 6 characters long and the player nick is not empty.
        /// </remarks>
        public void UpdateActivity(LobbyCodeModel lobbyCodeModel, PlayerNickModel playerNickModel)
        {
            IsActive = lobbyCodeModel.LobbyCode.Length == 6 && playerNickModel.PlayerNick.Length > 0;
        }
    }
}
