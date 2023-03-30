#region Using Statements
using System.Collections.Generic;
using WZIMpoly.Source.Board.Map;
#endregion

namespace WZIMpoly
{
    public class MapController
    {
        public readonly List<Tile> Tiles;

        public MapController()
        {
            Tiles = new List<Tile>();
            // TODO: Load tiles from XML
        }
    }
}
