using System.Collections.Generic;
using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene.GameButtonModels
{
    /// <summary>
    /// Represents the upgrade button model.
    /// </summary>
    internal sealed class UpgradeButtonModel : ButtonModel, IGameUpdateModel
    {
        /// <summary>
        /// The list of all subject tiles.
        /// </summary>
        private readonly List<SubjectTileModel> _subjectTiles;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeButtonModel"/> class.
        /// </summary>
        /// <param name="subjectTiles">
        /// The list of all subject tiles.
        /// </param>
        internal UpgradeButtonModel(List<SubjectTileModel> subjectTiles)
            : base("Upgrade")
        {
            _subjectTiles = subjectTiles;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Sets the button to active if the player can upgrade any tile.
        /// </remarks>
        public void Update(PlayerModel player, TileModel tile)
        {
            var beforeRollingDice = player.PlayerStatus == PlayerStatus.BeforeRollingDice;
            var canUpgradeAnyTile = PlayerCanUpgradeAnyTile(player);
            IsActive = beforeRollingDice && canUpgradeAnyTile
                || player.PlayerStatus == PlayerStatus.UpgradingFields;
        }

        /// <summary>
        /// Checks if the player can upgrade any tile.
        /// </summary>
        /// <param name="player">
        /// The player to check.
        /// </param>
        /// <returns>
        /// True if the player can upgrade any tile, otherwise false.
        /// </returns>
        private bool PlayerCanUpgradeAnyTile(PlayerModel player)
        {
            foreach(var tile in player.PurchasedTiles)
            {
                if (tile is SubjectTileModel t && t.CanUpgrade(player, _subjectTiles))
                {
                    return true;
                }
            }
            return false;
        }
    }
}