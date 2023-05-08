using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Provides a method to perform when the player crosses over a tile.
    /// </summary>
    interface ICrossable
    {
        /// <summary>
        /// Performs actions when the player crosses over this tile.
        /// </summary>
        /// <param name="player">
        /// The player who crossed over a tile.
        /// </param>
        internal abstract void OnCross(Player player);
    }
}