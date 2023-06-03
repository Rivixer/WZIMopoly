using System.Runtime.CompilerServices;
using WZIMopoly.Enums;

namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the local model button model.
    /// </summary>
    internal class LocalModeButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalModeButtonModel"/> class.
        /// </summary>
        public LocalModeButtonModel()
            : base("LobbyLocal") { }

        /// <inheritdoc/>
        public override void Update()
        {
            IsActive = WZIMopoly.GameType == GameType.Local;
        }
    }
}
