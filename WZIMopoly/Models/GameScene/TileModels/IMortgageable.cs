namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Provides the interface for the mortgageable tiles.
    /// </summary>
    internal interface IMortgageable
    {
        /// <summary>
        /// Gets the price for mortgaging the subject.
        /// </summary>
        int MortgagePrice { get; }

        /// <summary>
        /// Gets the value indicating whether
        /// the subject is currently mortgaged.
        /// </summary>
        bool IsMortgaged { get; }

        /// <summary>
        /// Mortgages the subject tile.
        /// </summary>
        void Mortgage();

        /// <summary>
        /// Unmortages the subject tile.
        /// </summary>
        void Unmortgage();

        /// <summary>
        /// Checks if the player can mortgage the subject tile.
        /// </summary>
        /// <param name="player">
        /// The player to check if can mortgage the subject tile.
        /// </param>
        /// <returns>
        /// True if the player can mortgage the subject tile, otherwise false.
        /// </returns>
        /// <remarks>
        /// The player can mortgage the subject tile if the player owns the tile
        /// and the grade is <see cref="SubjectGrade.Three"/>.
        /// </remarks>
        bool CanMortgage(PlayerModel player);

        /// <summary>
        /// Checks if the player can unmortgage the subject tile.
        /// </summary>
        /// <param name="player">
        /// The player to check if can unmortgage the subject tile.
        /// </param>
        /// <returns>
        /// True if the player can unmortgage the subject tile, otherwise false.
        /// </returns>
        /// <remarks>
        /// The player can unmortgage the subject tile if the player owns the tile,
        /// the tile is mortgaged and the player has enough money to pay the mortgage price.
        /// </remarks>
        bool CanUnmortgage(PlayerModel player);
    }
}
