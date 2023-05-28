using Microsoft.Xna.Framework.Graphics;
using WZIMopoly.Controllers.GameScene.SettingsButtonControllers;
using WZIMopoly.GUI.GameScene;
using WZIMopoly.GUI.GameScene.GUISettingsButtons;
using WZIMopoly.Models.GameScene;
using WZIMopoly.Models.GameScene.SettingsButtonModels;
using WZIMopoly.Scenes;

namespace WZIMopoly.Controllers.GameScene
{
    internal class SettingsController : Scene<SettingsModel, GUISettings>
    {
        public SettingsController(SettingsModel model, GUISettings view)
            : base(model, view) { }

        public override void Initialize()
        {
            Model.InitializeChild<ExitToMenuButtonModel, GUIExitToMenuButton, ExitToMenuButtonController>();
        }
    }
}
