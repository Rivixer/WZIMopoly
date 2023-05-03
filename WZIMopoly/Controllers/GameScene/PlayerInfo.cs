using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    /// <summary>
    /// Represents a controller for PlayerInfo.
    /// </summary>
    /// /// <remarks>
    /// Used to display player information such as
    /// nickname or amount of money in the game scene.
    /// <remarks>
    internal class PlayerInfo : Controller<PlayerInfoModel, GUIPlayerInfo>
    {
        internal PlayerInfo(PlayerInfoModel model, GUIPlayerInfo view)
            : base (model, view) { }
    }
}
