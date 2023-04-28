using System;
using System.Xml;
using WZIMopoly.GUI;
using WZIMopoly.Models;

namespace WZIMopoly.Controllers
{
    /// <summary>
    /// Represents a controller of the map.
    /// </summary>
    internal sealed class ConsoleController : Controller
    {
        /// <summary>
        /// Gets the model of the map controller.
        /// </summary>
        private new ConsoleModel Model => (ConsoleModel)base.Model;

        /// <summary>
        /// Initilizes a new instance of the <see cref="ConsoleController"/> class.
        /// </summary>
        /// <remarks>
        /// Loads tiles from a xml file calling <see cref="LoadTiles()"/> method.
        /// </remarks>
        internal ConsoleController(GUIConsole view, ConsoleModel model)
            : base(view, model, false)
        {
            //LoadTiles();
        }

        /// <summary>
        /// Loads tiles from a xml file.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
//        private void LoadTiles()
//        {
//            var TilesXml = new XmlDocument();

//#if WINDOWS
//            var path = "../../../Properties/Tiles.xml";
//#elif LINUX
//            var path = "WZIMopoly/Properties/Tiles.xml";
//#endif

//            TilesXml.Load(path);

//            string namespacePrefix = "WZIMopoly.Controllers.GameScene.Tiles";
//            foreach (XmlNode TileNode in TilesXml.DocumentElement.ChildNodes)
//            {
//                string RawTileType = TileNode.Attributes["type"].Value;
//                Type TileType = Type.GetType($"{namespacePrefix}.{RawTileType}");

//                if (TileType != null)
//                {
//                    Tile tile = (Tile)Activator.CreateInstance(TileType, TileNode);
//                    Model.Tiles.Add(tile);
//                }
//                else
//                {
//                    throw new ArgumentException($"Invalid value of type attribute: {RawTileType}; " +
//                        $"in tile node with {TileNode.Attributes["id"].Value} id in xml file.");
//                }
//            }
//        }
    }
}
