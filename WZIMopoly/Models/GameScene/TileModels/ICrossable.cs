namespace WZIMopoly.Models.GameScene.TileModels
{
    /// <summary>
    /// Provides a method to perform when the player crosses over a tile.
    /// </summary>
    internal interface ICrossable
    {
        /// <summary>
        /// Performs actions when the player crosses over this tile.
        /// </summary>
        /// <param name="player">
        /// The player who crossed over a tile.
        /// </param>
        void OnCross(PlayerModel player);
    }
}