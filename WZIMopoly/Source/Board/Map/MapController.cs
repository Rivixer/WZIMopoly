#region Using Statements
using System.Collections.Generic;
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
            // TODO: Load tiles from XML
        }
    }
}
