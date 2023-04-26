using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a controller of the map.
    /// </summary>
    internal sealed class MapController : Controller
    {
        /// <summary>
        /// Gets the model of the map controller.
        /// </summary>
        private new MapModel Model => (MapModel)base.Model;

        /// <summary>
        /// The list of pawns.
        /// </summary>
        private readonly List<PawnController> _pawns = new();

        /// <summary>
        /// Initilizes a new instance of the <see cref="MapController"/> class.
        /// </summary>
        /// <remarks>
        /// Loads tiles from a xml file calling <see cref="LoadTiles()"/> method.
        /// </remarks>
        internal MapController(MapView view, MapModel model)
            : base(view, model, false)
        {
            LoadTiles();
        }

        /// <summary>
        /// Loads tiles from a xml file.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void LoadTiles()
        {
            var TilesXml = new XmlDocument();

#if WINDOWS
            var path = "../../../Properties/Tiles.xml";
#elif LINUX
            var path = "WZIMopoly/Properties/Tiles.xml";
#endif

            TilesXml.Load(path);

            string namespacePrefix = "WZIMopoly.Controllers.GameScene.Tiles";
            foreach (XmlNode TileNode in TilesXml.DocumentElement.ChildNodes)
            {
                string RawTileType = TileNode.Attributes["type"].Value;
                Type TileType = Type.GetType($"{namespacePrefix}.{RawTileType}");

                if (TileType != null)
                {
                    Tile tile = (Tile)Activator.CreateInstance(TileType, TileNode);
                    Model.Tiles.Add(tile);
                }
                else
                {
                    throw new ArgumentException($"Invalid value of type attribute: {RawTileType}; " +
                        $"in tile node with {TileNode.Attributes["id"].Value} id in xml file.");
                }
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
        internal void SetPlayersOnStart(List<Player> players)
        {
            foreach(Tile tile in Model.Tiles)
            {
                tile.Players.Clear();
            }
            Model.Tiles[0].Players = players;
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
        internal void CreatePawns(List<Player> players)
        {
            foreach(Player player in players)
            {
                var pawnModel = new PawnModel(player.Color);
                var guiPawn = new GUIPawn(player.Color);
                var pawnController = new PawnController(guiPawn, pawnModel);
                Children.Add(pawnController);
                _pawns.Add(pawnController);
            }
        }

        /// <summary>
        /// Updates positions of all pawns.
        /// </summary>
        internal void UpdatePawnPositions()
        {
            foreach (Tile tile in Model.Tiles)
            {
                List<Point> pawnPosition = tile.GetPawnPositions();
                foreach (var (Player, Position) in tile.Players.Zip(pawnPosition, (p1, p2) => (p1, p2)))
                {
                    PawnController pawn = _pawns.Find(pawn => ((PawnModel)pawn.Model).Color == Player.Color);
                    pawn.UpdatePosition(Position);
                }
            }
            
        }
    }
}
