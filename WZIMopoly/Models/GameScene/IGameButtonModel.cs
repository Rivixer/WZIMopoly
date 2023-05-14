namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Provides a method for updating the button
    /// model with the current player and tile as parameters.
    /// </summary>
    internal interface IGameButtonModel : IModelable
    {
        /// <summary>
        /// Updates the button model using the current
        /// player and tile that the player is on.
        /// </summary>
        /// <param name="player">
        /// The current player.
        /// </param>
        /// <param name="tile">
        /// The tile that the player is on.
        /// </param>
        internal virtual void Update(PlayerModel player, TileModel tile) { }
    }
}
