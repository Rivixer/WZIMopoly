#region Using Statements
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using WZIMopoly.Board;
#endregion

namespace WZIMopoly
{
    public class BoardController
    {
        public readonly MapController MapController;
        public readonly List<Player> Players;

        public List<Tile> Tiles => MapController.Tiles;

        public BoardController(List<Player> players)
        {
            MapController = new MapController();
            Players = players;
        }
    }
}
