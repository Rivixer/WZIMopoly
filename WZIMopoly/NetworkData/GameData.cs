using System;
using System.Collections.Generic;
using WZIMopoly.Controllers.GameScene;
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

        public int CurrentPlayerIndex { get; set; }

        public DiceModel DiceModel { get; set; }

        public List<TileModel> Tiles { get; set; }
    }
}
