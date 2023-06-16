using WZIMopoly.Enums;

namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the first bankruptcy button model.
    /// </summary>
    internal class FirstBankruptcyButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstBankruptcyButtonModel"/> class.
        /// </summary>
        public FirstBankruptcyButtonModel()
            : base("LobbyFirst") { }

        /// <inheritdoc/>
        public override void Update()
        {
            IsActive = GameSettings.GameEndType == GameEndType.FirstBankruptcy;
        }
    }
}
