using WZIMopoly.Enums;
using WZIMopoly.Models.GameScene.TileModels;

namespace WZIMopoly.Models.GameScene.GameSceneButtonModels
{
    /// <summary>
    /// The model of the pay to leave jail button.
    /// </summary>
    internal class PayToLeaveJailButtonModel : ButtonModel, IGameUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayToLeaveJailButtonModel"/> class.
        /// </summary>
        public PayToLeaveJailButtonModel() 
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
