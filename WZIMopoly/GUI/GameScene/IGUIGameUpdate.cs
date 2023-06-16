using WZIMopoly.Models.GameScene;
using WZIMopoly.Models;

namespace WZIMopoly.GUI.GameScene
{
    /// <summary>
    /// Provides a method for updating a view
    /// with the current player and tile as parameters.
    /// </summary>
    internal interface IGUIGameUpdate : IGUIable
    {
        /// <summary>
        /// Updates the view using the current
        /// player and tile that the player is on.
        /// </summary>
        /// <param name="player">
        /// The current player.
        /// </param>
        /// <param name="tile">
        /// The tile that the player is on.
        /// </param>
        void Update(PlayerModel player, TileModel tile);
    }
}
