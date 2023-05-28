using WZIMopoly.GUI.GameScene.GUISettingsButtons;
using WZIMopoly.Models.GameScene.SettingsButtonModels;

namespace WZIMopoly.Controllers.GameScene.SettingsButtonControllers
{
    internal class ExitToMenuButtonController : ButtonController<ExitToMenuButtonModel, GUIExitToMenuButton>
    {
        public ExitToMenuButtonController(ExitToMenuButtonModel model, GUIExitToMenuButton view)
            : base(model, view) { }
    }
}
