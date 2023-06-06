using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene.GameSceneButtonModels
{
    /// <summary>
    /// The model of the leave jail button.
    /// </summary>
    internal class LeaveJailButtonModel : ButtonModel, IGameUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveJailButtonModel"/> class.
        /// </summary>
        public LeaveJailButtonModel() 
            : base("LeaveJail") { }

        /// <inheritdoc/>
        /// <remarks>
        /// Sets the button to be active if the player is in jail
        /// and can pay for their release.
        /// </remarks>
        public void Update(PlayerModel player, TileModel tile)
        {
            var jail = tile as MandatoryLectureTileModel;
            IsActive = player.PlayerStatus == PlayerStatus.BeforeRollingDice
                && (WZIMopoly.GameType == GameType.Online && player == GameSettings.Client || WZIMopoly.GameType == GameType.Local)
                && (jail?.IsPrisoner(player) ?? false)
                && (jail?.CanPrisonerPayForRelease(player) ?? false);
        }
    }
}
