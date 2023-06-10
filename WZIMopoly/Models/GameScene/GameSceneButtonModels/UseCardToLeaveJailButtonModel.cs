using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene.GameSceneButtonModels
{
    /// <summary>
    /// The model of the use card to leave jail button.
    /// </summary>
    internal class UseCardToLeaveJailButtonModel : ButtonModel, IGameUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UseCardToLeaveJailButtonModel"/> class.
        /// </summary>
        public UseCardToLeaveJailButtonModel()
            : base("UseCard") { }

        /// <inheritdoc/>
        /// <remarks>
        /// Sets the button to be active if the player is in jail
        /// and can use card to release.
        /// </remarks>
        public void Update(PlayerModel player, TileModel tile)
        {
            var jail = tile as MandatoryLectureTileModel;
            IsActive = player.PlayerStatus == PlayerStatus.BeforeRollingDice
                && (WZIMopoly.GameType == GameType.Online && player == GameSettings.Client || WZIMopoly.GameType == GameType.Local)
                && player.NumberOfLeaveJailCards > 0
                && (jail?.IsPrisoner(player) ?? false);
        }
    }
}
