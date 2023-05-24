namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Provides a method for updating a model
    /// with the current player and tile as parameters.
    /// </summary>
    internal interface IGameUpdateModel : IModelable
    {
        /// <summary>
        /// Updates the model using the current
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
