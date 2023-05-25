using System.Collections.Generic;
using System.Linq;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents the upgrade tiles model.
    /// </summary>
    internal class UpgradeModel : Model, IGameUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeModel"/> class.
        /// </summary>
        /// <param name="tiles">
        /// The list of tile controllers.
        /// </param>
        public UpgradeModel(List<TileController> tiles)
        {
            TileControllers = tiles;
            TileModels = tiles.Select(x => x.Model).ToList();
        }

        /// <summary>
        /// Gets the list of tile controllers.
        /// </summary>
        public List<TileController> TileControllers { get; private set; }

        /// <summary>
        /// Gets the list of tile models.
        /// </summary>
        public List<TileModel> TileModels { get; private set; }

        /// <summary>
        /// Gets the player that is currently playing.
        /// </summary>
        public PlayerModel CurrentPlayer { get; private set; }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            CurrentPlayer = player;
        }

        /// <summary>
        /// Returns the list of tile ids that the player cannot upgrade.
        /// </summary>
        /// <param name="player">
        /// The player that is currently upgrading the fields.
        /// </param>
        /// <returns>
        /// The list of tile ids that the player cannot upgrade.
        /// </returns>
        public List<int> GetTileIdsThatPlayerCannotUpgrade(PlayerModel player)
        {
            var result = new List<int>();
            foreach (TileModel tile in TileModels)
            {
                if (tile is not SubjectTileModel t || !t.CanUpgrade(player, TileModels))
                {
                    result.Add(tile.Id);
                }
            }
            return result;
        }
    }
}
