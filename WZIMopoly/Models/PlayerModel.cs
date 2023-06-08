using System;
using System.Collections.Generic;
using WZIMopoly.Enums;
using WZIMopoly.Exceptions;
using WZIMopoly.Models.GameScene;
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
            _nick = player._nick;
            _playerType = player._playerType;
            _playerStatus = player._playerStatus;
            _money = player._money;
            _purchasedTiles = player._purchasedTiles;
            _mortgagedTiles = player._mortgagedTiles;
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
        /// <value>
        /// The amount of money the player has, more or equal to 0.
        /// </value>
        public int Money
        {
            get => _money;
            set
            {
                if (value < 0)
                {
                    MoneyToGetFromMortgage = -value;
                    throw new NotEnoughMoney(this, value);
                }
                MoneyToGetFromMortgage = 0;
                _money = value;
            }
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
        /// Gets the amount of money the player is short of for payment.
        /// </summary>
        /// <value>
        /// The amount of money the player is short of for payment, more or equal to 0.
        /// </value>
        public int MoneyToGetFromMortgage { get; private set; } = 0;
        /// </summary>
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

        /// <summary>
        /// Updates the player with the values of the specified player.
        /// </summary>
        /// <param name="player">
        /// The player to copy.
        /// </param>
        public void Update(PlayerModel player, List<TileModel> Tiles)
        {
            _nick = player._nick;
            _playerType = player._playerType;
            _playerStatus = player._playerStatus;
            _money = player._money;
            _purchasedTiles.Clear();
            foreach(var purchasedTile in player._purchasedTiles)
            {
                foreach (var t in Tiles)
                {
                    if (t.Id == purchasedTile.Id)
                    {
                        _purchasedTiles.Add(t as PurchasableTileModel);
                        break;
                    }
                }
            }
            _mortgagedTiles.Clear();
            foreach (var mortgagedTile in player._mortgagedTiles)
            {
                foreach (var t in Tiles)
                {
                    if (t.Id == mortgagedTile.Id)
                    {
                        _purchasedTiles.Add(t as PurchasableTileModel);
                        break;
                    }
                }
            }
            _mortgagedTiles.AddRange(player._mortgagedTiles);
        }

        /// <summary>
        /// Compares the player with other object.
        /// </summary>
        /// <param name="obj">
        /// An object to compare with.
        /// </param>
        /// <returns>
        /// True if the object is a player and has the
        /// same nick and color as this player, otherwise false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is PlayerModel player)
            {
                return player.Nick == _nick
                    && player.Color == _color;
            }
            return false;
        }

        /// <summary>
        /// Returns the hash code of the player.
        /// </summary>
        /// <returns>
        /// The hash code of the player.
        /// </returns>
        /// <remarks>
        /// The hash code is based on the nick and color of the player.
        /// </remarks>
        public override int GetHashCode()
        {
            return HashCode.Combine(_nick, _color);
        }
    }
}
