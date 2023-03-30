#region Using Statements
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using WZIMopoly.Source.Board.Map;
#endregion

namespace WZIMopoly
{
    public class BoardController
    {
        public readonly MapController MapController;

        public List<Tile> Tiles => MapController.Tiles;

        public BoardController()
        {
            MapController = new MapController();
        }
    }
}
