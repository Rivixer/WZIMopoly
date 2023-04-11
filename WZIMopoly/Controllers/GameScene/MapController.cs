using System;
using System.Xml;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScreen;

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
    }
}
