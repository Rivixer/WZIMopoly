#region Using Statements
using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Board;
#endregion

namespace WZIMopoly
{
    public class MapController
    {
        public readonly List<Tile> Tiles;

        public MapController()
        {
            Tiles = new List<Tile>();
            InitializeTiles();
        }

        /// <summary>
        /// Initializes tiles from a xml file.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void InitializeTiles()
        {
            var TilesXml = new XmlDocument();
            TilesXml.Load("../../../Source/Board/Map/Properties/Tiles.xml");

            string namespacePrefix = "WZIMopoly.Board";
            foreach (XmlNode TileNode in TilesXml.DocumentElement.ChildNodes)
            {
                string RawTileType = TileNode.Attributes["type"].Value;
                Type TileType = Type.GetType($"{namespacePrefix}.{RawTileType}");

                if (TileType != null)
                {
                    Tile tile = (Tile)Activator.CreateInstance(TileType, TileNode);
                    Tiles.Add(tile);
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
