using System.Linq;
using WZIMopoly.Enums;

namespace WZIMopoly.Models.LobbyScene
{
    /// <summary>
    /// Represents the start game button model.
    /// </summary>
    internal class StartGameButtonModel : ButtonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartGameButtonModel"/> class.
        /// </summary>
        public StartGameButtonModel()
            : base("LobbyStart") { }

        /// <inheritdoc/>
        public override void Update()
        {
            IsActive = 
                GameSettings.ActivePlayers.Count >= 2
                && GameSettings.ActivePlayers.Select(x=>x.Nick).Distinct().Count() == GameSettings.ActivePlayers.Count
                && !GameSettings.ActivePlayers.Where(x => x.Nick == "").Select(x => x.Nick).Any()
                && GameSettings.Client.PlayerType != PlayerType.OnlinePlayer;
        }
    }
}
