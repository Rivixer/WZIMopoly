using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene.GameButtonModels
{
    /// <summary>
    /// Represents the mortgage button model.
    /// </summary>
    internal sealed class MortgageButtonModel : ButtonModel, IGameUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MortgageButtonModel"/> class.
        /// </summary>
        internal MortgageButtonModel()
            : base("Mortgage") { }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// Sets the button to active if the player can mortgage
        /// any tile, sell its grades or unmortage any tile.
        /// </para>
        /// <para>
        /// The button is also active if the player is mortgaging fields.
        /// </para>
        /// </remarks>
        public void Update(PlayerModel player, TileModel tile)
        {
            bool beforeRollingDice = player.PlayerStatus == PlayerStatus.BeforeRollingDice;
            bool canMortgageAnyTile = PlayerCanMortgageAnyTile(player);
            bool canSellAnyGradeOfTile = PlayerCanSellAnyGradeOfTile(player);
            bool canUnmortgageAnyTile = PlayerCanUnmortgageAnyTile(player);
            IsActive = (WZIMopoly.GameType == GameType.Online && player == GameSettings.Client || WZIMopoly.GameType == GameType.Local)
                && (beforeRollingDice
                && (canMortgageAnyTile || canSellAnyGradeOfTile || canUnmortgageAnyTile)
                || player.PlayerStatus == PlayerStatus.MortgagingTiles);
        }

        /// <summary>
        /// Checks if the player can mortgage any tile.
        /// </summary>
        /// <param name="player">
        /// The player to check if can mortgage any tile.
        /// </param>
        /// <returns>
        /// True if the player can mortgage any tile, otherwise false.
        /// </returns>
        private static bool PlayerCanMortgageAnyTile(PlayerModel player)
        {
            foreach (TileModel tile in player.PurchasedTiles)
            {
                if (tile is SubjectTileModel t && t.CanMortgage(player))
                {
                    return true;
                }
            }
            return false;
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
