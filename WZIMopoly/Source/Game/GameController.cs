#region Using Statements
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using WZIMopoly.Source.Board.Map;
using WZIMopoly.Source.Enums;
#endregion
public class GameController
{
    private GameStatus _status;
    private List<Player> _players;
    private DataTime _startTime;
    private BoardController _boardController;
    private readonly BoardController _boardController
    {
        get;
        set; 
    }
    public GameController(GameStatus _status, List<Player> _players, DataTime _startTime, BoardController _boardController)
    {
        _status = GameStatus.InLobby;
        _players = _players;
        _startTime = _startTime;
        _boardController = _boardController;
    }

    public void StartGame()
    {
        _status = GameStatus.Running;
        _startTime = DataTime.Now;
    }

}
