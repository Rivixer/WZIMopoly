#region Using Statements
using System.Collections.Generic;
using WindowsWZIMpoly.Source.Board.Map;
#endregion

namespace WindowsWZIMpoly
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
