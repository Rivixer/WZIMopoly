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
        /// The color of the player.
        /// </summary>
        internal readonly string Color;

        /// <summary>
        /// The amount of money player has.
        /// </summary>
        internal int Money = 1500;

        /// <summary>
        /// The nick of the player.
        /// </summary>
        private string _nick;

        /// <summary>
        /// The list of tiles purchased by the player.
        /// </summary>
        readonly private List<PurchasableTileModel> _purchasedTiles = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerModel"/> class.
        /// </summary>
        /// <param name="nick">
        /// The nick of the player.
        /// </param>
        /// <param name="color">
        /// The color of the player.
        /// </param>
        internal PlayerModel(string nick, string color)
        {
            _nick = nick;
            Color = color;
        }

        /// <summary>
        /// Gets or sets the player status.
        /// </summary>
        internal PlayerStatus PlayerStatus { get; set; }

        /// <summary>
        /// Gets or sets the nick of the player.
        /// </summary>
        internal string Nick
        {
            get => _nick;
            set => _nick = value;
        }

        /// <summary>
        /// Gets pucharsed tiles by the player.
        /// </summary>
        internal List<PurchasableTileModel> PurchasedTiles => _purchasedTiles;
    }
}
