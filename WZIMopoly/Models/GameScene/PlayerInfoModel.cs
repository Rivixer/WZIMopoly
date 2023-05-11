namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents a player information model.
    /// </summary>
    internal class PlayerInfoModel : Model
    {
        /// <summary>
        /// Gets or privately sets the player model.
        /// </summary>
        internal PlayerModel Player { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInfoModel"/> class.
        /// </summary>
        /// <param name="player">
        /// The player model.
        /// </param>
        internal PlayerInfoModel(PlayerModel player)
        {
            Player = player;
        }
    }
}
