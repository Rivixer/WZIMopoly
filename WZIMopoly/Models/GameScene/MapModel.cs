using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using WZIMopoly.Controllers;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Controllers.GameScene.TileControllers;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a map model.
    /// </summary>
    internal sealed class MapModel : Model
    {
        /// <summary>
        /// Loads tiles from a xml file.
        /// </summary>
        internal void LoadTiles()
        {
            var tilesXml = new XmlDocument();

#if WINDOWS
            var path = "../../../Properties/Tiles.xml";
#elif LINUX
            var path = "WZIMopoly/Properties/Tiles.xml";
#endif

            tilesXml.Load(path);

            string controllerNamespace = "WZIMopoly.Controllers.GameScene.TileControllers";
            string modelNamespace = "WZIMopoly.Models.GameScene.TileModels";
            foreach (XmlNode tileNode in tilesXml.DocumentElement.ChildNodes)
            {
                string rawTileType = tileNode.Attributes["type"].Value;
                Type tileControllerType = Type.GetType($"{controllerNamespace}.{rawTileType}TileController");
                Type tileGenericControllerType = Type.GetType($"{controllerNamespace}.TileController");
                Type tileModelType = Type.GetType($"{modelNamespace}.{rawTileType}TileModel");


                TileModel tileModel = (TileModel)Activator.CreateInstance(
                    type: tileModelType,
                    bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                    binder: null,
                    args: new object[] { tileNode },
                    culture: null
                );

                var tileView = new GUITile(tileNode, tileModel);

                IControllerable tileController = (IControllerable)Activator.CreateInstance(
                    type: tileControllerType,
                    bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                    binder: null,
                    args: new object[] { tileModel, tileView },
                    culture: null
                );
                AddChild(tileController);
            }
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
        internal void CreatePawns(List<PlayerModel> players)
        {
            foreach (PlayerModel player in players)
            {
                InitializeChild<PawnModel, GUIPawn, PawnController>(player.Color);
            }
        }

        /// <summary>
        /// Sets all players on the start tile.
        /// </summary>
        /// <remarks>
        /// Clears all players from other tiles.
        /// </remarks>
        /// <param name="players">
        /// The list of players to be set on the start tile.
        /// </param>
        internal void SetPlayersOnStart(List<PlayerModel> players)
        {
            var tiles = GetAllControllers<TileController>();
            tiles.ForEach(x => x.Model.Players.Clear());

            var startTile = GetController<StartTileController>();
            startTile.Model.Players.AddRange(players);
        }

        /// <summary>
        /// Moves the player by a designated number of tiles.
        /// </summary>
        /// <param name="player">
        /// The player to move.
        /// </param>
        /// <param name="step">
        /// The number of tiles to pass.
        /// </param>
        internal void MovePlayer(PlayerModel player, int step)
        {
            var sourceTile = GetController<TileController>(x => x.Model.Players.Contains(player));
            sourceTile.Model.Players.Remove(player);

            var destinationTileIndex = (sourceTile.Model.Id + step) % 40;
            var destinationTile = GetController<TileController>(x => x.Model.Id == destinationTileIndex);
            destinationTile.Model.Players.Add(player);
            destinationTile.Model.OnStand(player);

            var passedTiles = GetAllControllers<TileController>((x) =>
            {
                //checking if player crossed start
                if (destinationTile.Model.Id < sourceTile.Model.Id)
                {
                    return x.Model.Id > sourceTile.Model.Id || x.Model.Id < destinationTile.Model.Id;
                }
                else
                {
                    return x.Model.Id > sourceTile.Model.Id && x.Model.Id < destinationTile.Model.Id;
                }
            });
            passedTiles.ForEach(x => (x.Model as ICrossable)?.OnCross(player));

            UpdatePawnPositions();
        }

        /// <summary>
        /// Updates positions of all pawns.
        /// </summary>
        internal void UpdatePawnPositions()
        {
            foreach (var tile in GetAllControllers<TileController>())
            {
                List<Point> pawnPosition = tile.View.GetPawnPositions();
                foreach (var (Player, Position) in tile.Model.Players.Zip(pawnPosition, (p1, p2) => (p1, p2)))
                {
                    var ctrl = GetController<PawnController>((x) => x.Model.Color == Player.Color);
                    ctrl.UpdatePosition(Position);
                }
            }
        }
    }
}
