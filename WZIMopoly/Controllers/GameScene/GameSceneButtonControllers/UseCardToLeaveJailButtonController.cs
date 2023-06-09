using WZIMopoly.Attributes;
using WZIMopoly.GUI.GameScene.GUIGameSceneButtons;
using WZIMopoly.Models.GameScene.GameSceneButtonModels;

namespace WZIMopoly.Controllers.GameScene.GameSceneButtonControllers
{
    /// <summary>
    /// Represents the controller of the use card to leave jail button.
    /// </summary>
    [UpdatesNetwork]
    internal class UseCardToLeaveJailButtonController : ButtonController<UseCardToLeaveJailButtonModel, GUIUseCardToLeaveJailButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UseCardToLeaveJailButtonController"/> class.
        /// </summary>
        /// <param name="model">
        /// The model of the use card to leave jail button.
        /// </param>
        /// <param name="view">
        /// The view of the use card to leave jail button.
        /// </param>
        public UseCardToLeaveJailButtonController(UseCardToLeaveJailButtonModel model, GUIUseCardToLeaveJailButton view)
            : base(model, view) { }
    }
}
