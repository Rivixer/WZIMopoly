using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.GUI.GameScene;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a map model.
    /// </summary>
    internal sealed class MapModel : Model
    {
        /// <summary>
        /// Gets or sets the list of tiles.
        /// </summary>
        internal List<TileController<TileModel>> Tiles { get; set; } = new();

        /// <summary>
        /// Loads tiles from a xml file.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        internal void LoadTiles()
        {
            var TilesXml = new XmlDocument();

#if WINDOWS
            var path = "../../../Properties/Tiles.xml";
#elif LINUX
            var path = "WZIMopoly/Properties/Tiles.xml";
#endif

            TilesXml.Load(path);

            string controllersNamespacePrefix = "WZIMopoly.Controllers.GameScene.TileControllers";
            string modelNamespacePrefix = "WZIMopoly.Controllers.GameScene.TileControllers";
            foreach (XmlNode TileNode in TilesXml.DocumentElement.ChildNodes)
            {
                string rawTileType = TileNode.Attributes["type"].Value;
                Type tileControllerType = Type.GetType($"{controllersNamespacePrefix}.{rawTileType}TileController");
                Type tileModelType = Type.GetType($"{modelNamespacePrefix}.{rawTileType}TileModel");

                TileModel tileModel = (TileModel)Activator.CreateInstance(
                    type: tileModelType,
                    bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                    binder: null,
                    args: new object[] { TileNode },
                    culture: null
                );

                tileControllerType.MakeGenericType(tileModelType);

                TileController<TileModel> tileController = (TileController<TileModel>)Activator.CreateInstance(
                    type: tileControllerType,
                    bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                    binder: null,
                    args: new object[] { TileNode, tileModel },
                    culture: null
                );
                Tiles.Add(tileController);
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
            Tiles.ForEach(x => x.Model.Players.Clear());
            Tiles[0].Model.Players.AddRange(players);
        }

        /// <summary>
        /// Updates positions of all pawns.
        /// </summary>
        internal void UpdatePawnPositions()
        {
            foreach (var tile in Tiles)
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
