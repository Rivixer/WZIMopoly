using System;
using System.Collections.Generic;
using System.Linq;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

#nullable enable

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents the trade model.
    /// </summary>
    [Serializable]
    internal class TradeModel : Model
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeModel"/> class.
        /// </summary>
        /// <param name="tiles">
        /// The list of all tile controllers.
        /// </param>
        /// <param name="playerInfoCtrls">
        /// The list of all player info controllers.
        /// </param>
        public TradeModel(List<TileController> tiles, List<PlayerInfoController> playerInfoCtrls)
        {
            TileControllers = tiles;
            TileModels = tiles.Select(x => x.Model).ToList();
            PlayerInfoCtrls = playerInfoCtrls;
        }

        /// <summary>
        /// Gets the list of all tile controllers.
        /// </summary>
        [field: NonSerialized]
        public List<TileController> TileControllers { get; private set; }

        /// <summary>
        /// Gets the list of all tile models.
        /// </summary>
        [field: NonSerialized]
        public List<TileModel> TileModels { get; private set; }

        /// <summary>
        /// Gets the list of all player info controllers.
        /// </summary>
        [field: NonSerialized]
        public List<PlayerInfoController> PlayerInfoCtrls { get; private set; }

        /// <summary>
        /// Gets or sets the offeror.
        /// </summary>
        public PlayerModel? Offeror { get; set; }

        /// <summary>
        /// Gets or sets the recipient.
        /// </summary>
        public PlayerModel? Recipient { get; set; }

        /// <summary>
        /// Gets or sets the offered money.
        /// </summary>
        /// <remarks>
        /// If the value is negative,
        /// the offeror wants money from the recipient.
        /// </remarks>
        public int OfferedMoney { get; set; }

        /// <summary>
        /// The list of tiles offered by the offeror.
        /// </summary>
        public List<PurchasableTileModel> ChosenOfferorTiles { get; set; } = new();

        /// <summary>
        /// The list of tiles offeror wants from the recipient.
        /// </summary>
        public List<PurchasableTileModel> ChosenRecipientTiles { get; set; } = new();

        /// <summary>
        /// Gets the total value of the tiles offered by the offeror.
        /// </summary>
        public int ChosenOfferorTilesValue => ChosenOfferorTiles.Sum(x => x.GetValue());

        /// <summary>
        /// Gets the total value of the tiles offeror wants from the recipient.
        /// </summary>
        public int ChosenRecipientTilesValue => ChosenRecipientTiles.Sum(x => x.GetValue());

        /// <summary>
        /// Gets the total value of the trade.
        /// </summary>
        public int TotalValue => ChosenOfferorTilesValue + ChosenRecipientTilesValue + Math.Abs(OfferedMoney);

        /// <summary>
        /// Updates the trade model.
        /// </summary>
        /// <param name="model">
        /// The trade model to update from.
        /// </param>
        public void Update(TradeModel model)
        {
            OfferedMoney = model.OfferedMoney;
            foreach (var player in GameSettings.Players)
            {
                if (player.Equals(model.Offeror))
                {
                    Offeror = player;
                }
                else if (player.Equals(model.Recipient))
                {
                    Recipient = player;
                }
            }
            ChosenOfferorTiles.Clear();
            foreach (var chosenTile in model.ChosenOfferorTiles)
            {
                foreach (var tile in TileModels)
                {
                    if (tile is PurchasableTileModel t && t.Equals(chosenTile))
                    {
                        ChosenOfferorTiles.Add(t);
                    }
                }
            }
            ChosenRecipientTiles.Clear();
            foreach (var chosenTile in model.ChosenRecipientTiles)
            {
                foreach (var tile in TileModels)
                {
                    if (tile is PurchasableTileModel t && t.Equals(chosenTile))
                    {
                        ChosenRecipientTiles.Add(t);
                    }
                }
            }
        }
    }
}
