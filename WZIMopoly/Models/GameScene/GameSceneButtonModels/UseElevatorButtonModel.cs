using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene.GameSceneButtonModels
{
    /// <summary>
    /// Represents the use elevator button model.
    /// </summary>
    internal class UseElevatorButtonModel : ButtonModel, IGameUpdateModel
    {
        /// <summary>
        /// Whether the button was clicked on player's current turn.
        /// </summary>
        private bool _wasUsed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UseElevatorButtonModel"/> class.
        /// </summary>
        public UseElevatorButtonModel()
            : base("UseElevator") { }

        /// <inheritdoc/>
        public void Update(PlayerModel player, TileModel tile)
        {
            if (player.PlayerStatus == PlayerStatus.BeforeRollingDice)
                _wasUsed = false;

            IsActive = (WZIMopoly.GameType == GameType.Online && player.Equals(GameSettings.Client)|| WZIMopoly.GameType == GameType.Local)
                && tile is ElevatorTileModel
                && player.PlayerStatus == PlayerStatus.AfterRollingDice
                && !_wasUsed;
        }

        /// <inheritdoc/>
        public override void AfterUpdate()
        {
            if (WasClickedInThisFrame)
                _wasUsed = true;
        }
    }
}
