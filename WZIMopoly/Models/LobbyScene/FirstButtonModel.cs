using WZIMopoly.Enums;

namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the first bankruptcy button model.
    /// </summary>
    internal class FirstButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstButtonModel"/> class.
        /// </summary>
        public FirstButtonModel()
            : base("LobbyFirst") { }

        /// <inheritdoc/>
        public override void Update()
        {
            IsActive = GameSettings.gameEndType == GameEndType.FirstBankruptcy;
        }
    }
}
