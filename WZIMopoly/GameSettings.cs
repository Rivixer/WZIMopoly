using System.Collections.Generic;
using System.Linq;
using WZIMopoly.Enums;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;
using WZIMopoly.NetworkData;
using WZIMopolyNetworkingLibrary;
using System;

namespace WZIMopoly
{
    /// <summary>
    /// Represents the game settings.
    /// </summary>
    internal static class GameSettings
    {
        /// <summary>
        /// The array of default player models.
        /// </summary>
        private static readonly PlayerModel[] s_defaultPlayers = new PlayerModel[4]
        {
            new PlayerModel("Player1", "Red", PlayerType.Local),
            new PlayerModel("Player2", "Blue", PlayerType.None),
            new PlayerModel("Player3", "Green", PlayerType.None),
            new PlayerModel("Player4", "Yellow", PlayerType.None),
        };

        /// <summary>
        /// The current player index.
        /// </summary>
        private static int _currentPlayerIndex = 0;

        /// <summary>
        /// The temporary current player index.
        /// </summary>
        private static int? _tempCurrentPlayerIndex;

        /// <summary>
        /// Gets and sets the game end type.
        /// </summary>
        public static GameEndType GameEndType { get; set; } = GameEndType.LastNotBankrupt;

        /// <summary>
        /// Gets and sets the max game time measured in minutes.
        /// </summary>
        public static int? MaxGameTime { get; set; } = 20;

        /// <summary>
        /// Gets the players.
        /// </summary>
        /// <value>
        /// The list of all players. (4 players)
        /// </value>
        /// <remarks>
        /// This list of players contains all players,
        /// independently of their <see cref="PlayerType"/>.
        /// </remarks>
        public static List<PlayerModel> Players { get; } = new();

        /// <summary>
        /// Gets the current player.
        /// </summary>
        public static PlayerModel CurrentPlayer => Players[_currentPlayerIndex];

        /// <summary>
        /// Changes the current player to the next one.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the current player is the last one,
        /// the first player will be selected.
        /// </para>
        /// <para>
        /// If the current player is bankrupt,
        /// the next non-bankrupt player will be selected.
        /// </para>
        /// </remarks>
        public static void NextPlayer()
        {
            if (++_currentPlayerIndex >= ActivePlayers.Count)
            {
                _currentPlayerIndex = 0;
            }
            if (ActivePlayers[_currentPlayerIndex].PlayerStatus == PlayerStatus.Bankrupt)
            {
                NextPlayer();
            }
        }

        /// <summary>
        /// Gets the active players.
        /// </summary>
        /// <value>
        /// The list of active players. (2 to 4 players)
        /// </value>
        /// <remarks>
        /// This list of players contains only the players
        /// that do not contain <see cref="PlayerType.None"/> type.
        /// </remarks>
        public static List<PlayerModel> ActivePlayers => Players.FindAll(x => x.PlayerType != PlayerType.None);

#nullable enable

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <remarks>
        /// The client is the player that is currently playing from this computer
        /// and it is used to identify the player in the online game.
        /// </remarks>
        public static int? ClientIndex { get; set; } = null;

        /// <summary>
        /// Gets the client.
        /// </summary>
        public static PlayerModel? Client => ClientIndex.HasValue ? Players[ClientIndex.Value] : Players[0];

#nullable disable

        /// <summary>
        /// Sets the temporary player as the current player.
        /// </summary>
        /// <param name="player">
        /// The player to be set as the temporary player.
        /// </param>
        public static void SetTemporaryPlayerAsCurrent(PlayerModel player)
        {
            _tempCurrentPlayerIndex = _currentPlayerIndex;
            _currentPlayerIndex = Players.IndexOf(player);
        }

        /// <summary>
        /// Restores the current player from the temporary player.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// The temporary current player index is not set.
        /// </exception>
        public static void RestoreCurrentPlayer()
        {
            if (_tempCurrentPlayerIndex == null)
            {
                throw new ArgumentException("Temporary current player index not set.");
            }
            else
            {
                _currentPlayerIndex = (int)_tempCurrentPlayerIndex;
            }
        }

        /// <summary>
        /// Creates the players with default values.
        /// </summary>
        public static void CreatePlayers()
        {
            Players.Clear();
            foreach (var defPlayer in s_defaultPlayers)
            {
                Players.Add(new PlayerModel(defPlayer));
            }
        }

        /// <summary>
        /// Resets the players to default values.
        /// </summary>
        public static void ResetPlayers()
        {
            for (int i = 0; i < 4; i++)
            {
                Players[i].Reset();
            }
        }

        /// <summary>
        /// Sends the game data to the server.
        /// </summary>
        /// <param name="model">
        /// The game model from which the data will be sent.
        /// </param>
        public static void SendGameData(GameModel model)
        {
            if (WZIMopoly.GameType == GameType.Online)
            {
                var gameData = new GameData()
                {
                    ActivePlayers = ActivePlayers,
                    CurrentPlayerIndex = _currentPlayerIndex,
                    TempCurrentPlayerIndex = _tempCurrentPlayerIndex,
                    DiceModel = model.GetModel<DiceModel>(),
                    Tiles = model.GetAllModelsRecursively<TileModel>(),
                    TradeModel = model.GetModel<TradeModel>(),
                };
                var data = new byte[] { (byte)PacketType.GameStatus };
                data = data.Concat(gameData.ToByteArray()).ToArray();
                NetworkService.Connection.Send(data, 0, data.Length);
            }
        }

        /// <summary>
        /// Updates the game status using data from the server.
        /// </summary>
        /// <param name="data">
        /// The game data received from the server.
        /// </param>
        /// <param name="model">
        /// The game model to be updated.
        /// </param>
        public static void UpdateGameData(GameData data, GameModel model)
        {
            // Update tiles on the map
            var tiles = model.GetAllModelsRecursively<TileModel>();
            for (int i = 0; i < 40; i++)
            {
                tiles[i].Update(data.Tiles[i]);
            }

            // Update players
            for (int i = 0; i < ActivePlayers.Count; i++)
            {
                Players[i].Update(data.ActivePlayers[i], tiles);
            }

            // Update the current player
            _currentPlayerIndex = data.CurrentPlayerIndex;
            _tempCurrentPlayerIndex = data.TempCurrentPlayerIndex;

            // Update the dice model
            var diceModel = model.GetModel<DiceModel>();
            diceModel.Update(data.DiceModel);

            // Update the trade model
            var tradeModel = model.GetModel<TradeModel>();
            tradeModel.Update(data.TradeModel);

            // Update AllTiles property on each tile
            // (maybe it is not necessary, but I don't have
            // the patience to test it)
            tiles = model.GetAllModelsRecursively<TileModel>();
            foreach (var tile in tiles)
            {
                tile.SetAllTiles(model.GetAllModelsRecursively<TileModel>());
            }

            // Refresh pawns on the map
            var mapView = model.GetView<GUIMap>();
            mapView.UpdatePawnPositions();
        }
    }
}
