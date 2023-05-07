using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Performs actions when the player crosses over this tile.
    /// </summary>
    /// <param name="player">
    /// The player who crossed over this tile.
    /// </param>
    interface ICrossable
    {
        internal abstract void OnCross(Player player);
        
    }
}