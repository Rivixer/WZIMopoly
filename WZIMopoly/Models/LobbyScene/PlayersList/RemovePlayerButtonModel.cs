﻿using WZIMopoly.Enums;

namespace WZIMopoly.Models.LobbyScene.PlayersList
{
    /// <summary>
    /// Represents the model of the remove player button.
    /// </summary>
    internal class RemovePlayerButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemovePlayerButtonModel"/> class.
        /// </summary>
        /// <param name="player">
        /// The player model.
        /// </param>
        public RemovePlayerButtonModel(PlayerModel player)
            : base("PlayerX")
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
            bool playerIsNotLocal = Player.PlayerType != PlayerType.Local;
            bool playerIsHost = GameSettings.Players.IndexOf(Player) == 0;
            IsActive = !playerIsNotLocal && !playerIsHost && PlayerIsLast();
        }

        /// <summary>
        /// Checks if the player is added last.
        /// </summary>
        /// <returns>
        /// True if the player is added last, otherwise false.
        /// </returns>
        private bool PlayerIsLast()
        {
            int activePlayers = GameSettings.ActivePlayers.Count;
            int currentIndex = GameSettings.Players.IndexOf(Player);
            return currentIndex == activePlayers - 1;
        }
    }
}
