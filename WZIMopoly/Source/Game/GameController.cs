#region Using Statements
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using WZIMopoly.Source.Board.Map;
using WZIMopoly.Enums;
using System.ComponentModel.DataAnnotations;
#endregion
namespace WZIMopoly.GameController
{
    public class GameController
    {
        private GameStatus _status;
        private List<Player> _players;
        private DataType _startTime;
        private BoardController _boardController;
        public BoardController BoardController { get => _boardController; set => _boardController = value; }

        public GameController(List<Player> players)
        {
            _status = GameStatus.InLobby;
            _players = players;
        }
        public void StartGame()
        {
            _status = GameStatus.InLobby;
            _startTime = DataType.DateTime;
        }
    }
}

