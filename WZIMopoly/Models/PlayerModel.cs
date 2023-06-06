using System;
using System.Collections.Generic;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models
{
    /// <summary>
    /// Represents a player.
    /// </summary>
    [Serializable]
    internal class PlayerModel : Model
    {
        /// <summary>
        /// The list of tiles purchased by the player.
        /// </summary>
        private readonly List<PurchasableTileModel> _purchasedTiles = new();

        /// <summary>
        /// The list of mortgaged tiles by the player.
        /// </summary>
        private readonly List<PurchasableTileModel> _mortgagedTiles = new();

        /// <summary>
        /// The default nick of the player.
        /// </summary>
        [NonSerialized]
        private readonly string _defaultNick;

        /// <summary>
        /// The color of the player.
        /// </summary>
        private readonly string _color;

        /// <summary>
        /// The nick of the player.
        /// </summary>
        private string _nick;

        /// <summary>
        /// The player type.
        /// </summary>
        private PlayerType _playerType = PlayerType.None;

        /// <summary>
        /// The player status.
        /// </summary>
        private PlayerStatus _playerStatus = PlayerStatus.WaitingForTurn;

        /// <summary>
        /// The amount of money player has.
        /// </summary>
        private int _money = 1500;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerModel"/> class.
        /// </summary>
        /// <param name="defaultNick">
        /// The nick of the player.
        /// </param>
        /// <param name="color">
        /// The color of the player.
        /// </param>
        internal PlayerModel(string defaultNick, string color, PlayerType type = PlayerType.None)
        {
            _defaultNick = defaultNick;
            _nick = defaultNick;
            _color = color;
            PlayerType = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerModel"/> class
        /// with the same values as the specified object.
        /// </summary>
        /// <param name="player">
        /// The player to copy.
        /// </param>
        public PlayerModel(PlayerModel player)
        {
            _nick = player.Nick;
            _playerType = player.PlayerType;
            _playerStatus = player.PlayerStatus;
            _money = player.Money;
            _purchasedTiles = player.PurchasedTiles;
            _mortgagedTiles = player.MortgagedTiles;
            _defaultNick = player._defaultNick;
            _color = player._color;
        }

        /// <summary>
        /// Gets the color of the player.
        /// </summary>
        public string Color => _color;

        /// <summary>
        /// Gets or sets the player type.
        /// </summary>
        internal PlayerType PlayerType
        {
            get => _playerType;
            set => _playerType = value;
        }

        /// <summary>
        /// Gets or sets the nick of the player.
        /// </summary>
        public string Nick
        {
            get => _nick;
            set => _nick = value;
        }

        /// <summary>
        /// Gets or sets the amount of money the player has.
        /// </summary>
        public int Money
        {
            get => _money;
            set => _money = value;
        }

        /// <summary>
        /// Gets or sets the player status.
        /// </summary>
        public PlayerStatus PlayerStatus
        {
            get => _playerStatus;
            set => _playerStatus = value;
        }

        /// <summary>
        /// Gets pucharsed tiles by the player.
        /// </summary>
        public List<PurchasableTileModel> PurchasedTiles => _purchasedTiles;

        /// <summary>
        /// Gets mortgaged tiles by the player.
        /// </summary>
        public List<PurchasableTileModel> MortgagedTiles => _mortgagedTiles;

        /// <summary>
        /// Transfers money from the player to another player.
        /// </summary>
        /// <param name="player">
        /// The player that will receive the money.
        /// </param>
        /// <param name="amount">
        /// The amount of money to transfer.
        /// </param>
        public void TransferMoneyTo(PlayerModel player, int amount)
        {
            Money -= amount;
            player.Money += amount;
        }

        /// <summary>
        /// Resets the nick of the player to the default one.
        /// </summary>
        public void ResetNick()
        {
            _nick = _defaultNick;
        }

        /// <summary>
        /// Resets the player to the default state.
        /// </summary>
        public void Reset()
        {
            _nick = _defaultNick;
            _money = 1500;
            _playerType = PlayerType.None;
            _playerStatus = PlayerStatus.WaitingForTurn;
            _purchasedTiles.Clear();
            _mortgagedTiles.Clear();
        }

        public void Update(PlayerModel player)
        {
            _nick = player.Nick;
            _playerType = player.PlayerType;
            _playerStatus = player.PlayerStatus;
            _money = player.Money;
            _purchasedTiles.Clear();
            _purchasedTiles.AddRange(player.PurchasedTiles);
            _mortgagedTiles.Clear();
            _mortgagedTiles.AddRange(player.MortgagedTiles);
        }

        public override bool Equals(object obj)
        {
            if (obj is PlayerModel player)
            {
                Debug.WriteLine("AAA" + (player.Color == _color).ToString());
                return player.Nick == _nick
                    && player.Color == _color;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_nick, _color);
        }
    }
}
