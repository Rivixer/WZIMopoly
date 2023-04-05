#region Using Statements
using System.Collections.Generic;
using WZIMopoly.Enums;
using System;
#endregion

namespace WZIMopoly.GameController
{
    public class GameController
    {
        private GameStatus _status;
        private List<Player> _players;
        private DateTime _startTime;
        private BoardController _boardController;
        public BoardController BoardController  => _boardController;

        public GameController(List<Player> players)
        {
            _status = GameStatus.InLobby;
            _players = players;
        }
        public void StartGame()
        {
            _status = GameStatus.Running;
            _startTime = DateTime.Now;
        }
    }
}

