using WZIMopoly.Models;

namespace WZIMopoly.Controllers.GameScene.Tiles
{
    /// <summary>
    /// Provides a method that allows to call a function that registers exceeding a given tile.
    /// </summary>
    interface ICrossable
    {
        public abstract void OnCross(Player player);
    }
}
