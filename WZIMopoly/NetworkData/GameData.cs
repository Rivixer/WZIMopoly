using System;
using System.Collections.Generic;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.NetworkData
{
    /// <summary>
    /// Represents the data structure for game information.
    /// </summary>
    [Serializable]
    internal class GameData : NetData
    {
        /// <summary>
        /// Gets or sets the list of active players in the game.
        /// </summary>
        public List<PlayerModel> ActivePlayers { get; set; }

        /// <summary>
        /// Gets or sets the current player index.
        /// </summary>
        public int CurrentPlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the dice model.
        /// </summary>
        public DiceModel DiceModel { get; set; }

        /// <summary>
        /// Gets or sets the list of tiles.
        /// </summary>
        public List<TileModel> Tiles { get; set; }
    }
}
