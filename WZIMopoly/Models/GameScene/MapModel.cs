using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Controllers.GameScene.TileControllers;
using WZIMopoly.Enums;
using WZIMopoly.Exceptions;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a map model.
    /// </summary>
    internal class MapModel : Model
    {
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
        /// Loads tiles from a xml file.
        /// </summary>
        /// <returns>
        /// The list of loaded tiles.
        /// </returns>
        public List<TileController> LoadTiles()
        {
            var tilesXml = new XmlDocument();
#if WINDOWS
            var path = "../../../Properties/Tiles.xml";
#elif LINUX
            var path = "WZIMopoly/Properties/Tiles.xml";
#endif

            tilesXml.Load(path);

            var tiles = new List<TileController>();
            string controllerNamespace = "WZIMopoly.Controllers.GameScene.TileControllers";
            string modelNamespace = "WZIMopoly.Models.GameScene.TileModels";
            foreach (XmlNode tileNode in tilesXml.DocumentElement.ChildNodes)
            {
                string rawTileType = tileNode.Attributes["type"].Value;
                Type tileControllerType = Type.GetType($"{controllerNamespace}.{rawTileType}TileController");
                Type tileGenericControllerType = Type.GetType($"{controllerNamespace}.TileController");
                Type tileModelType = Type.GetType($"{modelNamespace}.{rawTileType}TileModel");

                TileModel tileModel;
                if (tileModelType.IsAssignableTo(typeof(TileModel)))
                {
                    MethodInfo loadMethod = tileModelType.GetMethod("LoadFromXml");
                    tileModel = (TileModel)loadMethod.Invoke(null, new object[] { tileNode });
                }
                else
                {
                    throw new InvalidTypeException(
                        $"Tile model type {tileModelType} is not assignable to {typeof(TileModel)}");
                }

                TileController tileController;
                if (tileModel is SubjectTileModel)
                {
                    tileController = (TileController)Activator.CreateInstance(
                        type: tileControllerType,
                        bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                        binder: null,
                        args: new object[] { tileModel, new GUISubjectTile(tileNode, tileModel as SubjectTileModel) },
                        culture: null
                    );
                }
                else if (tileModel is PurchasableTileModel)
                {
                    tileController = (TileController)Activator.CreateInstance(
                        type: tileControllerType,
                        bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                        binder: null,
                        args: new object[] { tileModel, new GUIPurchasableTile(tileNode, tileModel as PurchasableTileModel) },
                        culture: null
                    );
                }
                else
                {
                    tileController = (TileController)Activator.CreateInstance(
                        type: tileControllerType,
                        bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                        binder: null,
                        args: new object[] { tileModel, new GUITile(tileNode, tileModel)},
                        culture: null
                    );
                }

                tiles.Add(tileController);
            }

            tiles.ForEach(AddChild);
            tiles.ForEach(x => x.Model.SetAllTiles(tiles.Select(x => x.Model).ToList()));

            var deaneryTile = tiles.First(x => x.Model is DeaneryTileModel);
            var mandatoryLectureTile = GetModel<MandatoryLectureTileModel>();
            deaneryTile.Model.OnStand += (player) =>
            {
                TeleportPlayer(player, mandatoryLectureTile);
                mandatoryLectureTile.AddPrisoner(player);
                ActivateOnStandTile(player);
            };
            return tiles;
        }

        /// <summary>
        /// Creates pawns for all players.
        /// </summary>
        /// <remarks>
        /// Adds pawns to the list of pawns and to the children of the map.
        /// </remarks>
        /// <param name="players">
        /// The list of players to create pawns for.
        /// </param>
        public void CreatePawns(List<PlayerModel> players)
        {
            foreach (PlayerModel player in players)
            {
                var model = new PawnModel(player.Color);
                var view = new GUIPawn(model);
                var controller = new PawnController(model, view);
                AddChildBefore<TileController>(controller);
            }
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
        /// A positive number of tiles to pass.
        /// </param>
        /// <returns>
        /// The list of tiles that the player has passed.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="step"/> is not a positive number.
        /// </exception>
        /// <remarks>
        /// If the player crosses the <see cref="ICrossable"/> tile,
        /// the <see cref="ICrossable.OnCross"/> method is called.
        /// </remarks>
        public List<TileController> MovePlayer(PlayerModel player, uint step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step must be a positive number.");
            }
            var sourceTile = GetController<TileController>(x => x.Model.Players.Contains(player));
            sourceTile.Model.Players.Remove(player);

            var destinationTileIndex = (sourceTile.Model.Id + step) % 40;
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
        /// Activates the tile that the player is standing on 
        /// and handles the <see cref="NotEnoughMoney"/> exception.
        /// </summary>
        /// <param name="player">
        /// The player that is standing on the tile.
        /// </param>
        /// <remarks>
        /// If the player doesn't have enough money to pay the rent,
        /// the player is asked to mortgage tiles or sell their grades.
        /// </remarks>
        public void ActivateOnStandTile(PlayerModel player, MortgageController mortgageCtrl, GameModel model)
        {
            void ActivateAgain()
            {
                ActivateOnStandTile(player, mortgageCtrl, model);
                mortgageCtrl.OnTileClicked -= ActivateAgain;
            }

            var tile = GetController<TileController>(x => x.Model.Players.Contains(player));
            tile.Model.OnPlayerStand(player);

            try
            {
                ActivateOnStandTile(player);
                player.PlayerStatus = PlayerStatus.AfterRollingDice;
                GameSettings.SendGameData(model);
            }
            catch (NotEnoughMoney ex)
            {
                if (player.HowMuchMoneyCanPlayerGetBack() >= Math.Abs(ex.Amount))
                {
                    player.PlayerStatus = PlayerStatus.SavingFromBankruptcy;
                    mortgageCtrl.OnTileClicked += ActivateAgain;
                }
                else if (tile.Model is PurchasableTileModel t)
                {
                    player.GoBankrupt(t.Owner);
                }
                else
                {
                    player.GoBankrupt();
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
