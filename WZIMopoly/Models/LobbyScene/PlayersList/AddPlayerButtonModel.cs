using WZIMopoly.Enums;

namespace WZIMopoly.Models.LobbyScene.PlayersList
{
    /// <summary>
    /// Represents the model of the add player button.
    /// </summary>
    internal class AddPlayerButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddPlayerButtonModel"/> class.
        /// </summary>
        /// <param name="player">
        /// The player model.
        /// </param>
        public AddPlayerButtonModel(PlayerModel player)
            : base("PlayerAdd")
        {
            Player = player;
        }

        /// <summary>
        /// Gets or sets the player model.
        /// </summary>
        public PlayerModel Player { get; set; }

        /// <inheritdoc/>
        public override void Update()
        {
            var playerIsNone = Player.PlayerType == PlayerType.None;
            var gameIsLocal = WZIMopoly.GameType == GameType.Local;
            IsActive = playerIsNone && gameIsLocal && !PreviousPlayerIsNone();
            base.Update();
        }

        /// <summary>
        /// Checks if the previous player is none.
        /// </summary>
        /// <returns>
        /// True if the previous player is none, otherwise false.
        /// </returns>
        private bool PreviousPlayerIsNone()
        {
            bool previousPlayerIsNone;
            int currentIndex = GameSettings.Players.IndexOf(Player);
            if (currentIndex > 0)
            {
                PlayerModel previousPlayer = GameSettings.Players[currentIndex - 1];
                previousPlayerIsNone = previousPlayer.PlayerType == PlayerType.None;
            }
            else
            {
                previousPlayerIsNone = false;
            }
            return previousPlayerIsNone;
        }
    }
}
