using System.Collections.Generic;
using System.Linq;
using WZIMopoly.Controllers.GameScene;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents the mortgage model.
    /// </summary>
    internal class MortgageModel : Model, IGameUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MortgageModel"/> class.
        /// </summary>
        /// <param name="tiles">
        /// The list of tile controllers.
        /// </param>
        public MortgageModel(List<TileController> tiles)
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
        /// Retrieves the tile ids that the player cannot mortgage or sell their grade.
        /// </summary>
        /// <param name="player">
        /// The player that is currently mortgaging the fields.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{int}"/> that represents the collection of tile ids
        /// that the player cannot mortgage or sell their grade.
        /// </returns>
        public IEnumerable<int> GetTileIdsThatPlayerCannotMortgage(PlayerModel player)
        {
            foreach (TileModel tile in TileModels)
            {
                if (tile is IMortgageable t
                    && (!t.CanMortgage(player)
                    || !((t as SubjectTileModel)?.CanSellGrade(player) ?? false)))
                {
                    yield return tile.Id;
                }
            }
        }

        /// <summary>
        /// Retrieves the player's mortgaged tile ids.
        /// </summary>
        /// <param name="player">
        /// The player that is currently mortgaging the fields.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{int}"/> that represents the collection of tile ids
        /// that are mortgaged by the player.
        /// </returns>
        public IEnumerable<int> GetIdsOfTilesThatAreMortgaged(PlayerModel player)
        {
            foreach(TileModel tile in TileModels)
            {
                if (tile is PurchasableTileModel t && t.IsMortgaged && t.Owner.Equals(player))
                    yield return t.Id;
            }
        }
    }
}
