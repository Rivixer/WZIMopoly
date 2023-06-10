using WZIMopoly.Controllers.GameScene.GameSceneButtonControllers;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.Models.GameScene
{
    /// <summary>
    /// Represents the jail model.
    /// </summary>
    internal class JailModel : Model
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JailModel"/> class.
        /// </summary>
        public JailModel()
        {
            InitializeChild<PayToLeaveJailButtonModel, GUIPayToLeaveJailButton, PayToLeaveJailButtonController>();
            InitializeChild<UseCardToLeaveJailButtonModel, GUIUseCardToLeaveJailButton, UseCardToLeaveJailButtonController>();
        }
    }
}
