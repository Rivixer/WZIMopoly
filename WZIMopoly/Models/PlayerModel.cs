using System.Collections.Generic;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models
{
    /// <summary>
    /// Represents a player.
    /// </summary>
    internal class PlayerModel : Model
    {
        /// <summary>
        /// The list of tiles purchased by the player.
        /// </summary>
        private readonly List<PurchasableTileModel> _purchasedTiles = new();

        /// <summary>
        /// The default nick of the player.
        /// </summary>
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
        /// The amount of money player has.
        /// </summary>
        private int _money = 1500;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerModel"/> class.
        /// </summary>
        /// <param name="nick">
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
        /// Gets the color of the player.
        /// </summary>
        public string Color => _color;

        /// <summary>
        /// Gets or sets the player type.
        /// </summary>
        internal PlayerType PlayerType { get; set; } = PlayerType.None;

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
        public PlayerStatus PlayerStatus { get; set; } = PlayerStatus.WaitingForTurn;

        /// <summary>
        /// Gets pucharsed tiles by the player.
        /// </summary>
        public List<PurchasableTileModel> PurchasedTiles => _purchasedTiles;

        /// <summary>
        /// Resets the nick of the player to the default one.
        /// </summary>
        public void ResetNick()
        {
            _nick = _defaultNick;
        }
    }
}
