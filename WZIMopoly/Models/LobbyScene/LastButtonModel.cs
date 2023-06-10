using WZIMopoly.Enums;

namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the last not bankrupt button model.
    /// </summary>
    internal class LastButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LastButtonModel"/> class.
        /// </summary>
        public LastButtonModel()
            : base("LobbyLast") { }

        /// <inheritdoc/>
        public override void Update()
        {
            IsActive = GameSettings.gameEndType == GameEndType.LastNotBankrupt;
        }
    }
}
