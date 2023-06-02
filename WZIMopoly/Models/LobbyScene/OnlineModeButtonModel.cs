using WZIMopoly.Enums;

namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the online model button model.
    /// </summary>
    internal class OnlineModeButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineModeButtonModel"/> class.
        /// </summary>
        public OnlineModeButtonModel()
            : base("LobbyOnline") { }

        /// <inheritdoc/>
        public override void Update()
        {
            IsActive = WZIMopoly.GameType == GameType.Online;
        }
    }
}
