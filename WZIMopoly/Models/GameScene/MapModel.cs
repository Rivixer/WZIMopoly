using System;
using System.Collections.Generic;
using System.Linq;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Controllers.GameScene.TileControllers;
using WZIMopoly.Enums;
using WZIMopoly.Exceptions;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a map model.
    /// </summary>
    internal partial class MapModel : Model
    {
        /// <summary>
        /// The random number generator.
        /// </summary>
        private static readonly Random s_random = new();

        /// <summary>
        /// Activates all <see cref="ICrossable"/> tiles that the player has passed.
        /// </summary>
        /// <param name="player">
        /// The player that has passed the tiles.
        /// </param>
        /// <param name="passedTiles">
        /// The list of tiles that the player has passed.
        /// </param>
        public static void ActivateCrossableTiles(PlayerModel player, List<TileModel> passedTiles)
        {
            foreach (var tile in passedTiles)
            {
                (tile as ICrossable)?.OnCross(player);
            }
        }

        /// <inheritdoc cref="ActivateCrossableTiles(PlayerModel, List{TileModel})"/>
        public static void ActivateCrossableTiles(PlayerModel player, List<TileController> passedTiles)
        {
            var passedTileModels = passedTiles.Select(x => x.Model).ToList();
            ActivateCrossableTiles(player, passedTileModels);
        }

        /// <summary>
        /// Sets all players on the start tile.
        /// </summary>
        /// <remarks>
        /// Clears all players from other tiles.
        /// </remarks>
        public void SetPlayersOnStart()
        {
            var tiles = GetAllControllers<TileController>();
            tiles.ForEach(x => x.Model.Players.Clear());

            var startTile = GetController<StartTileController>();
            startTile.Model.Players.AddRange(GameSettings.ActivePlayers);
        }

        /// <summary>
        /// Moves the player by a designated number of tiles.
        /// </summary>
        /// <param name="player">
        /// The player to move.
        /// </param>
        /// <param name="step">
        /// A number of tiles to pass.
        /// </param>
        /// <returns>
        /// The list of tiles that the player has passed.
        /// </returns>
        public List<TileController> MovePlayer(PlayerModel player, int step)
        {
            var sourceTile = GetPlayerTile(player);
            sourceTile.Model.Players.Remove(player);

            var destinationTileIndex = (sourceTile.Model.Id + step + 40) % 40;
            var destinationTile = GetController<TileController>(x => x.Model.Id == destinationTileIndex);
            destinationTile.Model.Players.Add(player);

            var passedTiles = GetAllControllers<TileController>((x) =>
            {
                // checking if the player has crossed the start tile
                if (destinationTile.Model.Id < sourceTile.Model.Id)
                {
                    return x.Model.Id > sourceTile.Model.Id || x.Model.Id < destinationTile.Model.Id;
                }
                else
                {
                    return x.Model.Id > sourceTile.Model.Id && x.Model.Id < destinationTile.Model.Id;
                }
            });
            return passedTiles;
        }

        /// <summary>
        /// Moves the player to a designated tile.
        /// </summary>
        /// <param name="player">
        /// The player to be moved.
        /// </param>
        /// <param name="tileModel">
        /// The tile to move the player to.
        /// </param>
        /// <returns>
        /// The list of tiles that the player has passed.
        /// </returns>
        public List<TileController> MovePlayer(PlayerModel player, TileModel tileModel)
        {
            List<TileController> passedTiles;
            var sourceTile = GetPlayerTile(player).Model;
            if (sourceTile.Id > tileModel.Id)
            {
                passedTiles = MovePlayer(player, 40 - (sourceTile.Id - tileModel.Id));
            }
            else
            {
                passedTiles = MovePlayer(player, (tileModel.Id - sourceTile.Id));
            }
            return passedTiles;
        }

        /// <inheritdoc cref="MovePlayer(PlayerModel, TileModel)"/>
        public List<TileController> MovePlayer(PlayerModel player, TileController tileController)
        {
            return MovePlayer(player, tileController.Model);
        }

        /// <summary>
        /// Finds the nearest tile of a given type from the player.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the tile to find.
        /// </typeparam>
        /// <param name="player">
        /// The player to find the tile from.
        /// </param>
        /// <returns>
        /// The nearest tile of the given type.
        /// </returns>
        public T FindNearestTile<T>(PlayerModel player)
            where T : TileController
        {
            List<T> tiles = GetAllControllers<T>();
            var playerTile = GetPlayerTile(player);
            return tiles.OrderBy(x => Math.Min(
                Math.Abs(x.Model.Id - playerTile.Model.Id),
                Math.Abs(x.Model.Id + 40 - playerTile.Model.Id))).First();
        }

        /// <summary>
        /// Returns the tile that the player is standing on.
        /// </summary>
        /// <param name="player">
        /// The player to check.
        /// </param>
        /// <returns>
        /// The tile that the player is standing on.
        /// </returns>
        public TileController GetPlayerTile(PlayerModel player)
        {
            return GetController<TileController>(x => x.Model.Players.Contains(player));
        }

        /// <summary>
        /// Activates the tile that the player is standing on.
        /// </summary>
        /// <param name="player">
        /// The player that is standing on the tile.
        /// </param>
        public void ActivateOnStandTile(PlayerModel player)
        {
            var tile = GetController<TileController>(x => x.Model.Players.Contains(player));
            tile.Model.OnPlayerStand(player);
        }

        /// <summary>
        /// Handles the player's bankruptcy.
        /// </summary>
        /// <param name="action">
        /// The action that may cause the bankruptcy.
        /// </param>
        /// <param name="mortgageCtrl">
        /// The mortgage controller used to mortgage the player's
        /// tiles or their grades unless the player has enough money to pay.
        /// </param>
        /// <param name="model">
        /// The game model used to send the game data to the server.
        /// </param>
        /// <example>
        /// <code>
        /// HandleBankrupt( delegate { player.Money -= 100; }, player, mortgageCtrl, model);
        /// </code>
        /// </example>
        /// <remarks>
        /// <para>
        /// While the action is being executed and the player goes bankrupt,
        /// the player's status is set to <see cref="PlayerStatus.SavingFromBankruptcy"/>.
        /// </para>
        /// <para>
        /// Until the player has enough money to pay,
        /// they are asked to mortgage their tiles or their grades.
        /// </para>
        /// <para>
        /// If the player's properties are not enough to pay,
        /// the player goes bankrupt. Their status
        /// is set to <see cref="PlayerStatus.Bankrupt"/>.<br/>
        /// If the player was standing on a purchasable tile,
        /// the tile's owner gets the player's properties.
        /// </para>
        /// </remarks>
        public void HandleBankrupt(Action action, MortgageController mortgageCtrl, GameModel model)
        {
            void HandleAgain()
            {
                HandleBankrupt(action, mortgageCtrl, model);
                mortgageCtrl.OnTileClicked -= HandleAgain;
            }

            try
            {
                action();
                GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.AfterRollingDice;
                GameSettings.SendGameData(model);
            }
            catch (NotEnoughMoney ex)
            {
                var tile = GetPlayerTile(GameSettings.CurrentPlayer);
                if (GameSettings.CurrentPlayer.HowMuchMoneyCanPlayerGetBack() >= Math.Abs(ex.Amount))
                {
                    GameSettings.CurrentPlayer.PlayerStatus = PlayerStatus.SavingFromBankruptcy;
                    mortgageCtrl.OnTileClicked += HandleAgain;
                }
                else if (tile.Model is PurchasableTileModel t)
                {
                    GameSettings.CurrentPlayer.GoBankrupt(t.Owner);
                }
                else
                {
                    GameSettings.CurrentPlayer.GoBankrupt();
                }
            }
        }

        /// <summary>
        /// Teleports the player to the destination tile.
        /// </summary>
        /// <param name="player">
        /// The player to teleport.
        /// </param>
        /// <param name="destinationTile">
        /// The tile the player will be teleported to.
        /// </param>
        public void TeleportPlayer(PlayerModel player, TileModel destinationTile)
        {
            var sourceTileModel = GetModel<TileModel>(x => x.Players.Contains(player));
            sourceTileModel.Players.Remove(player);
            destinationTile.Players.Add(player);
        }

        /// <inheritdoc cref="TeleportPlayer(PlayerModel, TileModel)"/>
        public void TeleportPlayer(PlayerModel player, TileController destinationTile)
        {
            TeleportPlayer(player, destinationTile.Model);
        }

        /// <inheritdoc cref="TeleportPlayer(PlayerModel, TileModel)"/>
        /// <param name="destinationTileId">
        /// The tile's ID the player will be teleported to.
        /// </param>
        /// <remarks>
        /// If the tile with the given ID doesn't exist, do nothing.
        /// </remarks>
        public void TeleportPlayer(PlayerModel player, int destinationTileId)
        {
            var destinationTile = GetModel<TileModel>(x => x.Id == destinationTileId);
            if (destinationTile != null)
            {
                TeleportPlayer(player, destinationTile);
            }
        }
    }
}
