using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene.GameButtonModels
{
    /// <summary>
    /// Represents the unmortgage button model.
    /// </summary>
    internal sealed class UnmortgageButtonModel : ButtonModel, IGameUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnmortgageButtonModel"/> class.
        /// </summary>
        internal UnmortgageButtonModel()
            : base("Unmortgage") { }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// Sets the button to active if the player can sell its grades or unmortage any tile.
        /// </para>
        /// <para>
        /// The button is also active if the player is mortgaging fields.
        /// </para>
        /// </remarks>
        public void Update(PlayerModel player, TileModel tile)
        {
            bool beforeRollingDice = player.PlayerStatus == PlayerStatus.BeforeRollingDice;
            bool canSellAnyGradeOfTile = PlayerCanSellAnyGradeOfTile(player);
            bool canUnmortgageAnyTile = PlayerCanUnmortgageAnyTile(player);
            IsActive = beforeRollingDice
                && (canSellAnyGradeOfTile || canUnmortgageAnyTile)
                || player.PlayerStatus == PlayerStatus.MortgagingTiles;
        }


        /// <summary>
        /// Checks if the player can unmortgage any tile.
        /// </summary>
        /// <param name="player">
        /// The player to check if can unmortgage any tile.
        /// </param>
        /// <returns>
        /// True if the player can unmortgage any tile, otherwise false.
        /// </returns>
        private static bool PlayerCanUnmortgageAnyTile(PlayerModel player)
        {
            foreach (TileModel tile in player.MortgagedTiles)
            {
                if (tile is SubjectTileModel t && t.CanUnmortgage(player))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the player can sell any grade of tile.
        /// </summary>
        /// <param name="player">
        /// The player to check if can sell any grade of tile.
        /// </param>
        /// <returns>
        /// True if the player can sell any grade of tile, otherwise false.
        /// </returns>
        private static bool PlayerCanSellAnyGradeOfTile(PlayerModel player)
        {
            foreach (TileModel tile in player.PurchasedTiles)
            {
                if (tile is SubjectTileModel t && t.CanSellGrade(player))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
