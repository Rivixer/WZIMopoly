#region Using Statements
using System;
using System.Collections.Generic;
using System.Xml;
using WZIMopoly.Source.Board.Map;
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

        private void InitializeTiles()
        {
            var TilesXml = new XmlDocument();
            TilesXml.Load("../../../Source/Board/Map/Properties/Tiles.xml");

            string namespacePrefix = "WZIMopoly.Source.Board.Map.Tiles";
            foreach (XmlNode TileNode in TilesXml.DocumentElement.ChildNodes)
            {
                string RawTileType = TileNode.Attributes["type"].InnerText;
                Type TileType = Type.GetType($"{namespacePrefix}.{RawTileType}");

                if (TileType != null)
                {
                    Tile tile = (Tile)Activator.CreateInstance(TileType, TileNode);
                    Tiles.Add(tile);
                }
                else
                {
                    throw new ArgumentException($"Invalid type in xml file: {RawTileType}");
                }
            }
        }
    }
}
