using WZIMopoly.GUI.GameScene;
using WZIMopoly.Models.GameScene;

namespace WZIMopoly.Controllers.GameScene
{
    internal class PlayerInfo : Controller<PlayerInfoModel, GUIPlayerInfo>
    {
        internal PlayerInfo(PlayerInfoModel model, GUIPlayerInfo view)
            : base (model, view) { }
    }
}
