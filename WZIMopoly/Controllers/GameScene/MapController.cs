using Microsoft.Xna.Framework.Content;
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
        private new MapView View => (MapView)base.View; 
        /// <summary>
        /// Creates list od pawns
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
            TilesXml.Load("../../../Properties/Tiles.xml");

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

        internal void SetPlayersOnStart(List<Player> players)
        {
            foreach(Tile item in Model.Tiles)
            {
                item.Players.Clear();
            }
            Model.Tiles[0].Players = players;
        }

        internal void CreatePawns(List<Player> players)
        {
            foreach(Player item in players)
            {
                var pawnModel = new PawnModel(players[i].Color);
                var guiPawn = new GUIPawn(players[i].Color);
                var pawnController = new PawnController(guiPawn, pawnModel);
                Children.Add(pawnController);
                _pawns.Add(pawnController);
            }
        }

        internal void UpdatePawnPositions()
        {
            foreach (Tile tile in Model.Tiles)
            {
                foreach (var (Player, Position) in tile.Players.Zip(UpdatePawnPositions, (p1, p2) => (p1, p2)))
                {
                    PawnController pawn = _pawns.Find(pawn => ((PawnModel)pawn.Model).Color == Player.Color);
                    pawn.UpdatePosition(Position);
                }
            }
            
        }
    }
}
