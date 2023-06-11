using WZIMopoly.Enums;

namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the last not bankrupt button model.
    /// </summary>
    internal class LastNotBankruptButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LastNotBankruptButtonModel"/> class.
        /// </summary>
        public LastNotBankruptButtonModel()
            : base("LobbyLast") { }

        /// <inheritdoc/>
        public override void Update()
        {
            IsActive = GameSettings.GameEndType == GameEndType.LastNotBankrupt;
        }
    }
}
